using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
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
using System.Diagnostics;
using Bluegrams.Application;


//https://github.com/kozakovi4/YandexDiskSharp
//https://github.com/AdamsLair/treeviewadv
//https://sourceforge.net/projects/treeviewadv/
//https://github.com/Toqe/Downloader

namespace CloudFolderBrowser
{
    public enum CloudServiceType { Yadisk, Mega, h5ai, Allsync, TheTrove, Other}
       

    public partial class MainForm : Form
    {
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
        const string allsyncUrl = "https://cloud.allsync.com";
        SyncFilesForm activeSyncForm;
        bool continueAfterCheck = true;

        IClient webdavClient;
        List<CloudFolder> nextLevel;

        int checkedFilesNumber = 0;
        double checkedFilesSize = 0.0;
        public static long freeSpace;

        public CloudServiceType cloudServiceType;

        //Textbox filter
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);


        public MainForm()
        {
            InitializeComponent();                                    

            SendMessage(filter_textBox.Handle, 0x1501, 1, "Filter by name");

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

            yadiskPublicFolder_treeViewAdv.ContextMenuStrip = nodeItem_contextMenuStrip;

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

            Resource rl = rc.GetResource("disk:/");
            yadiskFolder = new CloudFolder(rl);
            GetFolders(new List<CloudFolder> { yadiskFolder });

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
            ColumnNode checkedNode = (ColumnNode)e.Path.FirstNode;
            if (checkedNode.CheckState == CheckState.Checked)
            {
                //if (checkedNode.Parent.CheckState != CheckState.Checked && checkedNode.Parent.Index !=-1)
                //    UpdateParentCheckState((ColumnNode)checkedNode.Parent);
                //CheckAllSubnodes(checkedNode, false);
                checkedFilesSize += ((CloudFolder)checkedNode.Tag).Size - ((CloudFolder)checkedNode.Tag).SizeTopDirectoryOnly;
                label1.Text = $"{Math.Round(checkedFilesSize / 1024000.0, 2)} MB checked";
                return;
            }
            if (checkedNode.CheckState == CheckState.Unchecked)
            {
                checkedFilesSize -= ((CloudFolder)checkedNode.Tag).Size;
                if (checkedFilesSize < 0.0001)
                    checkedFilesSize = 0;
                label1.Text = $"{Math.Round(checkedFilesSize / 1024000.0, 2)} MB checked";

                //if (checkedNode.Parent.Index != -1)
                //    UpdateParentCheckState((ColumnNode)checkedNode.Parent);                
                //CheckAllSubnodes(checkedNode, true);                                
                return;
            }
            if (checkedNode.CheckState == CheckState.Indeterminate)
            {
                checkedFilesSize += ((CloudFolder)checkedNode.Tag).SizeTopDirectoryOnly;//((CloudFolder)parentNode.Tag).Files.Sum(x => x.Size) / 1024000.0;
                //checkedFilesSize = Math.Round(checkedFilesSize, 2);
                label1.Text = $"{Math.Round(checkedFilesSize / 1024000.0, 2)} MB checked";
                //if(parentNode.Parent.Index != -1)
                //UpdateParentCheckState((ColumnNode)parentNode.Parent);                
                //CheckAllSubnodes(parentNode, true);    
                return;
            }

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

        #region #LOAD WEB DATA

        #region Yadisk

        void LoadYadisk(string publicKey)
        {          
            try
            {
                rl_root = rc.GetPublicResource(publicKey, limit: 999);
                cloudPublicFolder = new CloudFolder(rl_root);
                GetFolders(new List<CloudFolder> { cloudPublicFolder });
                cloudPublicFolder.CalculateFolderSize();
                UpdateTreeModel();                
            }
            catch
            {
                MessageBox.Show("Cannot retrieve data from URL");
            }
        }

        void GetFolders(List<CloudFolder> folders)
        {
            tasks = new List<Task>();
            List<CloudFolder> nextLevel = new List<CloudFolder>();
            CloudFolder s;
            string rootPublicKey = folders[0].PublicKey;
            foreach (CloudFolder folder in folders)
            {
                foreach (CloudFolder subfolder in folder.Subfolders)
                    tasks.Add(Task.Factory.StartNew(function: () => s = GetSubfolders(subfolder, rootPublicKey)));
            }
            Task.WaitAll(tasks.ToArray());
            tasks.Clear();
            foreach (CloudFolder folder in folders)
            {
                foreach (CloudFolder subfolder in folder.Subfolders)
                    if (subfolder.Subfolders.Count > 0)
                        nextLevel.Add(subfolder);
            }
            if (nextLevel.Count > 0)
                GetFolders(nextLevel);
        }

        CloudFolder GetSubfolders(CloudFolder subfolder, string rootPublicKey = null)
        {
            try
            {
                if (rootPublicKey != null && !subfolder.Path.Contains(@"disk:/"))
                {
                    ResourceList rl = rc.GetPublicResource(rootPublicKey, path: subfolder.Path, limit: 999);
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
                    //subfolder.Size = Math.Round(subfolder.Size / 1024000, 2);
                }
                else
                    subfolder.Copy(rc.GetResource(subfolder.Path, limit: 999));
            }
            catch (System.Exception e)
            { }
            return subfolder;
        }

        #endregion

        #region h5ai

        void Load_h5ai(string url)
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
                ParseWebIndexFolder(cloudPublicFolder, path);
                cloudPublicFolder.CalculateFolderSize();
                UpdateTreeModel();
                
            }
            catch
            {
                MessageBox.Show("Cannot retrieve data from URL");
            }

        }

        void ParseWebIndexFolder(CloudFolder folder, string path)
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
                data = webpage.DownloadString(WebIndexFolderDomain + path);
            }

            HtmlWeb web = new HtmlWeb();
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
                    ParseWebIndexFolder(subfolder, subfolder.Path);
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

        #region THE TROVE

        void Load_TheTrove(string url)
        {
            url = url.Replace("index.html", "");
            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            cloudPublicFolder = new CloudFolder("", DateTime.Now, DateTime.Now, 0);
            List<string> uriStructure = new List<string>();
            MatchCollection mc = Regex.Matches(url, "(?:https?://)?(?:[^@\n]+@)?(?:www.)?([^:/\n?]+)");
            // GroupCollection gc = Regex.Match(url, "(?:https?://)?(?:[^@\n]+@)?(?:www.)?([^:/\n?]+)").Groups;

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

            ParseTheTroveFolder(cloudPublicFolder, path);
            cloudPublicFolder.Size = cloudPublicFolder.SizeTopDirectoryOnly + cloudPublicFolder.Subfolders.Sum(x => x.Size);
            //cloudPublicFolder.CalculateFolderSize();
            UpdateTreeModel();           
        }

        void ParseTheTroveFolder(CloudFolder folder, string path)
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
                data = webpage.DownloadString(TroveRootFolderAddress + folder.Path);
            }

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(data);
            HtmlNode table = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='file-list']");
            HtmlNodeCollection rows = htmlDoc.DocumentNode.SelectNodes("//*[@id='file-list']/tr");


            for(int i = 1; i < rows.Count; i++)
            {
                HtmlNodeCollection cells = htmlDoc.DocumentNode.SelectNodes($"//*[@id='file-list']/tr[{i+1}]/td");
                if(rows[i].Attributes["class"].Value == "litem dir")                
                {                    
                    CloudFolder subfolder = new CloudFolder(cells[1].InnerText, DateTime.MinValue, DateTime.Parse(cells[2].InnerText), ParseSizeToKb(cells[3].InnerText));
                    subfolder.Path = folder.Path + HttpUtility.UrlDecode(cells[1].FirstChild.Attributes["href"].Value) + "/";
                    folder.Subfolders.Add(subfolder);
                    ParseTheTroveFolder(subfolder, subfolder.Path);
                }
                if (rows[i].Attributes["class"].Value == "litem file")
                {
                    CloudFile file = new CloudFile(cells[1].InnerText, DateTime.MinValue, DateTime.Parse(cells[2].InnerText), ParseSizeToKb(cells[3].InnerText))
                    {
                        Path = folder.Path + HttpUtility.UrlDecode(cells[1].FirstChild.Attributes["href"].Value.Remove(0,1))                        
                    };
                    file.PublicUrl = new Uri(TroveRootFolderAddress + file.Path);
                    folder.AddFile(file);
                    folder.SizeTopDirectoryOnly += file.Size;
                }
            }
        }
             
        public static string GetFinalRedirect(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            if (string.IsNullOrWhiteSpace(url))
                return url;

            int maxRedirCount = 5;  // prevent infinite loops
            string newUrl = url;
            do
            {
                HttpWebRequest req = null;
                HttpWebResponse resp = null;
                try
                {
                    req = (HttpWebRequest)HttpWebRequest.Create(url);
                    req.Method = "HEAD";
                    req.AllowAutoRedirect = false;
                    resp = (HttpWebResponse)req.GetResponse();
                    switch (resp.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return newUrl;
                        case HttpStatusCode.Redirect:
                        case HttpStatusCode.MovedPermanently:
                        case HttpStatusCode.RedirectKeepVerb:
                        case HttpStatusCode.RedirectMethod:
                            newUrl = resp.Headers["Location"];
                            if (newUrl == null)
                                return url;

                            if (newUrl.IndexOf("://", System.StringComparison.Ordinal) == -1)
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
                catch (WebException)
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
            } while (newUrl.Contains("snip") || maxRedirCount-- > 0);

            return newUrl;
        }

        long ParseSizeToKb(string size)
        {
            size = size.Replace('.', ',');
            if (size.Contains(" KB"))
                return (long)Double.Parse(size.Replace(" KB", "")) *1024;

            if (size.Contains(" MB"))
                return (long)Double.Parse(size.Replace(" MB", ""))*1024000;

            if (size.Contains(" GB"))
                return (long)Double.Parse(size.Replace(" GB", "")) *1024000000;

            return 0;
        }
        #endregion

        #region Allsync  
        
        async void LoadAllsync(string url, bool onlyCheck = false)
        {            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            cloudPublicFolder = new CloudFolder("", DateTime.Now, DateTime.Now, 0);
            List<string> uriStructure = new List<string>();
            url = HttpUtility.UrlDecode(url);
            MatchCollection mc = Regex.Matches(url, "(?:https?://)?(?:[^@\n]+@)?(?:www.)?([^:/\n?]+)");
            // GroupCollection gc = Regex.Match(url, "(?:https?://)?(?:[^@\n]+@)?(?:www.)?([^:/\n?]+)").Groups;

            foreach (Match m in mc)            
                uriStructure.Add(m.Value);            

            string path = "";
            allsyncRootFolderAddress = @"https://cloud.allsync.com/s/" + uriStructure[2] + "?path=";

            if (uriStructure.Count < 5)
            {
                cloudPublicFolder.Name = "";
                path = "/";
            }
            else
            {
                for (int i = 4; i < uriStructure.Count; i++)                
                    path += "/" + uriStructure[i] + "/";
                
                cloudPublicFolder.Name = uriStructure[uriStructure.Count - 1];
            }
            cloudPublicFolder.Path = path;
            //RyadiskPublicFolderegex.Match(url, ".*/(.*)/").Groups[1].Value;   

            webdavClient = new Client(new NetworkCredential { UserName = uriStructure[2], Password = "" });
            webdavClient.Server = allsyncUrl;
            webdavClient.BasePath = "/public.php/webdav/";

            if(onlyCheck)
            {
                CheckAllsyncFolder();
                return;
            }

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var webpage = new System.Net.WebClient();
            webpage.Headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:59.0) Gecko/20100101 Firefox/59.0";            
            Task<string> ts = webpage.DownloadStringTaskAsync(new Uri(allsyncRootFolderAddress));

            await LoadAllsyncInOneGo();
            if (cloudPublicFolder.Subfolders.Count == 0)
                return;

            if (cloudPublicFolder.Name == "")
            {
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(ts.Result);
                HtmlNode rootFolderName = htmlDoc.DocumentNode.SelectSingleNode("//h1[@class='header-appname']");
                cloudPublicFolder.Name = rootFolderName.InnerText;
                cloudPublicFolder.Name = Regex.Replace(cloudPublicFolder.Name, @"\t|\n|\r", "");
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
                DialogResult continueDialogResult = MessageBox.Show("Cannot retrieve data from url. Maybe link is dead. Still continue?", "", MessageBoxButtons.YesNo);
                continueAfterCheck = continueDialogResult == DialogResult.Yes;
                return;
            }
        }

        async Task LoadAllsyncInOneGo()
        {
            IEnumerable<WebDAVClient.Model.Item> items;
            try
            {
                items = await webdavClient.List(cloudPublicFolder.Path, 9999);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Bad url of no connection");
                return;
            }
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

            }

            foreach (var item in items)
            {
                if (!item.IsCollection)
                {
                    string path = HttpUtility.UrlDecode(item.Href).Replace("/public.php/webdav", "");
                    string parentFolderPath = path.Remove(path.Length - item.DisplayName.Length, item.DisplayName.Length);
                    string url = allsyncRootFolderAddress.Replace("?", "/download?") + HttpUtility.UrlEncode(parentFolderPath.Remove(parentFolderPath.Length - 1, 1)) + "&files=" + HttpUtility.UrlEncode(item.DisplayName);
                    CloudFile file = new CloudFile(
                            item.DisplayName,
                            DateTime.MinValue,
                            (DateTime)item.LastModified,
                            (long)item.ContentLength
                            );
                    file.Path = path;
                    file.PublicUrl = new Uri(url);

                    CloudFolder parentFolder;
                    if (parentFolderPath != "")
                        parentFolder = allFolders.Find(x => x.Path == parentFolderPath);
                    else
                        parentFolder = cloudPublicFolder;
                    parentFolder.AddFile(file);
                    //parentFolder.Files.Add(file);
                    parentFolder.SizeTopDirectoryOnly += file.Size;
                }
            }
        }

        #endregion

        void LoadMega(string url)
        {
            
            MegaApiClient megaClient = new MegaApiClient();
            megaClient.LoginAnonymous();
            try
            {
                var nodes = megaClient.GetNodesFromLink(new Uri(url));
                cloudPublicFolder = new CloudFolder(nodes.ElementAt(0).Name, nodes.ElementAt(0).CreationDate, DateTime.MinValue, 0);
                cloudPublicFolder.Path = "/";
                Dictionary<string, CloudFolder> allfolders = new Dictionary<string, CloudFolder>();
                allfolders.Add(nodes.ElementAt(0).Id, cloudPublicFolder);
                foreach (var node in nodes)
                {
                    if (node.Type == NodeType.Directory)
                    {
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
                        CloudFile file = new CloudFile(node.Name, node.CreationDate, (DateTime)node.ModificationDate, node.Size);
                        CloudFolder parentFolder = allfolders[node.ParentId];
                        file.Path = parentFolder.Path + file.Name;
                        file.MegaNode = node;
                        parentFolder.SizeTopDirectoryOnly += file.Size;
                        parentFolder.AddFile(file);
                        //parentFolder.Files.Add(file);
                    }
                }
                cloudPublicFolder.CalculateFolderSize();
                UpdateTreeModel();               
            }
            catch
            {
                MessageBox.Show("Cannot retrieve data from URL");
            }
        }

        void LoadFolderJson()
        {
            cloudServiceType = GetCloudServiceType(yadiskPublicFolderKey_textBox.Text);
            cloudPublicFolder = new CloudFolder();
            Directory.CreateDirectory("jsons");
            foreach (string fileName in Directory.GetFiles("jsons"))
            {
                if (fileName == @"jsons\" + publicFolders_comboBox.Text + ".json")
                {
                    string jsonString = File.ReadAllText(fileName);
                    cloudPublicFolder = JsonConvert.DeserializeObject<CloudFolder>(jsonString, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                    UpdateTreeModel();

                    if(cloudServiceType == CloudServiceType.Allsync)
                    {
                        LoadAllsync(yadiskPublicFolderKey_textBox.Text, true);
                        if (continueAfterCheck)
                            syncFolders_button.Enabled = true;
                        continueAfterCheck = true;
                    }
                    else
                        syncFolders_button.Enabled = true;
                    return;
                }
            }
        }

        void SaveFolderJson(CloudFolder folder)
        {
            Directory.CreateDirectory("jsons");
            //File.WriteAllText("jsons/" + publicFolders_comboBox.Text + ".json", JsonConvert.SerializeObject(folder));
            File.WriteAllText("jsons/" + publicFolders_comboBox.Text + ".json", JsonConvert.SerializeObject(folder, new JsonSerializerSettings()
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
            yadiskPublicFolder_treeViewAdv.BeginUpdate();
            yadiskPublicFolder_model.Nodes.Add(rootNode);
            BuildSubfolderNodes(rootNode);
            BuildFullFolderStructure(rootNode);

            yadiskPublicFolder_treeViewAdv.Model = new SortedTreeModel(yadiskPublicFolder_model);
            yadiskPublicFolder_treeViewAdv.NodeFilter = filter;

            yadiskPublicFolder_treeViewAdv.EndUpdate();
            yadiskPublicFolder_treeViewAdv.Root.Children[0].Expand();

            if(syncFolderPath_textBox.Text !="")
                syncFolders_button.Enabled = true;
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

        void SyncFiles()
        {
            if (checkedFolders.Count == 0 && mixedFolders.Count == 0)
            {
                MessageBox.Show("No folders checked!");
                return;
            }
            //if (checkedFolders.Count == 1 && checkedFolders[0].Name == ((Folder)syncFolder_model.Nodes[0].Tag).Name)
            //{
            //    return;                
            //}
            List<CloudFile> missingFiles = new List<CloudFile>();
            DirectoryInfo syncFolderDirectory = new DirectoryInfo(syncFolder.Path);

            foreach (CloudFolder folder in mixedFolders)
            {
                if (folder.Path == @"/" || syncFolder.Subfolders.ConvertAll(x => x.Path.Replace(syncFolder.Path, @"\")).Contains(folder.Path.Replace("/", @"\")))
                {
                    DirectoryInfo di = new DirectoryInfo(syncFolder.Path + folder.Path.Replace(@"/", @"\"));
                    FileInfo[] flatSyncFolderFilesList = di.GetFiles("*", SearchOption.TopDirectoryOnly);
                    missingFiles.AddRange(CompareFilesLists(folder.Files, flatSyncFolderFilesList));
                }
                else
                {
                    Directory.CreateDirectory(syncFolder.Path + folder.Path.Replace(@"/", @"\"));
                    missingFiles.AddRange(folder.Files);
                }
            }

            foreach (CloudFolder folder in checkedFolders)
            {
                //string spath = syncFolder.Subfolders[0].Path.Replace(syncFolder.Path, @"\");
                //string fpath = folder.Path.Replace("/", @"\");
                //bool a = spath == fpath;
                if (folder.Path == @"/" || folder.Path == null || syncFolder.Subfolders.ConvertAll(x => x.Path.Replace(syncFolder.Path, @"\")).Contains(folder.Path.Replace("/", @"\")))
                //if (folder.Path == @"/" || (DirectoryInfo[] dis = syncFolderDirectory.GetDirectories("*", SearchOption.AllDirectories)).Count() > 0)
                {
                    DirectoryInfo di = new DirectoryInfo(syncFolder.Path + folder.Path.Replace(@"/", @"\"));
                    List<CloudFile> flatYadiskFilesList = folder.GetFlatFilesList();
                    FileInfo[] flatSyncFolderFilesList = di.GetFiles("*", SearchOption.AllDirectories);
                    missingFiles.AddRange(CompareFilesLists(flatYadiskFilesList, flatSyncFolderFilesList));
                }
                else
                {
                    Directory.CreateDirectory(syncFolder.Path + folder.Path.Replace(@"/", @"\"));
                    missingFiles.AddRange(folder.GetFlatFilesList());
                }
            }
            if (missingFiles.Count > 0)
            {
                CloudFolder newFilesFolder = new CloudFolder(cloudPublicFolder.Name, cloudPublicFolder.Created, cloudPublicFolder.Modified, cloudPublicFolder.Size);
                newFilesFolder.PublicKey = cloudPublicFolder.PublicKey;
                newFilesFolder.Files.AddRange(missingFiles);
                newFilesFolder.Size = (missingFiles.ConvertAll(x => x.Size)).Sum();
                SyncFilesForm syncFilesForm = new SyncFilesForm(newFilesFolder, cloudServiceType);
                activeSyncForm = syncFilesForm;
                syncFilesForm.Show();
            }
            else
                MessageBox.Show("No new files!");
        }

        List<CloudFile> CompareFilesLists(List<CloudFile> yadiskFilesList, FileInfo[] syncFolderFilesList)
        {
            bool isMissing;
            List<CloudFile> missingFiles = new List<CloudFile>();
            foreach (CloudFile file in yadiskFilesList)
            {
                isMissing = true;
                foreach (FileInfo sfile in syncFolderFilesList)
                {
                    string path = sfile.FullName.Replace(syncFolder.Path, @"\");
                    if (sfile.Name == file.Name && path.Replace(@"\", @"/") == file.Path)
                    {
                        isMissing = false;
                        break;
                    }
                }
                if (isMissing)
                    missingFiles.Add(file);
            }
            return missingFiles;
        }

        #endregion

        #region #EVENT HANDLERS

        #region #TREEVIEW

        private void syncFolderPath_textBox_TextChanged(object sender, EventArgs e)
        {
            if (syncFolderPath_textBox.Text != "")
            {
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
            }
        }

        private void treeViewAdv_Expanded(object sender, TreeViewAdvEventArgs e)
        {
            if (!e.Node.CanExpand)
                return;
            e.Node.Tree.AutoSizeColumn(e.Node.Tree.Columns[0]);
            e.Node.Tree.AutoSizeColumn(e.Node.Tree.Columns[3]);
            //e.Node.Tree.Columns[0].Width += 10;
            //e.Node.Tree.Columns[3].Width += 10;
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

        private void PublicFolders_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (publicFolders_comboBox.Text != "secret_folder")            
                yadiskPublicFolderKey_textBox.Text = publicFolders_comboBox.SelectedValue.ToString();            
            else
                yadiskPublicFolderKey_textBox.Text = "this url is too mysterious for you";

            hotDictKey = publicFolders_comboBox.Text;
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

        CloudServiceType GetCloudServiceType(string url)
        {
            if (url.Contains("yadi.sk"))
                return CloudServiceType.Yadisk;
            if (url.Contains("cloud.allsync.com"))
                return CloudServiceType.Allsync;
            if (url.Contains("mega.nz"))
                return CloudServiceType.Mega;
            if (url.Contains("dl.lynxcore.org") || url.Contains("dnd.jambrose.info") || url.Contains("enthusiasticallyconfused.com"))
                return CloudServiceType.h5ai;
            if (url.Contains("thetrove.net"))
                return CloudServiceType.TheTrove;

            return CloudServiceType.Other;
        }

        #region #BUTTONS       

        private void LoadPublicFolderKey_button_Click(object sender, EventArgs e)
        {
            string cloudFolderUrl = yadiskPublicFolderKey_textBox.Text;
            if (flatList_checkBox.Checked)
                flatList_checkBox.Checked = false;

            if (yadiskPublicFolderKey_textBox.Text != "")
            {
                syncFolders_button.Enabled = false;

                if (publicFolders_comboBox.Text == "secret_folder")                
                    LoadMega(publicFolders["secret_folder"]);

                if (yadiskPublicFolderKey_textBox.Text.Contains("snipli.com") || yadiskPublicFolderKey_textBox.Text.Contains("snip.li"))
                    cloudFolderUrl = GetFinalRedirect(yadiskPublicFolderKey_textBox.Text);

                if (cloudFolderUrl.Contains("snip.li") || cloudFolderUrl.Contains("snipli.com"))
                {
                    MessageBox.Show("Timeout or snipli link is dead");
                    return;
                }

                cloudServiceType = GetCloudServiceType(cloudFolderUrl);                

                switch (cloudServiceType)
                {
                    case CloudServiceType.Yadisk:
                        LoadYadisk(cloudFolderUrl);
                        break;
                    case CloudServiceType.Allsync:
                        LoadAllsync(cloudFolderUrl);
                        break;
                    case CloudServiceType.Mega:
                        LoadMega(cloudFolderUrl);
                        break;                    
                    case CloudServiceType.h5ai:
                        Load_h5ai(cloudFolderUrl);
                        break;
                    case CloudServiceType.TheTrove:
                        Load_TheTrove(cloudFolderUrl);
                        break;
                    case CloudServiceType.Other:
                        MessageBox.Show("Unsupported link!");
                        break;                   
                }   
                yadiskPublicFolderKey_textBox.ReadOnly = true;
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
            publicFolders.Add(publicFolders_comboBox.Text, yadiskPublicFolderKey_textBox.Text);
            //publicFolders_comboBox.EndUpdate();            
            publicFolders_comboBox.DataSource = new BindingSource(publicFolders, null);
            publicFolders_comboBox.Update();
            //selectedItem = new KeyValuePair<string, string>(publicFolders_comboBox.Text, selectedItem.Value);
            //publicFolders.Select(x => x.V)
            //publicFolders_comboBox.SelectedItem = publicFolders_comboBox.Text;
            UpdatePublicFoldersSetting();
            savePublicFolderKey_button.Enabled = false;

        }

        private void AddNewPublicFolder_button_Click(object sender, EventArgs e)
        {
            publicFolders.Add("New item" + (publicFolders.Count - 4), yadiskPublicFolderKey_textBox.Text);
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
            yadiskPublicFolderKey_textBox.ReadOnly = false;
            loadPublicFolderKey_button.Text = "Load";
            savePublicFolderKey_button.Enabled = true;
        }

       

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void syncFolders_button_Click(object sender, EventArgs e)
        {
            activeSyncForm?.CloseForm();
            checkedFolders = new List<CloudFolder>();
            mixedFolders = new List<CloudFolder>();
            GetCheckedFolders((ColumnNode)yadiskPublicFolder_model.Nodes[0]);
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

}

