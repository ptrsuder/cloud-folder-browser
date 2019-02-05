using CG.Web.MegaApiClient;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudFolderBrowser
{
    public class MegaFileDownload
    {
        public MegaApiClient MegaClient { get; }
        public string SavePath { get; set; }
        INode Node { get; }
        public Task DownloadTask { get; set; }
        public IProgress<double> Progress { get; }
        public double ProgressPercent;
        public bool Finished = false;
        public ProgressBar ProgressBar { get; set; }
        public Label ProgressLabel { get; set; }
        public MegaDownload MegaDownload { get; set; }
        

        public MegaFileDownload(MegaApiClient megaClient, MegaDownload megaDownload, INode fileNode, string savePath)
        {            
            SavePath = savePath;
            Node = fileNode;
            MegaClient = megaClient;            
            var progressHandler = new Progress<double>(value =>
            {
                ProgressPercent = value;                
            });
            progressHandler.ProgressChanged += new EventHandler<double>(ProgreessChanged);
            Progress = progressHandler as IProgress<double>;
            MegaDownload = megaDownload;            
        }

        void ProgreessChanged(object sender, double e)
        {
            ProgressBar.Value = (int) e;
            ProgressLabel.Text = $"{(int)(e * Node.Size / 100000000)}/{ (int)(Node.Size / 1000000)} MB [{Math.Round(e,2)}%] {Node.Name}";
        }

        public async void StartDownload()
        {
            try
            {
                ProgressBar.Tag = this;
                ProgressLabel.Text = "";
                ProgressLabel.Visible = true;
                Directory.CreateDirectory(Path.GetDirectoryName(SavePath));
                FileInfo file = new FileInfo(SavePath);
                DialogResult overwriteFile = DialogResult.Yes;
                if (file.Exists)
                {
                    overwriteFile = MessageBox.Show($"File [{file.Name}] already exists. Overwrite?", "", MessageBoxButtons.YesNo);
                    if (overwriteFile == DialogResult.Yes)
                        file.Delete();  
                }
                if (overwriteFile == DialogResult.Yes)
                {
                    DownloadTask = MegaClient.DownloadFileAsync(Node, SavePath, Progress, MegaDownload.cancellationTokenSource.Token);
                    await DownloadTask;
                }              
                MegaDownload.UpdateQueue(this);
            }
            catch(Exception ex)
            {
                if (DownloadTask.IsCanceled)
                    DownloadTask.Dispose();
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                Finished = true;
                ProgressBar.Tag = null;
                ProgressBar.Value = 0;
                ProgressLabel.Visible = false;
            }
        }
    }

    public class MegaFolderDownload
    {
        public MegaApiClient MegaClient { get; }
        public string SavePath { get; set; }
        public INode Node { get; set; }
        public Task<Stream> DownloadTask { get; set; }
        public IProgress<double> Progress { get; }
        public MegaFolderDownload(MegaApiClient megaClient, INode folderNode, string savePath)
        {
            SavePath = savePath;
            Node = folderNode;
            MegaClient = megaClient;
        }
        public async void StartDownload()
        {
            DownloadTask = MegaClient.DownloadAsync(Node, Progress);
        }
    }
}
