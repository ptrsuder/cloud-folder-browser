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


        public CommonDownload(List<CloudFile> files, ProgressBar[] progressBars, Label[] progressLabels, CloudServiceType cloudServiceType, int overwriteMode = 3, NetworkCredential networkCredential = null)
        {
            progressbars = progressBars;
            progresslabels = progressLabels;
            downloads = new List<CommonFileDownload>();
            OverwriteMode = overwriteMode;
            CloudService = cloudServiceType;

            downloadFolderPath = MainForm.syncFolderPath + "/New Files " + DateTime.Now.Date.ToShortDateString();

            try
            {
                foreach (CloudFile file in files)
                {
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

                if (downloadQueue.Count > 0)
                {
                    //if (d.ProgressBar.Tag == null)
                    //{
                    CommonFileDownload newd = downloadQueue.Dequeue();
                    newd.ProgressBar = d.ProgressBar;
                    newd.ProgressLabel = d.ProgressLabel;                   
                    newd.StartDownload();
                    //}
                }
                if (finishedDownloads == downloads.Count)
                {                             
                    DownloadsFinishedForm downloadsFinishedForm = new DownloadsFinishedForm(downloadFolderPath, "All downloads are finished!");
                    downloadsFinishedForm.Show();
                }
            }
            finishedDownloads++;
            progresslabels[progresslabels.Length - 1].Text = $"{finishedDownloads}/{downloads.Count} files finished";
        }

        public void Stop()
        {
            foreach (var fileDownload in this.downloads)
                cancellationTokenSource.Cancel();   
        }

    }
}
