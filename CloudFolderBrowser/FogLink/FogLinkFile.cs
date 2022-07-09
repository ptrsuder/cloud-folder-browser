using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CG.Web.MegaApiClient;

namespace CloudFolderBrowser
{
    public class FogLinkFile
    {
        public string Name { get; set; }
        public long? Size { get; set; }
        public long Size2 { get => Size ?? 0; }
        public string Id { get; set; }
        public string ParentId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime CreationDate { get => Created ?? DateTime.MinValue; }
        public DateTime? Modified { get; set; }
        public DateTime ModificationDate { get => Modified ?? DateTime.MinValue; }
        public string Path { get; set; }
        public string EncryptedLink { get; set; }
        public bool IsFile { get; set; }

        public NodeType Type { get => IsFile ? NodeType.File : NodeType.Directory; }

    }
}
