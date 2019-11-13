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
    public partial class PasswordForm : Form
    {
        public string Password = "";

        public PasswordForm()
        {
            InitializeComponent();            
        }

        private void confirmPassword_button_Click(object sender, EventArgs e)
        {
            Password = password_textBox.Text;
            this.Close();
        }

        private void cancelPassword_button_Click(object sender, EventArgs e)
        {
            Password = "null";
            this.Close();
        }
    }
}
