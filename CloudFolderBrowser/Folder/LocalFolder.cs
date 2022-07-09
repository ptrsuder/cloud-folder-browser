using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFolderBrowser
{
    public class LocalFolder : Folder
    {
        public List<FileInfo> Files { get; set; }

        public LocalFolder() { }

        public LocalFolder(DirectoryInfo di)
        {
            Name = di.Name;
            Path = di.FullName + @"\";
            Modified = di.LastWriteTime;
            Created = di.CreationTime;
            Subfolders = new List<IFolder>();
            Files = new List<FileInfo>();
            foreach (DirectoryInfo subdi in di.GetDirectories())
            {
                Subfolders.Add(new LocalFolder(subdi));
            }
            foreach (FileInfo file in di.GetFiles())
            {
                Files.Add(file);
                SizeTopDirectoryOnly += file.Length;
            }
        }

        public FileInfo[] GetFiles()
        {
            DirectoryInfo di = new DirectoryInfo(Path);
            return di.GetFiles("*", SearchOption.AllDirectories);
        }
    }

}
