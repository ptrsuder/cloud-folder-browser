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
    public partial class EditLinkForm : Form
    {
        public string LinkName {get;set;}
        public string LinkUrl { get; set; }
        public EditLinkForm(string name, string url)
        {
            InitializeComponent();            
            name_textBox.Text = name;
            url_textBox.Text = url;
            CenterToParent();
        }

        public EditLinkForm()
        {
            InitializeComponent();           
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            LinkName = name_textBox.Text;
            LinkUrl = url_textBox.Text;
            
            Close();
        }
    }
}
