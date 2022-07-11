


namespace CloudFolderBrowser
{
    interface IDownload
    {       
        List<IFileDownload> FailedDownloads { get; set; }

        string DownloadFolderPath { get; set; }

        Task Start();
        void Stop();        

        event EventHandler DownloadCompleted;
        event EventHandler DownloadFailed;
    }
}
