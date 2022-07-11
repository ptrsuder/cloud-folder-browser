using CG.Web.MegaApiClient;

namespace CloudFolderBrowser
{
    public class MegaFileDownload : FileDownload
    {
        public MegaApiClient MegaClient { get; }
        INode Node { get; }
        public MegaDownload ParentDownload { get; set; }

        public string ShareId { get; set; }

        public MegaFileDownload(MegaApiClient megaClient, MegaDownload megaDownload, PublicNode fileNode, string savePath)
        {
            SavePath = savePath.Replace(@" /", @"/");
            Node = fileNode;
            MegaClient = megaClient;
            var progressHandler = new Progress<double>(value =>
            {
                ProgressPercent = value;
            });
            progressHandler.ProgressChanged += new EventHandler<double>(ProgreessChanged);
            Progress = progressHandler;
            ParentDownload = megaDownload;
        }

        public MegaFileDownload(MegaApiClient megaClient, MegaDownload megaDownload, INode fileNode, string savePath)
        {
            SavePath = savePath.Replace(@" /", @"/");
            Node = fileNode;
            MegaClient = megaClient;
            var progressHandler = new Progress<double>(value =>
            {
                ProgressPercent = value;
            });
            progressHandler.ProgressChanged += new EventHandler<double>(ProgreessChanged);
            Progress = progressHandler;
            ParentDownload = megaDownload;            
        }

        void ProgreessChanged(object sender, double e)
        {
            ProgressBar.Value = (int)e;
            ProgressLabel.Text = $"{(int)(e * Node.Size / 100000000)}/{(int)(Node.Size / 1000000)} MB [{Math.Round(e, 2)}%] {Node.Name}";
        }

        public async Task StartDownload()
        {
            ProgressBar.Tag = this;
            ProgressLabel.Text = "";
            ProgressLabel.Visible = true;
            var folderPath = Path.GetDirectoryName(SavePath);
            Directory.CreateDirectory(folderPath);
            FileInfo file = new FileInfo(SavePath);
            DialogResult overwriteFile = DialogResult.Yes;
            if (file.Exists)
            {
                switch (ParentDownload.OverwriteMode)
                {
                    case 0:
                        overwriteFile = DialogResult.No;
                        break;
                    case 1:
                        overwriteFile = DialogResult.Yes;
                        break;
                    case 2:
                        if (Node.ModificationDate > file.CreationTime)
                            overwriteFile = DialogResult.Yes;
                        else
                            overwriteFile = DialogResult.No;
                        break;
                    case 3:
                        overwriteFile = MessageBox.Show($"File [{file.Name}] already exists. Overwrite?", "", MessageBoxButtons.YesNo);
                        break;
                }
                if (overwriteFile == DialogResult.Yes)
                    file.Delete();
            }
            if (overwriteFile == DialogResult.Yes)
            {
                try
                {
                    if (Node is PublicNode)
                        DownloadTask = MegaClient.DownloadFileAsync(Node, SavePath, Progress, ParentDownload.CancellationTokenSource.Token);
                    else
                    if (ParentDownload.ShareId != "")
                        DownloadTask = MegaClient.DownloadFileAsync(new PublicNode(Node, ParentDownload.ShareId), SavePath, Progress, ParentDownload.CancellationTokenSource.Token);
                    else
                        DownloadTask = MegaClient.DownloadFileAsync(Node, SavePath, Progress, ParentDownload.CancellationTokenSource.Token);
                    await DownloadTask;
                }
                catch (Exception ex)
                {
                    if (DownloadTask.IsCanceled)
                    {
                        DownloadTask?.Dispose();
                        if (File.Exists(SavePath))
                            File.Delete(SavePath);
                        ProgressBar.Value = 0;
                        ProgressLabel.Text = "";
                    }
                    else
                    {
                        var logFileName = $"00-DOWNLOAD-LOG-{DateTime.Now.ToString("MM-dd-yyyy")}.txt";
                        string log = $"\n{DateTime.Now}\nNode id: {Node.Id}\nSavePath:{SavePath}\nexception: {ex.Message}\n";
                        File.AppendAllText(logFileName, log);

                        if (RemainedRetries >= 0)
                        {
                            await Task.Delay(ParentDownload.RetryDelay);
                            RetryDownload();
                        }
                        return;
                    }
                }
            }
            ParentDownload.UpdateQueue(this);
        }

        public async Task RetryDownload()
        {
            RemainedRetries--;
            DownloadFailed = false;
            ProgressBar.Value = 0;
            ProgressLabel.Text = "";
            await StartDownload();
            if (DownloadFailed)
                return;
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
