using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using CG.Web.MegaApiClient;
using Newtonsoft.Json;
using WebDAVClient;
using Exception = System.Exception;

namespace CloudFolderBrowser
{
    public class MainFormModel
    {
        public List<CloudFolder> AllFolders = new List<CloudFolder>();
        public CloudFolder CloudPublicFolder = new CloudFolder();
        bool debugMode = false; 
        public CloudServiceType CloudServiceType;

        public bool LoadedFromFile = false;


        public async Task<List<CloudFile>> GetMissingFiles(List<CloudFolder> checkedFolders, List<CloudFolder> mixedFolders, string syncFolderPath, bool ignoreExistingFiles)
        {
            List<CloudFile> missingFiles = new List<CloudFile>();

            DirectoryInfo syncFolderDirectory = new DirectoryInfo(syncFolderPath);

            foreach (CloudFolder folder in mixedFolders)
            {
                DirectoryInfo di = new DirectoryInfo(syncFolderPath + folder.Path.Replace(@"/", @"\"));
                if (!di.Exists)
                {
                    missingFiles.AddRange(folder.Files);
                    continue;
                }
                FileInfo[] flatSyncFolderFilesList = di.GetFiles("*", SearchOption.TopDirectoryOnly);
                if (!ignoreExistingFiles)
                    missingFiles.AddRange(folder.Files);
                else
                    missingFiles.AddRange(await CompareFilesLists(folder.Files, flatSyncFolderFilesList, syncFolderPath));
            }

            foreach (CloudFolder folder in checkedFolders)
            {
                DirectoryInfo di = new DirectoryInfo(syncFolderPath + folder.Path.Replace(@"/", @"\"));
                List<CloudFile> flatCloudFolderFilesList = folder.GetFlatFilesList();
                if (!di.Exists)
                {
                    missingFiles.AddRange(folder.GetFlatFilesList());
                    continue;
                }
                FileInfo[] flatSyncFolderFilesList = di.GetFiles("*", SearchOption.AllDirectories);
                if (!ignoreExistingFiles)
                    missingFiles.AddRange(folder.GetFlatFilesList());
                else
                    missingFiles.AddRange(await CompareFilesLists(flatCloudFolderFilesList, flatSyncFolderFilesList, syncFolderPath));
            }
            return missingFiles;
        }

        async static Task<List<CloudFile>> CompareFilesLists(List<CloudFile> cloudFolderFileList, FileInfo[] syncFolderFileList, string syncFolderPath)
        {
            List<CloudFile> missingFiles = new List<CloudFile>();

            await Task.Run(() =>
            {
                List<CloudFile> localFiles = syncFolderFileList.ToList().ConvertAll(
                    x => new CloudFile(x.Name, DateTime.Now, DateTime.Now, x.Length)
                    { 
                        Path = x.FullName.Replace(syncFolderPath, @"\").Replace(@"\", @"/") 
                    });

                missingFiles = cloudFolderFileList.Except(localFiles, new FileComparer()).ToList();
            });

            return missingFiles;
        }

        public async Task LoadMega(string url, IProgress<int> progress = null)
        {            
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

            MegaApiClient megaClient = new MegaApiClient();
            megaClient.LoginAnonymous();
            int filecount = 0;

            url = url.Replace("#F!", "folder/").Replace("!", "#");
            string lastId = Utility.GetLastId(url);
            await Task.Run(() =>
            {

                IEnumerable<INode> nodes;
                if (url.Contains("mega.nz/file"))
                {
                    nodes = megaClient.GetFullNodesFromLink(new Uri(url), out _);
                    progress?.Report(1);
                    var node = nodes.ElementAt(0);

                    CloudPublicFolder = new CloudFolder(nodes.ElementAt(0).Name, nodes.ElementAt(0).CreationDate ?? DateTime.MinValue, DateTime.MinValue, 0);
                    CloudPublicFolder.Path = "/";                   
                    CloudPublicFolder.OriginalString = url;
                    var match = Regex.Match(url, "(/folder/)(.*)#([^/]*)(/(.*))?");
                    CloudPublicFolder.PublicKey = match.Groups[2].Value;
                    CloudPublicFolder.PublicDecryptionKey = match.Groups[3].Value;

                    CloudFile file = new CloudFile(node.Name, node.CreationDate ?? DateTime.MinValue, node.ModificationDate ?? DateTime.MinValue, node.Size);
                    CloudFolder parentFolder = CloudPublicFolder;
                    file.Path = parentFolder.Path + file.Name;
                    file.MegaNode = node;
                    file.PublicUrl = new Uri($"{CloudPublicFolder.OriginalString}/file/{node.Id}");
                    parentFolder.SizeTopDirectoryOnly += file.Size;
                    parentFolder.AddFile(file);
                    filecount++;
                }
                else
                {
                    nodes = megaClient.GetFullNodesFromLink(new Uri(url), out _);
                    CloudPublicFolder = new CloudFolder(nodes.ElementAt(0).Name, nodes.ElementAt(0).CreationDate ?? DateTime.MinValue, DateTime.MinValue, 0);
                    CloudPublicFolder.Path = "/";
                    CloudPublicFolder.MegaNode = nodes.ElementAt(0);
                    CloudPublicFolder.OriginalString = url;
                    var match = Regex.Match(url, "(/folder/)(.*)#([^/]*)(/(.*))?");
                    CloudPublicFolder.PublicKey = match.Groups[2].Value;
                    CloudPublicFolder.PublicDecryptionKey = match.Groups[3].Value;
                    Dictionary<string, CloudFolder> allfolders = new Dictionary<string, CloudFolder>();
                    allfolders.Add(nodes.ElementAt(0).Id, CloudPublicFolder);
                    AllFolders = new List<CloudFolder>() { CloudPublicFolder };
                    foreach (var node in nodes)
                    {
                        if (node.Type == NodeType.Directory)
                        {
                            if (node.Id == nodes.ElementAt(0).Id) //root node
                                continue;
                            CloudFolder subfolder = new CloudFolder(node.Name, node.CreationDate ?? DateTime.MinValue, DateTime.MinValue, node.Size);
                            CloudFolder parentFolder = allfolders[node.ParentId];
                            subfolder.MegaNode = node;
                            subfolder.Path = parentFolder.Path + subfolder.Name + "/";
                            allfolders.Add(node.Id, subfolder);
                            parentFolder.AddSubfolder(subfolder);
                            AllFolders.Add(subfolder);
                            continue;
                        }
                        if (node.Type == NodeType.File)
                        {
                            CloudFile file = new CloudFile(node.Name, node.CreationDate ?? DateTime.MinValue, node.ModificationDate ?? DateTime.MinValue, node.Size);
                            CloudFolder parentFolder = allfolders[node.ParentId];
                            file.Path = parentFolder.Path + file.Name;
                            file.MegaNode = node;                            

                            file.PublicUrl = new Uri($"{CloudPublicFolder.OriginalString}/file/{node.Id}");
                            parentFolder.SizeTopDirectoryOnly += file.Size;
                            parentFolder.AddFile(file);
                            filecount++;
                            //parentFolder.Files.Add(file);
                        }
                    }
                }               
            });
            CloudPublicFolder.CalculateFolderSize();            
        }

        public async Task LoadMega2(List<FogLinkFile> nodes, string originalString) //oldfoglink
        {
            int filecount = 0;
            
                await Task.Run(() =>
                {
                    CloudPublicFolder = new CloudFolder(nodes.ElementAt(0).Name, nodes.ElementAt(0).CreationDate, DateTime.MinValue, 0);
                    CloudPublicFolder.Path = "/";
                    CloudPublicFolder.EncryptedUrl = nodes.ElementAt(0).EncryptedLink;
                    CloudPublicFolder.OriginalString = originalString;

                    Dictionary<string, CloudFolder> megaFolders = new Dictionary<string, CloudFolder>();
                    megaFolders.Add(nodes.ElementAt(0).Id, CloudPublicFolder);

                    AllFolders = new List<CloudFolder>() { CloudPublicFolder };

                    foreach (var node in nodes)
                    {
                        if (node.Type == NodeType.Directory)
                        {
                            CloudFolder subfolder = new CloudFolder(node.Name, node.CreationDate, DateTime.MinValue, node.Size2);
                            if (node.ParentId == null || !megaFolders.ContainsKey(node.ParentId))
                                continue;
                            CloudFolder parentFolder = megaFolders[node.ParentId];
                            subfolder.Path = parentFolder.Path + subfolder.Name + "/";
                            subfolder.EncryptedUrl = node.EncryptedLink;
                            megaFolders.Add(node.Id, subfolder);
                            parentFolder.AddSubfolder(subfolder);
                            AllFolders.Add(subfolder);
                            continue;
                        }
                        if (node.Type == NodeType.File)
                        {
                            CloudFile file = new CloudFile(node.Name, node.CreationDate, node.CreationDate, node.Size2);
                            CloudFolder parentFolder = megaFolders[node.ParentId];
                            file.Path = parentFolder.Path + file.Name;
                            file.EncryptedUrl = node.EncryptedLink;
                            parentFolder.SizeTopDirectoryOnly += file.Size;
                            parentFolder.AddFile(file);
                            filecount++;
                            //parentFolder.Files.Add(file);
                        }
                    }

                });
            CloudPublicFolder.CalculateFolderSize();
             
          
        }

        public async Task LoadMega(List<FogLinkFile> nodes, string originalString) //foglink
        {
            int filecount = 0;

            await Task.Run(() =>
            {             
                CloudPublicFolder = new CloudFolder("MEGA", nodes.ElementAt(0).ModificationDate, nodes.ElementAt(0).ModificationDate, 0);
                CloudPublicFolder.Path = "/";
                if (!nodes[0].IsFile)
                    CloudPublicFolder.EncryptedUrl = nodes.ElementAt(0).EncryptedLink;
                CloudPublicFolder.OriginalString = originalString;

                AllFolders = new List<CloudFolder>() { CloudPublicFolder };

                if (!nodes[0].IsFile)
                    BuildFolderStructure(CloudPublicFolder, nodes.ElementAt(0));
                else
                {
                    var parentFolder = CloudPublicFolder;
                    var node = nodes[0];
                    CloudFile file = new CloudFile(node.Name, node.ModificationDate, node.ModificationDate, node.Size2);
                    file.Path = parentFolder.Path + file.Name;
                    file.EncryptedUrl = node.EncryptedLink;
                    parentFolder.SizeTopDirectoryOnly += file.Size;
                    parentFolder.AddFile(file);
                }
            });
            CloudPublicFolder.CalculateFolderSize();
        }

        void BuildFolderStructure(CloudFolder parentFolder, FogLinkFile parent)
        {            
            foreach (var node in parent.Children)
            {
                if (node.Type == NodeType.Directory)
                {
                    CloudFolder subfolder = new CloudFolder(node.Name, node.CreationDate, DateTime.MinValue, node.Size2);  
                    subfolder.Path = parentFolder.Path + subfolder.Name + "/";
                    subfolder.EncryptedUrl = node.EncryptedLink;                 
                    parentFolder.AddSubfolder(subfolder);
                    AllFolders.Add(subfolder);
                    BuildFolderStructure(subfolder, node);
                    continue;
                }
                if (node.Type == NodeType.File)
                {
                    CloudFile file = new CloudFile(node.Name, node.CreationDate, node.CreationDate, node.Size2);                    
                    file.Path = parentFolder.Path + file.Name;
                    file.EncryptedUrl = node.EncryptedLink;
                    parentFolder.SizeTopDirectoryOnly += file.Size;
                    parentFolder.AddFile(file);                                  
                }
            }
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
        private void AddParentNode(ref Dictionary<string, CloudFolder> allfolders, IEnumerable<INode> nodes,
            List<INode> filteredNodes, INode node, CloudFolder folder)
        {
            var parentNode = nodes.Where(x => x.Id == node.ParentId).FirstOrDefault();
            if (parentNode == null || allfolders.ContainsKey(parentNode.Id))
                return;
            CloudFolder parentFolder = new CloudFolder(parentNode.Name, parentNode.CreationDate ?? DateTime.MinValue, DateTime.MinValue, parentNode.Size);
            parentFolder.Path = CloudPublicFolder.Path + parentFolder.Name + "/";
            parentFolder.AddSubfolder(folder);
            //allfolders.Add(parentNode.Id, parentFolder);
            filteredNodes.Add(parentNode);
            AddParentNode(ref allfolders, nodes, filteredNodes, parentNode, parentFolder);
        }


        public IClient webdavClient;
        string allsyncUrl = "https://allsync.com";
        public string allsyncRootFolderAddress = "";
        public Dictionary<string, string> savedPasswords = new Dictionary<string, string>();
        public string folderKey = "", password = "";

        public async Task<bool> PreloadAllsync(string url, bool onlyCheck = false)
        {
            CloudPublicFolder = new CloudFolder("", DateTime.Now, DateTime.Now, 0);
            CloudPublicFolder.OriginalString = url;
            List<string> uriStructure = new List<string>();
            url = HttpUtility.UrlDecode(url);
            MatchCollection mc = Regex.Matches(url, "(?:https?://)?(?:[^@\n]+@)?(?:www.)?([^:/\n?]+)");

            foreach (Match m in mc)
                uriStructure.Add(m.Value);
            allsyncUrl = uriStructure[0];
            string path = "";

            allsyncRootFolderAddress = allsyncUrl + @"/s/" + uriStructure[2] + "?path=";
            if(url.Contains(".qloud")) allsyncRootFolderAddress = allsyncUrl + @"/s/" + uriStructure[3] + "?path=";

            if (uriStructure.Count < 5)
            {
                CloudPublicFolder.Name = "";
                path = "/";
            }
            else
            {
                for (int i = 4; i < uriStructure.Count; i++)
                    path += "/" + uriStructure[i];
                path += "/";
                CloudPublicFolder.Name = uriStructure[uriStructure.Count - 1];
            }
            CloudPublicFolder.Path = path;           

            folderKey = uriStructure[2];
            if(url.Contains("qloud")) folderKey = uriStructure[3];

            WriteToLog($"\n{DateTime.Now}\n Searching for password for {folderKey} \n\n");
            if (savedPasswords.ContainsKey(folderKey))
            {
                password = savedPasswords[folderKey];
                WriteToLog($"\n{DateTime.Now}\n {folderKey} - password {password} \n\n");

            }
            UpdateWebdavClient(folderKey, password);              

            if (onlyCheck)
            {
                var success = await CheckAllsyncFolder();
                WriteToLog($"\n{DateTime.Now}\n Storing password {password} \n\n");
                WebdavCredential = new NetworkCredential { UserName = folderKey, Password = password };
                return success;
            }     
            return true;
        }

        public async Task<int> LoadAllsync(string folderKey, string password = "", IProgress<int> progress = null)
        {
            WebDAVClient.Model.Item[] items;
            try
            {
                if (password != "")
                    UpdateWebdavClient(folderKey, password);
                items = (await webdavClient.ListShared(CloudPublicFolder.Path, 999)).ToArray();
                progress?.Report(1);
            }
            catch (WebDAVClient.Helpers.WebDAVException ex)
            {
                return ex.GetHttpCode();                
            }
            //catch (Exception ex2)
            //{
            //    MessageBox.Show("Bad url or no connection");
            //    return;
            //}

            password = (webdavClient as Client).Credentials.Password;
            if (savedPasswords.ContainsKey(folderKey))
                savedPasswords[folderKey] = password;
            else
                savedPasswords.Add(folderKey, password);
            Properties.Settings.Default.savedPasswordsJson = JsonConvert.SerializeObject(savedPasswords);
            Properties.Settings.Default.Save();

            WebdavCredential = new NetworkCredential { UserName = folderKey, Password = password };
            WriteToLog($"\n{DateTime.Now}\nStoring(2) password: {password} \n\n");

            List<CloudFolder> allFolders = new List<CloudFolder> { CloudPublicFolder };
            AllFolders = new List<CloudFolder>() { CloudPublicFolder };
            foreach (var item in items)
            {
                if (item.IsCollection)
                {
                    string path = HttpUtility.UrlDecode(item.Href).Replace("/public.php/webdav", "");                    
                    if (!allFolders.ConvertAll(x => x.Path).Contains(path))
                    {
                        CloudFolder newFolder = new CloudFolder(item.DisplayName, DateTime.MinValue, (DateTime)item.LastModified, 0);
                        newFolder.Path = path;
                        allFolders.Add(newFolder);
                        AllFolders.Add(newFolder);
                    }
                }
            }
            try
            {
                foreach (var folder in allFolders)
                {
                    if (folder.Path == CloudPublicFolder.Path || folder.Path == "")
                        continue;
                    string parentFolderPath = folder.Path.Remove(folder.Path.Length - 1 - folder.Name.Length, folder.Name.Length + 1);                    
                    allFolders.Find(x => x.Path == parentFolderPath).AddSubfolder(folder);                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during folder structure building.");
                return 9999;
            }

            foreach (var item in items)
            {
                if (!item.IsCollection)
                {
                    string encodedPath = item.Href.Replace("/public.php/webdav/", "/download?path=");
                    string path = HttpUtility.UrlDecode(item.Href).Replace("/public.php/webdav/", "");
                    string parentFolderPath = "/" + path.Remove(path.Length - item.DisplayName.Length, item.DisplayName.Length);
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
                        parentFolder = CloudPublicFolder;
                    parentFolder.AddFile(file);
                    parentFolder.SizeTopDirectoryOnly += file.Size;
                }
            }
            //Array.Clear(items);
            //items = null;
            //GC.Collect();
            if (CloudPublicFolder.Subfolders.Count == 0 && CloudPublicFolder.Files.Count == 0)
                return 9999;

            CloudPublicFolder.CalculateFolderSize();
            return 200;
        }

        async Task<bool> CheckAllsyncFolder()
        {           
            try
            {
                var items = await webdavClient.ListShared(CloudPublicFolder.Path, 1);
                if (items.Count() > 0)
                    return true;                
            }
            catch (Exception ex)
            {
                DialogResult continueDialogResult = MessageBox.Show("Cannot retrieve data from url. Maybe link is dead.", "", MessageBoxButtons.OK);
                return(continueDialogResult == DialogResult.Yes);                
            }
            return false;
        }

        public void WriteToLog(string message, bool force = false)
        {
            if (debugMode || force)
            {
                var logFileName = $"download-log-{DateTime.Now.ToString("MM-dd-yyyy")}.txt";
                File.AppendAllText(logFileName, message);
            }
        }

        public NetworkCredential WebdavCredential = null;

        void UpdateWebdavClient(string folderKey, string password = "")
        {
            NetworkCredential webdavCredential = new NetworkCredential { UserName = folderKey, Password = password };
            webdavClient = new Client(webdavCredential);
            webdavClient.Server = allsyncUrl;
            webdavClient.BasePath = "/public.php/webdav/";
            Dictionary<string, string> customHeaders = new Dictionary<string, string>();
            customHeaders.Add("X-Requested-With", "XMLHttpRequest");
            customHeaders.Add("Accept-Encoding", "gzip, deflate, br");            
            webdavClient.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:95.0) Gecko/20100101 Firefox/95.0";
            webdavClient.CustomHeaders = customHeaders;           
        }
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
}
