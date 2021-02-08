﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using Newtonsoft.Json;
using YandexDiskSharp.Models;
using CG.Web.MegaApiClient;
using System.Runtime.InteropServices;
using System.Web.UI;
using WebDAVClient;
using System.Net;

namespace CloudFolderBrowser
{
    public partial class SyncFilesForm : Form
    {
        List<CloudFile> checkedFiles;
        TreeModel newFiles_model, newFilesFlat_model;
        long checkedFilesSize = 0;        
        CloudFolder rootFolder;
        CloudServiceType cloudServiceType;
        int maximumDownloads = 4;
        List<ProgressBar> progressBars;
        List<Label> progressLabels;
        bool HideForm = true;
        MegaApiClient megaApiClient;
        NetworkCredential NetworkCredential;
        MegaDownload megaDownload;
        CommonDownload commonDownload;
        Dictionary<int, string> overwriteModes = new Dictionary<int, string>()
        {
            {0, "None" },
            {1, "Overwrite all" },
            {2, "Overwrite older"},
            {3, "Ask" }
        };

        //Textbox filter
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public SyncFilesForm(MainForm parentForm, CloudFolder newFilesFolder, CloudServiceType cloudServiceName, NetworkCredential networkCredential)
        {
            InitializeComponent();

            NetworkCredential = networkCredential;
            SendMessage(filter_textBox.Handle, 0x1501, 1, "Filter by name");
            filter_textBox.TextChangedComplete += filter_TextChangedComplete;

            overwriteMode_comboBox.DataSource = new BindingSource(overwriteModes, null);
            overwriteMode_comboBox.DisplayMember = "Value";
            overwriteMode_comboBox.ValueMember = "Key";

            //nodeCheckBox2.IsVisibleValueNeeded += CheckIndex;

            cloudServiceType = cloudServiceName;
            if (cloudServiceType == CloudServiceType.Mega)
            {
                if (!parentForm.LoadedFromFile)
                    downloadMega_button.Enabled = true;
                else
                    MessageBox.Show("Files were loaded from file and will not be available for download. Load from link instead.");
                
                getJdLinks_button.Enabled = false;
                downloadFiles_button.Enabled = false;       
            }
            if (cloudServiceType == CloudServiceType.Yadisk)
            {
                if (Properties.Settings.Default.loginedYandex)
                    addFilesToYadisk_button.Enabled = true;
                getJdLinks_button.Enabled = true;                
            }            

            progressBars = new List<ProgressBar> { progressBar1, progressBar2, progressBar3, progressBar4 };
            progressLabels = new List<Label> { label1, label2, label3, label4, DownloadProgress_label };

            rootFolder = newFilesFolder;
            //nodeCheckBox2.CheckStateChanged += new EventHandler<TreePathEventArgs>(NodeCheckStateChanged);
            newFilesTreeViewAdv.ShowNodeToolTips = true;
            newFilesTreeViewAdv.NodeControls[2].ToolTipProvider = new ToolTipProvider();
            newFilesTreeViewAdv.NodeFilter = filter;
            //newFilesTreeViewAdv.Expanded += new EventHandler<TreeViewAdvEventArgs>(Form1.treeViewAdv_Expanded);
            //newFilesTreeViewAdv.Collapsed += new EventHandler<TreeViewAdvEventArgs>(Form1.treeViewAdv_Expanded);

            newFiles_model = new TreeModel();
            newFilesFlat_model = new TreeModel();

            ColumnNode rootFlatNode = new ColumnNode(newFilesFolder.Name, newFilesFolder.Created, newFilesFolder.Modified, newFilesFolder.Size);
            newFilesFlat_model.Nodes.Add(rootFlatNode);
            ColumnNode rootNode = new ColumnNode(newFilesFolder.Name, newFilesFolder.Created, newFilesFolder.Modified, newFilesFolder.Size);
            //rootNode.Tag = newFilesFolder;
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

                string[] folders = ParsePath(file.Path);
                if (folders == null) //file is in root folder
                {
                    ColumnNode subNode = new ColumnNode(file.Name, file.Created, file.Modified, file.Size);
                    subNode.Tag = file;
                    rootNode.Nodes.Add(subNode);                   
                }
                else
                {
                    ColumnNode currentNode = rootNode;
                    //creating folder nodes and file node from file path
                    string currentFolderPath = @"/";
                    for (int i = 0; i < folders.Length; i++)
                    {
                        if (folders[i] == "")
                        {
                        }
                        currentFolderPath += folders[i] + @"/";
                        ColumnNode subNode = new ColumnNode(folders[i], file.Created, file.Modified, 0);
                        var folderNode = folderNodes.Find(x => (x as ColumnNode).Path == currentFolderPath);
                        if (folderNode != null)                           
                            currentNode = folderNode;
                        else
                        {
                            subNode.Path = currentFolderPath;
                            currentNode.Nodes.Add(subNode);                            
                            folderNodes.Add(subNode);
                            currentNode = subNode;
                        }
                        if (i == folders.Length - 1)
                        {
                            ColumnNode fileNode = new ColumnNode(file.Name, file.Created, file.Modified, file.Size);
                            fileNode.Tag = file;
                            currentNode.Nodes.Add(fileNode);
                            currentNode.Size += fileNode.Size;
                        }
                    }
                }
            }
            foreach(ColumnNode node in folderNodes)
            {
                if(node.Parent.Index != -1)
                    (node.Parent as ColumnNode).Size += node.Size;
            }
            //Form1.BuildSubfolderNodes(rootNode);
            //BuildFullFolderStructure(rootNode);
            newFilesTreeViewAdv.EndUpdate();
            newFilesTreeViewAdv.Root.Children[0].Expand();
            newFiles_model.Nodes[0].CheckState = CheckState.Checked;
           
            this.Show();
            //CheckAllSubnodes(newFiles_model.Nodes[0], false);
        }       

        private bool filter(object obj)
        {
            TreeNodeAdv viewNode = obj as TreeNodeAdv;
            Node n = viewNode != null ? viewNode.Tag as Node : obj as Node;
            ColumnNode nn = (ColumnNode)n;
            bool hideByName = n == null || n.Text.ToUpper().Contains(this.filter_textBox.Text.ToUpper()) || n.Nodes.Any(filter);            
            return hideByName;
        }

        public void CloseForm()
        {
            HideForm = false;
            this.Close();
        }

        void GetCheckedFiles(Node node)
        {
            if (node.CheckState == CheckState.Checked)
                AddAllFiles(node);
            foreach (Node subnode in node.Nodes)
            {
                if (subnode.Tag == null || subnode.Tag.GetType().ToString() == "CloudFolderBrowser.CloudFolder")
                {
                    if (subnode.CheckState == CheckState.Indeterminate)
                        GetCheckedFiles(subnode);
                    if (subnode.CheckState == CheckState.Checked)
                        AddAllFiles(subnode);
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
            DirectoryInfo di = new DirectoryInfo("Links");

            if (checkedFiles.Count == 0)
            {
                MessageBox.Show("No files checked!");
                return;
            }
            if(!di.Exists)
                Directory.CreateDirectory("Links");
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
                    File.WriteAllText("Links" + @"\" + pak.numberId, JsonConvert.SerializeObject(pak));
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
                        link = new JDLink(file.Name, file.PublicUrl.AbsoluteUri.Replace("#", "%23").Replace(",", "%2C").Replace("?", "%3F"));
                        break;
                }
                link.downloadLink.size = (int)file.Size;
                File.WriteAllText("Links" + @"\" + pak.numberId + "_" + pak.linksCount.ToString("D3"), JsonConvert.SerializeObject(link));
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

            string dirPath = @"linkcontainers\" + rootFolderName;            
            Directory.CreateDirectory(dirPath);
            System.IO.Compression.ZipFile.CreateFromDirectory("Links", dirPath + @"\linkcollector" + number + ".zip");

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
                        string savePath = MainForm.cloudPublicFolder.Name + file.Path;
                        string[] folders = ParsePath(savePath);
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
                        Link link = MainForm.rc.SaveToDiskPublicResource(MainForm.cloudPublicFolder.PublicKey, file.Name, file.Path, savePath);
                    }
                    MessageBox.Show("Finished");
                }
            }
            else
                MessageBox.Show("No files checked!");
        }

        string[] ParsePath(string path, bool includeFilename = false)
        {
            string pattern = @"([^/]+)/";
            if (includeFilename) pattern = @"([^/]+)";           

            MatchCollection matches = Regex.Matches(path, pattern);
            if (matches.Count == 0)
                return null;
            string[] folderNames = new string[matches.Count];

            for (int i = 0; i < folderNames.Length; i++)
            {
                folderNames[i] = matches[i].Groups[1].Value;
            }
            return folderNames;
        }

        #region #TREEVIEW
               
        private void treeViewAdv_Expanded(object sender, TreeViewAdvEventArgs e)
        {
            if (!e.Node.CanExpand)
                return;
            e.Node.Tree.AutoSizeColumn(e.Node.Tree.Columns[0]);
            e.Node.Tree.AutoSizeColumn(e.Node.Tree.Columns[3]);
            e.Node.Tree.Columns[0].Width += (int)Math.Round(e.Node.Tree.Columns[0].Width * 0.3, 0);
        }

        private void treeViewAdv_Collapsed(object sender, TreeViewAdvEventArgs e)
        {
            if (!e.Node.CanExpand)
                return;
            e.Node.Tree.AutoSizeColumn(e.Node.Tree.Columns[0], false);
            e.Node.Tree.AutoSizeColumn(e.Node.Tree.Columns[3], false);
            e.Node.Tree.Columns[0].Width += (int)Math.Round(e.Node.Tree.Columns[0].Width * 0.3, 0);
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

        void NodeCheckStateChanged(object sender, TreePathEventArgs e)
        {
            ColumnNode checkedNode = (ColumnNode)e.Path.FirstNode;
            
            if (checkedNode.CheckState == CheckState.Checked)
            {                
                //if (checkedNode.Parent.CheckState != CheckState.Checked && checkedNode.Parent.Index !=-1)
                //    UpdateParentCheckState((ColumnNode)checkedNode.Parent);
                //CheckAllSubnodes(checkedNode, false);
                //checkedFilesSize += ((CloudFolder)checkedNode.Tag).Size - ((CloudFolder)checkedNode.Tag).SizeTopDirectoryOnly;
                //label1.Text = $"{Math.Round(checkedFilesSize / 1024000.0, 2)} MB checked";                
            }
            else if (checkedNode.CheckState == CheckState.Unchecked)
            {
                //checkedFilesSize -= ((CloudFolder)checkedNode.Tag).Size;
                //if (checkedFilesSize < 0.0001)
                //    checkedFilesSize = 0;
                //label1.Text = $"{Math.Round(checkedFilesSize / 1024000.0, 2)} MB checked";

                //if (checkedNode.Parent.Index != -1)
                //    UpdateParentCheckState((ColumnNode)checkedNode.Parent);                
                //CheckAllSubnodes(checkedNode, true);                                
                
            }
            else if (checkedNode.CheckState == CheckState.Indeterminate)
            {
                if (checkedNode.Tag?.GetType().ToString() == "CloudFolderBrowser.CloudFile")
                {
                    checkedNode.CheckState = CheckState.Unchecked;
                }
                //checkedFilesSize += ((CloudFolder)checkedNode.Tag).SizeTopDirectoryOnly;//((CloudFolder)parentNode.Tag).Files.Sum(x => x.Size) / 1024000.0;
                //checkedFilesSize = Math.Round(checkedFilesSize, 2);
                //label1.Text = $"{Math.Round(checkedFilesSize / 1024000.0, 2)} MB checked";
                //if(parentNode.Parent.Index != -1)
                //UpdateParentCheckState((ColumnNode)parentNode.Parent);                
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
                
        void TransferNodeCheckState(Node node)
        {
            foreach (Node subnode in node.Nodes)
            {
                if (flatList2_checkBox.Checked) //hierarchy -> flat
                {
                    if (subnode.Tag == null || subnode.Tag.GetType().ToString() == "CloudFolderBrowser.CloudFolder")
                    {
                        //if (subnode.CheckState != CheckState.Unchecked)
                            TransferNodeCheckState(subnode);
                        continue;
                    }
                    else
                    {
                        Node n = newFilesFlat_model.Nodes[0].Nodes.First(x => x.Tag == subnode.Tag);
                        n.CheckState = subnode.CheckState;
                    }
                    //if (subnode.CheckState == CheckState.Checked)
                    //{
                    //    Node n = newFilesFlat_model.Nodes[0].Nodes.First(x => x.Tag == subnode.Tag);
                    //    n.CheckState = CheckState.Checked;
                    //}
                }
                else //flat -> hierarchy
                {

                    Node n = FindNodeByPath(newFiles_model.Nodes[0], ((CloudFile)subnode.Tag).Path);
                    n.CheckState = subnode.CheckState;
                    //if (subnode.CheckState == CheckState.Checked)
                    //{
                    //    Node n = FindNodeByPath(newFiles_model.Nodes[0], ((CloudFile)subnode.Tag).Path);
                    //    n.CheckState = CheckState.Checked;
                    //}
                }
            }
        }

        Node FindNodeByPath(Node root, string path)
        {
            string[] parsedPath = ParsePath(path, true);
            Node currentNode = root;
            int i = 0;
            while(i<parsedPath.Length)
            {
                if (currentNode.Nodes == null)
                    return null;
                foreach(Node n in currentNode.Nodes)
                {
                    if(n.Text == parsedPath[i])
                    {
                        currentNode = n;
                        i++;                        
                        break;
                    }
                }                
            }
            return currentNode;

        }

       
        #endregion 

        #region #BUTTONS

        private void getFiles_button_Click(object sender, EventArgs e)
        {
            checkedFiles = new List<CloudFile>();
            checkedFilesSize = 0;
            GetCheckedFiles(newFiles_model.Nodes[0]);
            AddCheckedFilesToYadisk();
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
            maximumDownloads = (int) maximumDownloads_numericUpDown.Value;
            checkedFiles = new List<CloudFile>();
            checkedFilesSize = 0;
            GetCheckedFiles((((SortedTreeModel)newFilesTreeViewAdv.Model).InnerModel as TreeModel).Nodes[0]);            

            DialogResult dialogResult = MessageBox.Show($"Got links for {checkedFiles.Count} files [{(int)(checkedFilesSize / 1000000)} MB]  Continue?", "Result", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
                return;

            if(folderNewFiles_checkBox.Checked)
                Directory.CreateDirectory(MainForm.syncFolderPath + @"\New Files " + DateTime.Now.ToShortDateString());

            ProgressBar[] usedProgressBars = new ProgressBar[maximumDownloads];
            Label[] usedLabels = new Label[maximumDownloads + 1];
            for (int i = 0; i < maximumDownloads; i++)
            {
                usedProgressBars[i] = progressBars[i];
                usedLabels[i] = progressLabels[i];
            }
            usedLabels[maximumDownloads] = progressLabels[progressLabels.Count - 1];
            
            commonDownload = new CommonDownload(checkedFiles, usedProgressBars, usedLabels, cloudServiceType, overwriteMode_comboBox.SelectedIndex, NetworkCredential, folderNewFiles_checkBox.Checked);
            commonDownload.Start();

            stopDownload_button.Enabled = true;
            stopDownload_button.Visible = true;
        }
        
        private void downloadMega_button_Click(object sender, EventArgs e)
        {
            maximumDownloads = (int)maximumDownloads_numericUpDown.Value;
            checkedFiles = new List<CloudFile>();
            checkedFilesSize = 0;
            GetCheckedFiles((((SortedTreeModel)newFilesTreeViewAdv.Model).InnerModel as TreeModel).Nodes[0]);

            DialogResult dialogResult = MessageBox.Show($"Got links for {checkedFiles.Count} files [{(int)(checkedFilesSize / 1000000)} MB]  Continue?", "Result", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
                return;

            Directory.CreateDirectory(MainForm.syncFolderPath + @"\New Files " + DateTime.Now.ToShortDateString());

            ProgressBar[] usedProgressBars = new ProgressBar[maximumDownloads];
            Label[] usedLabels = new Label[maximumDownloads + 1];
            for (int i = 0; i < maximumDownloads; i++)
            {
                usedProgressBars[i] = progressBars[i];
                usedLabels[i] = progressLabels[i];
            }
            usedLabels[maximumDownloads] = progressLabels[progressLabels.Count - 1];

            if (megaApiClient == null)
            {
                megaApiClient = new MegaApiClient();
                megaApiClient.LoginAnonymous();
            }

            megaDownload = new MegaDownload(megaApiClient, checkedFiles, usedProgressBars, usedLabels, overwriteMode_comboBox.SelectedIndex);
            megaDownload.Start();

            stopDownload_button.Enabled = true;
            stopDownload_button.Visible = true;
        }
        
        #endregion

        private void treeViewAdv_ColumnClicked(object sender, TreeColumnEventArgs e)
        {
            TreeColumn clicked = e.Column;
            if (clicked.SortOrder == SortOrder.Ascending)
                clicked.SortOrder = SortOrder.Descending;
            else
                clicked.SortOrder = SortOrder.Ascending;

            (((TreeViewAdv)sender).Model as SortedTreeModel).Comparer = new FolderItemSorter(clicked.Header, clicked.SortOrder);
        }
         
        private void syncFilesForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (HideForm)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void maximumDownloads_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            maximumDownloads = (int) maximumDownloads_numericUpDown.Value;
        }

        private void stopDownloads_Click(object sender, EventArgs e)
        {
            megaDownload?.Stop();
            commonDownload?.Stop();
        }

        private void filter_TextChangedComplete(object sender, EventArgs e)
        {
            newFilesTreeViewAdv.UpdateNodeFilter();
        }

        private void flatList2_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (flatList2_checkBox.Checked)
            {
                TransferNodeCheckState(newFiles_model.Nodes[0]);
                newFilesTreeViewAdv.ShowNodeToolTips = true;
                newFilesTreeViewAdv.Model = new SortedTreeModel(newFilesFlat_model);
                //newFilesTreeViewAdv.Root.Children[0].Expand();
                newFilesTreeViewAdv.ExpandAll();
                newFilesTreeViewAdv.AutoSizeColumn(newFilesTreeViewAdv.Columns[0]);
                newFilesTreeViewAdv.AutoSizeColumn(newFilesTreeViewAdv.Columns[3]);
            }
            else
            {
                TransferNodeCheckState(newFilesFlat_model.Nodes[0]);
                newFilesTreeViewAdv.ShowNodeToolTips = false;
                //yadiskPublicFolder_treeViewAdv.NodeControls[2].ToolTipProvider = null;
                newFilesTreeViewAdv.Model = new SortedTreeModel(newFiles_model);
                newFilesTreeViewAdv.Root.Children[0].Expand();              
            }            
        }
    }    

    public class DownloadLink
    {
        public DownloadLink(string name, string url)
        {
            this.name = name;
            this.url = url;
        }
        public string name { get; set; }
        public string url { get; set; }        
        public int size { get; set; } = -1;
        public string host { get; set; } = "http links";
        public bool enabled { get; set; } = true;
        //public long created { get; set; } = 20180407;
        //public long uid { get; set; } = 20180407;
        //public string urlProtection { get; set; } = "UNSET";
        //public int current { get; set; } = 0;
        //public object linkStatus { get; set; }
        //public object chunkProgress { get; set; }
        //public object finalLinkState { get; set; } = null;
        //public string availablestatus { get; set; } = "TRUE";
        //public object propertiesString { get; set; } = null;
    }
    
    public class JDLink
    {
        public JDLink(string name, string url)
        {
            this.downloadLink = new DownloadLink(name, url);         
        }
        public object id { get; set; }
        public object name { get; set; }
        public bool enabled { get; set; } = true;
        //public long created { get; set; } = 20180407;
        //public long uid { get; set; } = 20180407;
        public DownloadLink downloadLink { get; set; }
        //public List<string> sourceUrls { get; set; }
        //public object archiveInfo { get; set; } = null;
        //public OriginDetails originDetails { get; set; }
    }

    public class JDPackage
    {        
        public JDPackage(string id, string name)
        {
            this.packageID = id;
            this.name = name;
            //this.links = new List<object>();
        }
        //public string type { get; set; } = "NORMAL";
        public string packageID { get; set; }
        //public List<object> links { get; set; }
        //public object comment { get; set; } = "";
        public string name { get; set; }
        //public string priority { get; set; } = "DEFAULT";
        //public bool expanded { get; set; } = false;
       // public long created { get; set; }
        //public long uid { get; set; }
        public string downloadFolder { get; set; } = "<jd:packagename>";
        //public string sorterId { get; set; } = "ASC.jd.controlling.linkcrawler.CrawledPackage";
        [JsonIgnore]
        public int linksCount { get; set; } = 0;
        [JsonIgnore]
        public string numberId { get; set; }
    }
}
