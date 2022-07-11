using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using CG.Web.MegaApiClient;
using HtmlAgilityPack;
using Newtonsoft.Json;
using WebDAVClient;
using YandexDiskSharp;
using YandexDiskSharp.Models;
using Exception = System.Exception;


//https://github.com/kozakovi4/YandexDiskSharp
//https://github.com/AdamsLair/treeviewadv
//https://sourceforge.net/projects/treeviewadv/
//https://github.com/Toqe/Downloader

namespace CloudFolderBrowser
{
    public enum CloudServiceType { Yadisk, Mega, h5ai, Allsync, TheTrove, Other}    

    public partial class MainForm : Form
    {
        public MainFormModel Model { get; set; } = new MainFormModel();

        string AppVersion = "0.10.00";

        public static RestClient rc;
        public ResourceList rl_root;
        public List<Task> tasks;
        public List<Task<CloudFolder>> tasks2;
        public string syncFolderPath = "";
        public TreeModel cloudPublicFolder_model, cloudFlatFolder_model, syncFolder_model, newFiles_model;
        public LocalFolder syncFolder;
        public CloudFolder yadiskFolder;
        
        public List<CloudFolder> checkedFolders, mixedFolders;        
        Dictionary<string, string> publicFolders = new Dictionary<string, string>();
        string hotDictKey = "";
        string WebIndexFolderDomain = "";
        string TroveRootFolderAddress = "";        
        
        SyncFilesForm activeSyncForm;
        public bool usingFogLink = false;
       
        public IClient webdavClient;
        public MegaApiClient megaClient = new MegaApiClient();
        public INode MegaRootNode = null;

        int checkedFilesNumber = 0;
        double checkedFilesSize = 0.0;
        public  long freeSpace;

        const double b2Mb = 1.0 / (1024 * 1024);       

       
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
                publicFolders.Add("ExampleFolderName", "https://examplefolderurl.com");              
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
            cloudPublicFolder_treeViewAdv.NodeControls[2].ToolTipProvider = new ToolTipProvider();

            refreshFolder_menuItem.Click += refreshFolder_menuItem_Click;
            openFolder_menuItem.Click += openFolder_menuItem_Click;

            if (Properties.Settings.Default.lastSyncFolderPath != "")
            {
                syncFolderPath = Properties.Settings.Default.lastSyncFolderPath;
                syncFolderPath_textBox.Text = syncFolderPath;
            }

            HttpWebRequest.DefaultWebProxy = null;
            WebRequest.DefaultWebProxy = null;  

            if (Properties.Settings.Default.savedPasswordsJson != "")
            {
                Model.savedPasswords = JsonConvert.DeserializeObject<Dictionary<string, string>>(Properties.Settings.Default.savedPasswordsJson);
            }

            checkAllToolStripMenuItem.Click += CheckAllToolStripMenuItem_Click;
            checkNoneToolStripMenuItem.Click += CheckNoneToolStripMenuItem_Click;
            expandAllToolStripMenuItem.Click += ExpandAllToolStripMenuItem_Click;
            collapseAllToolStripMenuItem.Click += CollapseAllToolStripMenuItem_Click;

            appVersion_linkLabel.Text = "v " + AppVersion;
           
            if (Properties.Settings.Default.loginedMega && Properties.Settings.Default.loginTokenMega != "")
            {                
                var megaLoginToken = JsonConvert.DeserializeObject<MegaApiClient.LogonSessionToken>(
                    Properties.Settings.Default.loginTokenMega, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                LoginMega(megaLoginToken);                
            }

            if(Properties.Settings.Default.fogLinkAddress != "")           
                FogLink.ServerAddress = new Uri(Properties.Settings.Default.fogLinkAddress);       
            else
                FogLink.ServerAddress = new Uri("https://tg-sharer-foglink.herokuapp.com/");            
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

        string GetFolderPath()
        {
            FolderBrowserDialog fldsd = new FolderBrowserDialog();
            fldsd.Description = "Choose folder to sync";
            if (Properties.Settings.Default.lastSyncFolderPath != "")
                fldsd.InitialDirectory = Properties.Settings.Default.lastSyncFolderPath;
            else
                fldsd.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            fldsd.ShowDialog();
            return fldsd.SelectedPath;
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
            checkedFiles_label.Text = $"Selected: {Math.Round(checkedFilesSize * b2Mb, 2)} MB | {checkedFilesNumber} files";
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
                    if (cst == CheckState.Checked)
                    {
                        if (origState == CheckState.Indeterminate)
                        {

                        }
                        if (origState == CheckState.Unchecked)
                        {
                            checkedFilesSize += ((CloudFolder)subnode.Tag).SizeTopDirectoryOnly;                                                    
                        }
                    }
                    
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

        #region LOAD WEB

        #region Yadisk

        async Task LoadYadisk(string publicKey)
        {          
            try
            {                
                rl_root = rc.GetPublicResource(publicKey, limit: 200);
                Model.CloudPublicFolder = new CloudFolder(rl_root);              
                await GetFolders(new List<CloudFolder> { Model.CloudPublicFolder });
                Model.CloudPublicFolder.CalculateFolderSize();
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

        #endregion

        #region h5ai

        async Task Load_h5ai(string url)
        {
            Model.CloudPublicFolder = new CloudFolder("", DateTime.Now, DateTime.Now, 0);
            List<string> uriStructure = new List<string>();
            MatchCollection mc = Regex.Matches(url, "(?:https?://)?(?:[^@\n]+@)?(?:www.)?([^:/\n?]+)");
            // GroupCollection gc = Regex.Match(url, "(?:https?://)?(?:[^@\n]+@)?(?:www.)?([^:/\n?]+)").Groups;

            foreach (Match m in mc)
                uriStructure.Add(m.Value);

            WebIndexFolderDomain = uriStructure[0];

            string path = "";
            for (int i = 1; i < uriStructure.Count; i++)
                path += @"/" + uriStructure[i];

            Model.CloudPublicFolder.Name = uriStructure[uriStructure.Count - 1];
            Model.CloudPublicFolder.Path = @"/";
            try
            {
                await ParseWebIndexFolder(Model.CloudPublicFolder, path);
                Model.CloudPublicFolder.CalculateFolderSize();
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
            Model.CloudPublicFolder = new CloudFolder("", DateTime.Now, DateTime.Now, 0);
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

            Model.CloudPublicFolder.Name = uriStructure[uriStructure.Count - 1];
            Model.CloudPublicFolder.Path = @"/";

            failedToParsePages = new List<string>();
            await ParseTheTroveFolder(Model.CloudPublicFolder, path);
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
            Model.CloudPublicFolder.CalculateFolderSize();                       
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
        
        async Task<bool> LoadAllsync(string url, bool onlyCheck = false)
        {
            await Model.PreloadAllsync(url, onlyCheck);
            var code = await Model.LoadAllsync(Model.folderKey, Model.password);

            if (code == 401)
            {
                Model.WriteToLog($"\n{DateTime.Now}\n Password {Model.password} wrong \n\n");

                PasswordForm passwordForm = new PasswordForm();
                var dr = passwordForm.ShowDialog();

                while (200 != await Model.LoadAllsync(Model.folderKey, passwordForm.Password) || dr == DialogResult.Cancel)
                {
                    dr = passwordForm.ShowDialog();                    
                }
                if (dr == DialogResult.Cancel)
                    return false;
            }            
            
            try
            {
                if (Model.CloudPublicFolder.Name == "")
                {
                    var webpage = new System.Net.WebClient();
                    webpage.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:59.0) Gecko/20100101 Firefox/59.0";
                    var ts = await webpage.DownloadStringTaskAsync(new Uri(Model.allsyncRootFolderAddress));

                    HtmlWeb web = new HtmlWeb();
                    HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    htmlDoc.LoadHtml(ts);
                    HtmlNode rootFolderName = htmlDoc.DocumentNode.SelectSingleNode("//h1[@class='header-appname']");
                    if (rootFolderName == null)
                        Model.CloudPublicFolder.Name = publicFolders_comboBox.Text;
                    else
                    {
                        Model.CloudPublicFolder.Name = rootFolderName.InnerText;
                        Model.CloudPublicFolder.Name = Regex.Replace(Model.CloudPublicFolder.Name, @"\t|\n|\r", "");
                    }
                }
                UpdateTreeModel();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot retrieve data from URL");
                Model.WriteToLog(ex.Message, true);
                return false;
            }                
        }
      
        #endregion
               
        #region MEGA

        public async Task LoginMega(string login, string password)
        {
            try
            {
                var loginToken = await megaClient.LoginAsync(login, password);

                Properties.Settings.Default.loginTokenMega = JsonConvert.SerializeObject(loginToken, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                loginMega_button.Text = "Sign out";

                var nodes = await megaClient.GetNodesAsync();
                MegaRootNode = nodes.FirstOrDefault();
            }
            catch (ApiException ex)
            {
                LogoutMega();

                var loginMegaForm = new LoginMegaForm(this);
                loginMegaForm.ShowDialog();
                return;
            }
            
            Properties.Settings.Default.loginedMega = true;
            Properties.Settings.Default.Save();

            await GetMegaInfo();
        }
        public async Task LoginMega(MegaApiClient.LogonSessionToken token)
        {           
            try
            {
                await megaClient.LoginAsync(token);
                loginMega_button.Text = "Sign out";

                var nodes = await megaClient.GetNodesAsync();
                MegaRootNode = nodes.FirstOrDefault();
            }
            catch (ApiException ex)
            {
                LogoutMega();            

                var loginMegaForm = new LoginMegaForm(this);
                loginMegaForm.ShowDialog();
            }

            Properties.Settings.Default.loginedMega = true;
            Properties.Settings.Default.Save();

            await GetMegaInfo();
        }
        async Task GetMegaInfo()
        {
            var accInfo = await megaClient.GetAccountInformationAsync();
            var trash = accInfo.Metrics.ToArray()[2].BytesUsed;
            long totalSpace = (long)(accInfo.TotalQuota * b2Mb),
                usedSpace = (long)(accInfo.UsedQuota * b2Mb),
                trashSize = (long)(trash * b2Mb);
            freeSpace = accInfo.TotalQuota - accInfo.UsedQuota;

            yadiskSpace_progressBar.Maximum = (int)totalSpace;
            yadiskSpace_progressBar.Value = (int)(usedSpace);

            if (usedSpace >= 0.95 * (totalSpace))
            {
                yadiskSpace_progressBar.ProgressColor = Color.OrangeRed;
            }
            else
            if (usedSpace >= 0.85 * (totalSpace))
                yadiskSpace_progressBar.ProgressColor = Color.Orange;
            else
            if (usedSpace >= 0.75 * (totalSpace))
                yadiskSpace_progressBar.ProgressColor = Color.Yellow;

            yadiskSpace_progressBar.Visible = true;

            double freeGb = (totalSpace - usedSpace) * 1.0 / 1024;
            yadiskSpace_progressBar.CustomText = $"Free: {(int)(freeGb * 100.0 * 1024 / totalSpace)}% |" +
               $" {Math.Round(freeGb, 2)}" +
               $" GB out of {totalSpace / 1024} GB";
        }

        public void LogoutMega()
        {
            megaClient.Logout();
            Properties.Settings.Default.loginTokenMega = "";
            Properties.Settings.Default.loginedMega = false;
            Properties.Settings.Default.Save();

            loginMega_button.Text = "MEGA Sign in";
            yadiskSpace_progressBar.Visible = false;
        }

        async Task LoadMega(string url)
        {            
            try
            {
                await Model.LoadMega(publicFolderKey_textBox.Text);                
                UpdateTreeModel();               
            }
            catch(Exception ex)
            {
                MessageBox.Show("Cannot retrieve data from URL");
                Model.WriteToLog(ex.Message, true);
            }
        }

        #endregion

        #endregion

        #region LOAD LOCAL        

        async Task LoadFolderJson(bool checkStatus = false)
        {
            var key = publicFolderKey_textBox.Text;
            if (key == "")
                checkStatus = false;


            Model.CloudPublicFolder = new CloudFolder();
            Directory.CreateDirectory("jsons");

            if (!key.Contains("http") && !key.IsBase64String())
                key = @"https://" + key;

            if (publicFolderKey_textBox.Text.ToLower().Contains("rebrand.ly"))
                key = await Utility.GetFinalRedirect(key);

            string hashString = Utility.GetHashString(key);

            foreach (string fileName in Directory.GetFiles("jsons"))
            {
                if (fileName == @"jsons\" + hashString + ".json")
                {
                    string jsonString = File.ReadAllText(fileName);
                    Model.CloudPublicFolder = JsonConvert.DeserializeObject<CloudFolder>(jsonString, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                    UpdateTreeModel();
                    Model.LoadedFromFile = true;
                    if (checkStatus)
                    {
                        Model.CloudServiceType = Utility.GetCloudServiceType(key);
                        if (Model.CloudServiceType == CloudServiceType.Allsync)
                        {
                            var success = await LoadAllsync(key, true);
                            if (success)
                                syncFolders_button.Enabled = true;
                        }
                        else
                        {
                            if (Model.CloudServiceType == CloudServiceType.Mega)
                            {
                                syncFolders_button.Enabled = false;
                            }
                            else
                                syncFolders_button.Enabled = true;
                        }
                    }
                    flatList_checkBox.Enabled = true;
                    return;
                }
            }
        }
        
        #endregion

        #region BUILD NODE TREE

        void UpdateTreeModel()
        {
            cloudPublicFolder_model = new TreeModel();
            cloudFlatFolder_model = new TreeModel();
            ColumnNode rootNode = new ColumnNode(HttpUtility.UrlDecode(Model.CloudPublicFolder.Name), Model.CloudPublicFolder.Created, Model.CloudPublicFolder.Modified, Model.CloudPublicFolder.Size);
            rootNode.Tag = Model.CloudPublicFolder;
           
            cloudPublicFolder_model.Nodes.Add(rootNode);
            BuildSubfolderNodes(rootNode);
            BuildFullFolderStructure(rootNode);

            cloudPublicFolder_treeViewAdv.Model = new SortedTreeModel(cloudPublicFolder_model);
            cloudPublicFolder_treeViewAdv.NodeFilter = filter;           
            
            cloudPublicFolder_treeViewAdv.Columns[0].MinColumnWidth = 100;

            if (syncFolderPath_textBox.Text != "")
            {
                syncFolders_button.Enabled = true;
                refreshFolder_menuItem.Enabled = true;
                openFolder_menuItem.Enabled = true;
            }
        }

        public void BuildSubfolderNodes(ColumnNode node)
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
                    cloudFlatFolder_model.Nodes.Add(new ColumnNode(subNode));
                }
            }
            else
            {
                foreach (LocalFolder subfolder in ((LocalFolder)node.Tag).Subfolders)
                {
                    ColumnNode subNode = new ColumnNode(HttpUtility.UrlDecode(subfolder.Name), subfolder.Created, subfolder.Modified, subfolder.Size);
                    subNode.Tag = subfolder;
                    node.Nodes.Add(subNode);
                }
                foreach (FileInfo file in ((LocalFolder)node.Tag).Files)
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
                if (subNode.Tag.GetType().ToString() == "CloudFolderBrowser.CloudFolder"
                    || subNode.Tag.GetType().ToString() == "CloudFolderBrowser.LocalFolder")
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

            var missingFiles = await Model.GetMissingFiles(checkedFolders,mixedFolders, syncFolder.Path, hideExistingFiles_checkBox.Checked);

            if (missingFiles.Count > 0)
            {
                CloudFolder newFilesFolder = new CloudFolder(Model.CloudPublicFolder.Name, Model.CloudPublicFolder.Created, Model.CloudPublicFolder.Modified, Model.CloudPublicFolder.Size);
                newFilesFolder.PublicKey = Model.CloudPublicFolder.PublicKey;
                newFilesFolder.Files.AddRange(missingFiles);
                newFilesFolder.Size = (missingFiles.ConvertAll(x => x.Size)).Sum();

                Model.WriteToLog($"\n{DateTime.Now}\n  Create SyncFilesForm with {Model.WebdavCredential?.UserName}-{Model.WebdavCredential?.Password} \n\n");
                SyncFilesForm syncFilesForm = new SyncFilesForm(this, newFilesFolder, Model);               
                activeSyncForm = syncFilesForm;
                activeSyncForm.DownloadCompleted += SyncForm_DownloadCompleted;
            }
            else
                MessageBox.Show("No new files!");
        }

        private async void SyncForm_DownloadCompleted(object sender, EventArgs e)
        {
            CreateSyncFolder(syncFolderPath_textBox.Text);
            LoadSyncFolder(syncFolderPath_textBox.Text);
        }

        void CreateSyncFolder(string path)
        {
            if (path != "")
            {
                if (!Directory.Exists(syncFolderPath_textBox.Text))
                    Directory.CreateDirectory(syncFolderPath_textBox.Text);
                syncFolder = new LocalFolder(new DirectoryInfo(syncFolderPath_textBox.Text));
                syncFolder.CalculateFolderSize();
            }
        }

        async Task LoadSyncFolder(string path)
        {
            if (path != "")
            {
                syncFolder_model = new TreeModel();
                ColumnNode rootNode2 = new ColumnNode(syncFolder.Name, syncFolder.Created, syncFolder.Modified, syncFolder.Size);
                rootNode2.Tag = syncFolder;

                BuildSubfolderNodes(rootNode2);
                BuildFullFolderStructure(rootNode2);
                syncFolder_model.Nodes.Add(rootNode2);

                syncFolder_treeViewAdv.Model = new SortedTreeModel(syncFolder_model);

                syncFolder_treeViewAdv.Root.Children[0].Expand();

                if (Model.CloudPublicFolder != null)
                    syncFolders_button.Enabled = true;

                openFolder_menuItem.Enabled = true;
                refreshFolder_menuItem.Enabled = true;
            }
        }

        #endregion


        #region #EVENT HANDLERS

        #region #TREEVIEW

        private async void refreshFolder_menuItem_Click(object sender, EventArgs e)
        {
            CreateSyncFolder(syncFolderPath_textBox.Text);
            LoadSyncFolder(syncFolderPath_textBox.Text);
        }
        private async void openFolder_menuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = syncFolderPath_textBox.Text, UseShellExecute = true });            
        }
        private async void syncFolderPath_textBox_TextChanged(object sender, EventArgs e)
        {
            CreateSyncFolder(syncFolderPath_textBox.Text);
            LoadSyncFolder(syncFolderPath_textBox.Text);
        }

      
        private void CollapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cloudPublicFolder_treeViewAdv.Model = new SortedTreeModel(cloudPublicFolder_model);
            cloudPublicFolder_treeViewAdv.Root.Children[0].Expand();
            cloudPublicFolder_treeViewAdv.AutoSizeColumn(cloudPublicFolder_treeViewAdv.Columns[0]);
        }

        private void ExpandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cloudPublicFolder_treeViewAdv.ExpandAll();
            cloudPublicFolder_treeViewAdv.AutoSizeColumn(cloudPublicFolder_treeViewAdv.Columns[0]);
        }

        private void CheckNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cloudPublicFolder_model.Nodes[0].IsChecked = false;
            CheckAllSubnodes(cloudPublicFolder_model.Nodes[0] as ColumnNode, true);
            checkedFilesSize = checkedFilesNumber = 0;
            checkedFiles_label.Text = $"Selected: {Math.Round(checkedFilesSize * b2Mb, 2)} MB | {checkedFilesNumber} files";
            cloudPublicFolder_treeViewAdv.Refresh();
        }

        private void CheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cloudPublicFolder_model.Nodes[0].IsChecked = true;
            CheckAllSubnodes(cloudPublicFolder_model.Nodes[0] as ColumnNode, false);
            checkedFilesSize = Model.CloudPublicFolder.Size;
            checkedFilesNumber = Model.CloudPublicFolder.FilesNumber;       
            checkedFiles_label.Text = $"Selected: {Math.Round(checkedFilesSize * b2Mb, 2)} MB | {checkedFilesNumber} files";
            cloudPublicFolder_treeViewAdv.Refresh();
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
            cloudPublicFolder_treeViewAdv.UpdateNodeFilter();
        }

        private void filter_TextChangedComplete(object sender, EventArgs e)
        {
            cloudPublicFolder_treeViewAdv.UpdateNodeFilter();
        }

        private void PublicFolders_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = ((KeyValuePair<string, string>)publicFolders_comboBox.SelectedItem);
            publicFolderKey_textBox.Text = selectedItem.Value;
            hotDictKey = selectedItem.Key;
        }

        private void FlatList_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (flatList_checkBox.Checked)
            {
                cloudPublicFolder_treeViewAdv.ShowNodeToolTips = true;
                cloudPublicFolder_treeViewAdv.Model = new SortedTreeModel(cloudFlatFolder_model);
                cloudPublicFolder_treeViewAdv.Root.Children[0].Expand();
            }
            else
            {
                cloudPublicFolder_treeViewAdv.ShowNodeToolTips = false;
                //yadiskPublicFolder_treeViewAdv.NodeControls[2].ToolTipProvider = null;
                cloudPublicFolder_treeViewAdv.Model = new SortedTreeModel(cloudPublicFolder_model);
                cloudPublicFolder_treeViewAdv.Root.Children[0].Expand();
            }
        }
                
        #region #BUTTONS       

        private async void LoadPublicFolderKey_button_Click(object sender, EventArgs e)
        {
            string cloudFolderUrl = publicFolderKey_textBox.Text;
            if (flatList_checkBox.Checked)
                flatList_checkBox.Checked = false;

            if (cloudFolderUrl != "")
            {               
                syncFolders_button.Enabled = false;

                if (cloudFolderUrl.IsBase64String())
                {
                    Model.CloudServiceType = CloudServiceType.Mega;
                    usingFogLink = true;
                    SetProgress(true);
                    await Model.LoadMega(await FogLink.GetDecodedAsync(cloudFolderUrl), publicFolderKey_textBox.Text);
                    SetProgress(false);
                    Model.LoadedFromFile = false;
                    UpdateTreeModel();
                    return;
                }

                if (!cloudFolderUrl.Contains("http"))
                    cloudFolderUrl = @"https://" + cloudFolderUrl;

                if (cloudFolderUrl.ToLower().Contains("rebrand.ly"))
                    cloudFolderUrl = Utility.GetFinalRedirect(cloudFolderUrl).Result;

                if (cloudFolderUrl == null ||
                    cloudFolderUrl.ToLower().Contains("rebrand.ly") ||
                    cloudFolderUrl.ToLower().Contains("rebrandly"))
                {
                    MessageBox.Show("Timeout or link is dead");
                    return;
                }

                Model.CloudServiceType = Utility.GetCloudServiceType(cloudFolderUrl);

                SetProgress(true);
                switch (Model.CloudServiceType)
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
                //await CreateDummyFolderStructure();
                publicFolderKey_textBox.ReadOnly = true;
                flatList_checkBox.Enabled = true;
                SetProgress(false);
                Model.LoadedFromFile = false;
            }
        }

        private void SavePublicFolderKey_button_Click(object sender, EventArgs e)
        {
            if(!publicFolders.Keys.Contains(hotDictKey))
                return;
            KeyValuePair<string, string> selectedItem = publicFolders.First(x => x.Key == hotDictKey);
            string hotDictValue = selectedItem.Value;
            
            publicFolders.Remove(hotDictKey);
            if (publicFolders.ContainsKey(publicFolders_comboBox.Text))
            {
                MessageBox.Show("Folder name should be unique!");
                return;
            }
            publicFolders.Add(publicFolders_comboBox.Text, publicFolderKey_textBox.Text);
                    
            publicFolders_comboBox.DataSource = new BindingSource(publicFolders, null);
            publicFolders_comboBox.Update();
          
            UpdatePublicFoldersSetting();            
            publicFolderKey_textBox.ReadOnly = true;
        }

        private void addPublicFolder_button_Click(object sender, EventArgs e)
        {
            var form = new EditLinkForm();
            var dr = form.ShowDialog();
            if (dr != DialogResult.OK)
                return;

            if (publicFolders.ContainsKey(form.LinkName))
                publicFolders[form.LinkName] = form.LinkUrl;
            else
                publicFolders.Add(form.LinkName, form.LinkUrl);

            publicFolders_comboBox.DataSource = new BindingSource(publicFolders, null);
            UpdatePublicFoldersSetting();
        }

        private void editPublicFolderKey_button_Click(object sender, EventArgs e)
        {
            var form = new EditLinkForm(publicFolders_comboBox.Text, publicFolderKey_textBox.Text);
            var dr = form.ShowDialog();
            if (dr != DialogResult.OK)
                return;

            var key = form.LinkName;

            if (!publicFolders.Keys.Contains(hotDictKey))
                return;
            KeyValuePair<string, string> selectedItem = publicFolders.First(x => x.Key == hotDictKey);
            string hotDictValue = selectedItem.Value;

            if (hotDictKey != key)
            {
                publicFolders.Remove(hotDictKey);
                publicFolders.Add(key, form.LinkUrl);
            }
            else
            {
                publicFolders[key] = form.LinkUrl;
            }

            publicFolders_comboBox.DataSource = new BindingSource(publicFolders, null);
            UpdatePublicFoldersSetting();   
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

        private async void loginMega_button_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.loginedMega)
            {
                var loginMegaForm = new LoginMegaForm(this);
                var result = loginMegaForm.ShowDialog();                
            }
            else
            {
                LogoutMega();                           
                loginMega_button.Text = "MEGA Sign in";
                yadiskSpace_progressBar.Visible = false;
            }
        }

        private void showSyncForm_button_Click(object sender, EventArgs e)
        {
            activeSyncForm?.Show();
        }
      
        private void createArchive_button_Click(object sender, EventArgs e)
        {          
        }

        private void appVersion_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = @"https://github.com/ptrsuder/cloud-folder-browser/releases/latest", UseShellExecute = true });                    
        }

        private void fogLink_button_Click(object sender, EventArgs e)
        {
            var form = new FogLinkForm();
            form.Show();
        }
             

        private async void syncFolders_button_Click(object sender, EventArgs e)
        {
            activeSyncForm?.CloseForm();
            checkedFolders = new List<CloudFolder>();
            mixedFolders = new List<CloudFolder>();               
            GetCheckedFolders(cloudPublicFolder_model.Nodes[0] as ColumnNode);
            await SyncFiles();
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
            Model.CloudPublicFolder.SaveToJson();
        }

        private void LoadFromFile_button_Click(object sender, EventArgs e)
        {
            LoadFolderJson(true);
        }
        #endregion

        #endregion
    }    
}

