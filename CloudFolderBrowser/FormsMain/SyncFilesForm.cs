﻿using System.Data;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using CG.Web.MegaApiClient;
using CloudFolderBrowser.FormsSecondary;
using CloudFolderBrowser.JDownloader;
using Newtonsoft.Json;
using YandexDiskSharp.Models;

namespace CloudFolderBrowser
{
    public partial class SyncFilesForm : Form
    {
        List<CloudFile> checkedFiles;
        TreeModel newFiles_model, newFilesFlat_model;
        long checkedFilesSize = 0;
        CloudFolder rootFolder;
        CloudServiceType cloudServiceType;
        List<ProgressBar> progressBars;
        List<Label> progressLabels;
        bool HideForm = true;
        MegaApiClient megaApiClient;
        NetworkCredential NetworkCredential;
        Download Download;

        MainForm MainForm;
        MainFormModel Model;

        public event EventHandler DownloadCompleted;
        protected virtual void OnDownloadCompleted(EventArgs e)
        {
            EventHandler handler = DownloadCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public int OverwriteMode = 0;
        public int MaximumDownloads = 4;
        public int RetryDelay = 300;
        public int RetryMax = 4;
        public double CheckFileSizeError = 0.999;
        public bool FolderNewFiles = false;
        public bool CheckDownloadedFileSize = false;

        //Textbox filter
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public void UpdateSettings()
        {
            OverwriteMode = Properties.Settings.Default.overwriteMode;
            MaximumDownloads = Properties.Settings.Default.maximumDownloads;
            RetryDelay = Properties.Settings.Default.retryDelay;
            RetryMax = Properties.Settings.Default.retryMax;
            CheckFileSizeError = Properties.Settings.Default.checkFileSizeError;
            FolderNewFiles = Properties.Settings.Default.folderNewFiles;
            CheckDownloadedFileSize = Properties.Settings.Default.checkDownloadedFileSize;
        }

        public SyncFilesForm(MainForm parentForm, CloudFolder newFilesFolder, MainFormModel model)
        {
            InitializeComponent();

            if (Properties.Settings.Default.maximumDownloads == 0)
            {
                //default settings
                Properties.Settings.Default.overwriteMode = OverwriteMode;
                Properties.Settings.Default.maximumDownloads = MaximumDownloads;
                Properties.Settings.Default.retryDelay = RetryDelay;
                Properties.Settings.Default.retryMax = RetryMax;
                Properties.Settings.Default.checkFileSizeError = CheckFileSizeError;
                Properties.Settings.Default.folderNewFiles = FolderNewFiles;
                Properties.Settings.Default.checkDownloadedFileSize = CheckDownloadedFileSize;
            }
            UpdateSettings();

            MainForm = parentForm;
            Model = model;
            NetworkCredential = Model.WebdavCredential;
            SendMessage(filter_textBox.Handle, 0x1501, 1, "Filter by name");
            filter_textBox.TextChangedComplete += filter_TextChangedComplete;

            cloudServiceType = Model.CloudServiceType;
            if (cloudServiceType == CloudServiceType.Mega)
            {
                if (!Model.LoadedFromFile)
                {
                    if (MainForm.usingFogLink)
                        downloadFiles_button.Enabled = false;

                    downloadFiles_button.Text = "MEGA download";

                    if (Properties.Settings.Default.loginedMega)
                        importMega_button.Enabled = true;
                }
                else
                {
                    downloadFiles_button.Enabled = false;
                    MessageBox.Show("Files were loaded from file and will not be available for download. Load from link instead.");
                }

                if (MainForm.usingFogLink && !Properties.Settings.Default.loginedMega)
                    MessageBox.Show("Not signed in MEGA: unable to import files.");

                getJdLinks_button.Enabled = true;
            }
            if (cloudServiceType == CloudServiceType.Yadisk)
            {
                if (Properties.Settings.Default.loginedYandex)
                    importMega_button.Enabled = true;
                getJdLinks_button.Enabled = true;
            }

            progressBars = new List<ProgressBar> { progressBar1, progressBar2, progressBar3, progressBar4 };
            progressLabels = new List<Label> { label1, label2, label3, label4, DownloadProgress_label };

            rootFolder = newFilesFolder;
            nodeCheckBox2.CheckStateChanged += new EventHandler<TreePathEventArgs>(NodeCheckStateChanged);
            newFilesTreeViewAdv.ShowNodeToolTips = true;
            newFilesTreeViewAdv.NodeControls[2].ToolTipProvider = new ToolTipProvider();
            newFilesTreeViewAdv.NodeFilter = filter;

            newFiles_model = new TreeModel();
            newFilesFlat_model = new TreeModel();

            ColumnNode rootFlatNode = new ColumnNode(newFilesFolder.Name, newFilesFolder.Created, newFilesFolder.Modified, newFilesFolder.Size);
            newFilesFlat_model.Nodes.Add(rootFlatNode);
            ColumnNode rootNode = new ColumnNode(newFilesFolder.Name, newFilesFolder.Created, newFilesFolder.Modified, newFilesFolder.Size);
            rootNode.Tag = rootFlatNode.Tag = Model.CloudPublicFolder;

            newFilesTreeViewAdv.Model = new SortedTreeModel(newFiles_model);
            newFilesTreeViewAdv.BeginUpdate();
            newFiles_model.Nodes.Add(rootNode);
            List<ColumnNode> folderNodes = new List<ColumnNode>();
            folderNodes.Add(rootNode);
            foreach (CloudFile file in newFilesFolder.Files)
            {
                ColumnNode ffileNode = new ColumnNode(file.Name, file.Created, file.Modified, file.Size);
                ffileNode.Tag = file;
                rootFlatNode.Nodes.Add(ffileNode);

                string[] folders = Utility.ParsePath(file.Path);
                if (folders == null) //file is in root folder
                {
                    ColumnNode subNode = new ColumnNode(file.Name, file.Created, file.Modified, file.Size);
                    subNode.Tag = file;
                    rootNode.Nodes.Add(subNode);
                    rootNode.Size += subNode.Size;
                }
                else
                {
                    ColumnNode currentNode = rootNode;
                    //creating folder nodes and file nodes from file path
                    string currentFolderPath = @"/";
                    for (int i = 0; i < folders.Length; i++)
                    {
                        if (folders[i] == "")
                        {
                        }
                        currentFolderPath += folders[i] + @"/";
                        ColumnNode subNode = new ColumnNode(folders[i], file.Created, file.Modified, 0);
                        var folderNode = folderNodes.Find(x => x.Path == currentFolderPath);
                        if (folderNode != null)
                            currentNode = folderNode;
                        else
                        {
                            subNode.Path = currentFolderPath;
                            var cloudFolder = Model.AllFolders.Where(x => x.Path == currentFolderPath).FirstOrDefault();
                            subNode.Tag = cloudFolder;
                            currentNode.Nodes.Add(subNode);
                            folderNodes.Add(subNode);
                            currentNode = subNode;
                        }
                        if (i == folders.Length - 1) //it's file
                        {
                            ColumnNode fileNode = new ColumnNode(file.Name, file.Created, file.Modified, file.Size);
                            fileNode.Tag = file;
                            currentNode.Nodes.Add(fileNode);
                            ffileNode.LinkedNode = fileNode;
                            fileNode.LinkedNode = ffileNode;
                        }
                        currentNode.Size += file.Size;
                    }
                }
            }
            newFilesTreeViewAdv.EndUpdate();
            newFilesTreeViewAdv.Root.Children[0].Expand();
            newFiles_model.Nodes[0].IsChecked = true;
            CheckAllSubnodes(newFiles_model.Nodes[0] as ColumnNode, false);
            newFilesTreeViewAdv.Columns[0].MinColumnWidth = 100;

            checkAllToolStripMenuItem.Click += CheckAllToolStripMenuItem_Click;
            checkNoneToolStripMenuItem.Click += CheckNoneToolStripMenuItem_Click;
            expandAllToolStripMenuItem.Click += ExpandAllToolStripMenuItem_Click;
            collapseAllToolStripMenuItem.Click += CollapseAllToolStripMenuItem_Click;
            Show();
        }

        public void CloseForm()
        {
            HideForm = false;
            Close();
        }

        List<CloudFolder> checkedFolders = new List<CloudFolder>();

        void GetCheckedFiles(Node node)
        {
            checkedFolders.Add(node.Tag as CloudFolder);

            if (node.CheckState == CheckState.Checked)
            {
                AddAllFiles(node);
                return;
            }

            foreach (Node subnode in node.Nodes)
            {
                if (subnode.Tag.GetType().ToString() == "CloudFolderBrowser.CloudFolder")
                {
                    var folder = subnode.Tag as CloudFolder;
                    if (subnode.CheckState == CheckState.Indeterminate)
                    {
                        GetCheckedFiles(subnode);
                    }
                    if (subnode.CheckState == CheckState.Checked)
                    {
                        checkedFolders.Add(folder);
                        AddAllFiles(subnode);
                    }
                    continue;
                }
                if (subnode.CheckState == CheckState.Checked)
                {
                    checkedFiles.Add((CloudFile)(subnode.Tag));
                    checkedFilesSize += ((CloudFile)(subnode.Tag)).Size;
                }
            }
        }

        void AddAllFiles(Node node)
        {
            foreach (Node subnode in node.Nodes)
            {
                if (subnode.Tag == null || subnode.Tag.GetType().ToString() == "CloudFolderBrowser.CloudFolder")
                {
                    checkedFolders.Add(subnode.Tag as CloudFolder);
                    AddAllFiles(subnode);
                }
                else
                {
                    checkedFiles.Add((CloudFile)(subnode.Tag));
                    checkedFilesSize += ((CloudFile)(subnode.Tag)).Size;
                }
            }
        }

        void CreateJdLinkcontainer()
        {
            var appPath = Directory.GetCurrentDirectory();

            DirectoryInfo di = new DirectoryInfo($"{appPath}\\Links");

            if (checkedFiles.Count == 0)
            {
                MessageBox.Show("No files checked!");
                return;
            }
            if (!di.Exists)
                di.Create();
            else
            {
                foreach (FileInfo file in di.EnumerateFiles())
                    file.Delete();
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    dir.Delete(true);
            }

            List<JDPackage> packages = new List<JDPackage>();

            if (cloudServiceType == CloudServiceType.Mega && megaApiClient == null)
            {
                megaApiClient = new MegaApiClient();
                megaApiClient.LoginAnonymous();
            }

            foreach (CloudFile file in checkedFiles)
            {
                //string folderPath = file.Path.Replace(file.Name, "");
                var folderPath = Path.GetDirectoryName(file.Path);
                JDPackage pak;
                if (!packages.ConvertAll(x => x.name).Contains(folderPath))
                {
                    pak = new JDPackage(folderPath, folderPath);
                    pak.numberId = packages.Count.ToString("D3");
                    pak.downloadFolder = folderPath;
                    packages.Add(pak);
                    File.WriteAllText($"{di.FullName}\\{pak.numberId}", JsonConvert.SerializeObject(pak));
                }
                else
                {
                    pak = packages.Find(x => x.name == folderPath);
                }
                JDLink link;
                switch (cloudServiceType)
                {
                    case CloudServiceType.Mega:
                        //string downloadLink = ""; //megaApiClient.GetDownloadLink(file.MegaNode).ToString();
                        link = new JDLink(file.Name, file.PublicUrl.OriginalString);
                        break;
                    case CloudServiceType.Yadisk:
                        YandexDiskSharp.RestClient restClient = new YandexDiskSharp.RestClient();
                        string downloadLink = restClient.GetPublicResourceDownloadLink(rootFolder.PublicKey, file.Path).Href.ToString();
                        link = new JDLink(file.Name, downloadLink);
                        break;
                    default:
                        link = new JDLink(file.Name, System.Web.HttpUtility.UrlDecode(file.PublicUrl.AbsoluteUri));
                        //link = new JDLink(file.Name, file.PublicUrl.AbsoluteUri.Replace("#", "%23").Replace(",", "%2C").Replace("?", "%3F"));                        
                        break;
                }
                link.downloadLink.size = (int)file.Size;
                File.WriteAllText($"{di.FullName}\\{pak.numberId}_{pak.linksCount.ToString("D3")}", JsonConvert.SerializeObject(link));
                pak.linksCount++;
            }

            DialogResult dialogResult = MessageBox.Show("Got links for " + (checkedFiles.Count) + " files! Continue?", "Result", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
                return;

            Random rnd = new Random();
            int number = rnd.Next(1, 9999);

            string rootFolderName = rootFolder.Name;
            foreach (char c in Path.GetInvalidFileNameChars())
                rootFolderName = rootFolderName.Replace(c.ToString(), "");

            string dirPath = $"{appPath}\\linkcontainers\\{rootFolderName}";

            Directory.CreateDirectory(dirPath);
            System.IO.Compression.ZipFile.CreateFromDirectory($"{appPath}\\Links", dirPath + @"\linkcollector" + number + ".zip");

            DownloadsFinishedForm downloadsFinishedForm = new DownloadsFinishedForm(dirPath, @"linkcollector" + number + ".zip created!");
            downloadsFinishedForm.Show();
        }

        void AddCheckedFilesToYadisk()
        {
            if (checkedFilesSize > MainForm.freeSpace)
            {
                MessageBox.Show("Not enougt free space on Yandex disk");
                return;
            }
            if (checkedFiles.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show(checkedFiles.Count + $" new files found. Size: {Math.Round(checkedFilesSize / 1000000.0, 2)} MB. Download?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (CloudFile file in checkedFiles)
                    {
                        string savePath = Model.CloudPublicFolder.Name + file.Path;
                        string[] folders = Utility.ParsePath(savePath);
                        CloudFolder currentFolder = (CloudFolder)MainForm.yadiskFolder.Subfolders[2];
                        savePath = currentFolder.Path;
                        for (int i = 0; i < folders.Length; i++)
                        {
                            if (!currentFolder.Subfolders.ConvertAll(x => x.Name).Contains(folders[i]))
                            {
                                Link linki = MainForm.rc.CreateResource(currentFolder.Path.Replace("disk:", "") + "/" + folders[i]);
                                CloudFolder createdFolder = new CloudFolder(folders[i], DateTime.Now, DateTime.Now, 0);
                                createdFolder.Path = currentFolder.Path + "/" + folders[i];
                                currentFolder.Subfolders.Add(createdFolder);
                            }
                            currentFolder = (CloudFolder)currentFolder.Subfolders.Find(x => x.Name == folders[i]);
                            savePath += "/" + folders[i];
                        }
                        //Uri link = (rc.GetPublicResourceDownloadLink(cloudPublicFolder.PublicKey, file.Path)).Href;  

                        //TODO: add whole folders if all files inside are checked
                        Link link = MainForm.rc.SaveToDiskPublicResource(Model.CloudPublicFolder.PublicKey, file.Name, file.Path, savePath);
                    }
                    MessageBox.Show("Finished");
                }
            }
            else
                MessageBox.Show("No files checked!");
        }

        async Task ImportCheckedToMega()
        {
            if (checkedFilesSize > MainForm.freeSpace)
            {
                MessageBox.Show("MEGA: Not enough free space");
                return;
            }
            if (checkedFiles.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show(checkedFiles.Count + $" new files found. Size: {Math.Round(checkedFilesSize / 1000000.0, 2)} MB. Download?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var nodes = new List<INode>() { };
                    foreach (CloudFolder folder in checkedFolders)
                    {
                        if (folder.MegaNode != null)
                            nodes.Add(folder.MegaNode);
                    }
                    foreach (CloudFile file in checkedFiles)
                    {
                        string savePath = Model.CloudPublicFolder.Name + file.Path;
                        var fileUri = new Uri($"https://mega.nz/folder/" +
                            $"{Model.CloudPublicFolder.PublicKey}#{Model.CloudPublicFolder.PublicDecryptionKey}/file/" + file.MegaNode.Id);

                        nodes.Add(file.MegaNode);
                    }
                    MainForm.megaClient.ImportNodes(nodes.ToArray(), MainForm.MegaRootNode);
                    MessageBox.Show("Finished");
                }
            }
            else
                MessageBox.Show("No files checked!");
        }

        async Task ImportCheckedEncryptedToMega()
        {
            if (checkedFilesSize > MainForm.freeSpace)
            {
                MessageBox.Show("MEGA: Not enough free space");
                return;
            }
            if (checkedFiles.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show(checkedFiles.Count + $" new files found. Size: {Math.Round(checkedFilesSize / 1000000.0, 2)} MB. Download?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var nodes = new List<string>() { };
                    foreach (CloudFolder folder in checkedFolders)
                    {
                        if (!string.IsNullOrEmpty(folder.EncryptedUrl))
                            nodes.Add(folder.EncryptedUrl);
                    }
                    foreach (CloudFile file in checkedFiles)
                    {
                        nodes.Add(file.EncryptedUrl);
                    }
                    HttpClient client = new HttpClient();
                    //MegaApiClient tempClient = new MegaApiClient();
                    //var token = tempClient.Login(Properties.Settings.Default.megaLogin, Properties.Settings.Default.megaPassword);
                    //tempClient.Logout();
                    var postData = new ImportLinksData() { login = Properties.Settings.Default.megaLogin, password = Properties.Settings.Default.megaPassword, links = nodes.ToArray() };
                    var postJson = JsonConvert.SerializeObject(postData);
                    var content = new StringContent(postJson, Encoding.UTF8, "application/json");
                    client.BaseAddress = FogLink.ServerAddress;
                    HttpResponseMessage response = await client.PostAsync(
                        $"MegaPrivater/import", content);
                    var megaCode = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        int code;
                        var codeOk = int.TryParse(megaCode, out code);
                        var error = codeOk ? ((ApiResultCode)code).ToString() : response.StatusCode.ToString();
                        MessageBox.Show($"Failed to import files: {(error)}");
                    }
                    else
                        MessageBox.Show("Finished");
                }
            }
            else
                MessageBox.Show("No files checked!");
        }

        public class ImportLinksData
        {
            public string logonToken;
            public string[] links;
            public string login;
            public string password;
        }

        private void DownloadFiles()
        {
            checkedFiles = new List<CloudFile>();
            checkedFilesSize = 0;
            GetCheckedFiles((((SortedTreeModel)newFilesTreeViewAdv.Model).InnerModel as TreeModel).Nodes[0]);

            DialogResult dialogResult =
                MessageBox.Show($"Got links for {checkedFiles.Count} files [{(int)(checkedFilesSize / 1000000)} MB]  Continue?", "Result",
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
                return;

            if (FolderNewFiles)
                Directory.CreateDirectory(MainForm.syncFolderPath + @"\0_New Files\" + DateTime.Now.ToShortDateString());

            ProgressBar[] usedProgressBars = new ProgressBar[MaximumDownloads];
            Label[] usedLabels = new Label[MaximumDownloads + 1];
            for (int i = 0; i < MaximumDownloads; i++)
            {
                usedProgressBars[i] = progressBars[i];
                usedLabels[i] = progressLabels[i];
            }
            usedLabels[MaximumDownloads] = progressLabels[progressLabels.Count - 1];

            Download = new CommonDownload(checkedFiles, usedProgressBars, usedLabels, toolTip1, cloudServiceType,
                MainForm.syncFolderPath, OverwriteMode, NetworkCredential, FolderNewFiles);
            Download.MaxDownloadRetries = RetryMax;
            Download.RetryDelay = RetryDelay;

            Download.CheckFileSizeError = CheckFileSizeError;
            Download.CheckDownloadedFileSize = CheckDownloadedFileSize;

            Download.DownloadCompleted += Download_DownloadCompleted;
            Download.Start();

            stopDownload_button.Enabled = true;
            stopDownload_button.Visible = true;
        }

        private async Task DownloadMega()
        {
            checkedFiles = new List<CloudFile>();
            checkedFilesSize = 0;
            GetCheckedFiles((((SortedTreeModel)newFilesTreeViewAdv.Model).InnerModel as TreeModel).Nodes[0]);

            DialogResult dialogResult = MessageBox.Show($"Got links for {checkedFiles.Count} files [{(int)(checkedFilesSize / 1000000)} MB]  Continue?", "Result", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
                return;

            if (FolderNewFiles)
                Directory.CreateDirectory(MainForm.syncFolderPath + @"\0_New Files\" + DateTime.Now.ToShortDateString());

            ProgressBar[] usedProgressBars = new ProgressBar[MaximumDownloads];
            Label[] usedLabels = new Label[MaximumDownloads + 1];
            for (int i = 0; i < MaximumDownloads; i++)
            {
                usedProgressBars[i] = progressBars[i];
                usedLabels[i] = progressLabels[i];
            }
            usedLabels[MaximumDownloads] = progressLabels[progressLabels.Count - 1];

            if (megaApiClient == null)
            {
                megaApiClient = new MegaApiClient();
                if (Properties.Settings.Default.loginedMega && Properties.Settings.Default.loginTokenMega != "")
                {
                    var megaLoginToken = JsonConvert.DeserializeObject<MegaApiClient.LogonSessionToken>(
                        Properties.Settings.Default.loginTokenMega, new JsonSerializerSettings()
                        {
                            TypeNameHandling = TypeNameHandling.Auto
                        });
                    try
                    {
                        await megaApiClient.LoginAsync(megaLoginToken);
                    }
                    catch
                    {
                        await megaApiClient.LoginAnonymousAsync();
                    }

                }
                else
                {                    
                    await megaApiClient.LoginAnonymousAsync();
                }                
            }

            Download = new MegaDownload(megaApiClient, checkedFiles, usedProgressBars, usedLabels, toolTip1,
                MainForm.syncFolderPath, OverwriteMode, FolderNewFiles, Model.CloudPublicFolder.PublicKey);
            Download.MaxDownloadRetries = RetryMax;
            Download.RetryDelay = RetryDelay;

            Download.CheckFileSizeError = CheckFileSizeError;
            Download.CheckDownloadedFileSize = CheckDownloadedFileSize;

            Download.DownloadCompleted += Download_DownloadCompleted;
            Download.Start();

            stopDownload_button.Enabled = true;
            stopDownload_button.Visible = true;
        }

        #region #TREEVIEW

        private void treeViewAdv_Expanded(object sender, TreeViewAdvEventArgs e)
        {
            if (!e.Node.CanExpand)
                return;
            e.Node.Tree.AutoSizeColumn(e.Node.Tree.Columns[0]);
            e.Node.Tree.AutoSizeColumn(e.Node.Tree.Columns[3]);
            //e.Node.Tree.Columns[0].Width += (int)Math.Round(e.Node.Tree.Columns[0].Width * 0.3, 0);
        }

        private void treeViewAdv_Collapsed(object sender, TreeViewAdvEventArgs e)
        {
            if (!e.Node.CanExpand)
                return;
            e.Node.Tree.AutoSizeColumn(e.Node.Tree.Columns[0], false);
            e.Node.Tree.AutoSizeColumn(e.Node.Tree.Columns[3], false);
            //e.Node.Tree.Columns[0].Width += (int)Math.Round(e.Node.Tree.Columns[0].Width * 0.2, 0);
        }

        void CheckIndex(object sender, NodeControlValueEventArgs e)
        {
            var currentNode = (ColumnNode)(e.Node.Tag);
            var parentNode = (currentNode.Parent);
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
            //if (currentNode.CheckBoxEnabled)
            //    currentNode.CheckState = CheckState.Checked;
            //else
            //    currentNode.CheckState = CheckState.Unchecked;
            e.Value = currentNode.CheckBoxEnabled;
        }

        void CheckAllSubnodes(ColumnNode node, bool uncheck)
        {
            CheckState cst = uncheck ? CheckState.Unchecked : CheckState.Checked;

            foreach (ColumnNode subnode in node.Nodes)
            {
                CheckState origState = subnode.CheckState;
                if (subnode.Tag == null || subnode.Tag.GetType().ToString() == "CloudFolderBrowser.CloudFolder")
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
                            //checkedFilesSize += ((CloudFolder)subnode.Tag).SizeTopDirectoryOnly;
                            //long folderFilesSize = ((CloudFolder)subnode.Tag).Files.Sum(x => x.Size);
                            //checkedFilesSize += Math.Round(folderFilesSize * b2Mb, 2);                            
                        }
                    }
                    //    checkedFilesSize += ((CloudFolder)subnode.Tag).Size;
                    subnode.CheckState = cst;
                    CheckAllSubnodes(subnode, uncheck);
                }
                else
                    subnode.CheckState = cst;

            }
        }

        void NodeCheckStateChanged(object sender, TreePathEventArgs e)
        {
            ColumnNode checkedNode = (ColumnNode)e.Path.LastNode;

            if (checkedNode.CheckState == CheckState.Checked)
            {
                //if (checkedNode.Tag?.GetType().ToString() != "CloudFolderBrowser.CloudFile") checkedFolders.Add(MainForm.);
                //if (checkedNode.Parent.CheckState != CheckState.Checked && checkedNode.Parent.Index != -1)
                //    UpdateParentCheckState((ColumnNode)checkedNode.Parent);
                CheckAllSubnodes(checkedNode, false);
                //checkedFilesSize += ((CloudFolder)checkedNode.Tag).Size - ((CloudFolder)checkedNode.Tag).SizeTopDirectoryOnly;
                //label1.Text = $"{Math.Round(checkedFilesSize * b2Mb, 2)} MB checked";
            }
            else if (checkedNode.CheckState == CheckState.Unchecked)
            {
                //checkedFilesSize -= ((CloudFolder)checkedNode.Tag).Size;
                //if (checkedFilesSize < 0.0001)
                //    checkedFilesSize = 0;
                //label1.Text = $"{Math.Round(checkedFilesSize * b2Mb, 2)} MB checked";

                //if (checkedNode.Parent.Index != -1)
                //    UpdateParentCheckState((ColumnNode)checkedNode.Parent);
                CheckAllSubnodes(checkedNode, true);

            }
            else if (checkedNode.CheckState == CheckState.Indeterminate)
            {
                if (checkedNode.Tag?.GetType().ToString() == "CloudFolderBrowser.CloudFile")
                {
                    checkedNode.CheckState = CheckState.Unchecked;
                }
                //checkedFilesSize += ((CloudFolder)checkedNode.Tag).SizeTopDirectoryOnly;//((CloudFolder)parentNode.Tag).Files.Sum(x => x.Size) * b2Mb;
                //checkedFilesSize = (long)Math.Round((double)checkedFilesSize, 2);
                //label1.Text = $"{Math.Round(checkedFilesSize * b2Mb, 2)} MB checked";
                //if (parentNode.Parent.Index != -1)
                //    UpdateParentCheckState((ColumnNode)parentNode.Parent);
                //CheckAllSubnodes(parentNode, true);

            }
            if (checkedNode.Parent.Index != -1)
                UpdateParentCheckState((ColumnNode)checkedNode.Parent);

        }

        static void UpdateParentCheckState(ColumnNode parentNode)
        {
            CheckState origState = parentNode.CheckState;
            int UnCheckedNodes = 0, CheckedNodes = 0, MixedNodes = 0;

            foreach (ColumnNode tnChild in parentNode.Nodes)
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

            if (MixedNodes > 0)
            {
                // at least one child is mixed, so parent must be mixed
                parentNode.CheckState = CheckState.Indeterminate;
            }
            else if (CheckedNodes > 0 && UnCheckedNodes == 0)
            {
                // all children are checked
                //if (parentNode.CheckState == CheckState.Indeterminate)
                parentNode.CheckState = CheckState.Checked;
                //parentNode.CheckState = CheckState.Indeterminate;
                //else
                //   parentNode.CheckState = CheckState.Indeterminate;
            }
            else if (CheckedNodes > 0)
            {
                // some children are checked, the rest are unchecked                   
                parentNode.CheckState = CheckState.Indeterminate;
            }
            if (CheckedNodes == 0 && MixedNodes == 0)
            {
                //if (parentNode.CheckState != CheckState.Unchecked)
                //    parentNode.CheckState = CheckState.Indeterminate;
                // all children are unchecked
                //if (parentNode.CheckState == CheckState.Checked)
                //    parentNode.CheckState = CheckState.Indeterminate;
                //else
                parentNode.CheckState = CheckState.Unchecked;
            }

            if (parentNode.CheckState != origState && parentNode.Parent.Index != -1)
                UpdateParentCheckState((ColumnNode)parentNode.Parent);

        }

        void TransferNodeCheckState(ColumnNode node)
        {
            foreach (ColumnNode subnode in node.Nodes)
            {
                if (flatList2_checkBox.Checked) //hierarchy -> flat
                {
                    if (subnode.Tag == null || subnode.Tag.GetType().ToString() == "CloudFolderBrowser.CloudFolder")
                    {
                        TransferNodeCheckState(subnode);
                        continue;
                    }
                    else
                        subnode.LinkedNode.IsChecked = subnode.IsChecked;
                }
                else
                {//flat -> hierarchy                
                    subnode.LinkedNode.IsChecked = subnode.IsChecked;
                    if (subnode.LinkedNode.Parent.Index != -1)
                        UpdateParentCheckState((ColumnNode)subnode.LinkedNode.Parent);
                }

            }
        }

        Node FindNodeByPath(Node root, string path)
        {
            string[] parsedPath = Utility.ParsePath(path, true);
            Node currentNode = root;
            int i = 0;
            while (i < parsedPath.Length)
            {
                if (currentNode.Nodes == null)
                    return null;
                foreach (Node n in currentNode.Nodes)
                {
                    if (n.Text == (parsedPath[i]))
                    {
                        currentNode = n;
                        i++;
                        break;
                    }
                }
            }
            return currentNode;

        }

        private void treeViewAdv_ColumnClicked(object sender, TreeColumnEventArgs e)
        {
            TreeColumn clicked = e.Column;
            if (clicked.SortOrder == SortOrder.Ascending)
                clicked.SortOrder = SortOrder.Descending;
            else
                clicked.SortOrder = SortOrder.Ascending;

            (((TreeViewAdv)sender).Model as SortedTreeModel).Comparer = new FolderItemSorter(clicked.Header, clicked.SortOrder);
        }

        private void CollapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flatList2_checkBox.Checked)
                return;
            newFilesTreeViewAdv.Model = new SortedTreeModel(newFiles_model);
            newFilesTreeViewAdv.Root.Children[0].Expand();
            newFilesTreeViewAdv.AutoSizeColumn(newFilesTreeViewAdv.Columns[0]);
        }

        private void ExpandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newFilesTreeViewAdv.ExpandAll();
            newFilesTreeViewAdv.AutoSizeColumn(newFilesTreeViewAdv.Columns[0]);
        }

        private void CheckNoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mdl = (newFilesTreeViewAdv.Model as SortedTreeModel).InnerModel as TreeModel;
            mdl.Nodes[0].IsChecked = false;
            CheckAllSubnodes(mdl.Nodes[0] as ColumnNode, true);
            newFilesTreeViewAdv.Refresh();
        }

        private void CheckAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var mdl = (newFilesTreeViewAdv.Model as SortedTreeModel).InnerModel as TreeModel;
            mdl.Nodes[0].IsChecked = true;
            CheckAllSubnodes(mdl.Nodes[0] as ColumnNode, false);
            newFilesTreeViewAdv.Refresh();
        }

        private bool filter(object obj)
        {
            TreeNodeAdv viewNode = obj as TreeNodeAdv;
            Node n = viewNode != null ? viewNode.Tag as Node : obj as Node;
            ColumnNode nn = (ColumnNode)n;
            bool hideByName = n == null || n.Text.ToUpper().Contains(this.filter_textBox.Text.ToUpper()) || n.Nodes.Any(filter);
            return hideByName;
        }

        #endregion

        #region #BUTTONS

        private void importYadisk_button_Click(object sender, EventArgs e)
        {
            checkedFiles = new List<CloudFile>();
            checkedFilesSize = 0;
            GetCheckedFiles(newFiles_model.Nodes[0]);
            AddCheckedFilesToYadisk();
        }
        private void importMega_button_Click(object sender, EventArgs e)
        {
            checkedFiles = new List<CloudFile>();
            checkedFilesSize = 0;
            checkedFolders = new List<CloudFolder>();
            GetCheckedFiles(newFiles_model.Nodes[0]);
            if (MainForm.usingFogLink)
                ImportCheckedEncryptedToMega();
            else
                ImportCheckedToMega();
        }

        private void getJdLinks_button_Click(object sender, EventArgs e)
        {
            checkedFiles = new List<CloudFile>();
            checkedFilesSize = 0;
            GetCheckedFiles(((newFilesTreeViewAdv.Model as SortedTreeModel).InnerModel as TreeModel).Nodes[0]);
            CreateJdLinkcontainer();
        }

        private void downloadFiles_button_Click(object sender, EventArgs e)
        {
            if (cloudServiceType == CloudServiceType.Mega)
                DownloadMega();
            else
                DownloadFiles();
        }

        private void stopDownloads_Click(object sender, EventArgs e)
        {
            Download?.Stop();
            OnDownloadCompleted(EventArgs.Empty);
        }

        #endregion

        private void syncFilesForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (HideForm)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void Download_DownloadCompleted(object sender, EventArgs e)
        {
            for (int i = 0; i < MaximumDownloads; i++)
            {
                progressBars[i].Value = 0;
                progressLabels[i].Text = "";
            }
            progressLabels[4].Text = "";
            string message2 = "", message1 = "All downloads are finished!";

            if (Download != null)
            {
                if (Download.FailedDownloads.Count > 0)
                    message2 += $" Failed: {Download.FailedDownloads.Count}";

                DownloadsFinishedForm downloadsFinishedForm = new DownloadsFinishedForm(Download.DownloadFolderPath, message1, message2);
                downloadsFinishedForm.Show();
            }
        }

        private void filter_TextChangedComplete(object sender, EventArgs e)
        {
            newFilesTreeViewAdv.UpdateNodeFilter();
        }

        private void flatList2_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (flatList2_checkBox.Checked)
            {
                newFilesTreeViewAdv.Model = new SortedTreeModel(newFilesFlat_model);
                TransferNodeCheckState(newFiles_model.Nodes[0] as ColumnNode);
                newFilesTreeViewAdv.ShowNodeToolTips = true;
                newFilesTreeViewAdv.ExpandAll();
                newFilesTreeViewAdv.AutoSizeColumn(newFilesTreeViewAdv.Columns[0]);
                newFilesTreeViewAdv.AutoSizeColumn(newFilesTreeViewAdv.Columns[3]);
            }
            else
            {
                newFilesTreeViewAdv.Model = new SortedTreeModel(newFiles_model);
                TransferNodeCheckState(newFilesFlat_model.Nodes[0] as ColumnNode);
                newFilesTreeViewAdv.ShowNodeToolTips = false;
                newFilesTreeViewAdv.Root.Children[0].Expand();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SyncSettingsForm(this).ShowDialog();
        }       
    }
}
