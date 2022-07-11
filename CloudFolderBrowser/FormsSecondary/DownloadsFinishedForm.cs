using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudFolderBrowser
{
    public partial class DownloadsFinishedForm : Form
    {
        string downloadFolder;

        public DownloadsFinishedForm(string directoryInfo, string message, string message2 = "")
        {            
            downloadFolder = directoryInfo;
            InitializeComponent();
            message_label.Text = message;
            failed_label.Text = message2;
            CenterToParent();
        }

        private void OK_button_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void ppenFolder_button_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = downloadFolder, UseShellExecute = true });
            Close();
            Dispose();
        }
    }
}
