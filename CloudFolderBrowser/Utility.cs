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

        public static async Task<string> GetFinalRedirect(string url)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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
                try
                {
                    HttpResponseMessage responseMessage = httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result;

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
            if (url.Contains("mega.nz"))
                return CloudServiceType.Mega;
            if (url.Contains("thetrove.is"))
                return CloudServiceType.TheTrove;
            if (url.Contains("h5ailink"))
                return CloudServiceType.h5ai;

            using (var webpage = new System.Net.WebClient())
            {
                webpage.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:59.0) Gecko/20100101 Firefox/59.0";
                var data = webpage.DownloadString(url);
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(data);
                HtmlNode mdnode = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='description']");
                if (mdnode != null)
                {
                    HtmlAttribute desc;
                    desc = mdnode.Attributes["content"];
                    string fulldescription = desc.Value;
                    if (fulldescription.ToLower().Contains("powered by h5ai"))
                        return CloudServiceType.h5ai;
                }
            }



            return CloudServiceType.Other;
        }


        static void Fetch(string url, string hash, string sk, string path)
        {            
            Dictionary<string, string> postParams = new Dictionary<string, string>();
            postParams.Add("hash", hash);
            postParams.Add("sk", sk);
            postParams.Add("offset", "0");

            using (var client = new HttpClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod("POST"), $"https://yadi.sk/public/api/get-dir-size");

                if (postParams != null)
                    requestMessage.Content = new FormUrlEncodedContent(postParams);   // This is where your content gets added to the request body

                //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "text/plain");

                var st = HttpUtility.UrlEncode($"{{\"hash\":\"{hash}\",\"sk\":\"{sk}\"}}").Replace("+", "%20");

                byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(st);
                var content = new ByteArrayContent(messageBytes);

                var body = new StringContent(st);
                requestMessage.Content = content;

                requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:72.0) Gecko/20100101 Firefox/72.0");
                requestMessage.Headers.Add("Referer", @"https://yadi.sk/d/uXiHcdtMNc2zzA");
                requestMessage.Headers.Add("Accept", "*/*");
                requestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                requestMessage.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                requestMessage.Headers.Add("Origin", "https://yadi.sk");
                requestMessage.Headers.Add("Host", "yadi.sk");

                HttpResponseMessage response = client.SendAsync(requestMessage).Result;

                var response2 = client.PostAsync($"https://yadi.sk/public/api/get-dir-size", new StringContent(st, Encoding.UTF8, "application/json"));
                response2.Wait();

                string apiResponse = response.Content.ReadAsStringAsync().Result;
                try
                {
                    // Attempt to deserialise the reponse to the desired type, otherwise throw an expetion with the response from the api.
                    //if (apiResponse != "")
                    //    return JsonConvert.DeserializeObject<T>(apiResponse);
                    //else
                    //    throw new Exception();
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error ocurred while calling the API. It responded with the following message: {response.StatusCode} {response.ReasonPhrase}");
                }
            }
        }

    }
}
