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
    public partial class ErrorForm : Form
    {
        public ErrorForm(string title, string message)
        {
            InitializeComponent();
            this.Text = title;
            message_richTextBox.Text = message;
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {

        }
    }
}
