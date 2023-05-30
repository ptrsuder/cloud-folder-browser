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
                client = new HttpClient();                
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

                return await response.Content.ReadAsStringAsync();
            }
            catch(HttpRequestException ex)
            {
                var errorMessage = "Failed to encode url";
                if (ex.HResult == -2147467259)
                    errorMessage = errorMessage + ": No connection to the FogLink server";
                return errorMessage;
            }

        }

        public static async Task<List<FogLinkFile>> GetDecodedAsync(string url)
        {
            HttpResponseMessage response = await client.GetAsync(
                $"MegaPrivater/decode?encriptedLink={WebUtility.UrlEncode(url)}");
            //response.EnsureSuccessStatusCode();

            var nodes = JsonConvert.DeserializeObject<FogLinkFile[]>(await response.Content.ReadAsStringAsync());
            return nodes.ToList();
        }
    }
}
