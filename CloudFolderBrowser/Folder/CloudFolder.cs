using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CG.Web.MegaApiClient;
using Newtonsoft.Json;
using YandexDiskSharp.Models;

namespace CloudFolderBrowser
{
    public class CloudFolder : Folder
    {
        public override int FilesNumberTopDirectoryOnly { get => Files.Count; }
        public string PublicKey { get; set; }
        public string PublicDecryptionKey { get; set; }
        public string EncryptedUrl { get; set; }
        public List<CloudFile> Files { get; set; }

        [JsonIgnore]
        public INode MegaNode { get; set; }

        public string OriginalString { get; set; }

        public CloudFolder(ResourceList rl)
        {
            PublicKey = rl.PublicKey;
            Name = rl.Name;
            Path = rl.Path;
            Modified = rl.Modified;
            Created = rl.Created;
            Size = 0;
            Subfolders = new List<IFolder>();
            Files = new List<CloudFile>();
            foreach (Resource item in rl.Items)
            {
                if (item.Type == YandexDiskSharp.Type.dir)
                    Subfolders.Add(new CloudFolder(item));
                else
                {
                    CloudFile r = new CloudFile(item);
                    Files.Add(r);
                    Size += r.Size;                    
                }
            }            
            SizeTopDirectoryOnly = Size;
        }

        public CloudFolder(Resource rl)
        {
            PublicKey = rl.PublicKey;
            Name = rl.Name;
            Path = rl.Path;
            Modified = rl.Modified;
            Created = rl.Created;
            Size = 0;
            Subfolders = new List<IFolder>();
            Files = new List<CloudFile>();
            if (rl.Embedded != null && rl.Embedded.Count > 0)
            {
                foreach (Resource item in rl.Embedded)
                {
                    if (item.Type == YandexDiskSharp.Type.dir)
                        Subfolders.Add(new CloudFolder(item));
                    else
                    {
                        CloudFile r = new CloudFile(item);
                        Files.Add(r);
                        Size += r.Size;                       
                    }
                }
                SizeTopDirectoryOnly = Size;
            }
        }

        [JsonConstructor]
        public CloudFolder(string name, DateTime created, DateTime modified, long size)
        {
            Name = name;
            Modified = modified;
            Created = created;
            Size = size;
            SizeTopDirectoryOnly = Size;
            Subfolders = new List<IFolder>();
            Files = new List<CloudFile>();
        }

        public CloudFolder()
        {
        }

        public void AddFile(CloudFile file)
        {
            this.Files.Add(file);
            if (file.Modified > this.Modified)
                this.Modified = file.Modified;
        }

        public void AddSubfolder(CloudFolder subfolder)
        {
            this.Subfolders.Add(subfolder);
            //if (subfolder.Modified > this.Modified)
            //    this.Modified = subfolder.Modified;
        }

        public void Copy(Resource rl)
        {
            foreach (Resource item in rl.Embedded)
            {
                if (item.Type == YandexDiskSharp.Type.dir)
                    Subfolders.Add(new CloudFolder(item));
                else
                {
                    CloudFile r = new CloudFile(item);
                    Files.Add(r);
                    Size += r.Size;                    
                }
            }
            SizeTopDirectoryOnly = Size;
        }

        public List<CloudFile> GetFlatFilesList()
        {
            List<CloudFile> flatFilesList = new List<CloudFile>(this.Files);
            AddFilesToList(this, ref flatFilesList);
            return flatFilesList;
        }

        void AddFilesToList(CloudFolder folder, ref List<CloudFile> flatFilesList)
        {
            foreach (CloudFolder subfolder in folder.Subfolders)
            {
                flatFilesList.AddRange(subfolder.Files);
                AddFilesToList(subfolder, ref flatFilesList);
            }
        }

        public void SaveToJson()
        {
            Directory.CreateDirectory("jsons");
            string hashString = Utility.GetHashString(OriginalString);            
            File.WriteAllText("jsons/" + hashString + ".json", JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            }));
        }        

        public async Task<CloudFolder> LoadFromJson(bool checkStatus = false)
        {
            var cloudFolder = new CloudFolder();
            Directory.CreateDirectory("jsons");
            string hashString = Utility.GetHashString(OriginalString);

            foreach (string fileName in Directory.GetFiles("jsons"))
            {
                if (fileName == @"jsons\" + hashString + ".json")
                {
                    string jsonString = File.ReadAllText(fileName);
                    cloudFolder = JsonConvert.DeserializeObject<CloudFolder>(jsonString, new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });                    
                    return cloudFolder;
                }
            }
            return null;
        }

        
    }

}
