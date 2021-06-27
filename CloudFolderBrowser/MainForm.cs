using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using YandexDiskSharp;
using YandexDiskSharp.Models;
using System.Runtime.InteropServices;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using Newtonsoft.Json;
using HtmlAgilityPack;
using System.Web;
using WebDAVClient;
using CG.Web.MegaApiClient;
using Exception = System.Exception;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;

//https://github.com/kozakovi4/YandexDiskSharp
//https://github.com/AdamsLair/treeviewadv
//https://sourceforge.net/projects/treeviewadv/
//https://github.com/Toqe/Downloader

namespace CloudFolderBrowser
{
    public enum CloudServiceType { Yadisk, Mega, h5ai, Allsync, TheTrove, Other}
          
    public partial class MainForm : Form
    {
        string AppVersion = "0.9.15";

        public static RestClient rc;
        public ResourceList rl_root;
        public List<Task> tasks;
        public List<Task<CloudFolder>> tasks2;
        public static string syncFolderPath = "";
        public static TreeModel yadiskPublicFolder_model, yadiskFlatFolder_model, syncFolder_model, newFiles_model;
        public Folder syncFolder;
        public static CloudFolder cloudPublicFolder, yadiskFolder;
        public static List<CloudFolder> checkedFolders, mixedFolders;        
        Dictionary<string, string> publicFolders;
        string hotDictKey = "";
        string WebIndexFolderDomain = "";
        string TroveRootFolderAddress = "";
        string allsyncRootFolderAddress = "";
        string allsyncUrl = "https://allsync.com";
        SyncFilesForm activeSyncForm;
        bool continueAfterCheck = true;

        public IClient webdavClient;

        bool debugMode = false;
        
        int checkedFilesNumber = 0;
        double checkedFilesSize = 0.0;
        public static long freeSpace;

        public CloudServiceType cloudServiceType;

        Dictionary<string, string> savedPasswords = new Dictionary<string, string>();

        //filter textbox 
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public MainForm()
        {
            InitializeComponent();

            SendMessage(filter_textBox.Handle, 0x1501, 1, "Filter by name");
            filter_textBox.TextChangedComplete += filter_TextChangedComplete;

            beforeDate_dateTimePicker.MaxDate = DateTime.Today;
            afterDate_dateTimePicker.MaxDate = DateTime.Today;
            beforeDate_dateTimePicker.Value = DateTime.Today;
            if (Properties.Settings.Default.publicFoldersJson == "")
            {
                publicFolders = new Dictionary<string, string>();
                publicFolders.Add("ExampleFolderName", "https://examplefolderurl.com");
                //Properties.Settings.Default.publicFoldersJson = JsonConvert.SerializeObject(publicFolders);
                //Properties.Settings.Default.Save();
            }
            else
                publicFolders = JsonConvert.DeserializeObject<Dictionary<string, string>>(Properties.Settings.Default.publicFoldersJson);

            publicFolders_comboBox.DataSource = new BindingSource(publicFolders, null);
            publicFolders_comboBox.DisplayMember = "Key";
            publicFolders_comboBox.ValueMember = "Value";

            if (publicFolders.Count <= 1)
            {
                deletePublicFolder_button.Enabled = false;
            }
            else
                deletePublicFolder_button.Enabled = true;

            //nodeCheckBox1.IsEditEnabledValueNeeded += CheckIndex;
            nodeCheckBox1.IsVisibleValueNeeded += CheckIndex;

            nodeCheckBox1.CheckStateChanged += new EventHandler<TreePathEventArgs>(NodeCheckStateChanged);
            yadiskPublicFolder_treeViewAdv.NodeControls[2].ToolTipProvider = new ToolTipProvider();

            refreshFolder_menuItem.Click += RefreshFolder_menuItem_Click;

            if (Properties.Settings.Default.lastSyncFolderPath != "")
            {
                syncFolderPath = Properties.Settings.Default.lastSyncFolderPath;
                syncFolderPath_textBox.Text = syncFolderPath;
            }

            HttpWebRequest.DefaultWebProxy = null;
            WebRequest.DefaultWebProxy = null;

            if (Properties.Settings.Default.loginedYandex)
            {
                loginYandex_button.Text = "Logout";
                try
                {
                    LoginYandex(Properties.Settings.Default.accessTokenYandex);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Cannot log in Yadisk. No internet connection?");
                }
            }
            else
            {
                rc = new RestClient();
            }

            if (Properties.Settings.Default.savedPasswordsJson != "")
            {
                savedPasswords = JsonConvert.DeserializeObject<Dictionary<string, string>>(Properties.Settings.Default.savedPasswordsJson);
            }

            checkAllToolStripMenuItem.Click += CheckAllToolStripMenuItem_Click;
            checkNoneToolStripMenuItem.Click += CheckNoneToolStripMenuItem_Click;
            expandAllToolStripMenuItem.Click += ExpandAllToolStripMenuItem_Click;
            collapseAllToolStripMenuItem.Click += CollapseAllToolStripMenuItem_Click;

            appVersion_linkLabel.Text = "v " + AppVersion;
        }

        void SetProgress(bool waiting = true)
        {
            if(waiting)
            {
                loadLink_progressBar.Style = ProgressBarStyle.Marquee;
                loadLink_progressBar.MarqueeAnimationSpeed = 40;
            }
            else
            {
                loadLink_progressBar.MarqueeAnimationSpeed = 0;
                loadLink_progressBar.Style = ProgressBarStyle.Continuous;
            }          
        }

        void UpdatePublicFoldersSetting()
        {
            if(publicFolders.Count <= 1)
            {
                deletePublicFolder_button.Enabled = false;
            }
            else
                deletePublicFolder_button.Enabled = true;
            Properties.Settings.Default.publicFoldersJson = JsonConvert.SerializeObject(publicFolders);
            Properties.Settings.Default.Save();
        }

        public void LoginYandex(string accessToken)
        {
            rc = new RestClient(accessToken);          

            //Resource rl = rc.GetResource("disk:/");
            //yadiskFolder = new CloudFolder(rl);
            //GetFolders(new List<CloudFolder> { yadiskFolder });

            #region DiskInfo
            Disk yd = rc.GetDisk();
            long totalSpace = yd.TotalSpace / 1024000, usedSpace = yd.UsedSpace / 1024000, trashSize = yd.TrashSize / 1024000;
            freeSpace = yd.TotalSpace - yd.TrashSize;

            yadiskSpace_progressBar.Maximum = (int)totalSpace;
            yadiskSpace_progressBar.Value = (int)usedSpace;

            if (usedSpace >= 0.9 * (totalSpace - trashSize))
                ModifyProgressBarColor.SetState(yadiskSpace_progressBar, 2);
            else
            if (usedSpace >= 0.7 * (totalSpace - trashSize))
                ModifyProgressBarColor.SetState(yadiskSpace_progressBar, 3);

            yadiskSpace_label.Text = $"Free space:" +
                $" {yadiskSpace_progressBar.Maximum - yadiskSpace_progressBar.Value - (int)trashSize}" +
                $" MB out of {yadiskSpace_progressBar.Maximum - trashSize} MB";
            #endregion

            yadiskSpace_progressBar.Visible = true;
            yadiskSpace_label.Visible = true;

            Properties.Settings.Default.loginedYandex = true;
            Properties.Settings.Default.Save();
        }

        public void LogoutYandex()
        {
            rc = new RestClient();
            Properties.Settings.Default.accessTokenYandex = "";
            Properties.Settings.Default.loginedYandex = false;
            Properties.Settings.Default.Save();
        }

        string GetFolderPath()
        {
            FolderSelectDialog fldsd = new FolderSelectDialog();
            fldsd.Title = "Choose folder to sync";
            if (Properties.Settings.Default.lastSyncFolderPath != "")
                fldsd.InitialDirectory = Properties.Settings.Default.lastSyncFolderPath;
            else
                fldsd.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            fldsd.ShowDialog();
            return fldsd.FileName;
        }
        
        #region #NODE CHECKBOX

        void CheckIndex(object sender, NodeControlValueEventArgs e)
        {
            var currentNode = (ColumnNode)(e.Node.Tag);
            var parentNode = (currentNode.Parent);
            string g = currentNode.Tag.GetType().ToString();
            bool isFolder = g == "CloudFolderBrowser.CloudFolder";
            bool parentChecked = false;
            bool parentCheckBoxEnabled = true;
            if (parentNode.Index != -1)
            {
                parentChecked = ((ColumnNode)parentNode).CheckState == CheckState.Checked;
                parentCheckBoxEnabled = ((ColumnNode)parentNode).CheckBoxEnabled;
            }

            if (parentCheckBoxEnabled && !parentChecked)
                currentNode.CheckBoxEnabled = true;
            else
                currentNode.CheckBoxEnabled = false;

            e.Value = isFolder && currentNode.CheckBoxEnabled;
        }

        void NodeCheckStateChanged(object sender, TreePathEventArgs e)
        {
            ColumnNode checkedNode = (ColumnNode)e.Path.LastNode;
            if (checkedNode.CheckState == CheckState.Checked)
            {
                //if (checkedNode.Parent.CheckState != CheckState.Checked && checkedNode.Parent.Index !=-1)
                //    UpdateParentCheckState((ColumnNode)checkedNode.Parent);
                //CheckAllSubnodes(checkedNode, false);
                checkedFilesSize += ((CloudFolder)checkedNode.Tag).Size - ((CloudFolder)checkedNode.Tag).SizeTopDirectoryOnly;
                checkedFilesNumber += ((CloudFolder)checkedNode.Tag).FilesNumber - ((CloudFolder)checkedNode.Tag).FilesNumberTopDirectoryOnly;
            }
            if (checkedNode.CheckState == CheckState.Unchecked)
            {
                checkedFilesSize -= ((CloudFolder)checkedNode.Tag).Size;
                checkedFilesNumber -= ((CloudFolder)checkedNode.Tag).FilesNumber;
                if (checkedFilesSize < 0.0001)
                    checkedFilesSize = 0;               
                //if (checkedNode.Parent.Index != -1)
                //    UpdateParentCheckState((ColumnNode)checkedNode.Parent);                
                //CheckAllSubnodes(checkedNode, true);                                               
            }
            if (checkedNode.CheckState == CheckState.Indeterminate)
            {
                checkedFilesSize += ((CloudFolder)checkedNode.Tag).SizeTopDirectoryOnly;
                checkedFilesNumber += ((CloudFolder)checkedNode.Tag).FilesNumberTopDirectoryOnly;
            }
            checkedFiles_label.Text = $"{Math.Round(checkedFilesSize / 1024000.0, 2)} MB checked | {checkedFilesNumber} files";
            return;
        }

        static void UpdateParentCheckState(ColumnNode parentNode)
        {
            CheckState origState = parentNode.CheckState;
            int UnCheckedNodes = 0, CheckedNodes = 0, MixedNodes = 0;

            foreach (ColumnNode tnChild in parentNode.Nodes)
            {
                if (tnChild.Tag.GetType().ToString() == "CloudFolderBrowser.CloudFolder")
                {
                    if (tnChild.CheckState == CheckState.Checked)
                        CheckedNodes++;
                    else if (tnChild.CheckState == CheckState.Indeterminate)
                    {
                        MixedNodes++;
                        break;
                    }
                    else
                        UnCheckedNodes++;
                }
            }

            if (MixedNodes > 0)
            {
                // at least one child is mixed, so parent must be mixed
                //parentNode.CheckState = CheckState.Indeterminate;
            }
            else if (CheckedNodes > 0 && UnCheckedNodes == 0)
            {
                // all children are checked
                if (parentNode.CheckState == CheckState.Indeterminate)
                    parentNode.CheckState = CheckState.Checked;
                //parentNode.CheckState = CheckState.Indeterminate;
                //else
                //   parentNode.CheckState = CheckState.Indeterminate;
            }
            else if (CheckedNodes > 0)
            {
                // some children are checked, the rest are unchecked
                if (parentNode.CheckState == CheckState.Checked)
                    parentNode.CheckState = CheckState.Unchecked;

                //parentNode.CheckState = CheckState.Indeterminate;
            }
            else
            {
                //if (parentNode.CheckState != CheckState.Unchecked)
                //    parentNode.CheckState = CheckState.Indeterminate;
                // all children are unchecked
                //if (parentNode.CheckState == CheckState.Checked)
                //    parentNode.CheckState = CheckState.Indeterminate;
                //else
                //    parentNode.CheckState = CheckState.Unchecked;
            }

            if (parentNode.CheckState != origState && parentNode.Parent.Index != -1)
                UpdateParentCheckState((ColumnNode)parentNode.Parent);

        }

        void CheckAllSubnodes(ColumnNode node, bool uncheck)
        {
            CheckState cst = uncheck ? CheckState.Unchecked : CheckState.Checked;

            foreach (ColumnNode subnode in node.Nodes)
            {
                CheckState origState = subnode.CheckState;
                if (subnode.Tag.GetType().ToString() == "CloudFolderBrowser.CloudFolder")
                {
                    if (cst == CheckState.Unchecked)
                    {

                    }
                    //    checkedFilesSize -= ((CloudFolder)subnode.Tag).Size;
                    if (cst == CheckState.Checked)
                    {
                        if (origState == CheckState.Indeterminate)
                        {

                        }
                        if (origState == CheckState.Unchecked)
                        {
                            checkedFilesSize += ((CloudFolder)subnode.Tag).SizeTopDirectoryOnly;
                            //long folderFilesSize = ((CloudFolder)subnode.Tag).Files.Sum(x => x.Size);
                            //checkedFilesSize += Math.Round(folderFilesSize / 1024000.0, 2);                            
                        }
                    }
                    //    checkedFilesSize += ((CloudFolder)subnode.Tag).Size;
                    subnode.CheckState = cst;
                    CheckAllSubnodes(subnode, uncheck);
                }
            }
        }

        void GetCheckedFolders(ColumnNode node)
        {
            if (node.CheckState == CheckState.Checked)
            {
                checkedFolders.Add((CloudFolder)(node.Tag));
                return;
            }
            //if mixed
            if (node.CheckState == CheckState.Indeterminate)
                mixedFolders.Add((CloudFolder)(node.Tag));

            foreach (ColumnNode subnode in node.Nodes)
            {
                if (subnode.CheckState == CheckState.Checked)
                {
                    checkedFolders.Add((CloudFolder)(subnode.Tag));
                    continue;
                }
                if (subnode.CheckState == CheckState.Indeterminate)
                {
                    mixedFolders.Add((CloudFolder)(subnode.Tag));
                    foreach (ColumnNode subnodeChild in subnode.Nodes)
                        if (subnodeChild.CheckState != CheckState.Unchecked && subnodeChild.Tag.GetType().ToString() == "CloudFolderBrowser.CloudFolder")
                            GetCheckedFolders(subnodeChild);
                }
                if (subnode.CheckState == CheckState.Unchecked && subnode.Nodes.Count > 0)
                {
                    GetCheckedFolders(subnode);
                }
            }
        }

        #endregion

        CloudServiceType GetCloudServiceType(string url)
        {
            if (url.Contains("thetrove.net"))
                url = url.Replace("thetrove.net", "thetrove.is");
            if (url.Contains("yadi.sk"))
                return CloudServiceType.Yadisk;
            if (url.Contains(".allsync.com"))
                return CloudServiceType.Allsync;
            if (url.Contains("mega.nz"))
                return CloudServiceType.Mega;
            if (url.Contains("thetrove.is"))
                return CloudServiceType.TheTrove;
            if (url.Contains("dl.lynxcore.org") ||
                url.Contains("dnd.jambrose.info") ||
                url.Contains("enthusiasticallyconfused.com") )
                //||
                //url.Contains("ezael.net") ||
                //url.Contains("docs.m0m0g33k.net") ||
                //url.Contains("ezael.net"))
                return CloudServiceType.h5ai;

            using (var webpage = new System.Net.WebClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                webpage.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:59.0) Gecko/20100101 Firefox/59.0";
                var data = webpage.DownloadString(url);
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(data);
                HtmlNode mdnode = htmlDoc.DocumentNode.SelectSingleNode("//meta[@name='description']");
                if (mdnode != null)
                {
                    HtmlAttribute desc;
                    desc = mdnode.Attributes["content"];
                    string fulldescription = desc.Value;
                    if (fulldescription.ToLower().Contains("powered by h5ai"))
                        return CloudServiceType.h5ai;
                }
            }

            

            return CloudServiceType.Other;
        }

        public async Task<string> GetFinalRedirect(string url)
        {
            ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            if (string.IsNullOrWhiteSpace(url))
                return url;

            int maxRedirCount = 5;  // prevent infinite loops
            string newUrl = url;
            do
            {
                HttpWebRequest req = null;
                HttpWebResponse resp = null;
                WebRequestHandler webRequestHandler = new WebRequestHandler();
                webRequestHandler.AllowAutoRedirect = false;
                HttpClient httpClient = new HttpClient(webRequestHandler);
                try
                {
                    HttpResponseMessage responseMessage = httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result;

                    //req.AllowAutoRedirect = false;
                    //resp = (HttpWebResponse)req.GetResponse();
                    switch (responseMessage.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return newUrl;
                        case HttpStatusCode.Redirect:
                        case HttpStatusCode.MovedPermanently:
                        case HttpStatusCode.RedirectKeepVerb:
                        case HttpStatusCode.RedirectMethod:
                            newUrl = responseMessage.Headers.Location.ToString();
                            if (newUrl == null)
                                return url;

                            if (newUrl.IndexOf("://", StringComparison.Ordinal) == -1)
                            {
                                // Doesn't have a URL Schema, meaning it's a relative or absolute URL
                                Uri u = new Uri(new Uri(url), newUrl);
                                newUrl = u.ToString();
                            }
                            break;
                        default:
                            return newUrl;
                    }
                    url = newUrl;
                }
                catch (WebException ex)
                {
                    // Return the last known good URL
                    return newUrl;
                }
                catch (System.Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (resp != null)
                        resp.Close();
                }
            }
            while (
            (newUrl.ToLower().Contains("snip") || newUrl.ToLower().Contains("rebrand.ly")) &&
            maxRedirCount-- > 0);

            return newUrl;
        }
        
        async Task LoadLinks(string[] links)
        {
            Fetch("https://yadi.sk/d/uXiHcdtMNc2zzA",
                "TQHSi9N+M1+HtuG/Vhg4K39OVVGdgJtTrLshvX6fB9u5f4sscPj4Sn7t5rzTtcSIq/J6bpmRyOJonT3VoXnDag==:/Fragged Aeternum",
                "y976d54f6f48c5c49dfc2793d6d32a351",
                "");

            SetProgress(true);
            CloudFolder archiveFolder = new CloudFolder("Archive", DateTime.Now, DateTime.Now, 0);
            archiveFolder.Name = "Archive";
            foreach (var l in links)
            {
                try
                {
                    string cloudFolderUrl = null;

                    syncFolders_button.Enabled = false;
                    string link = l;
                    if (!link.Contains("http"))
                        link = "https://" + link;
                    if (link.ToLower().Contains("rebrand.ly"))
                        cloudFolderUrl = GetFinalRedirect(link).Result;
                    else
                        cloudFolderUrl = link;


                    if (cloudFolderUrl == null ||
                        cloudFolderUrl.ToLower().Contains("snip.li") ||
                        cloudFolderUrl.ToLower().Contains("snipli.com") ||
                        cloudFolderUrl.ToLower().Contains("rebrand.ly"))
                    {
                        MessageBox.Show("Timeout or link is dead");
                        return;
                    }

                    cloudServiceType = GetCloudServiceType(cloudFolderUrl);

                    switch (cloudServiceType)
                    {
                        case CloudServiceType.Yadisk:
                            await LoadYadisk(cloudFolderUrl);
                            break;
                        case CloudServiceType.Allsync:
                            await LoadAllsync(cloudFolderUrl);
                            break;
                        case CloudServiceType.Mega:
                            await LoadMega(cloudFolderUrl);
                            break;
                        case CloudServiceType.h5ai:
                            await Load_h5ai(cloudFolderUrl);
                            break;
                        case CloudServiceType.TheTrove:
                            await Load_TheTrove(cloudFolderUrl);
                            break;
                        case CloudServiceType.Other:
                            MessageBox.Show("Unsupported link!");
                            break;
                    }
                    string name = l.Replace("rebrand.ly/", "").Replace("mega.nz/#F!", "");
                    archiveFolder.AddSubfolder(cloudPublicFolder);
                    archiveFolder.Size += cloudPublicFolder.Size;
                    SaveFolderJson(cloudPublicFolder, name);
                    publicFolderKey_textBox.ReadOnly = true;
                }
                catch(Exception ex)
                {
                    continue;
                }
            }
            SaveFolderJson(archiveFolder, archiveFolder.Name);    
            SetProgress(false);
        }

        async Task CreateDummyFolderStructure()
        {
            await CreateDummyFolder(cloudPublicFolder);            
        }

        async Task CreateDummyFolder(CloudFolder folder)
        {
            foreach (var file in folder.Files)
                File.Create(syncFolderPath + file.Path);

            foreach (var subfolder in folder.Subfolders)
            {
                Directory.CreateDirectory(syncFolderPath + "\\" + subfolder.Path);
                await Task.Run(() => CreateDummyFolder(subfolder as CloudFolder));
            }
        }

        #region #LOAD WEB DATA

        #region Yadisk

        async Task LoadYadisk(string publicKey)
        {          
            try
            {                
                rl_root = rc.GetPublicResource(publicKey, limit: 200);
                cloudPublicFolder = new CloudFolder(rl_root);              
                await GetFolders(new List<CloudFolder> { cloudPublicFolder });
                cloudPublicFolder.CalculateFolderSize();
                UpdateTreeModel();                
            }
            catch
            {
                MessageBox.Show("Cannot retrieve data from URL");
            }
        }

        async Task GetFolders(List<CloudFolder> folders)
        {            
            List<CloudFolder> nextLevel = new List<CloudFolder>();
  
            string rootPublicKey = folders[0].PublicKey;
            List<CloudFolder> list = new List<CloudFolder>();

            foreach (CloudFolder folder in folders)
            {
                foreach (CloudFolder subfolder in folder.Subfolders)
                    list.Add(subfolder);
            }
            ParallelOptions options = new ParallelOptions() { MaxDegreeOfParallelism = 1 };            
            Parallel.ForEach(list, options, (subfolder) =>{ GetSubfolders(subfolder, rootPublicKey);});           
           
            foreach (CloudFolder folder in folders)
            {
                foreach (CloudFolder subfolder in folder.Subfolders)
                    if (subfolder.Subfolders.Count > 0)
                        nextLevel.Add(subfolder);
            }
            if (nextLevel.Count > 0)
                await GetFolders(nextLevel);
        }

        CloudFolder GetSubfolders(CloudFolder subfolder, string rootPublicKey = null)
        {
            try
            {
                if (rootPublicKey != null && !subfolder.Path.Contains(@"disk:/"))
                {
                    ResourceList rl = rc.GetPublicResource(rootPublicKey, path: subfolder.Path, limit: 200);
                    foreach (Resource item in rl.Items)
                    {
                        if (item.Type == YandexDiskSharp.Type.dir)
                        {
                            CloudFolder ydf = new CloudFolder(item);
                            subfolder.Subfolders.Add(ydf);
                        }
                        else
                        {
                            CloudFile r = new CloudFile(item);                         
                            subfolder.AddFile(r);
                            subfolder.SizeTopDirectoryOnly += r.Size;
                        }
                    }                   
                }
                else
                    subfolder.Copy(rc.GetResource(subfolder.Path, limit: 200));
            }
            catch (Exception ex)
            { 
            }
            return subfolder;
        }

        void Fetch(string url, string hash, string sk, string path)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            Dictionary<string, string> postParams = new Dictionary<string, string>();
            postParams.Add("hash", hash);
            postParams.Add("sk", sk);
            postParams.Add("offset", "0");

            using (var client = new HttpClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod("POST"), $"https://yadi.sk/public/api/get-dir-size");

                if (postParams != null)
                    requestMessage.Content = new FormUrlEncodedContent(postParams);   // This is where your content gets added to the request body

                //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "text/plain");

                var st = HttpUtility.UrlEncode($"{{\"hash\":\"{hash}\",\"sk\":\"{sk}\"}}").Replace("+", "%20");

                byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(st);
                var content = new ByteArrayContent(messageBytes);

                var body = new StringContent(st);
                requestMessage.Content = content;               

                requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:72.0) Gecko/20100101 Firefox/72.0");
                requestMessage.Headers.Add("Referer", @"https://yadi.sk/d/uXiHcdtMNc2zzA");
                requestMessage.Headers.Add("Accept", "*/*");
                requestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                requestMessage.Headers.Add("Accept-Language", "en-US,en;q=0.5");
                requestMessage.Headers.Add("Origin", "https://yadi.sk");
                requestMessage.Headers.Add("Host", "yadi.sk");

                HttpResponseMessage response = client.SendAsync(requestMessage).Result;

                var response2 = client.PostAsync($"https://yadi.sk/public/api/get-dir-size", new StringContent(st, Encoding.UTF8, "application/json"));
                response2.Wait();

                string apiResponse = response.Content.ReadAsStringAsync().Result;
                try
                {
                    // Attempt to deserialise the reponse to the desired type, otherwise throw an expetion with the response from the api.
                    //if (apiResponse != "")
                    //    return JsonConvert.DeserializeObject<T>(apiResponse);
                    //else
                    //    throw new Exception();
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error ocurred while calling the API. It responded with the following message: {response.StatusCode} {response.ReasonPhrase}");
                }
            }
        }

        #endregion

        #region h5ai

        async Task Load_h5ai(string url)
        {           
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            cloudPublicFolder = new CloudFolder("", DateTime.Now, DateTime.Now, 0);
            List<string> uriStructure = new List<string>();
            MatchCollection mc = Regex.Matches(url, "(?:https?://)?(?:[^@\n]+@)?(?:www.)?([^:/\n?]+)");
            // GroupCollection gc = Regex.Match(url, "(?:https?://)?(?:[^@\n]+@)?(?:www.)?([^:/\n?]+)").Groups;

            foreach (Match m in mc)
                uriStructure.Add(m.Value);

            WebIndexFolderDomain = uriStructure[0];

            string path = "";
            for (int i = 1; i < uriStructure.Count; i++)
                path += @"/" + uriStructure[i];

            cloudPublicFolder.Name = uriStructure[uriStructure.Count - 1];
            cloudPublicFolder.Path = @"/";
            try
            {
                await ParseWebIndexFolder(cloudPublicFolder, path);
                cloudPublicFolder.CalculateFolderSize();
                UpdateTreeModel();
                
            }
            catch
            {
                MessageBox.Show("Cannot retrieve data from URL");
            }

        }

        async Task ParseWebIndexFolder(CloudFolder folder, string path)
        {
            if (folder.Name == "")
            {
                //[^https://]([^/]+) - every folder
                folder.Name = Regex.Match(path, ".*/(.*)/").Groups[1].Value;
                folder.Path = @"/";
            }
            string data = "";
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            using (var webpage = new System.Net.WebClient())
            {
                webpage.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:59.0) Gecko/20100101 Firefox/59.0";
                data = await webpage.DownloadStringTaskAsync(WebIndexFolderDomain + path);
            }
                        
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(data);
            HtmlNode table = htmlDoc.DocumentNode.SelectSingleNode("//body//table");
            foreach (HtmlNode row in table.ChildNodes)
            {
                if (row.ChildNodes[0].HasChildNodes && row.ChildNodes[0].FirstChild.Attributes["alt"].Value == "folder")
                {
                    CloudFolder subfolder = new CloudFolder(row.ChildNodes[1].InnerText, DateTime.Parse(row.ChildNodes[2].InnerText), DateTime.Parse(row.ChildNodes[2].InnerText), 0);
                    subfolder.Path = row.ChildNodes[1].FirstChild.Attributes["href"].Value;
                    folder.Subfolders.Add(subfolder);
                    try
                    {
                        await ParseWebIndexFolder(subfolder, subfolder.Path);
                    }
                    catch
                    {
                        continue;
                    }
                }
                if (row.ChildNodes[0].HasChildNodes && row.ChildNodes[0].FirstChild.Attributes["alt"].Value == "file")
                {
                    CloudFile file = new CloudFile(
                        row.ChildNodes[1].InnerText,
                        DateTime.Parse(row.ChildNodes[2].InnerText),
                        DateTime.Parse(row.ChildNodes[2].InnerText),
                        long.Parse(row.ChildNodes[3].InnerText.Replace("KB", "")) * 1000)
                    {
                        Path = HttpUtility.UrlDecode(row.ChildNodes[1].FirstChild.Attributes["href"].Value),
                        PublicUrl = new Uri(WebIndexFolderDomain + row.ChildNodes[1].FirstChild.Attributes["href"].Value)
                    };
                    folder.Files.Add(file);
                    folder.SizeTopDirectoryOnly += file.Size;
                }
            }
        }

        #endregion

        #region TheTrove

        async Task Load_TheTrove(string url)
        {
            url = url.Replace("index.html", "");
            if (url == "https://thetrove.is" || url == "https://thetrove.is/")
            {
                MessageBox.Show("Use path to specific folder!");
                return;
            }
            string[] bigFolders = new string[] {"Browse", "Books", "Assets" };
            foreach (var folder in bigFolders)
                if (url == $@"https://thetrove.is/{folder}" || url == $@"https://thetrove.is/{folder}/")
                {
                    MessageBox.Show("Too much data to load. Use path for more specific subfolder!");
                    return;
                }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            cloudPublicFolder = new CloudFolder("", DateTime.Now, DateTime.Now, 0);
            List<string> uriStructure = new List<string>();
            MatchCollection mc = Regex.Matches(url, "(?:https?://)?(?:[^@\n]+@)?(?:www.)?([^:/\n?]+)");            

            foreach (Match m in mc)
                uriStructure.Add(m.Value);

            TroveRootFolderAddress = uriStructure[0];

            string path = "";
            for (int i = 1; i < uriStructure.Count; i++)
            {
                TroveRootFolderAddress += @"/" + uriStructure[i];
                path += @"/" + uriStructure[i];
            }

            cloudPublicFolder.Name = uriStructure[uriStructure.Count - 1];
            cloudPublicFolder.Path = @"/";

            failedToParsePages = new List<string>();
            await ParseTheTroveFolder(cloudPublicFolder, path);
            string errorMessage = "Failed to parse some web pages ";
            if (failedToParsePages.Count > 0)
            {
                foreach (var page in failedToParsePages)
                {
                    errorMessage += $"\n{page}";
                }
                var errorForm = new ErrorForm("Warning", $"{errorMessage}");
                errorForm.Show();
            }
            cloudPublicFolder.CalculateFolderSize();                       
            UpdateTreeModel();
        }

        string EncodeTroveUrl(string url)
        {
            return url.Replace("#", "%23").Replace(",", "%2C").Replace("?", "%3F").Replace(" ", "%20"); 
        }

        string DecodeTroveUrl(string url)
        {
            return url.Replace("%23", "#").Replace("%2C", ",").Replace("%3F", "?").Replace("%20", " "); 
        }

        ErrorLogForm logForm = new ErrorLogForm();

        List<string> failedToParsePages;
        async Task ParseTheTroveFolder(CloudFolder folder, string path)
        {
            if (folder.Name == "")
            {
                //[^https://]([^/]+) - every folder
                folder.Name = Regex.Match(path, ".*/(.*)/").Groups[1].Value;
                folder.Path = @"/";
            }
            string data = "";
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var url = EncodeTroveUrl(TroveRootFolderAddress + folder.Path);
            using (var webpage = new System.Net.WebClient())
            {
                webpage.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:59.0) Gecko/20100101 Firefox/59.0";
                
                try
                {
                    //throw new System.Web.HttpException();
                    data = await webpage.DownloadStringTaskAsync(url);
                }
                catch(Exception ex)
                {
                    logForm.Show();
                    logForm.AddErrorLine($"Failed to load <{url}>");                 
                    return;
                }                   
            }

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(data);
            HtmlNode table = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='list']");
            ///html/body/div[2]/div[2]/table/tbody/tr[1]
            HtmlNodeCollection rows = htmlDoc.DocumentNode.SelectNodes("//*[@id='list']/tbody/tr");

            if (rows == null)
            {
                failedToParsePages.Add(url);               
                return;
            }

            for(int i = 1; i < rows.Count; i++)
            {
                HtmlNodeCollection cells = htmlDoc.DocumentNode.SelectNodes($"//*[@id='list']/tbody/tr[{i+1}]/td");
                HtmlNode a = cells[0].SelectSingleNode("./a");
                string href = a.Attributes["href"].Value;
                string title = a.Attributes["title"].Value;
                string date = cells[2].InnerText;
                string size = cells[1].InnerText;
                if (href.EndsWith("/"))                
                {                    
                    CloudFolder subfolder = new CloudFolder(title, DateTime.MinValue, DateTime.Parse(date), ParseSizeToKb(size));
                    subfolder.Path = folder.Path + DecodeTroveUrl(href);
                    folder.Subfolders.Add(subfolder);
                    await ParseTheTroveFolder(subfolder, subfolder.Path);
                }
                else
                {
                    CloudFile file = new CloudFile(title, DateTime.MinValue, DateTime.Parse(date), ParseSizeToKb(size))
                    {
                        Path = folder.Path + DecodeTroveUrl(href)                        
                    };
                    file.PublicUrl = new Uri(TroveRootFolderAddress + file.Path);
                    folder.AddFile(file);
                    folder.SizeTopDirectoryOnly += file.Size;
                }
            }
        }        
          
        long ParseSizeToKb(string size)
        {
            size = size.Replace('.', ',');
            if (size.Contains(" KiB"))
                return (long)Double.Parse(size.Replace(" KiB", "")) *1024;

            if (size.Contains(" MiB"))
                return (long)Double.Parse(size.Replace(" MiB", ""))*1024000;

            if (size.Contains(" GiB"))
                return (long)Double.Parse(size.Replace(" GiB", "")) *1024000000;

            return 0;
        }
        #endregion

        #region Allsync  
        
        async Task LoadAllsync(string url, bool onlyCheck = false)
        {           
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            cloudPublicFolder = new CloudFolder("", DateTime.Now, DateTime.Now, 0);
            List<string> uriStructure = new List<string>();
            url = HttpUtility.UrlDecode(url);
            MatchCollection mc = Regex.Matches(url, "(?:https?://)?(?:[^@\n]+@)?(?:www.)?([^:/\n?]+)");
            // GroupCollection gc = Regex.Match(url, "(?:https?://)?(?:[^@\n]+@)?(?:www.)?([^:/\n?]+)").Groups;

            foreach (Match m in mc)            
                uriStructure.Add(m.Value);
            allsyncUrl = uriStructure[0];
            string path = "";
            allsyncRootFolderAddress = allsyncUrl + @"/s/" + uriStructure[2] + "?path=";

            if (uriStructure.Count < 5)
            {
                cloudPublicFolder.Name = "";
                path = "/";
            }
            else
            {
                for (int i = 4; i < uriStructure.Count; i++)                
                    path += "/" + uriStructure[i];
                path += "/";
                cloudPublicFolder.Name = uriStructure[uriStructure.Count - 1];
            }
            cloudPublicFolder.Path = path;
            string password = "";
            string folderKey = uriStructure[2];
            WriteToLog($"\n{DateTime.Now}\n Searching for password for {folderKey} \n\n");
            if (savedPasswords.ContainsKey(folderKey))
            {
                password = savedPasswords[folderKey];
                WriteToLog($"\n{DateTime.Now}\n {folderKey} - password {password} \n\n");
                
            }
            UpdateWebdavClient(folderKey, password);

            if (onlyCheck)
            {
                await CheckAllsyncFolder();
                WriteToLog($"\n{DateTime.Now}\n Storing password {password} \n\n");
                WebdavCredential = new NetworkCredential { UserName = folderKey, Password = password };
                return;
            }

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var webpage = new System.Net.WebClient();
            webpage.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:59.0) Gecko/20100101 Firefox/59.0";            
            Task<string> ts = webpage.DownloadStringTaskAsync(new Uri(allsyncRootFolderAddress));
                       
            await LoadAllsyncInOneGo(uriStructure[2]);
            if (cloudPublicFolder.Subfolders.Count == 0 && cloudPublicFolder.Files.Count == 0)
                return;

            if (cloudPublicFolder.Name == "")
            {
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(ts.Result);
                HtmlNode rootFolderName = htmlDoc.DocumentNode.SelectSingleNode("//h1[@class='header-appname']");
                if (rootFolderName == null)
                    cloudPublicFolder.Name = publicFolders_comboBox.Text;
                else
                {
                    cloudPublicFolder.Name = rootFolderName.InnerText;
                    cloudPublicFolder.Name = Regex.Replace(cloudPublicFolder.Name, @"\t|\n|\r", "");
                }               
            }
            cloudPublicFolder.CalculateFolderSize();
            UpdateTreeModel();         
        }

        async Task CheckAllsyncFolder()
        { 
            IEnumerable<WebDAVClient.Model.Item> items;
            try
            {
                items = await webdavClient.List(cloudPublicFolder.Path, 1);
            }
            catch (System.Exception ex)
            {
                DialogResult continueDialogResult = MessageBox.Show("Cannot retrieve data from url. Maybe link is dead.", "", MessageBoxButtons.OK);
                continueAfterCheck = continueDialogResult == DialogResult.Yes;
                return;
            }
        }

        void WriteToLog(string message, bool force = false)
        {
            if(debugMode || force)
            {
                var logFileName = $"download-log-{DateTime.Now.ToString("MM-dd-yyyy")}.txt";
                File.AppendAllText(logFileName, message);
            }           
        }

        NetworkCredential WebdavCredential = null;

        void UpdateWebdavClient(string folderKey, string password = "")
        {
            NetworkCredential webdavCredential = new NetworkCredential { UserName = folderKey, Password = password };
            webdavClient = new Client(webdavCredential);
            webdavClient.Server = allsyncUrl;
            webdavClient.BasePath = "/public.php/webdav/";
            Dictionary<string, string> customHeaders = new Dictionary<string, string>();
            customHeaders.Add("X-Requested-With", "XMLHttpRequest");
            webdavClient.CustomHeaders = customHeaders;
        }

        async Task LoadAllsyncInOneGo(string folderKey, string password = "")
        {
            IEnumerable<WebDAVClient.Model.Item> items;           
            try
            {
                if (password != "")
                    UpdateWebdavClient(folderKey, password);
                items = await webdavClient.List(cloudPublicFolder.Path, 9999);                     
            }
            catch (WebDAVClient.Helpers.WebDAVException ex)
            {
                if (ex.GetHttpCode() == 401 || ex.GetHttpCode() == 500)
                {
                    WriteToLog($"\n{DateTime.Now}\n Password {password} wrong \n\n");
                    if (password == "")
                    {
                        PasswordForm passwordForm = new PasswordForm();
                        passwordForm.ShowDialog();
                        if (passwordForm.Password == "null")
                            return;
                        password = passwordForm.Password;
                    }

                    UpdateWebdavClient(folderKey, password);                    
                    await LoadAllsyncInOneGo(folderKey);                 
                    
                    return;
                }
                MessageBox.Show("Bad url of no connection \n" + ex.Message);
                return;
            }
            catch(Exception ex2)
            {
                MessageBox.Show("Bad url or no connection");
                return;
            }

            password = (webdavClient as Client).Credentials.Password;
            if (savedPasswords.ContainsKey(folderKey))
                savedPasswords[folderKey] = password;
            else
                savedPasswords.Add(folderKey, password);
            Properties.Settings.Default.savedPasswordsJson = JsonConvert.SerializeObject(savedPasswords);
            Properties.Settings.Default.Save();

            WebdavCredential = new NetworkCredential { UserName = folderKey, Password = password };
            WriteToLog($"\n{DateTime.Now}\nStoring(2) password: {password} \n\n");

            List<CloudFolder> allFolders = new List<CloudFolder> { cloudPublicFolder };

            foreach (var item in items)
            {
                if (item.IsCollection)
                {
                    string path = HttpUtility.UrlDecode(item.Href).Replace("/public.php/webdav", "");
                    //MatchCollection mc = Regex.Matches(item.Href, "(?:https?://)?(?:[^@\n]+@)?(?:www.)?([^:/\n?]+)");
                    if (!allFolders.ConvertAll(x => x.Path).Contains(path))
                    {
                        CloudFolder newFolder = new CloudFolder(item.DisplayName, DateTime.MinValue, (DateTime)item.LastModified, 0);
                        newFolder.Path = path;
                        allFolders.Add(newFolder);
                    }
                }
            }
            try
            {
                foreach (var folder in allFolders)
                {
                    if (folder.Path == cloudPublicFolder.Path || folder.Path == "")
                        continue;
                    string parentFolderPath = folder.Path.Remove(folder.Path.Length - 2 - folder.Name.Length, folder.Name.Length + 1);
                    //allFolders.Find(x => x.Path == parentFolderPath).Subfolders.Add(folder);
                    allFolders.Find(x => x.Path == parentFolderPath).AddSubfolder(folder);
                    //MatchCollection mc = Regex.Matches(folder.Path, "(?:[^@\n]+@)?(?:www.)?([^:/\n?]+)");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error during folder structure building.");
                return;
            }

            foreach (var item in items)
            {
                if (!item.IsCollection)
                {
                    string encodedPath = item.Href.Replace("/public.php/webdav", "/download?path=");
                    string path = HttpUtility.UrlDecode(item.Href).Replace("/public.php/webdav", "");
                    string parentFolderPath = path.Remove(path.Length - item.DisplayName.Length, item.DisplayName.Length);
                    string url = allsyncRootFolderAddress.Replace("?path=", "") + encodedPath;
                    CloudFile file = new CloudFile(
                            item.DisplayName,
                            DateTime.MinValue,
                            (DateTime)item.LastModified,
                            (long)item.ContentLength
                            );
                    file.Path = path;
                    file.PublicUrl = new Uri(url);
                    //file.PublicUrl = (webdavClient as Client).GetServerUrl(path, false).Result.Uri;

                    CloudFolder parentFolder;
                    if (parentFolderPath != "")
                        parentFolder = allFolders.Find(x => x.Path == parentFolderPath);
                    else
                        parentFolder = cloudPublicFolder;
                    parentFolder.AddFile(file);
                    parentFolder.SizeTopDirectoryOnly += file.Size;
                }
            }
        }

        #endregion

        async Task LoadMega(string url)
        {            
            MegaApiClient megaClient = new MegaApiClient();             
            megaClient.LoginAnonymous();
            int filecount = 0;
            try
            {
                url = url.Replace("#F!", "folder/").Replace("!", "#");
                string lastId = GetLastId(url);
                await Task.Run(() =>
                {
                    var nodes = megaClient.GetNodesFromLink(new Uri(url));                    
                    cloudPublicFolder = new CloudFolder(nodes.ElementAt(0).Name, nodes.ElementAt(0).CreationDate, DateTime.MinValue, 0);
                    cloudPublicFolder.Path = "/";
                    var match = Regex.Match(url, "(/folder/)([^/]+)");
                    cloudPublicFolder.PublicKey = Regex.Match(url, "(/folder/)(.*)#").Groups[2].Value;
                    Dictionary<string, CloudFolder> allfolders = new Dictionary<string, CloudFolder>();
                    allfolders.Add(nodes.ElementAt(0).Id, cloudPublicFolder);
                    List<INode> filteredNodes = nodes.ToList();
                    if (lastId != "")
                    {
                        var subRootFolderNode = nodes.Where(x => x.Id == lastId).FirstOrDefault();
                        CloudFolder subRootFolder = new CloudFolder(subRootFolderNode.Name, subRootFolderNode.CreationDate, DateTime.MinValue, subRootFolderNode.Size);
                        subRootFolder.Path = cloudPublicFolder.Path + subRootFolder.Name + "/";
                        cloudPublicFolder.AddSubfolder(subRootFolder);
                        allfolders.Add(subRootFolderNode.Id, subRootFolder);
                        filteredNodes = GetChildNodes(subRootFolderNode, nodes.ToArray());
                        filteredNodes.Reverse();
                    }
                    foreach (var node in filteredNodes)
                    {
                        if (node.Type == NodeType.Directory)
                        {
                            if (url.ToLower().Contains(node.Id.ToLower()))
                                continue;
                            CloudFolder subfolder = new CloudFolder(node.Name, node.CreationDate, DateTime.MinValue, node.Size);
                            CloudFolder parentFolder = allfolders[node.ParentId];
                            subfolder.Path = parentFolder.Path + subfolder.Name + "/";
                            allfolders.Add(node.Id, subfolder);
                            parentFolder.AddSubfolder(subfolder);
                            //parentFolder.Subfolders.Add(subfolder);
                            continue;
                        }
                        if (node.Type == NodeType.File)
                        {
                            CloudFile file = new CloudFile(node.Name, node.CreationDate, (DateTime)node.CreationDate, node.Size);
                            CloudFolder parentFolder = allfolders[node.ParentId];
                            file.Path = parentFolder.Path + file.Name;
                            file.MegaNode = node;
                            parentFolder.SizeTopDirectoryOnly += file.Size;
                            parentFolder.AddFile(file);
                            filecount++;
                            //parentFolder.Files.Add(file);
                        }
                    }

                });
                cloudPublicFolder.CalculateFolderSize();
                UpdateTreeModel();               
            }
            catch(Exception ex)
            {
                MessageBox.Show("Cannot retrieve data from URL");
                WriteToLog(ex.Message, true);
            }
        }

        private string GetLastId(string url)
        {
            //Regex uriRegex = new Regex("/(?<type>(file|folder))/(?<id>[^#]+)#(?<key>[^$/]+)(/folder/)?(?<lastid>[^/]+)");
            Regex uriRegex = new Regex(@"/(?<type>(file|folder))/(?<id>[^#]+)#(?<key>[^$/]+)(/folder/)?(?<lastid>[^/]+)?");           
            Match match = uriRegex.Match(url);
            if (match.Success == false)
            {
                throw new ArgumentException(string.Format("Invalid uri. Unable to extract Id and Key from the uri {0}", url));
            }            
            string lastId = match.Groups["lastid"].Value;
            return lastId;
           
        }

        List<INode> GetChildNodes(INode parent, INode[] nodes)
        {
            List<INode> allNewNodes = new List<INode>();
            List<INode> newNodes = new List<INode>();
            newNodes.AddRange(nodes.Where(x => x.ParentId == parent.Id));
            foreach (var node in newNodes.Where(x => x.Type == NodeType.Directory))
                allNewNodes.AddRange(GetChildNodes(node, nodes));
            allNewNodes.AddRange(newNodes);
            return allNewNodes;
        }


        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public bool LoadedFromFile = false;

        async Task LoadFolderJson(bool checkStatus = true)
        {
            var key = publicFolderKey_textBox.Text;
            if (key == "")
                checkStatus = false;

            cloudPublicFolder = new CloudFolder();
            Directory.CreateDirectory("jsons");
            string hashString = GetHashString(publicFolderKey_textBox.Text);

            foreach (string fileName in Directory.GetFiles("jsons"))
            {
                if (fileName == @"jsons\" + hashString + ".json")
                {
                    string jsonString = File.ReadAllText(fileName);
                    cloudPublicFolder = JsonConvert.DeserializeObject<CloudFolder>(jsonString, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                    UpdateTreeModel();
                    LoadedFromFile = true;
                    if (checkStatus)
                    {
                        if (publicFolderKey_textBox.Text.ToLower().Contains("snipli.com") ||
                            publicFolderKey_textBox.Text.ToLower().Contains("snip.li") ||
                            publicFolderKey_textBox.Text.ToLower().Contains("rebrand.ly"))
                            key = await GetFinalRedirect(publicFolderKey_textBox.Text);

                        cloudServiceType = GetCloudServiceType(key);
                        if (cloudServiceType == CloudServiceType.Allsync)
                        {
                            LoadAllsync(key, true);
                            if (continueAfterCheck)
                                syncFolders_button.Enabled = true;
                            continueAfterCheck = true;
                        }
                        else
                        {
                            if (cloudServiceType == CloudServiceType.Mega)
                            {
                                syncFolders_button.Enabled = false;
                            }
                            else
                                syncFolders_button.Enabled = true;
                        }
                    }
                    return;
                }
            }
        }

        void SaveFolderJson(CloudFolder folder)
        {
            Directory.CreateDirectory("jsons");
            string hashString = GetHashString(publicFolderKey_textBox.Text);
            //File.WriteAllText("jsons/" + publicFolders_comboBox.Text + ".json", JsonConvert.SerializeObject(folder));
            File.WriteAllText("jsons/" + hashString + ".json", JsonConvert.SerializeObject(folder, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            }));
            //File.WriteAllText("jsons/" + folder.Name + ".json", JsonConvert.SerializeObject(folder));
            //UpdatePublicFoldersSetting();
        }

        void SaveFolderJson(CloudFolder folder, string name)
        {
            Directory.CreateDirectory("jsons");
            //File.WriteAllText("jsons/" + publicFolders_comboBox.Text + ".json", JsonConvert.SerializeObject(folder));
            File.WriteAllText("archive/" + name + ".json", JsonConvert.SerializeObject(folder, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            }));
            //File.WriteAllText("jsons/" + folder.Name + ".json", JsonConvert.SerializeObject(folder));
            //UpdatePublicFoldersSetting();
        }

        #endregion

        #region #LOADING NODES

        void UpdateTreeModel()
        {
            yadiskPublicFolder_model = new TreeModel();
            yadiskFlatFolder_model = new TreeModel();
            ColumnNode rootNode = new ColumnNode(HttpUtility.UrlDecode(cloudPublicFolder.Name), cloudPublicFolder.Created, cloudPublicFolder.Modified, cloudPublicFolder.Size);
            rootNode.Tag = cloudPublicFolder;
           
            yadiskPublicFolder_model.Nodes.Add(rootNode);
            BuildSubfolderNodes(rootNode);
            BuildFullFolderStructure(rootNode);

            yadiskPublicFolder_treeViewAdv.Model = new SortedTreeModel(yadiskPublicFolder_model);
            yadiskPublicFolder_treeViewAdv.NodeFilter = filter;
           
            //yadiskPublicFolder_treeViewAdv.Root.Children[0].Expand();
            yadiskPublicFolder_treeViewAdv.Columns[0].MinColumnWidth = 100;

            if (syncFolderPath_textBox.Text != "")
            {
                syncFolders_button.Enabled = true;
                openSyncFolder_button.Enabled = true;
            }
        }

        public static void BuildSubfolderNodes(ColumnNode node)
        {
            if (node.Tag.GetType().ToString() == "CloudFolderBrowser.CloudFolder")
            {
                foreach (CloudFolder subfolder in ((CloudFolder)node.Tag).Subfolders)
                {
                    ColumnNode subNode = new ColumnNode(HttpUtility.UrlDecode(subfolder.Name), subfolder.Created, subfolder.Modified, subfolder.Size);
                    subNode.Tag = subfolder;
                    node.Nodes.Add(subNode);
                }
                foreach (CloudFile file in ((CloudFolder)node.Tag).Files)
                {
                    ColumnNode subNode = new ColumnNode(HttpUtility.UrlDecode(file.Name), file.Created, file.Modified, file.Size);
                    subNode.Tag = file;
                    node.Nodes.Add(subNode);
                    yadiskFlatFolder_model.Nodes.Add(new ColumnNode(subNode));
                }
            }
            else
            {
                foreach (Folder subfolder in ((Folder)node.Tag).Subfolders)
                {
                    ColumnNode subNode = new ColumnNode(HttpUtility.UrlDecode(subfolder.Name), subfolder.Created, subfolder.Modified, subfolder.Size);
                    subNode.Tag = subfolder;
                    node.Nodes.Add(subNode);
                }
                foreach (FileInfo file in ((Folder)node.Tag).Files)
                {
                    ColumnNode subNode = new ColumnNode(HttpUtility.UrlDecode(file.Name), file.CreationTime, file.LastWriteTime, file.Length);
                    subNode.Tag = file;
                    node.Nodes.Add(subNode);
                }
            }
        }

        private void BuildFullFolderStructure(ColumnNode rootNode)
        {
            foreach (ColumnNode subNode in rootNode.Nodes)
            {
                if (subNode.Tag.GetType().ToString() == "CloudFolderBrowser.CloudFolder" || subNode.Tag.GetType().ToString() == "CloudFolderBrowser.Folder")
                {
                    if (subNode.Nodes.Count == 0)
                        BuildSubfolderNodes(subNode);
                    BuildFullFolderStructure(subNode);
                }
            }
        }

        #endregion

        #region #SYNC

        async Task SyncFiles()
        {
            if (checkedFolders.Count == 0 && mixedFolders.Count == 0)
            {
                MessageBox.Show("No folders checked!");
                return;
            }
            //if (checkedFolders.Count == 1 && checkedFolders[0].Name == ((Folder)syncFolder_model.Nodes[0].Tag).Name)         
            //    return;                
         
            List<CloudFile> missingFiles = new List<CloudFile>();
            DirectoryInfo syncFolderDirectory = new DirectoryInfo(syncFolder.Path);

            foreach (CloudFolder folder in mixedFolders)
            {
                DirectoryInfo di = new DirectoryInfo(syncFolder.Path + folder.Path.Replace(@"/", @"\"));
                if(!di.Exists)
                {
                    missingFiles.AddRange(folder.Files);
                    continue;
                }                   
                FileInfo[] flatSyncFolderFilesList = di.GetFiles("*", SearchOption.TopDirectoryOnly);
                if(!hideExistingFiles_checkBox.Checked)
                    missingFiles.AddRange(folder.Files);
                else
                    missingFiles.AddRange(await CompareFilesLists(folder.Files, flatSyncFolderFilesList));
            }

            foreach (CloudFolder folder in checkedFolders)
            {        
                DirectoryInfo di = new DirectoryInfo(syncFolder.Path + folder.Path.Replace(@"/", @"\"));
                List<CloudFile> flatCloudFolderFilesList = folder.GetFlatFilesList();
                if (!di.Exists)
                {
                    missingFiles.AddRange(folder.GetFlatFilesList());
                    continue;
                }
                FileInfo[] flatSyncFolderFilesList = di.GetFiles("*", SearchOption.AllDirectories);
                if (!hideExistingFiles_checkBox.Checked)
                    missingFiles.AddRange(folder.GetFlatFilesList());
                else 
                    missingFiles.AddRange(await CompareFilesLists(flatCloudFolderFilesList, flatSyncFolderFilesList));
            }
            if (missingFiles.Count > 0)
            {
                CloudFolder newFilesFolder = new CloudFolder(cloudPublicFolder.Name, cloudPublicFolder.Created, cloudPublicFolder.Modified, cloudPublicFolder.Size);
                newFilesFolder.PublicKey = cloudPublicFolder.PublicKey;
                newFilesFolder.Files.AddRange(missingFiles);
                newFilesFolder.Size = (missingFiles.ConvertAll(x => x.Size)).Sum();

                WriteToLog($"\n{DateTime.Now}\n  Create SyncFilesForm with {WebdavCredential?.UserName}-{WebdavCredential?.Password} \n\n");
                SyncFilesForm syncFilesForm = new SyncFilesForm(this, newFilesFolder, cloudServiceType, WebdavCredential);               
                activeSyncForm = syncFilesForm;
                activeSyncForm.DownloadCompleted += SyncForm_DownloadCompleted;
            }
            else
                MessageBox.Show("No new files!");
        }

        private void SyncForm_DownloadCompleted(object sender, EventArgs e)
        {
            LoadSyncFolder(syncFolderPath_textBox.Text);
        }

        async Task<List<CloudFile>> CompareFilesLists(List<CloudFile> cloudFolderFileList, FileInfo[] syncFolderFileList)
        {          
            List<CloudFile> missingFiles = new List<CloudFile>();

            await Task.Run(() =>
            {
                List<CloudFile> localFiles = syncFolderFileList.ToList().ConvertAll(
                    x => new CloudFile(x.Name, DateTime.Now, DateTime.Now, x.Length) { Path = x.FullName.Replace(syncFolder.Path, @"\").Replace(@"\", @"/") });

                missingFiles = localFiles.Except(cloudFolderFileList, new FileComparer()).ToList();
            });
            
            return missingFiles;
        }

        class FileComparer : IEqualityComparer<CloudFile>
        {
            public bool Equals(CloudFile x, CloudFile y)
            {
                var a = WebUtility.UrlDecode(x.Path);
                var b = WebUtility.UrlDecode(y.Path);
                return (a == b);
            }

            public int GetHashCode(CloudFile x)
            {
                return x.Path.GetHashCode();
            }
        }

        #endregion

        #region #EVENT HANDLERS

        #region #TREEVIEW

        private void RefreshFolder_menuItem_Click(object sender, EventArgs e)
        {
            LoadSyncFolder(syncFolderPath_textBox.Text);
        }

        private void syncFolderPath_textBox_TextChanged(object sender, EventArgs e)
        {
            LoadSyncFolder(syncFolderPath_textBox.Text);
        }

        void LoadSyncFolder(string path)
        {
            if (path != "")
            {
                if (!Directory.Exists(syncFolderPath_textBox.Text))
                    Directory.CreateDirectory(syncFolderPath_textBox.Text);
                syncFolder = new Folder(new DirectoryInfo(syncFolderPath_textBox.Text));
                syncFolder.CalculateFolderSize();

                syncFolder_model = new TreeModel();
                ColumnNode rootNode2 = new ColumnNode(syncFolder.Name, syncFolder.Created, syncFolder.Modified, syncFolder.Size);
                rootNode2.Tag = syncFolder;
                syncFolder_treeViewAdv.Model = new SortedTreeModel(syncFolder_model);
                syncFolder_treeViewAdv.BeginUpdate();
                syncFolder_model.Nodes.Add(rootNode2);
                BuildSubfolderNodes(rootNode2);
                BuildFullFolderStructure(rootNode2);
                syncFolder_treeViewAdv.EndUpdate();
                syncFolder_treeViewAdv.Root.Children[0].Expand();

                if (cloudPublicFolder != null)
                    syncFolders_button.Enabled = true;

                openSyncFolder_button.Enabled = true;
            }
        }

        private void CollapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yadiskPublicFolder_treeViewAdv.Model = new SortedTreeModel(yadiskPublicFolder_model);
            yadiskPublicFolder_treeViewAdv.Root.Children[0].Expand();
            yadiskPublicFolder_treeViewAdv.AutoSizeColumn(yadiskPublicFolder_treeViewAdv.Columns[0]);
        }

        private void ExpandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yadiskPublicFolder_treeViewAdv.ExpandAll();
            yadiskPublicFolder_treeViewAdv.AutoSizeColumn(yadiskPublicFolder_treeViewAdv.Columns[0]);
        }

        private void CheckNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yadiskPublicFolder_model.Nodes[0].IsChecked = false;
            CheckAllSubnodes(yadiskPublicFolder_model.Nodes[0] as ColumnNode, true);
            yadiskPublicFolder_treeViewAdv.Refresh();
        }

        private void CheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yadiskPublicFolder_model.Nodes[0].IsChecked = true;
            CheckAllSubnodes(yadiskPublicFolder_model.Nodes[0] as ColumnNode, false);
            yadiskPublicFolder_treeViewAdv.Refresh();
        }


        private void treeViewAdv_Expanded(object sender, TreeViewAdvEventArgs e)
        {
            if (!e.Node.CanExpand)
                return;
            e.Node.Tree.AutoSizeColumn(e.Node.Tree.Columns[0]);
            e.Node.Tree.AutoSizeColumn(e.Node.Tree.Columns[3]);
            //e.Node.Tree.Columns[0].Width += (int) Math.Round(e.Node.Tree.Columns[0].Width * 0.3, 0);
            //e.Node.Tree.Columns[3].Width += 10;
        }

        private void treeViewAdv_Collapsed(object sender, TreeViewAdvEventArgs e)
        {
            if (!e.Node.CanExpand)
                return;
            e.Node.Tree.AutoSizeColumn(e.Node.Tree.Columns[0], false);
            e.Node.Tree.AutoSizeColumn(e.Node.Tree.Columns[3], false);
            //e.Node.Tree.Columns[0].Width += (int)Math.Round(e.Node.Tree.Columns[0].Width * 0.3, 0);
        }

        private void treeViewAdv_ColumnClicked(object sender, TreeColumnEventArgs e)
        {

            TreeColumn clicked = e.Column;
            if (clicked.SortOrder == SortOrder.Ascending)
                clicked.SortOrder = SortOrder.Descending;
            else
                clicked.SortOrder = SortOrder.Ascending;

            if(((TreeViewAdv)sender).Model != null)
                (((TreeViewAdv)sender).Model as SortedTreeModel).Comparer = new FolderItemSorter(clicked.Header, clicked.SortOrder);
        }

        private bool filter(object obj)
        {
            TreeNodeAdv viewNode = obj as TreeNodeAdv;
            Node n = viewNode != null ? viewNode.Tag as Node : obj as Node;
            ColumnNode nn = (ColumnNode)n;
            bool hideByName = n == null || n.Text.ToUpper().Contains(this.filter_textBox.Text.ToUpper()) || n.Nodes.Any(filter);
            bool hideByDate = nn == null || DateTime.Parse(nn.NodeControl3) >= afterDate_dateTimePicker.Value && DateTime.Parse(nn?.NodeControl3) <= beforeDate_dateTimePicker.Value;
            return hideByName && hideByDate;
        }

        #endregion

        private void Filter_textBox_TextChanged(object sender, EventArgs e)
        {
            yadiskPublicFolder_treeViewAdv.UpdateNodeFilter();
        }

        private void filter_TextChangedComplete(object sender, EventArgs e)
        {
            yadiskPublicFolder_treeViewAdv.UpdateNodeFilter();
        }


        private void PublicFolders_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (publicFolders_comboBox.Text != "secret_folder")            
                publicFolderKey_textBox.Text = publicFolders_comboBox.SelectedValue.ToString();            
            else
                publicFolderKey_textBox.Text = "this url is too mysterious for you";

            hotDictKey = ((KeyValuePair<string, string>)publicFolders_comboBox.SelectedItem).Key;
        }

        private void FlatList_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (flatList_checkBox.Checked)
            {
                yadiskPublicFolder_treeViewAdv.ShowNodeToolTips = true;
                yadiskPublicFolder_treeViewAdv.Model = new SortedTreeModel(yadiskFlatFolder_model);
                yadiskPublicFolder_treeViewAdv.Root.Children[0].Expand();
            }
            else
            {
                yadiskPublicFolder_treeViewAdv.ShowNodeToolTips = false;
                //yadiskPublicFolder_treeViewAdv.NodeControls[2].ToolTipProvider = null;
                yadiskPublicFolder_treeViewAdv.Model = new SortedTreeModel(yadiskPublicFolder_model);
                yadiskPublicFolder_treeViewAdv.Root.Children[0].Expand();
            }
        }
                
        #region #BUTTONS       

        private async void LoadPublicFolderKey_button_Click(object sender, EventArgs e)
        {
            string cloudFolderUrl = publicFolderKey_textBox.Text;
            if (flatList_checkBox.Checked)
                flatList_checkBox.Checked = false;

            if (publicFolderKey_textBox.Text != "")
            {
                syncFolders_button.Enabled = false;              

                if (publicFolderKey_textBox.Text.ToLower().Contains("snipli.com") ||
                    publicFolderKey_textBox.Text.ToLower().Contains("snip.li") ||
                    publicFolderKey_textBox.Text.ToLower().Contains("rebrand.ly"))
                    cloudFolderUrl = GetFinalRedirect(publicFolderKey_textBox.Text).Result;

                if (cloudFolderUrl == null ||
                    cloudFolderUrl.ToLower().Contains("snip.li") ||
                    cloudFolderUrl.ToLower().Contains("snipli.com") ||
                    cloudFolderUrl.ToLower().Contains("rebrand.ly"))
                {
                    MessageBox.Show("Timeout or link is dead");
                    return;
                }

                cloudServiceType = GetCloudServiceType(cloudFolderUrl);

                SetProgress(true);
                switch (cloudServiceType)
                {
                    case CloudServiceType.Yadisk:
                        await LoadYadisk(cloudFolderUrl);
                        break;
                    case CloudServiceType.Allsync:
                        await LoadAllsync(cloudFolderUrl);
                        break;
                    case CloudServiceType.Mega:
                        await LoadMega(cloudFolderUrl);
                        break;                    
                    case CloudServiceType.h5ai:
                        await Load_h5ai(cloudFolderUrl);
                        break;
                    case CloudServiceType.TheTrove:
                        await Load_TheTrove(cloudFolderUrl);
                        break;
                    case CloudServiceType.Other:
                        MessageBox.Show("Unsupported link!");
                        break;                   
                }
                await CreateDummyFolderStructure();
                publicFolderKey_textBox.ReadOnly = true;
                SetProgress(false);
                LoadedFromFile = false;
            }
        }

        private void SavePublicFolderKey_button_Click(object sender, EventArgs e)
        {
            if(!publicFolders.Keys.Contains(hotDictKey))
                return;
            KeyValuePair<string, string> selectedItem = publicFolders.First(x => x.Key == hotDictKey);
            string hotDictValue = selectedItem.Value;
            //publicFolders_comboBox.BeginUpdate();
            publicFolders.Remove(hotDictKey);
            if (publicFolders.ContainsKey(publicFolders_comboBox.Text))
            {
                MessageBox.Show("Folder name should be unique!");
                return;
            }
            publicFolders.Add(publicFolders_comboBox.Text, publicFolderKey_textBox.Text);
            //publicFolders_comboBox.EndUpdate();            
            publicFolders_comboBox.DataSource = new BindingSource(publicFolders, null);
            publicFolders_comboBox.Update();
            //selectedItem = new KeyValuePair<string, string>(publicFolders_comboBox.Text, selectedItem.Value);
            //publicFolders.Select(x => x.V)
            //publicFolders_comboBox.SelectedItem = publicFolders_comboBox.Text;
            UpdatePublicFoldersSetting();
            savePublicFolderKey_button.Enabled = false;
            publicFolderKey_textBox.ReadOnly = true;
        }

        private void AddNewPublicFolder_button_Click(object sender, EventArgs e)
        {
            var newkey = DateTime.Now.ToString("MM-dd-yyyy hh-mm-ss");           
            publicFolders.Add(newkey, publicFolderKey_textBox.Text);
            publicFolders_comboBox.DataSource = new BindingSource(publicFolders, null);
            //publicFolders_comboBox.Update();
            UpdatePublicFoldersSetting();
        }

        private void loginYandex_button_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.loginedYandex)
            {
                LoginForm loginForm = new LoginForm("yandex", this);
                loginYandex_button.Text = "Logout";
            }
            else
            {
                LogoutYandex();
                yadiskSpace_label.Visible = false;
                yadiskSpace_progressBar.Visible = false;
                loginYandex_button.Text = "Sign in Yandex";
            }
        }

        private void loginMega_button_Click(object sender, EventArgs e)
        {
            //TODO: loginMega
        }

        private void deletePublicFolder_button_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Delete " + publicFolders_comboBox.Text + "?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                publicFolders.Remove(publicFolders_comboBox.Text);
                publicFolders_comboBox.DataSource = new BindingSource(publicFolders, null);
                UpdatePublicFoldersSetting();
            }
        }

        private void showSyncForm_button_Click(object sender, EventArgs e)
        {
            activeSyncForm?.Show();
        }

        private void editPublicFolderKey_button_Click(object sender, EventArgs e)
        {
            publicFolderKey_textBox.ReadOnly = false;
            loadPublicFolderKey_button.Text = "Load";
            savePublicFolderKey_button.Enabled = true;
        }

        private void createArchive_button_Click(object sender, EventArgs e)
        {
            string[] links = File.ReadAllLines("links.txt");
            LoadLinks(links);
        }

        private void openSyncFolder_button_Click(object sender, EventArgs e)
        {
            Process.Start(syncFolderPath_textBox.Text);
        }

        private void appVersion_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://github.com/ptrsuder/cloud-folder-browser/releases/latest");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void syncFolders_button_Click(object sender, EventArgs e)
        {
            activeSyncForm?.CloseForm();
            checkedFolders = new List<CloudFolder>();
            mixedFolders = new List<CloudFolder>();               
            GetCheckedFolders(yadiskPublicFolder_model.Nodes[0] as ColumnNode);
            SyncFiles();
            showSyncForm_button.Enabled = true;
        }

        private void browseSyncFolder_button_Click(object sender, EventArgs e)
        {
            syncFolderPath = GetFolderPath();
            if (syncFolderPath == "")
                return;
            syncFolderPath_textBox.Text = syncFolderPath;
            Properties.Settings.Default.lastSyncFolderPath = syncFolderPath;
            Properties.Settings.Default.Save();
        }

        private void SaveToFile_button_Click(object sender, EventArgs e)
        {
            SaveFolderJson(cloudPublicFolder);
        }

        private void LoadFromFile_button_Click(object sender, EventArgs e)
        {
            LoadFolderJson();
        }
        #endregion

        #endregion

    }
    
    public class CFBSettings
    {
        string lastSyncFolderPath;
        string publicFoldersJson;
        bool loginedYandex;
        bool loginedMega;
        string accessTokenYandex;
        string secretFolderUrl;

        public CFBSettings() { }
    }

}

