using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using WebDAVClient;

namespace CloudFolderBrowser
{
    public class AllsyncFileInfo
    {
        public string Path { get; set; }
        public long Size { get; set; }
        public string Name { get; set; }
        public DateTime ModificationDate { get; set; }

        public AllsyncFileInfo(string path, long size, string name, DateTime modificationDate)
        {
            Path = path; Size = size; Name = name; ModificationDate = modificationDate;
        }
    }

    public class CommonFileDownload : FileDownload
    {           
        public CommonDownload ParentDownload { get; set; }
        public CloudFile FileInfo { get; }
        private NetworkCredential _networkCredential;

        WebClient webClient = new WebClient();
        Dictionary<string, string> CustomHeaders = new Dictionary<string, string>() { { "X-Requested-With", "XMLHttpRequest" } };

        public CommonFileDownload(CommonDownload commonDownload, CloudFile fileInfo, string savePath, NetworkCredential networkCredential)
        {
            SavePath = savePath.Replace("%27", "'").Replace("/", "\\");           
            FileInfo = fileInfo;
            var progressHandler = new Progress<double>(value =>
            {
                ProgressPercent = value;                
            });
            progressHandler.ProgressChanged += new EventHandler<double>(ProgreessChanged);
            Progress = progressHandler as IProgress<double>;
            ParentDownload = commonDownload;
            _networkCredential = networkCredential;
        }

        void ProgreessChanged(object sender, double e)
        {
            ProgressBar.Value = (int) e;
            ProgressLabel.Text = $"{(int)(e * FileInfo.Size / 100000000)}/{ (int)(FileInfo.Size / 1000000)} MB [{Math.Round(e,2)}%] {FileInfo.Name}";
        }

        string EncodeAllsyncUrl(string url)
        {
            return EncodeWebUrl(url).Replace(" ", "%20");
        }

        string EncodeWebUrl(string url)
        {
            return url.Replace("#", "%23").Replace(",", "%2C").Replace("?", "%3F");
        }

        string EncodeQloudUrl(string url)
        {
            return EncodeAllsyncUrl(url).Replace(":", "%3A").Replace(";", "%3B").Replace("&", "%26");
        }

        public async Task StartDownload()
        {
            if (RemainedRetries == -1)
            {
                ParentDownload.FinishedDownloads++;
                ParentDownload.FailedDownloads.Add(this);             
                if (File.Exists(SavePath))
                    File.Delete(SavePath);
                ParentDownload.UpdateQueue(this);
                return;
            }

            webClient = new WebClient();
            ProgressBar.Tag = this;
            ProgressLabel.Text = "";
            ProgressLabel.Visible = true;
            Directory.CreateDirectory(Path.GetDirectoryName(SavePath));
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
                        if (FileInfo.Modified > file.CreationTime)
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
                string downloadPath = EncodeWebUrl(FileInfo.PublicUrl.OriginalString);

                var filename = Path.GetFileName(SavePath);
                var dir = Path.GetDirectoryName(SavePath);

                foreach(var ch in Path.GetInvalidFileNameChars())
                    filename = filename.Replace(ch.ToString(), "");

                SavePath = dir + "\\" + filename;

                if (ParentDownload.CloudService == CloudServiceType.Allsync)
                {
                    var encodedUrl = new Uri(downloadPath);
                    var host = encodedUrl.Host;
                    downloadPath = $"https://{host}/public.php/webdav/{EncodeAllsyncUrl(FileInfo.Path)}";
                }
                if(ParentDownload.CloudService == CloudServiceType.QCloud)
                {                                          
                    downloadPath = $"https://efss.qloud.my/index.php/s/{_networkCredential.UserName}/download?path=/&files={HttpUtility.UrlEncode(FileInfo.Path)}";                  
                }
                try
                {
                    DownloadTask = DownloadFileAsync(downloadPath, SavePath, Progress, ParentDownload.CancellationTokenSource.Token, _networkCredential);
                    await DownloadTask;              
                }
                catch (WebException ex) when (ex.Status == WebExceptionStatus.RequestCanceled)
                {
                    if (DownloadTask.IsCanceled)
                        DownloadTask.Dispose();

                    if (File.Exists(SavePath))
                        File.Delete(SavePath);
                }
                catch (Exception ex)
                {
                    if(ex.HResult == -2146233029 || ex.HResult == -2146233079)
                    {
                        if (DownloadTask.IsCanceled)
                            DownloadTask.Dispose();

                        if (File.Exists(SavePath))
                            File.Delete(SavePath);

                        return;
                    }
                    var logFileName = $"download-log-{DateTime.Now.ToString("MM-dd-yyyy")}.txt";
                    string log = $"{DateTime.Now}\ndownloadPath: {downloadPath}\nSavePath: {downloadPath}\nexception: {ex.Message}\n";
                    File.AppendAllText(logFileName, log);

                    if (RemainedRetries >= 0)
                    {
                        await Task.Delay(ParentDownload.RetryDelay);
                        RetryDownload();
                    }                
                    
                    return;

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


        public async Task DownloadFileAsync(string downloadUri, string outputFile, IProgress<double> progress, CancellationToken cancellationToken, NetworkCredential networkCredential)
        {            
            var headers = new Dictionary<string, string> { { "translate", "f" } };
            if (CustomHeaders != null)
            {
                foreach (var keyValuePair in CustomHeaders)
                {
                    headers.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }
            using (var registration = cancellationToken.Register(() => webClient.CancelAsync()))
            {
                webClient.Headers = new WebHeaderCollection();
                if (networkCredential != null)
                    webClient.Credentials = networkCredential;

                foreach (var keyValuePair in CustomHeaders)
                    webClient.Headers.Add(keyValuePair.Key, keyValuePair.Value);

                webClient.DownloadProgressChanged += (s, e) =>
                {
                    progress.Report(e.ProgressPercentage);
                };
                //var rand = new Random();
                //if (rand.Next(0, 2) == 1)
                //    throw new WebException();
                await webClient.DownloadFileTaskAsync(downloadUri, outputFile);
            }
        }
    }
}
