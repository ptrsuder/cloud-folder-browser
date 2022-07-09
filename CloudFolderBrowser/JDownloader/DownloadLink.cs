using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFolderBrowser.JDownloader
{
    public class DownloadLink
    {
        public DownloadLink(string name, string url)
        {
            this.name = name;
            this.url = url;
        }
        public string name { get; set; }
        public string url { get; set; }
        public int size { get; set; } = -1;
        public string host { get; set; } = "http links";
        public bool enabled { get; set; } = true;
        //public long created { get; set; } = 20180407;
        //public long uid { get; set; } = 20180407;
        //public string urlProtection { get; set; } = "UNSET";
        //public int current { get; set; } = 0;
        //public object linkStatus { get; set; }
        //public object chunkProgress { get; set; }
        //public object finalLinkState { get; set; } = null;
        //public string availablestatus { get; set; } = "TRUE";
        //public object propertiesString { get; set; } = null;
    }
}
