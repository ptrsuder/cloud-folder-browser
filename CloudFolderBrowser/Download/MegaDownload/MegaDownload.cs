using CG.Web.MegaApiClient;

namespace CloudFolderBrowser
{
    public class MegaDownload: Download
    {                                        
        public string ShareId;      

        public MegaDownload(MegaApiClient megaClient, List<CloudFile> files, ProgressBar[] progressBars, Label[] progressLabels, 
            ToolTip toolTip, string baseDownloadPath, int overwriteMode = 3, bool folderNewFiles = true, string shareId = "")
        {
            progressbars = progressBars;
            progresslabels = progressLabels;
            ToolTip = toolTip;
            Downloads = new List<IFileDownload>();
            OverwriteMode = overwriteMode;
            ShareId = shareId;

            MegaApiClient megaApiClient = new MegaApiClient();
            megaApiClient.LoginAnonymous();

            if (folderNewFiles)
                DownloadFolderPath = baseDownloadPath + @"\0_New Files\" + DateTime.Now.Date.ToShortDateString();
            else
                DownloadFolderPath = baseDownloadPath + "\\";

            try
            {
                foreach (CloudFile file in files)
                {
                    //TODO: IMPROVE MATCHING METHOD
                    //var newFolderDir = new DirectoryInfo(downloadFolderPath);
                    //var newFolderFiles = newFolderDir.GetFiles("*", SearchOption.AllDirectories);
                    //var matchedFiles = newFolderFiles.Where(x => x.Name == file.Name).ToArray();
                    //if (matchedFiles.Length > 0)
                    //    continue;                    

                    MegaFileDownload megaFileDownload;
                    if(file.MegaNode is PublicNode)
                        megaFileDownload = new MegaFileDownload(megaApiClient, this, file.MegaNode as PublicNode, DownloadFolderPath + file.Path);
                    else
                        megaFileDownload = new MegaFileDownload(megaApiClient, this, file.MegaNode, DownloadFolderPath + file.Path);
                    DownloadQueue.Enqueue(megaFileDownload);
                    Downloads.Add(megaFileDownload);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override async Task Start()
        {
            progresslabels[progresslabels.Length - 1].Text = "";
            progresslabels[progresslabels.Length - 1].Visible = true;

            FailedDownloads = new List<IFileDownload>();

            if (DownloadQueue.Count > 0)
                for (int i = 0; i < progressbars.Length; i++)
                {
                    if (progressbars[i].Tag == null)
                    {
                        await semaphoreSlim.WaitAsync();
                        var dd = DownloadQueue.Dequeue() as MegaFileDownload;
                        semaphoreSlim.Release();
                        dd.ProgressBar = progressbars[i];
                        dd.ProgressLabel = progresslabels[i];
                        ToolTip.SetToolTip(dd.ProgressLabel, Path.GetFileName(dd.SavePath));
                        dd.StartDownload();
                        if (DownloadQueue.Count == 0) break;
                    }
                }
        }           

        public async Task UpdateQueue(MegaFileDownload d)
        {
            d.Finished = true;
            d.ProgressBar.Tag = null;
            d.ProgressBar.Value = 0;
            d.ProgressLabel.Visible = false;

            if (DownloadQueue.Count > 0 && !CancellationTokenSource.IsCancellationRequested)
            {
                await semaphoreSlim.WaitAsync();
                var newd = DownloadQueue.Dequeue() as MegaFileDownload;
                semaphoreSlim.Release();
                newd.ProgressBar = d.ProgressBar;
                newd.ProgressLabel = d.ProgressLabel;
                ToolTip.SetToolTip(newd.ProgressLabel, Path.GetFileName(newd.SavePath));
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
}
