using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudFolderBrowser
{
    public partial class ErrorLogForm : Form
    {
        public ErrorLogForm()
        {
            InitializeComponent();
        }

        List<string> Log = new List<string>();

        public void AddErrorLine(string line)
        {
            Log.Add(line);
            log_richTextBox.AppendText($"[{DateTime.Now}] {line} \n");
        }
    }
}
