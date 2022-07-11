using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFolderBrowser
{
    public interface IFileDownload
    {
        string SavePath { get; set; }

        Task DownloadTask { get; set; }
        IProgress<double> Progress { get; set; }

        double ProgressPercent { get; set; }

        bool Finished { get; set; }

        ProgressBar ProgressBar { get; set; }

        Label ProgressLabel { get; set; }        

        int RemainedRetries { get; set; }

        bool DownloadFailed { get; set; }       
    }
}
