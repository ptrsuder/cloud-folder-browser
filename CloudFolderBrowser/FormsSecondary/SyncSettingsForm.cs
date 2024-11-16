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

            overwriteMode_comboBox.SelectedIndex = ParentForm.OverwriteMode;
            maximumDownloads_numericUpDown.Value = ParentForm.MaximumDownloads;

            checkFileSizeError_numericUpDown.Value = (decimal)ParentForm.CheckFileSizeError;
            maxDownloadRetries_numericUpDown.Value = ParentForm.RetryMax;
            retryDelay_numericUpDown.Value = ParentForm.RetryDelay;

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
            ParentForm.MaximumDownloads = (int)maximumDownloads_numericUpDown.Value;
        }

        private void overwriteMode_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParentForm.OverwriteMode = (int)overwriteMode_comboBox.SelectedIndex;
        }

        private void folderNewFiles_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            ParentForm.FolderNewFiles = folderNewFiles_checkBox.Checked;
        }

        private void maxDownloadRetries_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ParentForm.RetryMax = (int)maxDownloadRetries_numericUpDown.Value;
        }

        private void retryDelay_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ParentForm.RetryDelay = (int)retryDelay_numericUpDown.Value;
        }

        private void checkFileSizeError_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ParentForm.CheckFileSizeError = (double)checkFileSizeError_numericUpDown.Value;
        }

        private void checkDownloadedFileSize_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            ParentForm.CheckDownloadedFileSize = checkDownloadedFileSize_checkBox.Checked;
        }
    }
}
