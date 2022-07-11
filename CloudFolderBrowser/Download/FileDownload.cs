using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFolderBrowser
{
    public class FileDownload : IFileDownload
    {
        public string SavePath { get; set; }     

        public Task DownloadTask { get; set; }

        public IProgress<double> Progress { get; set; }

        public double ProgressPercent { get; set; }

        public bool Finished { get; set; } = false;

        public ProgressBar ProgressBar { get; set; }

        public Label ProgressLabel { get; set; }

        public MegaDownload MegaDownload { get; set; }

        public int RemainedRetries { get; set; } = 4;

        public bool DownloadFailed { get; set; } = false;
        
    }
}
