using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace CloudFolderBrowser
{
    public class CommonDownload: Download
    {      
        public CommonDownload(List<CloudFile> files, ProgressBar[] progressBars,
            Label[] progressLabels, ToolTip toolTip, CloudServiceType cloudServiceType,
            string baseDownloadPath, int overwriteMode = 3, NetworkCredential networkCredential = null, bool folderNewFiles = true)
        {
            progressbars = progressBars;
            progresslabels = progressLabels;
            Downloads = new List<IFileDownload>();
            OverwriteMode = overwriteMode;
            CloudService = cloudServiceType;
            ToolTip = toolTip;

            FailedDownloads = new List<IFileDownload>();

            if (folderNewFiles)
                DownloadFolderPath = baseDownloadPath + "\\0_New Files\\" + DateTime.Now.Date.ToShortDateString();
            else
                DownloadFolderPath = baseDownloadPath + "\\";

            DownloadFailed += new EventHandler(CommonDownload_DownloadFailed);

            try
            {
                foreach (CloudFile file in files)
                {
                    var newFolderDir = new DirectoryInfo(DownloadFolderPath);
                    var newFolderFiles = newFolderDir.GetFiles("*", SearchOption.AllDirectories);
                    //var matchedFiles = newFolderFiles.Where(x => x.Name == file.Name).ToArray();
                    //if (matchedFiles.Length > 0)
                    //    continue;
                    CommonFileDownload fileDownload = new CommonFileDownload(this, file, DownloadFolderPath + file.Path.Remove(0,1).Replace("/","\\"), networkCredential);
                    DownloadQueue.Enqueue(fileDownload);
                    Downloads.Add(fileDownload);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CommonDownload_DownloadFailed(object? sender, EventArgs e)
        {
            var args = e as DownloadEventArgs;
            var download = args.Download;
            if (download.RemainedRetries >= 0)
            {
                download.RetryDownload();
            }
        }

        public override async Task Start()
        {
            semaphoreSlim = new SemaphoreSlim(1, 1);
            ServicePointManager.DefaultConnectionLimit = 4;

            progresslabels[progresslabels.Length - 1].Text = "";
            progresslabels[progresslabels.Length - 1].Visible = true;
               
            if (DownloadQueue.Count > 0)
                for (int i = 0; i < progressbars.Length; i++)
                {
                    if (progressbars[i].Tag == null)
                    {
                        await semaphoreSlim.WaitAsync();
                        var dd = DownloadQueue.Dequeue() as CommonFileDownload;
                        semaphoreSlim.Release();
                        dd.ProgressBar = progressbars[i];
                        dd.ProgressLabel = progresslabels[i];
                        ToolTip.SetToolTip(dd.ProgressLabel, dd.FileInfo.Name);
                        dd.StartDownload();
                        
                        if (DownloadQueue.Count == 0) break;
                    }
                }            
        }

        public async Task UpdateQueue(CommonFileDownload d)
        {
            d.Finished = true;
            d.ProgressBar.Tag = null;
            d.ProgressBar.Value = 0;
            d.ProgressLabel.Visible = false;

            if (DownloadQueue.Count > 0 && !CancellationTokenSource.IsCancellationRequested)
            {
                await semaphoreSlim.WaitAsync();
                var newd = DownloadQueue.Dequeue() as CommonFileDownload;
                semaphoreSlim.Release();

                newd.ProgressBar = d.ProgressBar;
                newd.ProgressLabel = d.ProgressLabel;
                ToolTip.SetToolTip(newd.ProgressLabel, newd.FileInfo.Name);
                
                await newd.StartDownload();                
                if (newd.DownloadFailed)
                    return;
            }
            
            FinishedDownloads++;
            progresslabels[progresslabels.Length - 1].Text = $"{FinishedDownloads}/{Downloads.Count} files finished";

            if (FinishedDownloads == Downloads.Count && !CancellationTokenSource.IsCancellationRequested)
            {
                OnDownloadCompleted(EventArgs.Empty);               
            }
        }        

        public override void Stop()
        {            
            CancellationTokenSource.Cancel();           
            OnDownloadCompleted(EventArgs.Empty);
        }

    }

    public class DownloadEventArgs: EventArgs
    {
        public CommonFileDownload Download { get; }

        public DownloadEventArgs(CommonFileDownload dl)
        {
            Download = dl;
        }
    }
}
