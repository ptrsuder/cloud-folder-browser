namespace CloudFolderBrowser
{
    partial class SyncFilesForm
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
            this.components = new System.ComponentModel.Container();
            this.newFilesTreeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
            this.name_newTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.created_newTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.modified_newTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.size_newTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.nodeCheckBox2 = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.nodeStateIcon2 = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
            this.nodeTextBox9 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox10 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox11 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox12 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.addFilesToYadisk_button = new System.Windows.Forms.Button();
            this.flatList2_checkBox = new System.Windows.Forms.CheckBox();
            this.getJdLinks_button = new System.Windows.Forms.Button();
            this.downloadFiles_button = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.progressBar4 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.downloadMega_button = new System.Windows.Forms.Button();
            this.DownloadProgress_label = new System.Windows.Forms.Label();
            this.maximumDownloads_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openDestFolder_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.maximumDownloads_numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // newFilesTreeViewAdv
            // 
            this.newFilesTreeViewAdv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newFilesTreeViewAdv.BackColor = System.Drawing.SystemColors.Window;
            this.newFilesTreeViewAdv.Columns.Add(this.name_newTreeColumn);
            this.newFilesTreeViewAdv.Columns.Add(this.created_newTreeColumn);
            this.newFilesTreeViewAdv.Columns.Add(this.modified_newTreeColumn);
            this.newFilesTreeViewAdv.Columns.Add(this.size_newTreeColumn);
            this.newFilesTreeViewAdv.DefaultToolTipProvider = null;
            this.newFilesTreeViewAdv.DragDropMarkColor = System.Drawing.Color.Black;
            this.newFilesTreeViewAdv.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.newFilesTreeViewAdv.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.newFilesTreeViewAdv.LineColor = System.Drawing.SystemColors.ControlDark;
            this.newFilesTreeViewAdv.Location = new System.Drawing.Point(13, 30);
            this.newFilesTreeViewAdv.Model = null;
            this.newFilesTreeViewAdv.Name = "newFilesTreeViewAdv";
            this.newFilesTreeViewAdv.NodeControls.Add(this.nodeCheckBox2);
            this.newFilesTreeViewAdv.NodeControls.Add(this.nodeStateIcon2);
            this.newFilesTreeViewAdv.NodeControls.Add(this.nodeTextBox9);
            this.newFilesTreeViewAdv.NodeControls.Add(this.nodeTextBox10);
            this.newFilesTreeViewAdv.NodeControls.Add(this.nodeTextBox11);
            this.newFilesTreeViewAdv.NodeControls.Add(this.nodeTextBox12);
            this.newFilesTreeViewAdv.NodeFilter = null;
            this.newFilesTreeViewAdv.SelectedNode = null;
            this.newFilesTreeViewAdv.Size = new System.Drawing.Size(510, 490);
            this.newFilesTreeViewAdv.TabIndex = 0;
            this.newFilesTreeViewAdv.Text = "treeViewAdv1";
            this.newFilesTreeViewAdv.UseColumns = true;
            this.newFilesTreeViewAdv.ColumnClicked += new System.EventHandler<Aga.Controls.Tree.TreeColumnEventArgs>(this.treeViewAdv_ColumnClicked);
            this.newFilesTreeViewAdv.Collapsed += new System.EventHandler<Aga.Controls.Tree.TreeViewAdvEventArgs>(this.treeViewAdv_Expanded);
            this.newFilesTreeViewAdv.Expanded += new System.EventHandler<Aga.Controls.Tree.TreeViewAdvEventArgs>(this.treeViewAdv_Expanded);
            // 
            // name_newTreeColumn
            // 
            this.name_newTreeColumn.Header = "Name";
            this.name_newTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.name_newTreeColumn.TooltipText = null;
            // 
            // created_newTreeColumn
            // 
            this.created_newTreeColumn.Header = "Created";
            this.created_newTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.created_newTreeColumn.TooltipText = null;
            // 
            // modified_newTreeColumn
            // 
            this.modified_newTreeColumn.Header = "Modified";
            this.modified_newTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.modified_newTreeColumn.TooltipText = null;
            // 
            // size_newTreeColumn
            // 
            this.size_newTreeColumn.Header = "Size";
            this.size_newTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.size_newTreeColumn.TooltipText = null;
            // 
            // nodeCheckBox2
            // 
            this.nodeCheckBox2.DataPropertyName = "CheckState";
            this.nodeCheckBox2.EditEnabled = true;
            this.nodeCheckBox2.LeftMargin = 0;
            this.nodeCheckBox2.ParentColumn = this.name_newTreeColumn;
            // 
            // nodeStateIcon2
            // 
            this.nodeStateIcon2.DataPropertyName = "StateIcon";
            this.nodeStateIcon2.LeftMargin = 1;
            this.nodeStateIcon2.ParentColumn = this.name_newTreeColumn;
            this.nodeStateIcon2.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
            // 
            // nodeTextBox9
            // 
            this.nodeTextBox9.DataPropertyName = "NodeControl1";
            this.nodeTextBox9.IncrementalSearchEnabled = true;
            this.nodeTextBox9.LeftMargin = 3;
            this.nodeTextBox9.ParentColumn = this.name_newTreeColumn;
            // 
            // nodeTextBox10
            // 
            this.nodeTextBox10.DataPropertyName = "NodeControl2";
            this.nodeTextBox10.IncrementalSearchEnabled = true;
            this.nodeTextBox10.LeftMargin = 3;
            this.nodeTextBox10.ParentColumn = this.created_newTreeColumn;
            // 
            // nodeTextBox11
            // 
            this.nodeTextBox11.DataPropertyName = "NodeControl3";
            this.nodeTextBox11.IncrementalSearchEnabled = true;
            this.nodeTextBox11.LeftMargin = 3;
            this.nodeTextBox11.ParentColumn = this.modified_newTreeColumn;
            // 
            // nodeTextBox12
            // 
            this.nodeTextBox12.DataPropertyName = "NodeControl4";
            this.nodeTextBox12.IncrementalSearchEnabled = true;
            this.nodeTextBox12.LeftMargin = 3;
            this.nodeTextBox12.ParentColumn = this.size_newTreeColumn;
            // 
            // addFilesToYadisk_button
            // 
            this.addFilesToYadisk_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addFilesToYadisk_button.Enabled = false;
            this.addFilesToYadisk_button.Location = new System.Drawing.Point(528, 121);
            this.addFilesToYadisk_button.Name = "addFilesToYadisk_button";
            this.addFilesToYadisk_button.Size = new System.Drawing.Size(176, 73);
            this.addFilesToYadisk_button.TabIndex = 1;
            this.addFilesToYadisk_button.Text = "Add checked to YaDisk";
            this.addFilesToYadisk_button.UseVisualStyleBackColor = true;
            this.addFilesToYadisk_button.Click += new System.EventHandler(this.getFiles_button_Click);
            // 
            // flatList2_checkBox
            // 
            this.flatList2_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flatList2_checkBox.AutoSize = true;
            this.flatList2_checkBox.Location = new System.Drawing.Point(13, 527);
            this.flatList2_checkBox.Name = "flatList2_checkBox";
            this.flatList2_checkBox.Size = new System.Drawing.Size(58, 17);
            this.flatList2_checkBox.TabIndex = 2;
            this.flatList2_checkBox.Text = "Flat list";
            this.flatList2_checkBox.UseVisualStyleBackColor = true;
            this.flatList2_checkBox.CheckedChanged += new System.EventHandler(this.flatList2_checkBox_CheckedChanged);
            // 
            // getJdLinks_button
            // 
            this.getJdLinks_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.getJdLinks_button.Location = new System.Drawing.Point(528, 30);
            this.getJdLinks_button.Name = "getJdLinks_button";
            this.getJdLinks_button.Size = new System.Drawing.Size(176, 85);
            this.getJdLinks_button.TabIndex = 3;
            this.getJdLinks_button.Text = "Get JDownloader links";
            this.getJdLinks_button.UseVisualStyleBackColor = true;
            this.getJdLinks_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // downloadFiles_button
            // 
            this.downloadFiles_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadFiles_button.Location = new System.Drawing.Point(528, 247);
            this.downloadFiles_button.Name = "downloadFiles_button";
            this.downloadFiles_button.Size = new System.Drawing.Size(173, 40);
            this.downloadFiles_button.TabIndex = 4;
            this.downloadFiles_button.Text = "Download";
            this.downloadFiles_button.UseVisualStyleBackColor = true;
            this.downloadFiles_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(528, 377);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(173, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // progressBar2
            // 
            this.progressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar2.Location = new System.Drawing.Point(528, 419);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(173, 23);
            this.progressBar2.TabIndex = 6;
            // 
            // progressBar3
            // 
            this.progressBar3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar3.Location = new System.Drawing.Point(528, 458);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(173, 23);
            this.progressBar3.TabIndex = 7;
            // 
            // progressBar4
            // 
            this.progressBar4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar4.Location = new System.Drawing.Point(528, 497);
            this.progressBar4.Name = "progressBar4";
            this.progressBar4.Size = new System.Drawing.Size(173, 23);
            this.progressBar4.TabIndex = 8;
            // 
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(528, 361);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "label1";
            this.toolTip1.SetToolTip(this.label1, "$\"{label1.Text}\"");
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(527, 403);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(527, 445);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(527, 484);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // downloadMega_button
            // 
            this.downloadMega_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadMega_button.Enabled = false;
            this.downloadMega_button.Location = new System.Drawing.Point(528, 200);
            this.downloadMega_button.Name = "downloadMega_button";
            this.downloadMega_button.Size = new System.Drawing.Size(173, 41);
            this.downloadMega_button.TabIndex = 13;
            this.downloadMega_button.Text = "Mega download";
            this.downloadMega_button.UseVisualStyleBackColor = true;
            this.downloadMega_button.Click += new System.EventHandler(this.downloadMega_button_Click);
            // 
            // DownloadProgress_label
            // 
            this.DownloadProgress_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadProgress_label.AutoSize = true;
            this.DownloadProgress_label.Location = new System.Drawing.Point(528, 331);
            this.DownloadProgress_label.Name = "DownloadProgress_label";
            this.DownloadProgress_label.Size = new System.Drawing.Size(96, 13);
            this.DownloadProgress_label.TabIndex = 14;
            this.DownloadProgress_label.Text = "DownloadProgress";
            this.DownloadProgress_label.Visible = false;
            // 
            // maximumDownloads_numericUpDown
            // 
            this.maximumDownloads_numericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maximumDownloads_numericUpDown.Location = new System.Drawing.Point(663, 295);
            this.maximumDownloads_numericUpDown.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.maximumDownloads_numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maximumDownloads_numericUpDown.Name = "maximumDownloads_numericUpDown";
            this.maximumDownloads_numericUpDown.Size = new System.Drawing.Size(35, 20);
            this.maximumDownloads_numericUpDown.TabIndex = 15;
            this.maximumDownloads_numericUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.maximumDownloads_numericUpDown.ValueChanged += new System.EventHandler(this.maximumDownloads_numericUpDown_ValueChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.26F);
            this.label5.Location = new System.Drawing.Point(527, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Maximum downloads";
            // 
            // openDestFolder_button
            // 
            this.openDestFolder_button.Enabled = false;
            this.openDestFolder_button.Location = new System.Drawing.Point(626, 527);
            this.openDestFolder_button.Name = "openDestFolder_button";
            this.openDestFolder_button.Size = new System.Drawing.Size(75, 23);
            this.openDestFolder_button.TabIndex = 17;
            this.openDestFolder_button.Text = "Open folder";
            this.openDestFolder_button.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.openDestFolder_button.UseVisualStyleBackColor = true;
            // 
            // SyncFilesForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 556);
            this.Controls.Add(this.openDestFolder_button);
            this.Controls.Add(this.addFilesToYadisk_button);
            this.Controls.Add(this.getJdLinks_button);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.maximumDownloads_numericUpDown);
            this.Controls.Add(this.DownloadProgress_label);
            this.Controls.Add(this.downloadMega_button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar4);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.downloadFiles_button);
            this.Controls.Add(this.flatList2_checkBox);
            this.Controls.Add(this.newFilesTreeViewAdv);
            this.MinimumSize = new System.Drawing.Size(436, 590);
            this.Name = "SyncFilesForm2";
            this.Text = "SyncFilesForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SyncFilesForm2_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.maximumDownloads_numericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Aga.Controls.Tree.TreeViewAdv newFilesTreeViewAdv;
        private Aga.Controls.Tree.TreeColumn name_newTreeColumn;
        private Aga.Controls.Tree.TreeColumn created_newTreeColumn;
        private Aga.Controls.Tree.TreeColumn modified_newTreeColumn;
        private Aga.Controls.Tree.TreeColumn size_newTreeColumn;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox9;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox nodeCheckBox2;
        private Aga.Controls.Tree.NodeControls.NodeStateIcon nodeStateIcon2;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox10;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox11;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox12;
        private System.Windows.Forms.Button addFilesToYadisk_button;
        private System.Windows.Forms.CheckBox flatList2_checkBox;
        private System.Windows.Forms.Button getJdLinks_button;
        private System.Windows.Forms.Button downloadFiles_button;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.ProgressBar progressBar4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button downloadMega_button;
        private System.Windows.Forms.Label DownloadProgress_label;
        private System.Windows.Forms.NumericUpDown maximumDownloads_numericUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button openDestFolder_button;
    }
}