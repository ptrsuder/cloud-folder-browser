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
            Path = "";
            this.Size = Size;
        }

        public CloudFile(Resource r)
        {
            Name = r.Name;
            Created = r.Created;
            PublicUrl = r.PublicUrl;
            Modified = r.Modified;
            Path = r.Path;
        }

        public string Name;
        public DateTime Created;
        public DateTime Modified;
        public long Size;
        public Uri PublicUrl { get; set; }
        public string EncryptedUrl { get; set; }
        public string Path { get; set; }

        [JsonIgnore]
        public INode MegaNode { get; set; }

    }

}
