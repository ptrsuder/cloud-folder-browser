namespace CloudFolderBrowser
{
    partial class DownloadsFinishedForm
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
            this.OK_button = new System.Windows.Forms.Button();
            this.opneFolder_button = new System.Windows.Forms.Button();
            this.message_label = new System.Windows.Forms.Label();
            this.failed_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OK_button
            // 
            this.OK_button.Location = new System.Drawing.Point(131, 69);
            this.OK_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OK_button.Name = "OK_button";
            this.OK_button.Size = new System.Drawing.Size(88, 27);
            this.OK_button.TabIndex = 0;
            this.OK_button.Text = "OK";
            this.OK_button.UseVisualStyleBackColor = true;
            this.OK_button.Click += new System.EventHandler(this.OK_button_Click);
            // 
            // opneFolder_button
            // 
            this.opneFolder_button.Location = new System.Drawing.Point(227, 69);
            this.opneFolder_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.opneFolder_button.Name = "opneFolder_button";
            this.opneFolder_button.Size = new System.Drawing.Size(88, 27);
            this.opneFolder_button.TabIndex = 1;
            this.opneFolder_button.Text = "Open folder";
            this.opneFolder_button.UseVisualStyleBackColor = true;
            this.opneFolder_button.Click += new System.EventHandler(this.ppenFolder_button_Click);
            // 
            // message_label
            // 
            this.message_label.AutoSize = true;
            this.message_label.Location = new System.Drawing.Point(14, 18);
            this.message_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.message_label.Name = "message_label";
            this.message_label.Size = new System.Drawing.Size(130, 15);
            this.message_label.TabIndex = 2;
            this.message_label.Text = "All downloads finished!";
            // 
            // failed_label
            // 
            this.failed_label.AutoSize = true;
            this.failed_label.Location = new System.Drawing.Point(14, 38);
            this.failed_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.failed_label.Name = "failed_label";
            this.failed_label.Size = new System.Drawing.Size(111, 15);
            this.failed_label.TabIndex = 2;
            this.failed_label.Text = "Failed downloads: 0";
            // 
            // DownloadsFinishedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 108);
            this.Controls.Add(this.failed_label);
            this.Controls.Add(this.message_label);
            this.Controls.Add(this.opneFolder_button);
            this.Controls.Add(this.OK_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(400, 400);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "DownloadsFinishedForm";
            this.ShowIcon = false;
            this.Text = "Finished";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK_button;
        private System.Windows.Forms.Button opneFolder_button;
        private System.Windows.Forms.Label message_label;
        private Label failed_label;
    }
}