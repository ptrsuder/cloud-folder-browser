using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFolderBrowser
{
    public abstract class Download : IDownload
    {
        public List<IFileDownload> Downloads { get; set; }

        protected readonly Queue<IFileDownload> DownloadQueue = new Queue<IFileDownload>();

        protected ProgressBar[] progressbars { get; set; }

        protected Label[] progresslabels { get; set; }

        protected ToolTip ToolTip { get; set; }

        public CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

        public int OverwriteMode { get; set; }

        public CloudServiceType CloudService { get; set; }

        public int FinishedDownloads = 0, MaxDownloadRetries = 4, RetryDelay = 300;

        public bool CheckDownloadedFileSize = true;

        public double CheckFileSizeError = 0.999;

        public List<IFileDownload> FailedDownloads { get; set; }

        public string DownloadFolderPath { get; set; }

        protected SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public event EventHandler DownloadCompleted;
        protected virtual void OnDownloadCompleted(EventArgs e)
        {
            EventHandler handler = DownloadCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler DownloadFailed;
        protected virtual void OnDownloadFailed(DownloadEventArgs e)
        {
            EventHandler handler = DownloadFailed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public abstract Task Start();
        public abstract void Stop();
    }
}
