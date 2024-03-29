﻿namespace CloudFolderBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncFilesForm));
            this.importMega_button = new System.Windows.Forms.Button();
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
            this.DownloadProgress_label = new System.Windows.Forms.Label();
            this.maximumDownloads_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.stopDownload_button = new System.Windows.Forms.Button();
            this.overwriteMode_comboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.newFilesTreeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
            this.name_newTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.created_newTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.modified_newTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.size_newTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeCheckBox2 = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.nodeStateIcon2 = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
            this.nodeTextBox9 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox10 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox11 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox12 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.folderNewFiles_checkBox = new System.Windows.Forms.CheckBox();
            this.filter_textBox = new CloudFolderBrowser.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.maximumDownloads_numericUpDown)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // importMega_button
            // 
            this.importMega_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.importMega_button.Enabled = false;
            this.importMega_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.importMega_button.Location = new System.Drawing.Point(882, 179);
            this.importMega_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.importMega_button.Name = "importMega_button";
            this.importMega_button.Size = new System.Drawing.Size(303, 46);
            this.importMega_button.TabIndex = 1;
            this.importMega_button.Text = "Import to MEGA";
            this.importMega_button.UseVisualStyleBackColor = true;
            this.importMega_button.Click += new System.EventHandler(this.importMega_button_Click);
            // 
            // flatList2_checkBox
            // 
            this.flatList2_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flatList2_checkBox.AutoSize = true;
            this.flatList2_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.flatList2_checkBox.Location = new System.Drawing.Point(189, 615);
            this.flatList2_checkBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.flatList2_checkBox.Name = "flatList2_checkBox";
            this.flatList2_checkBox.Size = new System.Drawing.Size(60, 19);
            this.flatList2_checkBox.TabIndex = 2;
            this.flatList2_checkBox.Text = "Flat list";
            this.flatList2_checkBox.UseVisualStyleBackColor = true;
            this.flatList2_checkBox.CheckedChanged += new System.EventHandler(this.flatList2_checkBox_CheckedChanged);
            // 
            // getJdLinks_button
            // 
            this.getJdLinks_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.getJdLinks_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.getJdLinks_button.Location = new System.Drawing.Point(882, 35);
            this.getJdLinks_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.getJdLinks_button.Name = "getJdLinks_button";
            this.getJdLinks_button.Size = new System.Drawing.Size(303, 73);
            this.getJdLinks_button.TabIndex = 3;
            this.getJdLinks_button.Text = "Generate JDownloader links";
            this.getJdLinks_button.UseVisualStyleBackColor = true;
            this.getJdLinks_button.Click += new System.EventHandler(this.getJdLinks_button_Click);
            // 
            // downloadFiles_button
            // 
            this.downloadFiles_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadFiles_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downloadFiles_button.Location = new System.Drawing.Point(882, 232);
            this.downloadFiles_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.downloadFiles_button.Name = "downloadFiles_button";
            this.downloadFiles_button.Size = new System.Drawing.Size(303, 46);
            this.downloadFiles_button.TabIndex = 4;
            this.downloadFiles_button.Text = "Download";
            this.downloadFiles_button.UseVisualStyleBackColor = true;
            this.downloadFiles_button.Click += new System.EventHandler(this.downloadFiles_button_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(882, 435);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(303, 27);
            this.progressBar1.TabIndex = 5;
            // 
            // progressBar2
            // 
            this.progressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar2.Location = new System.Drawing.Point(882, 483);
            this.progressBar2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(303, 27);
            this.progressBar2.TabIndex = 6;
            // 
            // progressBar3
            // 
            this.progressBar3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar3.Location = new System.Drawing.Point(882, 528);
            this.progressBar3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(303, 27);
            this.progressBar3.TabIndex = 7;
            // 
            // progressBar4
            // 
            this.progressBar4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar4.Location = new System.Drawing.Point(882, 573);
            this.progressBar4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.progressBar4.Name = "progressBar4";
            this.progressBar4.Size = new System.Drawing.Size(303, 27);
            this.progressBar4.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(884, 417);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "label1";
            this.toolTip1.SetToolTip(this.label1, "$\"{label1.Text}\"");
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(883, 465);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(883, 512);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(883, 557);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // DownloadProgress_label
            // 
            this.DownloadProgress_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadProgress_label.AutoSize = true;
            this.DownloadProgress_label.Location = new System.Drawing.Point(883, 387);
            this.DownloadProgress_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DownloadProgress_label.Name = "DownloadProgress_label";
            this.DownloadProgress_label.Size = new System.Drawing.Size(106, 15);
            this.DownloadProgress_label.TabIndex = 14;
            this.DownloadProgress_label.Text = "DownloadProgress";
            this.DownloadProgress_label.Visible = false;
            // 
            // maximumDownloads_numericUpDown
            // 
            this.maximumDownloads_numericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maximumDownloads_numericUpDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maximumDownloads_numericUpDown.Location = new System.Drawing.Point(1144, 357);
            this.maximumDownloads_numericUpDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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
            this.maximumDownloads_numericUpDown.Size = new System.Drawing.Size(41, 23);
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
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(976, 360);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Maximum concurrent downloads";
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 200;
            this.toolTip1.AutoPopDelay = 2000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 40;
            // 
            // stopDownload_button
            // 
            this.stopDownload_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stopDownload_button.Enabled = false;
            this.stopDownload_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopDownload_button.Location = new System.Drawing.Point(882, 607);
            this.stopDownload_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.stopDownload_button.Name = "stopDownload_button";
            this.stopDownload_button.Size = new System.Drawing.Size(303, 30);
            this.stopDownload_button.TabIndex = 18;
            this.stopDownload_button.Text = "Cancel downloads";
            this.stopDownload_button.UseVisualStyleBackColor = true;
            this.stopDownload_button.Visible = false;
            this.stopDownload_button.Click += new System.EventHandler(this.stopDownloads_Click);
            // 
            // overwriteMode_comboBox
            // 
            this.overwriteMode_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.overwriteMode_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.overwriteMode_comboBox.FormattingEnabled = true;
            this.overwriteMode_comboBox.Location = new System.Drawing.Point(976, 322);
            this.overwriteMode_comboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.overwriteMode_comboBox.Name = "overwriteMode_comboBox";
            this.overwriteMode_comboBox.Size = new System.Drawing.Size(208, 23);
            this.overwriteMode_comboBox.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(878, 325);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 15);
            this.label6.TabIndex = 21;
            this.label6.Text = "Overwrite mode";
            // 
            // newFilesTreeViewAdv
            // 
            this.newFilesTreeViewAdv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newFilesTreeViewAdv.BackColor = System.Drawing.SystemColors.Window;
            this.newFilesTreeViewAdv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.newFilesTreeViewAdv.ColumnHeaderHeight = 17;
            this.newFilesTreeViewAdv.Columns.Add(this.name_newTreeColumn);
            this.newFilesTreeViewAdv.Columns.Add(this.created_newTreeColumn);
            this.newFilesTreeViewAdv.Columns.Add(this.modified_newTreeColumn);
            this.newFilesTreeViewAdv.Columns.Add(this.size_newTreeColumn);
            this.newFilesTreeViewAdv.ContextMenuStrip = this.contextMenuStrip1;
            this.newFilesTreeViewAdv.DefaultToolTipProvider = null;
            this.newFilesTreeViewAdv.DragDropMarkColor = System.Drawing.Color.Black;
            this.newFilesTreeViewAdv.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.newFilesTreeViewAdv.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.newFilesTreeViewAdv.LineColor = System.Drawing.SystemColors.ControlDark;
            this.newFilesTreeViewAdv.Location = new System.Drawing.Point(15, 35);
            this.newFilesTreeViewAdv.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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
            this.newFilesTreeViewAdv.Size = new System.Drawing.Size(856, 565);
            this.newFilesTreeViewAdv.TabIndex = 0;
            this.newFilesTreeViewAdv.Text = "treeViewAdv1";
            this.newFilesTreeViewAdv.UseColumns = true;
            this.newFilesTreeViewAdv.ColumnClicked += new System.EventHandler<Aga.Controls.Tree.TreeColumnEventArgs>(this.treeViewAdv_ColumnClicked);
            this.newFilesTreeViewAdv.Collapsed += new System.EventHandler<Aga.Controls.Tree.TreeViewAdvEventArgs>(this.treeViewAdv_Collapsed);
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
            this.created_newTreeColumn.Width = 65;
            // 
            // modified_newTreeColumn
            // 
            this.modified_newTreeColumn.Header = "Modified";
            this.modified_newTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.modified_newTreeColumn.TooltipText = null;
            this.modified_newTreeColumn.Width = 65;
            // 
            // size_newTreeColumn
            // 
            this.size_newTreeColumn.Header = "Size";
            this.size_newTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.size_newTreeColumn.TooltipText = null;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllToolStripMenuItem,
            this.checkNoneToolStripMenuItem,
            this.expandAllToolStripMenuItem,
            this.collapseAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(138, 92);
            // 
            // checkAllToolStripMenuItem
            // 
            this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
            this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.checkAllToolStripMenuItem.Text = "Check all";
            // 
            // checkNoneToolStripMenuItem
            // 
            this.checkNoneToolStripMenuItem.Name = "checkNoneToolStripMenuItem";
            this.checkNoneToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.checkNoneToolStripMenuItem.Text = "Check none";
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.expandAllToolStripMenuItem.Text = "Expand all";
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.collapseAllToolStripMenuItem.Text = "Collapse all";
            // 
            // nodeCheckBox2
            // 
            this.nodeCheckBox2.DataPropertyName = "CheckState";
            this.nodeCheckBox2.EditEnabled = true;
            this.nodeCheckBox2.LeftMargin = 0;
            this.nodeCheckBox2.ParentColumn = this.name_newTreeColumn;
            this.nodeCheckBox2.ThreeState = true;
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
            // folderNewFiles_checkBox
            // 
            this.folderNewFiles_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.folderNewFiles_checkBox.AutoSize = true;
            this.folderNewFiles_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.folderNewFiles_checkBox.Location = new System.Drawing.Point(998, 295);
            this.folderNewFiles_checkBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.folderNewFiles_checkBox.Name = "folderNewFiles_checkBox";
            this.folderNewFiles_checkBox.Size = new System.Drawing.Size(188, 19);
            this.folderNewFiles_checkBox.TabIndex = 22;
            this.folderNewFiles_checkBox.Text = "Download to \"New Files\" folder";
            this.folderNewFiles_checkBox.UseVisualStyleBackColor = true;
            // 
            // filter_textBox
            // 
            this.filter_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.filter_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filter_textBox.Location = new System.Drawing.Point(15, 610);
            this.filter_textBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.filter_textBox.Name = "filter_textBox";
            this.filter_textBox.Size = new System.Drawing.Size(166, 23);
            this.filter_textBox.TabIndex = 19;
            this.filter_textBox.TextChangedCompleteDelay = System.TimeSpan.Parse("00:00:00.6000000");
            // 
            // SyncFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 642);
            this.Controls.Add(this.folderNewFiles_checkBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.overwriteMode_comboBox);
            this.Controls.Add(this.filter_textBox);
            this.Controls.Add(this.stopDownload_button);
            this.Controls.Add(this.importMega_button);
            this.Controls.Add(this.getJdLinks_button);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.maximumDownloads_numericUpDown);
            this.Controls.Add(this.DownloadProgress_label);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(506, 675);
            this.Name = "SyncFilesForm";
            this.Text = "Download files";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.syncFilesForm2_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.maximumDownloads_numericUpDown)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.Button importMega_button;
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
        private System.Windows.Forms.Label DownloadProgress_label;
        private System.Windows.Forms.NumericUpDown maximumDownloads_numericUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button stopDownload_button;
        private CloudFolderBrowser.TextBox filter_textBox;
        private System.Windows.Forms.ComboBox overwriteMode_comboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox folderNewFiles_checkBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkNoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
    }
}