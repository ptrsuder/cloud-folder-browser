using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudFolderBrowser.FormsSecondary
{
    public partial class SyncSettingsForm : Form
    {
        SyncFilesForm ParentForm;
        public SyncSettingsForm(SyncFilesForm parentForm)
        {
            ParentForm = parentForm;

            InitializeComponent();

            overwriteMode_comboBox.DataSource = new BindingSource(overwriteModes, null);
            overwriteMode_comboBox.DisplayMember = "Value";
            overwriteMode_comboBox.ValueMember = "Key";

            StartPosition = FormStartPosition.CenterParent;

            toolTip1.SetToolTip(checkDownloadedFileSize_checkBox, "Check file size on disk and in cloud if checked. Redownload if mismatch");
            toolTip1.SetToolTip(checkFileSizeError_numericUpDown, "Margin of error between file sizes. Value of 1.0 means sizes should be identical");
            toolTip1.SetToolTip(retryDelay_numericUpDown, "Delay in ms between download retries");
            toolTip1.SetToolTip(maxDownloadRetries_numericUpDown, "Maximum amount of download retries before file is skipped");
            toolTip1.SetToolTip(folderNewFiles_checkBox, "Will download files into separate <New Files DATE> folder");
            toolTip1.SetToolTip(overwriteMode_comboBox, "Default behavior if file with same name already exists on disk");
        }

        Dictionary<int, string> overwriteModes = new Dictionary<int, string>()
        {
            {0, "None" },
            {1, "Overwrite all" },
            {2, "Overwrite older"},
            {3, "Ask" }
        };

        private void maximumDownloads_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            
            Properties.Settings.Default.Save();
        }

        private void overwriteMode_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Properties.Settings.Default.Save();
        }

        private void folderNewFiles_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            
            Properties.Settings.Default.Save();
        }

        private void maxDownloadRetries_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
           
            Properties.Settings.Default.Save();
        }

        private void retryDelay_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            
            Properties.Settings.Default.Save();
        }

        private void checkFileSizeError_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            
            Properties.Settings.Default.Save();
        }

        private void checkDownloadedFileSize_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            
            Properties.Settings.Default.Save();
        }

        private void SyncSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.maximumDownloads = (int)maximumDownloads_numericUpDown.Value;
            Properties.Settings.Default.overwriteMode = (int)overwriteMode_comboBox.SelectedIndex;
            Properties.Settings.Default.folderNewFiles = folderNewFiles_checkBox.Checked;
            Properties.Settings.Default.retryMax = (int)maxDownloadRetries_numericUpDown.Value;
            Properties.Settings.Default.checkFileSizeError = (double)checkFileSizeError_numericUpDown.Value;
            Properties.Settings.Default.checkDownloadedFileSize = checkDownloadedFileSize_checkBox.Checked;
            Properties.Settings.Default.retryDelay = (int)retryDelay_numericUpDown.Value;
            ParentForm.UpdateSettings();
        }

        private void SyncSettingsForm_Load(object sender, EventArgs e)
        {
            overwriteMode_comboBox.SelectedIndex = Properties.Settings.Default.overwriteMode;
            maximumDownloads_numericUpDown.Value = Properties.Settings.Default.maximumDownloads;

            checkDownloadedFileSize_checkBox.Checked = Properties.Settings.Default.checkDownloadedFileSize;
            checkFileSizeError_numericUpDown.Value = (decimal)Properties.Settings.Default.checkFileSizeError;
            maxDownloadRetries_numericUpDown.Value = Properties.Settings.Default.maximumDownloads;
            retryDelay_numericUpDown.Value = Properties.Settings.Default.retryDelay;

            folderNewFiles_checkBox.Checked = Properties.Settings.Default.folderNewFiles;
        }
    }
}
