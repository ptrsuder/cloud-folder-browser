using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace CloudFolderBrowser
{
    internal static class Utility
    {
        static public string[] ParsePath(string path, bool includeFilename = false)
        {
            string pattern = @"([^/]+)/";
            if (includeFilename) pattern = @"([^/]+)";

            MatchCollection matches = Regex.Matches(path, pattern);
            if (matches.Count == 0)
                return null;
            string[] folderNames = new string[matches.Count];

            for (int i = 0; i < folderNames.Length; i++)
            {
                folderNames[i] = matches[i].Groups[1].Value;
            }
            return folderNames;
        }

        public static bool IsBase64String(this string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }

        public static async Task<string> GetFinalRedirect(string url, string userAgent)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls13;
            
            if (string.IsNullOrWhiteSpace(url))
                return url;

            int maxRedirCount = 5;  // prevent infinite loops
            string newUrl = url;
            do
            {
                HttpWebRequest req = null;
                HttpWebResponse resp = null;
                SocketsHttpHandler webRequestHandler = new SocketsHttpHandler();
                webRequestHandler.AllowAutoRedirect = false;
                HttpClient httpClient = new HttpClient(webRequestHandler);
                httpClient.DefaultRequestVersion = HttpVersion.Version20;
                httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);

                try
                {

                    HttpResponseMessage responseMessage = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

                    switch (responseMessage.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return newUrl;
                        case HttpStatusCode.Redirect:
                        case HttpStatusCode.MovedPermanently:
                        case HttpStatusCode.RedirectKeepVerb:
                        case HttpStatusCode.RedirectMethod:
                            newUrl = responseMessage.Headers.Location.ToString();
                            if (newUrl == null)
                                return url;

                            if (newUrl.IndexOf("://", StringComparison.Ordinal) == -1)
                            {
                                // Doesn't have a URL Schema, meaning it's a relative or absolute URL
                                Uri u = new Uri(new Uri(url), newUrl);
                                newUrl = u.ToString();
                            }
                            break;
                        default:
                            return newUrl;
                    }
                    url = newUrl;
                }
                catch (WebException ex)
                {
                    // Return the last known good URL
                    return newUrl;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (resp != null)
                        resp.Close();
                }
            }
            while (
            (newUrl.ToLower().Contains("rebrand.ly")) &&
            maxRedirCount-- > 0);

            return newUrl;
        }

        static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetLastId(string url)
        {
            Regex uriRegex = new Regex(@"/(?<type>(file|folder))/(?<id>[^#]+)#(?<key>[^$/]+)(/folder/)?(?<lastid>[^/]+)?");
            Match match = uriRegex.Match(url);
            if (match.Success == false)
            {
                throw new ArgumentException(string.Format("Invalid uri. Unable to extract Id and Key from the uri {0}", url));
            }
            string lastId = match.Groups["lastid"].Value;
            return lastId;

        }


        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public static CloudServiceType GetCloudServiceType(string url)
        {
            if (url.Contains("yadi.sk"))
                return CloudServiceType.Yadisk;
            if (url.Contains(".allsync.com"))
                return CloudServiceType.Allsync;
            if (url.Contains("efss.qloud."))
                return CloudServiceType.QCloud;
            if (url.Contains("mega.nz"))
                return CloudServiceType.Mega;
            if (url.Contains("thetrove.is"))
                return CloudServiceType.TheTrove;
            if (url.Contains("h5ailink"))
                return CloudServiceType.h5ai;

            //using (var webpage = new WebClient())
            //{
            //    webpage.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:59.0) Gecko/20100101 Firefox/59.0";
            //    var data = webpage.DownloadString(url);
            //    HtmlWeb web = new HtmlWeb();
            //    HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            //    htmlDoc.LoadHtml(data);
            //    HtmlNode mdnode = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='description']");
            //    if (mdnode != null)
            //    {
            //        HtmlAttribute desc;
            //        desc = mdnode.Attributes["content"];
            //        string fulldescription = desc.Value;
            //        if (fulldescription.ToLower().Contains("powered by h5ai"))
            //            return CloudServiceType.h5ai;
            //    }
            //}

            return CloudServiceType.Other;
        }       
    }
}
