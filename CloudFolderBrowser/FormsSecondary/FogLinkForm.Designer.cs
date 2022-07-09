namespace CloudFolderBrowser
{
    partial class FogLinkForm
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
            this.in_textBox = new System.Windows.Forms.TextBox();
            this.encrypt_button = new System.Windows.Forms.Button();
            this.out_textBox = new System.Windows.Forms.TextBox();
            this.serverAddress_textBox = new CloudFolderBrowser.TextBox();
            this.saveServerAddress_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // in_textBox
            // 
            this.in_textBox.Location = new System.Drawing.Point(13, 95);
            this.in_textBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.in_textBox.Name = "in_textBox";
            this.in_textBox.Size = new System.Drawing.Size(866, 23);
            this.in_textBox.TabIndex = 1;
            // 
            // encrypt_button
            // 
            this.encrypt_button.Location = new System.Drawing.Point(12, 155);
            this.encrypt_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.encrypt_button.Name = "encrypt_button";
            this.encrypt_button.Size = new System.Drawing.Size(88, 27);
            this.encrypt_button.TabIndex = 2;
            this.encrypt_button.Text = "encrypt";
            this.encrypt_button.UseVisualStyleBackColor = true;
            this.encrypt_button.Click += new System.EventHandler(this.encrypt_button_Click);
            // 
            // out_textBox
            // 
            this.out_textBox.Location = new System.Drawing.Point(13, 125);
            this.out_textBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.out_textBox.Name = "out_textBox";
            this.out_textBox.ReadOnly = true;
            this.out_textBox.Size = new System.Drawing.Size(866, 23);
            this.out_textBox.TabIndex = 4;
            // 
            // serverAddress_textBox
            // 
            this.serverAddress_textBox.Location = new System.Drawing.Point(13, 32);
            this.serverAddress_textBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.serverAddress_textBox.Name = "serverAddress_textBox";
            this.serverAddress_textBox.Size = new System.Drawing.Size(414, 23);
            this.serverAddress_textBox.TabIndex = 0;
            this.serverAddress_textBox.TextChangedCompleteDelay = System.TimeSpan.Parse("00:00:00.6000000");            
            // 
            // saveServerAddress_button
            // 
            this.saveServerAddress_button.Location = new System.Drawing.Point(434, 32);
            this.saveServerAddress_button.Name = "saveServerAddress_button";
            this.saveServerAddress_button.Size = new System.Drawing.Size(75, 23);
            this.saveServerAddress_button.TabIndex = 5;
            this.saveServerAddress_button.Text = "Save";
            this.saveServerAddress_button.UseVisualStyleBackColor = true;
            this.saveServerAddress_button.Click += new System.EventHandler(this.saveServerAddress_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "FogLink server adress";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "URL encryption";
            // 
            // FogLinkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 193);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveServerAddress_button);
            this.Controls.Add(this.out_textBox);
            this.Controls.Add(this.encrypt_button);
            this.Controls.Add(this.in_textBox);
            this.Controls.Add(this.serverAddress_textBox);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FogLinkForm";
            this.Text = "FogLink";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox serverAddress_textBox;
        private System.Windows.Forms.TextBox in_textBox;
        private System.Windows.Forms.Button encrypt_button;
        private System.Windows.Forms.TextBox out_textBox;
        private Button saveServerAddress_button;
        private Label label1;
        private Label label2;
    }
}