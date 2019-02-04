using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CG.Web.MegaApiClient;


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
                
        public MegaDownload(MegaApiClient megaClient, List<CloudFile> files, ProgressBar[] progressBars, Label[] progressLabels)
        {
            progressbars = progressBars;
            progresslabels = progressLabels;
            downloads = new List<MegaFileDownload>();

            MegaApiClient megaApiClient = new MegaApiClient();
            megaApiClient.LoginAnonymous();

            downloadFolderPath = MainForm.syncFolderPath + "/New Files " + DateTime.Now.Date.ToShortDateString();

            try
            {
                foreach (CloudFile file in files)
                {
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
                if (downloadQueue.Count > 0)
                {
                    if (d.ProgressBar.Tag == null)
                    {
                        MegaFileDownload newd = downloadQueue.Dequeue();
                        newd.ProgressBar = d.ProgressBar;
                        newd.ProgressLabel = d.ProgressLabel;
                        newd.ProgressLabel.Visible = true;
                        newd.StartDownload();
                    }
                }
                if(finishedDownloads == downloads.Count)
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
            //TODO: TO ADD
        }

    }
}
