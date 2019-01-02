using System;
using Newtonsoft.Json;
using YandexDiskSharp.Models;
using CG.Web.MegaApiClient;

namespace CloudFolderBrowser
{
    public class CloudFile
    {
        [JsonConstructor]
        public CloudFile(string Name, DateTime Created, DateTime Modified, long Size)
        {
            this.Name = Name;
            this.Created = Created;
            this.Modified = Modified;
            this.Path = "";
            this.Size = Size;
        }

        public CloudFile(Resource r)
        {
            this.Name = r.Name;
            this.Created = r.Created;
            this.PublicUrl = r.PublicUrl;
            this.Modified = r.Modified;
            this.Path = r.Path;
        }

        public string Name;
        public DateTime Created;
        public DateTime Modified;
        public long Size;
        public Uri PublicUrl { get; set; }
        public string Path { get; set; }
        public INode MegaNode { get; set; }
        //string mediaType;       
    }

}
