using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFolderBrowser
{
    public class Folder : IFolder
    {
        public long Size { get; set; }
        public long SizeTopDirectoryOnly { get; set; }
        public int FilesNumber { get; set; }
        public virtual int FilesNumberTopDirectoryOnly { get; set; }

        string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value.EndsWith("."))
                    _name = value.Remove(value.Length - 1, 1);
                else
                    _name = value;
            }
        }
        public string Path { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
        public List<IFolder> Subfolders { get; set; }

        public void CalculateFolderSize()
        {
            Size += SizeTopDirectoryOnly;
            FilesNumber += FilesNumberTopDirectoryOnly;

            foreach (IFolder subfolder in Subfolders)
            {
                subfolder.CalculateFolderSize();
                Size += subfolder.Size;
                FilesNumber += subfolder.FilesNumber;
                if (subfolder.Modified > Modified)
                    Modified = subfolder.Modified;
            }
        }
    }

}
