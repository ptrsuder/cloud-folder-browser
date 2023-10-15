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
            in_textBox = new System.Windows.Forms.TextBox();
            encrypt_button = new Button();
            out_textBox = new System.Windows.Forms.TextBox();
            serverAddress_textBox = new TextBox();
            saveServerAddress_button = new Button();
            label1 = new Label();
            label2 = new Label();
            progressBar1 = new ProgressBar();
            SuspendLayout();
            // 
            // in_textBox
            // 
            in_textBox.Location = new Point(13, 95);
            in_textBox.Margin = new Padding(4, 3, 4, 3);
            in_textBox.Name = "in_textBox";
            in_textBox.Size = new Size(866, 23);
            in_textBox.TabIndex = 1;
            // 
            // encrypt_button
            // 
            encrypt_button.Location = new Point(12, 155);
            encrypt_button.Margin = new Padding(4, 3, 4, 3);
            encrypt_button.Name = "encrypt_button";
            encrypt_button.Size = new Size(88, 27);
            encrypt_button.TabIndex = 2;
            encrypt_button.Text = "encrypt";
            encrypt_button.UseVisualStyleBackColor = true;
            encrypt_button.Click += encrypt_button_Click;
            // 
            // out_textBox
            // 
            out_textBox.Location = new Point(13, 125);
            out_textBox.Margin = new Padding(4, 3, 4, 3);
            out_textBox.Name = "out_textBox";
            out_textBox.ReadOnly = true;
            out_textBox.Size = new Size(866, 23);
            out_textBox.TabIndex = 4;
            // 
            // serverAddress_textBox
            // 
            serverAddress_textBox.Location = new Point(13, 32);
            serverAddress_textBox.Margin = new Padding(4, 3, 4, 3);
            serverAddress_textBox.Name = "serverAddress_textBox";
            serverAddress_textBox.Size = new Size(414, 23);
            serverAddress_textBox.TabIndex = 0;
            serverAddress_textBox.TextChangedCompleteDelay = TimeSpan.Parse("00:00:00.6000000");
            // 
            // saveServerAddress_button
            // 
            saveServerAddress_button.Location = new Point(434, 32);
            saveServerAddress_button.Name = "saveServerAddress_button";
            saveServerAddress_button.Size = new Size(75, 23);
            saveServerAddress_button.TabIndex = 5;
            saveServerAddress_button.Text = "save";
            saveServerAddress_button.UseVisualStyleBackColor = true;
            saveServerAddress_button.Click += saveServerAddress_button_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 14);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 6;
            label1.Text = "FogLink server adress";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 77);
            label2.Name = "label2";
            label2.Size = new Size(88, 15);
            label2.TabIndex = 6;
            label2.Text = "URL encryption";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(107, 155);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(772, 27);
            progressBar1.TabIndex = 7;
            // 
            // FogLinkForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(898, 193);
            Controls.Add(progressBar1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(saveServerAddress_button);
            Controls.Add(out_textBox);
            Controls.Add(encrypt_button);
            Controls.Add(in_textBox);
            Controls.Add(serverAddress_textBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "FogLinkForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FogLink";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox serverAddress_textBox;
        private System.Windows.Forms.TextBox in_textBox;
        private Button encrypt_button;
        private System.Windows.Forms.TextBox out_textBox;
        private Button saveServerAddress_button;
        private Label label1;
        private Label label2;
        private ProgressBar progressBar1;
    }
}