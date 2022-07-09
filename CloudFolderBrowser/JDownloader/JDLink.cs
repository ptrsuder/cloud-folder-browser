using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFolderBrowser.JDownloader
{
    public class JDLink
    {
        public JDLink(string name, string url)
        {
            downloadLink = new DownloadLink(name, url);
        }
        public object id { get; set; }
        public object name { get; set; }
        public bool enabled { get; set; } = true;
        //public long created { get; set; } = 20180407;
        //public long uid { get; set; } = 20180407;
        public DownloadLink downloadLink { get; set; }
        //public List<string> sourceUrls { get; set; }
        //public object archiveInfo { get; set; } = null;
        //public OriginDetails originDetails { get; set; }
    }
}
