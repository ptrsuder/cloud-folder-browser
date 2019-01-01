using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace YandexDiskSharp.Utilities
{
    internal static class Extensions
    {
        public static JsonTextReader GetJsonTextReader(this HttpWebRequest request)
        {
            try
            {
                return new JsonTextReader(new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    CloseInput = true
                };
            }
            catch (WebException ex)
            {
                using (var jsonReader = new JsonTextReader(new StreamReader(ex.Response.GetResponseStream())) { CloseInput = true })
                    throw new DiskException(new Models.Exception(jsonReader), ex, ex.Status, ex.Response);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
