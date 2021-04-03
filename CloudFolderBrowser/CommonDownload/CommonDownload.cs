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
using WebDAVClient;
using System.Net;

namespace CloudFolderBrowser
{
    public class CommonDownload
    {
        List<FileInfo> files { get; set; }               
        public bool finished { get; internal set; } = false;
        public List<CommonFileDownload> downloads { get; set; }
        private readonly Queue<CommonFileDownload> downloadQueue = new Queue<CommonFileDownload>();        
        ProgressBar[] progressbars;
        Label[] progresslabels;
        int finishedDownloads = 0;
        string downloadFolderPath;
        public CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public int OverwriteMode;
        public CloudServiceType CloudService;

        public event EventHandler DownloadCompleted;
        protected virtual void OnDownloadCompleted(EventArgs e)
        {
            EventHandler handler = DownloadCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public CommonDownload(List<CloudFile> files, ProgressBar[] progressBars,
            Label[] progressLabels, CloudServiceType cloudServiceType,
            int overwriteMode = 3, NetworkCredential networkCredential = null, bool folderNewFiles = true)
        {
            progressbars = progressBars;
            progresslabels = progressLabels;
            downloads = new List<CommonFileDownload>();
            OverwriteMode = overwriteMode;
            CloudService = cloudServiceType;           

            if (folderNewFiles)
                downloadFolderPath = MainForm.syncFolderPath + "/0_New Files/" + DateTime.Now.Date.ToShortDateString();
            else
                downloadFolderPath = MainForm.syncFolderPath;

            //var logFileName = $"download-log-{DateTime.Now.ToString("MM-dd-yyyy")}.txt";
            //string log = $"{DateTime.Now}\ndownloadFolderPath:{downloadFolderPath}\nlogin: {networkCredential.UserName}\npassword: {networkCredential.Password}\n";
            //File.AppendAllText(logFileName, log);

            try
            {
                foreach (CloudFile file in files)
                {
                    var newFolderDir = new DirectoryInfo(MainForm.syncFolderPath + "/0_New Files/");
                    var newFolderFiles = newFolderDir.GetFiles("*", SearchOption.AllDirectories);
                    var matchedFiles = newFolderFiles.Where(x => x.Name == file.Name).ToArray();
                    if (matchedFiles.Length > 0)
                        continue;
                    CommonFileDownload fileDownload = new CommonFileDownload(this, file, downloadFolderPath + file.Path, networkCredential);
                    downloadQueue.Enqueue(fileDownload);
                    downloads.Add(fileDownload);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Start()
        {
            ServicePointManager.DefaultConnectionLimit = 4;

            progresslabels[progresslabels.Length - 1].Text = "";
            progresslabels[progresslabels.Length - 1].Visible = true;
            lock (downloadQueue)
            {          
                if (downloadQueue.Count > 0)
                    for(int i = 0; i < progressbars.Length; i++)
                    {
                        if (progressbars[i].Tag == null)
                        {
                            CommonFileDownload dd = downloadQueue.Dequeue();
                            dd.ProgressBar = progressbars[i];
                            dd.ProgressLabel = progresslabels[i];                            
                            dd.StartDownload();                            
                            if (downloadQueue.Count == 0) break;
                        }
                    }
            }
        }

        public void UpdateQueue(CommonFileDownload d)
        {
            lock (downloadQueue)
            {
                d.Finished = true;
                d.ProgressBar.Tag = null;
                d.ProgressBar.Value = 0;
                d.ProgressLabel.Visible = false;

                if (downloadQueue.Count > 0 && !cancellationTokenSource.IsCancellationRequested)
                {                   
                    CommonFileDownload newd = downloadQueue.Dequeue();
                    newd.ProgressBar = d.ProgressBar;
                    newd.ProgressLabel = d.ProgressLabel;                   
                    newd.StartDownload();
                }                
            }
            finishedDownloads++;
            progresslabels[progresslabels.Length - 1].Text = $"{finishedDownloads}/{downloads.Count} files finished";

            if (finishedDownloads == downloads.Count && !cancellationTokenSource.IsCancellationRequested)
            {
                OnDownloadCompleted(EventArgs.Empty);
                DownloadsFinishedForm downloadsFinishedForm = new DownloadsFinishedForm(downloadFolderPath, "All downloads are finished!");
                downloadsFinishedForm.Show();
            }
        }

        public void Stop()
        {            
            cancellationTokenSource.Cancel();
            OnDownloadCompleted(EventArgs.Empty);
        }

    }
}
