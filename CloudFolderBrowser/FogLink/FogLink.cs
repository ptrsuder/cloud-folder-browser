using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CloudFolderBrowser
{
    public class FogLink
    {
        static HttpClient client = new HttpClient();

        public static Uri ServerAddress
        {
            get => client.BaseAddress;
            set
            {
                client.BaseAddress = value;
            }
        }

        public static async Task<string> GetEncodedAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(
                    $"MegaPrivater/encode?url={WebUtility.UrlEncode(url)}");
                //response.EnsureSuccessStatusCode();

                return response.Content.ReadAsStringAsync().Result;
            }
            catch
            {
                return "";
            }

        }

        public static async Task<List<FogLinkFile>> GetDecodedAsync(string url)
        {
            HttpResponseMessage response = await client.GetAsync(
                $"MegaPrivater/decode?encriptedLink={WebUtility.UrlEncode(url)}");
            //response.EnsureSuccessStatusCode();

            var nodes = JsonConvert.DeserializeObject<FogLinkFile[]>(response.Content.ReadAsStringAsync().Result);
            return nodes.ToList();
        }
    }
}
