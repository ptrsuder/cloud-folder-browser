﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudFolderBrowser
{
    public partial class LoginForm : Form
    {
        MainForm parentForm;

        public LoginForm(string serviceName, MainForm parentForm)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            if(serviceName == "yandex")
            {
                this.Show();
                string oauthReqLink = @"https://oauth.yandex.ru/authorize?response_type=token&client_id=b7b67bceb36f41058a0e9a49f1ed87b4";
                login_webBrowser.Navigate(oauthReqLink);

            }      
        }

        private void login_webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (login_webBrowser.Url.AbsoluteUri.Contains("https://oauth.yandex.ru/verification_code#access_token="))
            {
                var m = Regex.Match(login_webBrowser.Url.AbsoluteUri, "#access_token=(.+?)&");
                var accessToken = m.Groups[1].Value;
                Properties.Settings.Default.accessTokenYandex = accessToken;
                Properties.Settings.Default.Save();
               
                Close();
            }
            
        }
    }
}
