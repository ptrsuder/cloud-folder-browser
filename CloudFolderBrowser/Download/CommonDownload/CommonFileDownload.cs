using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
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

        void ProgreessChanged(long complete, long total)
        {

            double procentage = Math.Round(100.0 * (complete * 1.0 / total), 2);
            Progress.Report(procentage);        
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
            if (file.Exists && file.Length >= 0.999 * FileInfo.Size)
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

                var encodedUrl = new Uri(downloadPath);
                var host = encodedUrl.Host;

                if (ParentDownload.CloudService == CloudServiceType.Allsync)
                {                   
                    downloadPath = $"https://{host}/public.php/webdav/{EncodeAllsyncUrl(FileInfo.Path)}";
                }
                if(ParentDownload.CloudService == CloudServiceType.QCloud)
                {
                    downloadPath = $"https://{host}/index.php/s/{_networkCredential.UserName}/download?path=/&files={HttpUtility.UrlEncode(FileInfo.Path)}";                  
                }
                try
                {
                    DownloadTask = DownloadFileAsync(downloadPath, SavePath, _networkCredential, ParentDownload.CancellationTokenSource.Token, ProgreessChanged);

                    await DownloadTask;              
                }
                catch (WebException ex) when (ex.Status == WebExceptionStatus.RequestCanceled)
                {
                    if (DownloadTask.IsCanceled)
                        DownloadTask.Dispose();

                    if (File.Exists(SavePath))
                        File.Delete(SavePath);
                }
                catch (WebException ex) when (ex.Status != WebExceptionStatus.RequestCanceled)
                {
                    var logFileName = $"download-log-{DateTime.Now.ToString("MM-dd-yyyy")}.txt";
                    string log =
                        $"{DateTime.Now}\n" +
                        $"DownloadPath: {downloadPath}\n" +
                        $"SavePath: {downloadPath}\n" +
                        $"WebException:[{ex.Status}] {ex.Message}\n" +
                        $"Retry: {ParentDownload.MaxDownloadRetries - RemainedRetries}\n";

                    File.AppendAllText(logFileName, log);

                    if (RemainedRetries >= 0)
                    {
                        await Task.Delay(ParentDownload.RetryDelay);
                        RetryDownload();
                    }
                    return;
                }
                catch (Exception ex)
                {
                    if(ParentDownload.CancellationTokenSource.IsCancellationRequested)//ex.HResult == -2146233029) //|| ex.HResult == -2146233079)
                    {
                        if (DownloadTask.IsCanceled)
                            DownloadTask.Dispose();

                        if (File.Exists(SavePath))
                            File.Delete(SavePath);
                        return;
                    }

                    var logFileName = $"download-log-{DateTime.Now.ToString("MM-dd-yyyy")}.txt";
                    string log = 
                        $"{DateTime.Now}\n" +
                        $"DownloadPath: {downloadPath}\n" +
                        $"SavePath: {downloadPath}\n" +
                        $"Exception: {ex.Message}\n\n";
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

            if (File.Exists(SavePath))
                File.Delete(SavePath);

            await StartDownload();

            if (DownloadFailed)
                return;
        }


        //from https://github.com/dotnet/runtime/issues/31479 by waltdestler
        /// <summary>
        /// Downloads a file from the specified Uri into the specified stream.
        /// </summary>
        /// <param name="cancellationToken">An optional CancellationToken that can be used to cancel the in-progress download.</param>
        /// <param name="progressCallback">If not null, will be called as the download progress. The first parameter will be the number of bytes downloaded so far, and the second the total size of the expected file after download.</param>
        /// <returns>A task that is completed once the download is complete.</returns>
        public async Task DownloadFileAsync(string downloadUri, string outputFile, NetworkCredential networkCredential, CancellationToken cancellationToken = default, Action<long, long> progressCallback = null)
        {
            var uri = new Uri(downloadUri);
            Stream toStream = File.Create(outputFile);

            var progressStream = progressCallback != null ? new ProgressStream(toStream) : toStream as Stream;
            // for performance use original stream if no callback
            if (progressCallback != null)
                (progressStream as ProgressStream).UpdateProgress += progressCallback;
          

            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            if (toStream == null)
                throw new ArgumentNullException(nameof(toStream));

            if (uri.IsFile)
            {
                await using Stream file = File.OpenRead(uri.LocalPath);

                if (progressCallback != null)
                {
                    long length = file.Length;
                    byte[] buffer = new byte[4096];
                    int read;
                    int totalRead = 0;
                    while ((read = await file.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) > 0)
                    {
                        await toStream.WriteAsync(buffer, 0, read, cancellationToken).ConfigureAwait(false);
                        totalRead += read;
                        progressCallback(totalRead, length);
                    }
                    Debug.Assert(totalRead == length || length == -1);
                }
                else
                {
                    await file.CopyToAsync(toStream, cancellationToken).ConfigureAwait(false);
                }
            }
            else
            {
                using HttpClient client = new HttpClient();
                if(networkCredential != null)
                {
                    string svcCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(networkCredential.UserName + ":" + networkCredential.Password));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + svcCredentials);
                }
                using HttpResponseMessage response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode) throw new Exception();

                if (progressCallback != null)
                {
                    long length = response.Content.Headers.ContentLength ?? FileInfo.Size;
                    await using Stream stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                    byte[] buffer = new byte[4096];
                    int read;
                    int totalRead = 0;

                    try
                    {
                        while ((read = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) > 0)
                        {
                            await toStream.WriteAsync(buffer, 0, read, cancellationToken).ConfigureAwait(false);
                            totalRead += read;
                            progressCallback(totalRead, length);
                        }
                    }
                    catch(Exception ex)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            toStream.Close();
                            toStream.Dispose();
                        }
                        throw;
                    }

                    toStream.Close();
                    toStream.Dispose();
                    Debug.Assert(totalRead == length || length == -1);
                }
                else
                {
                    await response.Content.CopyToAsync(toStream).ConfigureAwait(false);
                }
            }
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
               
                await webClient.DownloadFileTaskAsync(downloadUri, outputFile); //.WaitAsync(new TimeSpan(99999), ct.Token);

                if (File.Exists(SavePath) && ParentDownload.CheckDownloadedFileSize)                
                    if(new FileInfo(SavePath).Length * ParentDownload.CheckFileSizeError < FileInfo.Size)                    
                        RetryDownload();
                    
                
                //for download fail testing
                //ct.CancelAfter(200);
                //if (new Random().Next(0, 2) == 1)
                //throw new WebException("", WebExceptionStatus.ConnectionClosed);
                //throw new Exception();
            }
        }
    }

    //from https://stackoverflow.com/a/77516636
    public class ProgressStream : Stream
    {
        private Stream _input;
        private long _progress;

        public event Action<long, long>? UpdateProgress;

        public ProgressStream(Stream input)
        {
            _input = input;
        }

        public override void Flush() => _input.Flush();

        public override Task FlushAsync(CancellationToken cancellationToken = default) => _input.FlushAsync(cancellationToken);

        public override int Read(byte[] buffer, int offset, int count)
        {
            int n = _input.Read(buffer, offset, count);
            _progress += n;
            UpdateProgress?.Invoke(_progress, _input.Length);
            return n;
        }     

        public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            int n = await _input.ReadAsync(buffer, cancellationToken);
            _progress += n;
            UpdateProgress?.Invoke(_progress, _input.Length);
            return n;
        }

        protected override void Dispose(bool disposing) => _input.Dispose();

        public override ValueTask DisposeAsync() => _input.DisposeAsync();

        public override void Write(byte[] buffer, int offset, int count) => throw new System.NotImplementedException();

        public override long Seek(long offset, SeekOrigin origin) => throw new System.NotImplementedException();

        public override void SetLength(long value) => throw new System.NotImplementedException();

        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => _input.Length;
        public override long Position
        {
            get { return _input.Position; }
            set { throw new System.NotImplementedException(); }
        }
    }
}
