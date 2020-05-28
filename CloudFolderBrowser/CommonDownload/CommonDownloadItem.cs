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

    public class CommonFileDownload
    {              
        public string SavePath { get; set; }       
        public Task DownloadTask { get; set; }
        public IProgress<double> Progress { get; }
        public double ProgressPercent;
        public bool Finished = false;
        public ProgressBar ProgressBar { get; set; }
        public Label ProgressLabel { get; set; }
        public CommonDownload ParentDownload { get; set; }
        public CloudFile FileInfo { get; }
        public NetworkCredential _networkCredential;

        public CommonFileDownload(CommonDownload commonDownload, CloudFile fileInfo, string savePath, NetworkCredential networkCredential)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            SavePath = savePath;           
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

        string EncodeWebUrl(string url)
        {
            return url.Replace("#", "%23").Replace(",", "%2C").Replace("?", "%3F");
        }

        public async Task StartDownload()
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
                    switch(ParentDownload.OverwriteMode)
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
                    CustomHeaders.Add("X-Requested-With", "XMLHttpRequest");
                    string downloadPath = EncodeWebUrl(FileInfo.PublicUrl.OriginalString);
                    if(ParentDownload.CloudService == CloudServiceType.Allsync)
                    {
                        var encodedUrl = new Uri(downloadPath);     
                        var host = encodedUrl.Host;
                        downloadPath = $"https://{host}/public.php/webdav{FileInfo.Path}";
                    }                        
                    DownloadTask = DownloadFileAsync(downloadPath, SavePath, Progress, ParentDownload.cancellationTokenSource.Token, _networkCredential);
                    //WebDacClient.webClient.DownloadProgressChanged += (s, e) =>
                    //{
                    //    var percent = e.ProgressPercentage;
                    //    var received = e.BytesReceived;
                    //    ProgressBar.Value = (int)percent;
                    //    ProgressLabel.Text = $"{(int)(received / 100000000)}/{ (int)(FileInfo.Size / 1000000)} MB [{percent}%] {FileInfo.Name}";
                    //};                    
                    await DownloadTask;                   
                }               
                ParentDownload.UpdateQueue(this);
            }
            catch(Exception ex)
            {
                if (DownloadTask.IsCanceled)
                    DownloadTask.Dispose();
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                //Finished = true;
                //ProgressBar.Tag = null;
                //ProgressBar.Value = 0;
                //ProgressLabel.Visible = false;
            }
        }

        WebClient webClient = new WebClient();
      
        Dictionary<string, string> CustomHeaders = new Dictionary<string, string>();

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
            try
            {
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
                    //var encodedUrl = new Uri(downloadUri);
                    //var url = downloadUri.Replace(encodedUrl.Scheme + @"://", "");
                    //url = HttpUtility.UrlEncode(url);
                    //var uri = $"{encodedUrl.Scheme}://{encodedUrl.Host}/{HttpUtility.UrlEncode(encodedUrl.AbsolutePath)}{HttpUtility.UrlEncode(encodedUrl.Fragment)}";
                    await webClient.DownloadFileTaskAsync(downloadUri, outputFile);
                }
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.RequestCanceled)
            {
                if (File.Exists(outputFile))
                    File.Delete(outputFile);
            }
            catch (WebException ex)
            {
               
            }
        }
    }
}
