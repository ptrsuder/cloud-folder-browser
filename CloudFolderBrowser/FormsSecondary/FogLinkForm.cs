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
            encrypt_button.Enabled = false;
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
            out_textBox.Text = await FogLink.GetEncodedAsync(in_textBox.Text);
            encrypt_button.Enabled = true;
            progressBar1.Visible = false;
        }

        private void saveServerAddress_button_Click(object sender, EventArgs e)
        {
            if (serverAddress_textBox.Text == "")
                serverAddress_textBox.Text = @"https://foglink.onrender.com";
            FogLink.ServerAddress = new Uri(serverAddress_textBox.Text);
            Properties.Settings.Default.fogLinkAddress = serverAddress_textBox.Text;
            Properties.Settings.Default.Save();
        }
    }
}
