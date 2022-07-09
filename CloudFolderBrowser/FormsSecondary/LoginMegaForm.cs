using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CG.Web.MegaApiClient;
using Newtonsoft.Json;

namespace CloudFolderBrowser
{
    public partial class LoginMegaForm : Form
    {
        MainForm ParentForm;
        public LoginMegaForm(MainForm parentForm)
        {
            ParentForm = parentForm;
            InitializeComponent();
            CenterToParent();
        }

        private void LoginMegaForm_Load(object sender, EventArgs e)
        {
            login_textBox.Text = Properties.Settings.Default.megaLogin;
            password_textBox.Text = Properties.Settings.Default.megaPassword;
        }

        private async void signIn_button_Click(object sender, EventArgs e)
        {
            MegaApiClient.LogonSessionToken loginToken = null;
            try
            {
                await ParentForm.LoginMega(login_textBox.Text, password_textBox.Text);
            }
            catch
            {
                MessageBox.Show("Failed to sign in.");
                return;
            }

            Properties.Settings.Default.megaLogin = login_textBox.Text;
            if (savePassword_checkBox.Checked)
            {                
                Properties.Settings.Default.megaPassword = password_textBox.Text;
                Properties.Settings.Default.Save();
            }
            else
            {                
                Properties.Settings.Default.megaPassword = "";
                Properties.Settings.Default.Save();
            }
            Close();            
        }
    }
}
