using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CloudFolderBrowser.JDownloader
{
    public class JDPackage
    {
        public JDPackage(string id, string name)
        {
            packageID = id;
            this.name = name;
            //this.links = new List<object>();
        }
        //public string type { get; set; } = "NORMAL";
        public string packageID { get; set; }
        //public List<object> links { get; set; }
        //public object comment { get; set; } = "";
        public string name { get; set; }
        //public string priority { get; set; } = "DEFAULT";
        //public bool expanded { get; set; } = false;
        // public long created { get; set; }
        //public long uid { get; set; }
        public string downloadFolder { get; set; } = "<jd:packagename>";
        //public string sorterId { get; set; } = "ASC.jd.controlling.linkcrawler.CrawledPackage";
        [JsonIgnore]
        public int linksCount { get; set; } = 0;
        [JsonIgnore]
        public string numberId { get; set; }
    }
}
