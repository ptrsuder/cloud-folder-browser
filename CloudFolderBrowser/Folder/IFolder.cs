

namespace CloudFolderBrowser
{    public interface IFolder
    {
        long Size { get; set; }
        long SizeTopDirectoryOnly { get; set; }
        int FilesNumber { get; set; }
        int FilesNumberTopDirectoryOnly { get; set; }
        string Name { get; set; }
        string Path { get; set; }
        DateTime Modified { get; set; }
        DateTime Created { get; set; }
        void CalculateFolderSize();
    }
}
