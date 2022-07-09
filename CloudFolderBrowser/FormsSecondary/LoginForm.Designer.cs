namespace CloudFolderBrowser
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.login_webBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // login_webBrowser
            // 
            this.login_webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.login_webBrowser.Location = new System.Drawing.Point(0, 0);
            this.login_webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.login_webBrowser.Name = "login_webBrowser";
            this.login_webBrowser.Size = new System.Drawing.Size(577, 388);
            this.login_webBrowser.TabIndex = 0;
            this.login_webBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.login_webBrowser_Navigated);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 388);
            this.Controls.Add(this.login_webBrowser);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser login_webBrowser;
    }
}