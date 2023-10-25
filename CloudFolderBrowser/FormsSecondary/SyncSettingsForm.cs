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
