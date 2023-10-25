namespace CloudFolderBrowser.FormsSecondary
{
    partial class SyncSettingsForm
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
            checkDownloadedFileSize_checkBox = new CheckBox();
            maxDownloadRetries_numericUpDown = new NumericUpDown();
            folderNewFiles_checkBox = new CheckBox();
            overwriteMode_comboBox = new ComboBox();
            maximumDownloads_numericUpDown = new NumericUpDown();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            retryDelay_numericUpDown = new NumericUpDown();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            groupBox5 = new GroupBox();
            checkFileSizeError_numericUpDown = new NumericUpDown();
            groupBox6 = new GroupBox();
            groupBox7 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)maxDownloadRetries_numericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maximumDownloads_numericUpDown).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)retryDelay_numericUpDown).BeginInit();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)checkFileSizeError_numericUpDown).BeginInit();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            SuspendLayout();
            // 
            // checkDownloadedFileSize_checkBox
            // 
            checkDownloadedFileSize_checkBox.AutoSize = true;
            checkDownloadedFileSize_checkBox.FlatStyle = FlatStyle.Flat;
            checkDownloadedFileSize_checkBox.Location = new Point(26, 92);
            checkDownloadedFileSize_checkBox.Name = "checkDownloadedFileSize_checkBox";
            checkDownloadedFileSize_checkBox.Size = new Size(161, 19);
            checkDownloadedFileSize_checkBox.TabIndex = 0;
            checkDownloadedFileSize_checkBox.Text = "CheckDownloadedFileSize";
            checkDownloadedFileSize_checkBox.UseVisualStyleBackColor = true;
            checkDownloadedFileSize_checkBox.CheckedChanged += checkDownloadedFileSize_checkBox_CheckedChanged;
            // 
            // maxDownloadRetries_numericUpDown
            // 
            maxDownloadRetries_numericUpDown.Location = new Point(6, 22);
            maxDownloadRetries_numericUpDown.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            maxDownloadRetries_numericUpDown.Name = "maxDownloadRetries_numericUpDown";
            maxDownloadRetries_numericUpDown.Size = new Size(120, 23);
            maxDownloadRetries_numericUpDown.TabIndex = 1;
            maxDownloadRetries_numericUpDown.ValueChanged += maxDownloadRetries_numericUpDown_ValueChanged;
            // 
            // folderNewFiles_checkBox
            // 
            folderNewFiles_checkBox.FlatStyle = FlatStyle.Flat;
            folderNewFiles_checkBox.Location = new Point(252, 167);
            folderNewFiles_checkBox.Margin = new Padding(4, 3, 4, 3);
            folderNewFiles_checkBox.Name = "folderNewFiles_checkBox";
            folderNewFiles_checkBox.Size = new Size(188, 19);
            folderNewFiles_checkBox.TabIndex = 23;
            folderNewFiles_checkBox.Text = "Download to \"New Files\" folder";
            folderNewFiles_checkBox.UseVisualStyleBackColor = true;
            folderNewFiles_checkBox.CheckedChanged += folderNewFiles_checkBox_CheckedChanged;
            // 
            // overwriteMode_comboBox
            // 
            overwriteMode_comboBox.FlatStyle = FlatStyle.Flat;
            overwriteMode_comboBox.FormattingEnabled = true;
            overwriteMode_comboBox.Location = new Point(7, 23);
            overwriteMode_comboBox.Margin = new Padding(4, 3, 4, 3);
            overwriteMode_comboBox.Name = "overwriteMode_comboBox";
            overwriteMode_comboBox.Size = new Size(151, 23);
            overwriteMode_comboBox.TabIndex = 26;
            overwriteMode_comboBox.SelectedIndexChanged += overwriteMode_comboBox_SelectedIndexChanged;
            // 
            // maximumDownloads_numericUpDown
            // 
            maximumDownloads_numericUpDown.BorderStyle = BorderStyle.FixedSingle;
            maximumDownloads_numericUpDown.Location = new Point(7, 22);
            maximumDownloads_numericUpDown.Margin = new Padding(4, 3, 4, 3);
            maximumDownloads_numericUpDown.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            maximumDownloads_numericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            maximumDownloads_numericUpDown.Name = "maximumDownloads_numericUpDown";
            maximumDownloads_numericUpDown.Size = new Size(151, 23);
            maximumDownloads_numericUpDown.TabIndex = 24;
            maximumDownloads_numericUpDown.Value = new decimal(new int[] { 4, 0, 0, 0 });
            maximumDownloads_numericUpDown.ValueChanged += maximumDownloads_numericUpDown_ValueChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(maxDownloadRetries_numericUpDown);
            groupBox1.Location = new Point(19, 27);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(145, 58);
            groupBox1.TabIndex = 28;
            groupBox1.TabStop = false;
            groupBox1.Text = "MaxDownloadRetries";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(retryDelay_numericUpDown);
            groupBox2.Location = new Point(19, 91);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(145, 58);
            groupBox2.TabIndex = 28;
            groupBox2.TabStop = false;
            groupBox2.Text = "RetryDelay";
            // 
            // retryDelay_numericUpDown
            // 
            retryDelay_numericUpDown.Location = new Point(6, 22);
            retryDelay_numericUpDown.Maximum = new decimal(new int[] { 900, 0, 0, 0 });
            retryDelay_numericUpDown.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            retryDelay_numericUpDown.Name = "retryDelay_numericUpDown";
            retryDelay_numericUpDown.Size = new Size(120, 23);
            retryDelay_numericUpDown.TabIndex = 1;
            retryDelay_numericUpDown.Value = new decimal(new int[] { 100, 0, 0, 0 });
            retryDelay_numericUpDown.ValueChanged += retryDelay_numericUpDown_ValueChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(overwriteMode_comboBox);
            groupBox3.Location = new Point(245, 96);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(170, 65);
            groupBox3.TabIndex = 29;
            groupBox3.TabStop = false;
            groupBox3.Text = "File Overwrite";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(maximumDownloads_numericUpDown);
            groupBox4.Location = new Point(245, 27);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(170, 58);
            groupBox4.TabIndex = 28;
            groupBox4.TabStop = false;
            groupBox4.Text = "Max concurrent downloads";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(checkFileSizeError_numericUpDown);
            groupBox5.Location = new Point(20, 28);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(145, 58);
            groupBox5.TabIndex = 28;
            groupBox5.TabStop = false;
            groupBox5.Text = "CheckFileSizeError";
            // 
            // checkFileSizeError_numericUpDown
            // 
            checkFileSizeError_numericUpDown.DecimalPlaces = 4;
            checkFileSizeError_numericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            checkFileSizeError_numericUpDown.Location = new Point(6, 22);
            checkFileSizeError_numericUpDown.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            checkFileSizeError_numericUpDown.Minimum = new decimal(new int[] { 99, 0, 0, 131072 });
            checkFileSizeError_numericUpDown.Name = "checkFileSizeError_numericUpDown";
            checkFileSizeError_numericUpDown.Size = new Size(120, 23);
            checkFileSizeError_numericUpDown.TabIndex = 1;
            checkFileSizeError_numericUpDown.Value = new decimal(new int[] { 999, 0, 0, 196608 });
            checkFileSizeError_numericUpDown.ValueChanged += checkFileSizeError_numericUpDown_ValueChanged;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(groupBox1);
            groupBox6.Controls.Add(groupBox2);
            groupBox6.Location = new Point(13, 181);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(213, 166);
            groupBox6.TabIndex = 30;
            groupBox6.TabStop = false;
            groupBox6.Text = "Download Retries";
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(groupBox5);
            groupBox7.Controls.Add(checkDownloadedFileSize_checkBox);
            groupBox7.Location = new Point(12, 27);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(214, 134);
            groupBox7.TabIndex = 31;
            groupBox7.TabStop = false;
            groupBox7.Text = "Filesize Check";
            // 
            // SyncSettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(526, 382);
            Controls.Add(groupBox7);
            Controls.Add(groupBox6);
            Controls.Add(groupBox3);
            Controls.Add(groupBox4);
            Controls.Add(folderNewFiles_checkBox);
            Name = "SyncSettingsForm";
            Text = "Sync Settings";
            ((System.ComponentModel.ISupportInitialize)maxDownloadRetries_numericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)maximumDownloads_numericUpDown).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)retryDelay_numericUpDown).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)checkFileSizeError_numericUpDown).EndInit();
            groupBox6.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private CheckBox checkDownloadedFileSize_checkBox;
        private NumericUpDown maxDownloadRetries_numericUpDown;
        private CheckBox folderNewFiles_checkBox;
        private ComboBox overwriteMode_comboBox;
        private NumericUpDown maximumDownloads_numericUpDown;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private NumericUpDown retryDelay_numericUpDown;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private NumericUpDown checkFileSizeError_numericUpDown;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
    }
}