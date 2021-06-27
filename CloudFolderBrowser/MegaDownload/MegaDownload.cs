using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CG.Web.MegaApiClient;
using System.Threading;

namespace CloudFolderBrowser
{
    public class MegaDownload
    {
        List<FileInfo> files { get; set; }               
        public bool finished { get; internal set; } = false;
        public List<MegaFileDownload> downloads { get; set; }
        private readonly Queue<MegaFileDownload> downloadQueue = new Queue<MegaFileDownload>();        
        ProgressBar[] progressbars;
        Label[] progresslabels;
        int finishedDownloads = 0;
        string downloadFolderPath;
        public CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public int OverwriteMode;
        ToolTip toolTip1;

        public event EventHandler DownloadCompleted;
        protected virtual void OnDownloadCompleted(EventArgs e)
        {
            EventHandler handler = DownloadCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public MegaDownload(MegaApiClient megaClient, List<CloudFile> files, ProgressBar[] progressBars, Label[] progressLabels, ToolTip toolTip, int overwriteMode = 3, bool folderNewFiles = true)
        {
            progressbars = progressBars;
            progresslabels = progressLabels;
            toolTip1 = toolTip;
            downloads = new List<MegaFileDownload>();
            OverwriteMode = overwriteMode;

            MegaApiClient megaApiClient = new MegaApiClient();
            megaApiClient.LoginAnonymous();

            if (folderNewFiles)
                downloadFolderPath = MainForm.syncFolderPath + @"\0_New Files\" + DateTime.Now.Date.ToShortDateString();
            else
                downloadFolderPath = MainForm.syncFolderPath + "\\";

            try
            {
                foreach (CloudFile file in files)
                {
                    //TODO: IMPROVE MATCHING METHOD
                    var newFolderDir = new DirectoryInfo(downloadFolderPath);
                    var newFolderFiles = newFolderDir.GetFiles("*", SearchOption.AllDirectories);
                    var matchedFiles = newFolderFiles.Where(x => x.Name == file.Name).ToArray();
                    if (matchedFiles.Length > 0)
                        continue;
                    MegaFileDownload megaFileDownload = new MegaFileDownload(megaApiClient, this, file.MegaNode, downloadFolderPath + file.Path);
                    downloadQueue.Enqueue(megaFileDownload);
                    downloads.Add(megaFileDownload);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Start()
        {
            progresslabels[progresslabels.Length - 1].Text = "";
            progresslabels[progresslabels.Length - 1].Visible = true;
            lock (downloadQueue)
            {          
                if (downloadQueue.Count > 0)
                    for(int i = 0; i < progressbars.Length; i++)
                    {
                        if (progressbars[i].Tag == null)
                        {
                            MegaFileDownload dd = downloadQueue.Dequeue();
                            dd.ProgressBar = progressbars[i];
                            dd.ProgressLabel = progresslabels[i];
                            toolTip1.SetToolTip(dd.ProgressLabel, Path.GetFileName(dd.SavePath));
                            dd.StartDownload();                       
                            if (downloadQueue.Count == 0) break;
                        }
                    }
            }
        }

        public void UpdateQueue(MegaFileDownload d)
        {
            lock (downloadQueue)
            {
                d.Finished = true;
                d.ProgressBar.Tag = null;
                d.ProgressBar.Value = 0;
                d.ProgressLabel.Visible = false;

                if (downloadQueue.Count > 0 && !cancellationTokenSource.IsCancellationRequested)
                {
                    //if (d.ProgressBar.Tag == null)
                    //{
                    MegaFileDownload newd = downloadQueue.Dequeue();
                    newd.ProgressBar = d.ProgressBar;
                    newd.ProgressLabel = d.ProgressLabel;
                    toolTip1.SetToolTip(newd.ProgressLabel, Path.GetFileName(newd.SavePath));
                    newd.StartDownload();
                    //}
                }
                if (finishedDownloads == downloads.Count && !cancellationTokenSource.IsCancellationRequested)
                {
                    OnDownloadCompleted(EventArgs.Empty);
                    DownloadsFinishedForm downloadsFinishedForm = new DownloadsFinishedForm(downloadFolderPath, "All downloads are finished!");
                    downloadsFinishedForm.Show();
                }
            }
            finishedDownloads++;
            progresslabels[progresslabels.Length - 1].Text = $"{finishedDownloads}/{downloads.Count} files finished";
        }

        public void Stop()
        {
            OnDownloadCompleted(EventArgs.Empty);
            cancellationTokenSource.Cancel();   
        }

    }
}
