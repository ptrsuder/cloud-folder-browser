using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudFolderBrowser
{
    public partial class FogLinkForm : Form
    {
        MainForm MainForm;        

        public FogLinkForm()
        {
            InitializeComponent();           
            serverAddress_textBox.Text = FogLink.ServerAddress.OriginalString;
        }        

        private async void encrypt_button_Click(object sender, EventArgs e)
        {             
            out_textBox.Text = await FogLink.GetEncodedAsync(serverAddress_textBox.Text);
        }         

        private void saveServerAddress_button_Click(object sender, EventArgs e)
        {
            FogLink.ServerAddress = new Uri(serverAddress_textBox.Text);
            Properties.Settings.Default.fogLinkAddress = serverAddress_textBox.Text;
            Properties.Settings.Default.Save();
        }
    }
}
