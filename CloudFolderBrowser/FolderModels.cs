using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YandexDiskSharp.Models;

namespace CloudFolderBrowser
{
    public interface IFolder
    {
        long Size { get; set; }
        long SizeTopDirectoryOnly { get; set; }
        string Name { get; set; }
        string Path { get; set; }
        DateTime Modified { get; set; }
        DateTime Created { get; set; }
        void CalculateFolderSize();
    }

    public class BaseFolder : IFolder
    {
        public long Size { get; set; }
        public long SizeTopDirectoryOnly { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public List<IFolder> Subfolders { get; set; }

        public void CalculateFolderSize()
        {
            this.Size += SizeTopDirectoryOnly;
            foreach (IFolder subfolder in this.Subfolders)
            {
                subfolder.CalculateFolderSize();
                this.Size += subfolder.SizeTopDirectoryOnly;
                if (subfolder.Modified > this.Modified)
                    this.Modified = subfolder.Modified;
            }
        }
    }

    public class Folder : BaseFolder
    {
        public List<FileInfo> Files { get; set; }

        public Folder() { }

        public Folder(DirectoryInfo di)
        {
            Name = di.Name;
            Path = di.FullName + @"\";
            Modified = di.LastWriteTime;
            Created = di.CreationTime;
            Subfolders = new List<IFolder>();
            Files = new List<FileInfo>();
            foreach (DirectoryInfo subdi in di.GetDirectories())
            {
                Subfolders.Add(new Folder(subdi));
            }
            foreach (FileInfo file in di.GetFiles())
            {
                Files.Add(file);
                SizeTopDirectoryOnly += file.Length;
            }
        }

        public FileInfo[] GetFiles()
        {
            DirectoryInfo di = new DirectoryInfo(this.Path);
            return di.GetFiles("*", SearchOption.AllDirectories);
        }
    }

    public class CloudFolder : BaseFolder
    {
        public string PublicKey { get; set; }
        public List<CloudFile> Files { get; set; }

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
    }

}
