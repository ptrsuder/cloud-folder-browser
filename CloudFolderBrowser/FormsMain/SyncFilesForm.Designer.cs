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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncFilesForm));
            importMega_button = new Button();
            flatList2_checkBox = new CheckBox();
            getJdLinks_button = new Button();
            downloadFiles_button = new Button();
            progressBar1 = new ProgressBar();
            progressBar2 = new ProgressBar();
            progressBar3 = new ProgressBar();
            progressBar4 = new ProgressBar();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            DownloadProgress_label = new Label();
            toolTip1 = new ToolTip(components);
            stopDownload_button = new Button();
            newFilesTreeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
            name_newTreeColumn = new Aga.Controls.Tree.TreeColumn();
            created_newTreeColumn = new Aga.Controls.Tree.TreeColumn();
            modified_newTreeColumn = new Aga.Controls.Tree.TreeColumn();
            size_newTreeColumn = new Aga.Controls.Tree.TreeColumn();
            contextMenuStrip1 = new ContextMenuStrip(components);
            checkAllToolStripMenuItem = new ToolStripMenuItem();
            checkNoneToolStripMenuItem = new ToolStripMenuItem();
            expandAllToolStripMenuItem = new ToolStripMenuItem();
            collapseAllToolStripMenuItem = new ToolStripMenuItem();
            nodeCheckBox2 = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            nodeStateIcon2 = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
            nodeTextBox9 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            nodeTextBox10 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            nodeTextBox11 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            nodeTextBox12 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            filter_textBox = new TextBox();
            menuStrip1 = new MenuStrip();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // importMega_button
            // 
            importMega_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            importMega_button.Enabled = false;
            importMega_button.FlatStyle = FlatStyle.Flat;
            importMega_button.Location = new Point(884, 94);
            importMega_button.Margin = new Padding(4, 3, 4, 3);
            importMega_button.Name = "importMega_button";
            importMega_button.Size = new Size(303, 46);
            importMega_button.TabIndex = 1;
            importMega_button.Text = "Import to MEGA";
            importMega_button.UseVisualStyleBackColor = true;
            importMega_button.Click += importMega_button_Click;
            // 
            // flatList2_checkBox
            // 
            flatList2_checkBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            flatList2_checkBox.AutoSize = true;
            flatList2_checkBox.FlatStyle = FlatStyle.Flat;
            flatList2_checkBox.Location = new Point(189, 611);
            flatList2_checkBox.Margin = new Padding(4, 3, 4, 3);
            flatList2_checkBox.Name = "flatList2_checkBox";
            flatList2_checkBox.Size = new Size(60, 19);
            flatList2_checkBox.TabIndex = 2;
            flatList2_checkBox.Text = "Flat list";
            flatList2_checkBox.UseVisualStyleBackColor = true;
            flatList2_checkBox.CheckedChanged += flatList2_checkBox_CheckedChanged;
            // 
            // getJdLinks_button
            // 
            getJdLinks_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            getJdLinks_button.FlatStyle = FlatStyle.Flat;
            getJdLinks_button.Location = new Point(884, 35);
            getJdLinks_button.Margin = new Padding(4, 3, 4, 3);
            getJdLinks_button.Name = "getJdLinks_button";
            getJdLinks_button.Size = new Size(303, 53);
            getJdLinks_button.TabIndex = 3;
            getJdLinks_button.Text = "Generate JDownloader links";
            getJdLinks_button.UseVisualStyleBackColor = true;
            getJdLinks_button.Click += getJdLinks_button_Click;
            // 
            // downloadFiles_button
            // 
            downloadFiles_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            downloadFiles_button.FlatStyle = FlatStyle.Flat;
            downloadFiles_button.Location = new Point(884, 147);
            downloadFiles_button.Margin = new Padding(4, 3, 4, 3);
            downloadFiles_button.Name = "downloadFiles_button";
            downloadFiles_button.Size = new Size(303, 46);
            downloadFiles_button.TabIndex = 4;
            downloadFiles_button.Text = "Download";
            downloadFiles_button.UseVisualStyleBackColor = true;
            downloadFiles_button.Click += downloadFiles_button_Click;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            progressBar1.Location = new Point(882, 435);
            progressBar1.Margin = new Padding(4, 3, 4, 3);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(303, 27);
            progressBar1.TabIndex = 5;
            // 
            // progressBar2
            // 
            progressBar2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            progressBar2.Location = new Point(882, 483);
            progressBar2.Margin = new Padding(4, 3, 4, 3);
            progressBar2.Name = "progressBar2";
            progressBar2.Size = new Size(303, 27);
            progressBar2.TabIndex = 6;
            // 
            // progressBar3
            // 
            progressBar3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            progressBar3.Location = new Point(882, 527);
            progressBar3.Margin = new Padding(4, 3, 4, 3);
            progressBar3.Name = "progressBar3";
            progressBar3.Size = new Size(303, 27);
            progressBar3.TabIndex = 7;
            // 
            // progressBar4
            // 
            progressBar4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            progressBar4.Location = new Point(882, 573);
            progressBar4.Margin = new Padding(4, 3, 4, 3);
            progressBar4.Name = "progressBar4";
            progressBar4.Size = new Size(303, 27);
            progressBar4.TabIndex = 8;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.Location = new Point(884, 417);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(301, 15);
            label1.TabIndex = 9;
            label1.Text = "label1";
            toolTip1.SetToolTip(label1, "$\"{label1.Text}\"");
            label1.Visible = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label2.Location = new Point(883, 465);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(302, 15);
            label2.TabIndex = 10;
            label2.Text = "label2";
            label2.Visible = false;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label3.Location = new Point(883, 512);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(302, 15);
            label3.TabIndex = 11;
            label3.Text = "label3";
            label3.Visible = false;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label4.Location = new Point(883, 557);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(302, 15);
            label4.TabIndex = 12;
            label4.Text = "label4";
            label4.Visible = false;
            // 
            // DownloadProgress_label
            // 
            DownloadProgress_label.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            DownloadProgress_label.AutoSize = true;
            DownloadProgress_label.Location = new Point(883, 387);
            DownloadProgress_label.Margin = new Padding(4, 0, 4, 0);
            DownloadProgress_label.Name = "DownloadProgress_label";
            DownloadProgress_label.Size = new Size(106, 15);
            DownloadProgress_label.TabIndex = 14;
            DownloadProgress_label.Text = "DownloadProgress";
            DownloadProgress_label.Visible = false;
            // 
            // toolTip1
            // 
            toolTip1.AutomaticDelay = 200;
            toolTip1.AutoPopDelay = 2000;
            toolTip1.InitialDelay = 200;
            toolTip1.IsBalloon = true;
            toolTip1.ReshowDelay = 40;
            // 
            // stopDownload_button
            // 
            stopDownload_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            stopDownload_button.Enabled = false;
            stopDownload_button.FlatStyle = FlatStyle.Flat;
            stopDownload_button.Location = new Point(882, 607);
            stopDownload_button.Margin = new Padding(4, 3, 4, 3);
            stopDownload_button.Name = "stopDownload_button";
            stopDownload_button.Size = new Size(303, 30);
            stopDownload_button.TabIndex = 18;
            stopDownload_button.Text = "Cancel downloads";
            stopDownload_button.UseVisualStyleBackColor = true;
            stopDownload_button.Visible = false;
            stopDownload_button.Click += stopDownloads_Click;
            // 
            // newFilesTreeViewAdv
            // 
            newFilesTreeViewAdv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            newFilesTreeViewAdv.BackColor = SystemColors.Window;
            newFilesTreeViewAdv.BorderStyle = BorderStyle.FixedSingle;
            newFilesTreeViewAdv.ColumnHeaderHeight = 17;
            newFilesTreeViewAdv.Columns.Add(name_newTreeColumn);
            newFilesTreeViewAdv.Columns.Add(created_newTreeColumn);
            newFilesTreeViewAdv.Columns.Add(modified_newTreeColumn);
            newFilesTreeViewAdv.Columns.Add(size_newTreeColumn);
            newFilesTreeViewAdv.ContextMenuStrip = contextMenuStrip1;
            newFilesTreeViewAdv.DefaultToolTipProvider = null;
            newFilesTreeViewAdv.DragDropMarkColor = Color.Black;
            newFilesTreeViewAdv.FullRowSelectActiveColor = Color.Empty;
            newFilesTreeViewAdv.FullRowSelectInactiveColor = Color.Empty;
            newFilesTreeViewAdv.LineColor = SystemColors.ControlDark;
            newFilesTreeViewAdv.Location = new Point(15, 35);
            newFilesTreeViewAdv.Margin = new Padding(4, 3, 4, 3);
            newFilesTreeViewAdv.Model = null;
            newFilesTreeViewAdv.Name = "newFilesTreeViewAdv";
            newFilesTreeViewAdv.NodeControls.Add(nodeCheckBox2);
            newFilesTreeViewAdv.NodeControls.Add(nodeStateIcon2);
            newFilesTreeViewAdv.NodeControls.Add(nodeTextBox9);
            newFilesTreeViewAdv.NodeControls.Add(nodeTextBox10);
            newFilesTreeViewAdv.NodeControls.Add(nodeTextBox11);
            newFilesTreeViewAdv.NodeControls.Add(nodeTextBox12);
            newFilesTreeViewAdv.NodeFilter = null;
            newFilesTreeViewAdv.SelectedNode = null;
            newFilesTreeViewAdv.Size = new Size(856, 565);
            newFilesTreeViewAdv.TabIndex = 0;
            newFilesTreeViewAdv.Text = "treeViewAdv1";
            newFilesTreeViewAdv.UseColumns = true;
            newFilesTreeViewAdv.ColumnClicked += treeViewAdv_ColumnClicked;
            newFilesTreeViewAdv.Collapsed += treeViewAdv_Collapsed;
            newFilesTreeViewAdv.Expanded += treeViewAdv_Expanded;
            // 
            // name_newTreeColumn
            // 
            name_newTreeColumn.Header = "Name";
            name_newTreeColumn.SortOrder = SortOrder.None;
            name_newTreeColumn.TooltipText = null;
            // 
            // created_newTreeColumn
            // 
            created_newTreeColumn.Header = "Created";
            created_newTreeColumn.SortOrder = SortOrder.None;
            created_newTreeColumn.TooltipText = null;
            created_newTreeColumn.Width = 65;
            // 
            // modified_newTreeColumn
            // 
            modified_newTreeColumn.Header = "Modified";
            modified_newTreeColumn.SortOrder = SortOrder.None;
            modified_newTreeColumn.TooltipText = null;
            modified_newTreeColumn.Width = 65;
            // 
            // size_newTreeColumn
            // 
            size_newTreeColumn.Header = "Size";
            size_newTreeColumn.SortOrder = SortOrder.None;
            size_newTreeColumn.TooltipText = null;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { checkAllToolStripMenuItem, checkNoneToolStripMenuItem, expandAllToolStripMenuItem, collapseAllToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(138, 92);
            // 
            // checkAllToolStripMenuItem
            // 
            checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
            checkAllToolStripMenuItem.Size = new Size(137, 22);
            checkAllToolStripMenuItem.Text = "Check all";
            // 
            // checkNoneToolStripMenuItem
            // 
            checkNoneToolStripMenuItem.Name = "checkNoneToolStripMenuItem";
            checkNoneToolStripMenuItem.Size = new Size(137, 22);
            checkNoneToolStripMenuItem.Text = "Check none";
            // 
            // expandAllToolStripMenuItem
            // 
            expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            expandAllToolStripMenuItem.Size = new Size(137, 22);
            expandAllToolStripMenuItem.Text = "Expand all";
            // 
            // collapseAllToolStripMenuItem
            // 
            collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            collapseAllToolStripMenuItem.Size = new Size(137, 22);
            collapseAllToolStripMenuItem.Text = "Collapse all";
            // 
            // nodeCheckBox2
            // 
            nodeCheckBox2.DataPropertyName = "CheckState";
            nodeCheckBox2.EditEnabled = true;
            nodeCheckBox2.LeftMargin = 0;
            nodeCheckBox2.ParentColumn = name_newTreeColumn;
            nodeCheckBox2.ThreeState = true;
            // 
            // nodeStateIcon2
            // 
            nodeStateIcon2.DataPropertyName = "StateIcon";
            nodeStateIcon2.LeftMargin = 1;
            nodeStateIcon2.ParentColumn = name_newTreeColumn;
            nodeStateIcon2.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
            // 
            // nodeTextBox9
            // 
            nodeTextBox9.DataPropertyName = "NodeControl1";
            nodeTextBox9.IncrementalSearchEnabled = true;
            nodeTextBox9.LeftMargin = 3;
            nodeTextBox9.ParentColumn = name_newTreeColumn;
            // 
            // nodeTextBox10
            // 
            nodeTextBox10.DataPropertyName = "NodeControl2";
            nodeTextBox10.IncrementalSearchEnabled = true;
            nodeTextBox10.LeftMargin = 3;
            nodeTextBox10.ParentColumn = created_newTreeColumn;
            // 
            // nodeTextBox11
            // 
            nodeTextBox11.DataPropertyName = "NodeControl3";
            nodeTextBox11.IncrementalSearchEnabled = true;
            nodeTextBox11.LeftMargin = 3;
            nodeTextBox11.ParentColumn = modified_newTreeColumn;
            // 
            // nodeTextBox12
            // 
            nodeTextBox12.DataPropertyName = "NodeControl4";
            nodeTextBox12.IncrementalSearchEnabled = true;
            nodeTextBox12.LeftMargin = 3;
            nodeTextBox12.ParentColumn = size_newTreeColumn;
            // 
            // filter_textBox
            // 
            filter_textBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            filter_textBox.BorderStyle = BorderStyle.FixedSingle;
            filter_textBox.Location = new Point(15, 610);
            filter_textBox.Margin = new Padding(4, 3, 4, 3);
            filter_textBox.Name = "filter_textBox";
            filter_textBox.Size = new Size(166, 23);
            filter_textBox.TabIndex = 19;
            filter_textBox.TextChangedCompleteDelay = TimeSpan.Parse("00:00:00.6000000");
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1192, 24);
            menuStrip1.TabIndex = 23;
            menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // SyncFilesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1192, 642);
            Controls.Add(menuStrip1);
            Controls.Add(filter_textBox);
            Controls.Add(stopDownload_button);
            Controls.Add(importMega_button);
            Controls.Add(getJdLinks_button);
            Controls.Add(DownloadProgress_label);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(progressBar4);
            Controls.Add(progressBar3);
            Controls.Add(progressBar2);
            Controls.Add(progressBar1);
            Controls.Add(downloadFiles_button);
            Controls.Add(flatList2_checkBox);
            Controls.Add(newFilesTreeViewAdv);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(506, 675);
            Name = "SyncFilesForm";
            Text = "Download files";
            FormClosing += syncFilesForm2_FormClosing;
            contextMenuStrip1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private Button importMega_button;
        private CheckBox flatList2_checkBox;
        private Button getJdLinks_button;
        private Button downloadFiles_button;
        private ProgressBar progressBar1;
        private ProgressBar progressBar2;
        private ProgressBar progressBar3;
        private ProgressBar progressBar4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label DownloadProgress_label;
        private ToolTip toolTip1;
        private Button stopDownload_button;
        private CloudFolderBrowser.TextBox filter_textBox;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem checkAllToolStripMenuItem;
        private ToolStripMenuItem checkNoneToolStripMenuItem;
        private ToolStripMenuItem expandAllToolStripMenuItem;
        private ToolStripMenuItem collapseAllToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem settingsToolStripMenuItem;
    }
}