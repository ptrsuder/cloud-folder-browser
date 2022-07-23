﻿namespace CloudFolderBrowser
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox publicFolderKey_textBox;
        private System.Windows.Forms.Button browseSyncFolder_button;
        private System.Windows.Forms.TextBox syncFolderPath_textBox;
        private Aga.Controls.Tree.TreeViewAdv cloudPublicFolder_treeViewAdv;
        private Aga.Controls.Tree.TreeViewAdv syncFolder_treeViewAdv;

        private Aga.Controls.Tree.TreeColumn name_treeColumn;
        private Aga.Controls.Tree.TreeColumn created_treeColumn;
        private Aga.Controls.Tree.TreeColumn modified_treeColumn;
        private Aga.Controls.Tree.TreeColumn size_treeColumn;

        private Aga.Controls.Tree.TreeColumn name_syncTreeColumn;
        private Aga.Controls.Tree.TreeColumn created_syncTreeColumn;
        private Aga.Controls.Tree.TreeColumn modified_syncTreeColumn;
        private Aga.Controls.Tree.TreeColumn size_syncTreeColumn;



        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.publicFolderKey_textBox = new System.Windows.Forms.TextBox();
            this.browseSyncFolder_button = new System.Windows.Forms.Button();
            this.syncFolderPath_textBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cloudPublicFolder_treeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
            this.name_treeColumn = new Aga.Controls.Tree.TreeColumn();
            this.created_treeColumn = new Aga.Controls.Tree.TreeColumn();
            this.modified_treeColumn = new Aga.Controls.Tree.TreeColumn();
            this.size_treeColumn = new Aga.Controls.Tree.TreeColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeCheckBox1 = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.nodeStateIcon1 = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
            this.nodeTextBox1 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox2 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox3 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox4 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.syncFolder_treeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
            this.name_syncTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.created_syncTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.modified_syncTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.size_syncTreeColumn = new Aga.Controls.Tree.TreeColumn();
            this.syncFolderTree_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshFolder_menuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolder_menuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeTextBox5 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox6 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox7 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox8 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.loadPublicFolderKey_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.beforeDate_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.checkedFiles_label = new System.Windows.Forms.Label();
            this.afterDate_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.filter_textBox = new CloudFolderBrowser.TextBox();
            this.flatList_checkBox = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.hideExistingFiles_checkBox = new System.Windows.Forms.CheckBox();
            this.syncFolders_button = new System.Windows.Forms.Button();
            this.showSyncForm_button = new System.Windows.Forms.Button();
            this.editPublicFolderKey_button = new System.Windows.Forms.Button();
            this.LoadFromFile_button = new System.Windows.Forms.Button();
            this.SaveToFile_button = new System.Windows.Forms.Button();
            this.publicFolders_comboBox = new System.Windows.Forms.ComboBox();
            this.addNewPublicFolder_button = new System.Windows.Forms.Button();
            this.deletePublicFolder_button = new System.Windows.Forms.Button();
            this.loginYandex_button = new System.Windows.Forms.Button();
            this.loginMega_button = new System.Windows.Forms.Button();
            this.loadLink_progressBar = new System.Windows.Forms.ProgressBar();
            this.createArchive_button = new System.Windows.Forms.Button();
            this.appVersion_linkLabel = new System.Windows.Forms.LinkLabel();
            this.fogLink_button = new System.Windows.Forms.Button();
            this.yadiskSpace_progressBar = new CloudFolderBrowser.TextProgressBar();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.syncFolderTree_contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // publicFolderKey_textBox
            // 
            this.publicFolderKey_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.publicFolderKey_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.publicFolderKey_textBox.Location = new System.Drawing.Point(0, 0);
            this.publicFolderKey_textBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.publicFolderKey_textBox.Name = "publicFolderKey_textBox";
            this.publicFolderKey_textBox.ReadOnly = true;
            this.publicFolderKey_textBox.Size = new System.Drawing.Size(362, 23);
            this.publicFolderKey_textBox.TabIndex = 3;
            this.publicFolderKey_textBox.Text = "https://yadi.sk/d/df_jk-ih3Z9TfM";
            // 
            // browseSyncFolder_button
            // 
            this.browseSyncFolder_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseSyncFolder_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.browseSyncFolder_button.Location = new System.Drawing.Point(0, 0);
            this.browseSyncFolder_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.browseSyncFolder_button.Name = "browseSyncFolder_button";
            this.browseSyncFolder_button.Size = new System.Drawing.Size(147, 23);
            this.browseSyncFolder_button.TabIndex = 4;
            this.browseSyncFolder_button.Text = "...";
            this.browseSyncFolder_button.UseVisualStyleBackColor = true;
            this.browseSyncFolder_button.Click += new System.EventHandler(this.browseSyncFolder_button_Click);
            // 
            // syncFolderPath_textBox
            // 
            this.syncFolderPath_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.syncFolderPath_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syncFolderPath_textBox.Location = new System.Drawing.Point(0, 0);
            this.syncFolderPath_textBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.syncFolderPath_textBox.Name = "syncFolderPath_textBox";
            this.syncFolderPath_textBox.ReadOnly = true;
            this.syncFolderPath_textBox.Size = new System.Drawing.Size(295, 23);
            this.syncFolderPath_textBox.TabIndex = 5;
            this.syncFolderPath_textBox.TextChanged += new System.EventHandler(this.syncFolderPath_textBox_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cloudPublicFolder_treeViewAdv, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.syncFolder_treeViewAdv, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 2);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 132);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(910, 684);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(459, 3);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.syncFolderPath_textBox);
            this.splitContainer2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.browseSyncFolder_button);
            this.splitContainer2.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer2.Size = new System.Drawing.Size(447, 23);
            this.splitContainer2.SplitterDistance = 295;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 9;
            // 
            // cloudPublicFolder_treeViewAdv
            // 
            this.cloudPublicFolder_treeViewAdv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cloudPublicFolder_treeViewAdv.BackColor = System.Drawing.SystemColors.Window;
            this.cloudPublicFolder_treeViewAdv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cloudPublicFolder_treeViewAdv.ColumnHeaderHeight = 17;
            this.cloudPublicFolder_treeViewAdv.Columns.Add(this.name_treeColumn);
            this.cloudPublicFolder_treeViewAdv.Columns.Add(this.created_treeColumn);
            this.cloudPublicFolder_treeViewAdv.Columns.Add(this.modified_treeColumn);
            this.cloudPublicFolder_treeViewAdv.Columns.Add(this.size_treeColumn);
            this.cloudPublicFolder_treeViewAdv.ContextMenuStrip = this.contextMenuStrip1;
            this.cloudPublicFolder_treeViewAdv.DefaultToolTipProvider = null;
            this.cloudPublicFolder_treeViewAdv.DragDropMarkColor = System.Drawing.Color.Black;
            this.cloudPublicFolder_treeViewAdv.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.cloudPublicFolder_treeViewAdv.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.cloudPublicFolder_treeViewAdv.LineColor = System.Drawing.SystemColors.ControlDark;
            this.cloudPublicFolder_treeViewAdv.LoadOnDemand = true;
            this.cloudPublicFolder_treeViewAdv.Location = new System.Drawing.Point(4, 33);
            this.cloudPublicFolder_treeViewAdv.Margin = new System.Windows.Forms.Padding(4);
            this.cloudPublicFolder_treeViewAdv.Model = null;
            this.cloudPublicFolder_treeViewAdv.Name = "cloudPublicFolder_treeViewAdv";
            this.cloudPublicFolder_treeViewAdv.NodeControls.Add(this.nodeCheckBox1);
            this.cloudPublicFolder_treeViewAdv.NodeControls.Add(this.nodeStateIcon1);
            this.cloudPublicFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox1);
            this.cloudPublicFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox2);
            this.cloudPublicFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox3);
            this.cloudPublicFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox4);
            this.cloudPublicFolder_treeViewAdv.NodeFilter = null;
            this.cloudPublicFolder_treeViewAdv.SelectedNode = null;
            this.cloudPublicFolder_treeViewAdv.Size = new System.Drawing.Size(447, 587);
            this.cloudPublicFolder_treeViewAdv.TabIndex = 7;
            this.cloudPublicFolder_treeViewAdv.UseColumns = true;
            this.cloudPublicFolder_treeViewAdv.ColumnClicked += new System.EventHandler<Aga.Controls.Tree.TreeColumnEventArgs>(this.treeViewAdv_ColumnClicked);
            this.cloudPublicFolder_treeViewAdv.Collapsed += new System.EventHandler<Aga.Controls.Tree.TreeViewAdvEventArgs>(this.treeViewAdv_Collapsed);
            this.cloudPublicFolder_treeViewAdv.Expanded += new System.EventHandler<Aga.Controls.Tree.TreeViewAdvEventArgs>(this.treeViewAdv_Expanded);
            // 
            // name_treeColumn
            // 
            this.name_treeColumn.Header = "Name";
            this.name_treeColumn.Sortable = true;
            this.name_treeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.name_treeColumn.TooltipText = null;
            this.name_treeColumn.Width = 120;
            // 
            // created_treeColumn
            // 
            this.created_treeColumn.Header = "Created";
            this.created_treeColumn.Sortable = true;
            this.created_treeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.created_treeColumn.TooltipText = null;
            this.created_treeColumn.Width = 65;
            // 
            // modified_treeColumn
            // 
            this.modified_treeColumn.Header = "Modified";
            this.modified_treeColumn.Sortable = true;
            this.modified_treeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.modified_treeColumn.TooltipText = null;
            this.modified_treeColumn.Width = 65;
            // 
            // size_treeColumn
            // 
            this.size_treeColumn.Header = "Size";
            this.size_treeColumn.Sortable = true;
            this.size_treeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.size_treeColumn.TooltipText = null;
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
            // nodeCheckBox1
            // 
            this.nodeCheckBox1.DataPropertyName = "CheckState";
            this.nodeCheckBox1.EditEnabled = true;
            this.nodeCheckBox1.LeftMargin = 3;
            this.nodeCheckBox1.ParentColumn = this.name_treeColumn;
            this.nodeCheckBox1.ReverseCheckOrder = true;
            this.nodeCheckBox1.ThreeState = true;
            // 
            // nodeStateIcon1
            // 
            this.nodeStateIcon1.DataPropertyName = "StateIcon";
            this.nodeStateIcon1.LeftMargin = 2;
            this.nodeStateIcon1.ParentColumn = this.name_treeColumn;
            this.nodeStateIcon1.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
            // 
            // nodeTextBox1
            // 
            this.nodeTextBox1.DataPropertyName = "NodeControl1";
            this.nodeTextBox1.IncrementalSearchEnabled = true;
            this.nodeTextBox1.LeftMargin = 3;
            this.nodeTextBox1.ParentColumn = this.name_treeColumn;
            // 
            // nodeTextBox2
            // 
            this.nodeTextBox2.DataPropertyName = "NodeControl2";
            this.nodeTextBox2.IncrementalSearchEnabled = true;
            this.nodeTextBox2.LeftMargin = 3;
            this.nodeTextBox2.ParentColumn = this.created_treeColumn;
            // 
            // nodeTextBox3
            // 
            this.nodeTextBox3.DataPropertyName = "NodeControl3";
            this.nodeTextBox3.IncrementalSearchEnabled = true;
            this.nodeTextBox3.LeftMargin = 3;
            this.nodeTextBox3.ParentColumn = this.modified_treeColumn;
            // 
            // nodeTextBox4
            // 
            this.nodeTextBox4.DataPropertyName = "NodeControl4";
            this.nodeTextBox4.IncrementalSearchEnabled = true;
            this.nodeTextBox4.LeftMargin = 3;
            this.nodeTextBox4.ParentColumn = this.size_treeColumn;
            // 
            // syncFolder_treeViewAdv
            // 
            this.syncFolder_treeViewAdv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.syncFolder_treeViewAdv.BackColor = System.Drawing.SystemColors.Window;
            this.syncFolder_treeViewAdv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.syncFolder_treeViewAdv.ColumnHeaderHeight = 17;
            this.syncFolder_treeViewAdv.Columns.Add(this.name_syncTreeColumn);
            this.syncFolder_treeViewAdv.Columns.Add(this.created_syncTreeColumn);
            this.syncFolder_treeViewAdv.Columns.Add(this.modified_syncTreeColumn);
            this.syncFolder_treeViewAdv.Columns.Add(this.size_syncTreeColumn);
            this.syncFolder_treeViewAdv.ContextMenuStrip = this.syncFolderTree_contextMenuStrip;
            this.syncFolder_treeViewAdv.DefaultToolTipProvider = null;
            this.syncFolder_treeViewAdv.DragDropMarkColor = System.Drawing.Color.Black;
            this.syncFolder_treeViewAdv.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.syncFolder_treeViewAdv.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.syncFolder_treeViewAdv.LineColor = System.Drawing.SystemColors.ControlDark;
            this.syncFolder_treeViewAdv.Location = new System.Drawing.Point(459, 33);
            this.syncFolder_treeViewAdv.Margin = new System.Windows.Forms.Padding(4);
            this.syncFolder_treeViewAdv.Model = null;
            this.syncFolder_treeViewAdv.Name = "syncFolder_treeViewAdv";
            this.syncFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox5);
            this.syncFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox6);
            this.syncFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox7);
            this.syncFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox8);
            this.syncFolder_treeViewAdv.NodeFilter = null;
            this.syncFolder_treeViewAdv.SelectedNode = null;
            this.syncFolder_treeViewAdv.Size = new System.Drawing.Size(447, 587);
            this.syncFolder_treeViewAdv.TabIndex = 6;
            this.syncFolder_treeViewAdv.UseColumns = true;
            this.syncFolder_treeViewAdv.ColumnClicked += new System.EventHandler<Aga.Controls.Tree.TreeColumnEventArgs>(this.treeViewAdv_ColumnClicked);
            this.syncFolder_treeViewAdv.Collapsed += new System.EventHandler<Aga.Controls.Tree.TreeViewAdvEventArgs>(this.treeViewAdv_Collapsed);
            this.syncFolder_treeViewAdv.Expanded += new System.EventHandler<Aga.Controls.Tree.TreeViewAdvEventArgs>(this.treeViewAdv_Expanded);
            // 
            // name_syncTreeColumn
            // 
            this.name_syncTreeColumn.Header = "Name";
            this.name_syncTreeColumn.Sortable = true;
            this.name_syncTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.name_syncTreeColumn.TooltipText = null;
            // 
            // created_syncTreeColumn
            // 
            this.created_syncTreeColumn.Header = "Created";
            this.created_syncTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.created_syncTreeColumn.TooltipText = null;
            this.created_syncTreeColumn.Width = 65;
            // 
            // modified_syncTreeColumn
            // 
            this.modified_syncTreeColumn.Header = "Modified";
            this.modified_syncTreeColumn.Sortable = true;
            this.modified_syncTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.modified_syncTreeColumn.TooltipText = null;
            this.modified_syncTreeColumn.Width = 65;
            // 
            // size_syncTreeColumn
            // 
            this.size_syncTreeColumn.Header = "Size";
            this.size_syncTreeColumn.Sortable = true;
            this.size_syncTreeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.size_syncTreeColumn.TooltipText = null;
            // 
            // syncFolderTree_contextMenuStrip
            // 
            this.syncFolderTree_contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshFolder_menuItem,
            this.openFolder_menuItem});
            this.syncFolderTree_contextMenuStrip.Name = "syncFolderTree_contextMenuStrip";
            this.syncFolderTree_contextMenuStrip.Size = new System.Drawing.Size(160, 48);
            // 
            // refreshFolder_menuItem
            // 
            this.refreshFolder_menuItem.Enabled = false;
            this.refreshFolder_menuItem.Name = "refreshFolder_menuItem";
            this.refreshFolder_menuItem.Size = new System.Drawing.Size(159, 22);
            this.refreshFolder_menuItem.Text = "Refresh folder";
            // 
            // openFolder_menuItem
            // 
            this.openFolder_menuItem.Enabled = false;
            this.openFolder_menuItem.Name = "openFolder_menuItem";
            this.openFolder_menuItem.Size = new System.Drawing.Size(159, 22);
            this.openFolder_menuItem.Text = "Open in exporer";
            // 
            // nodeTextBox5
            // 
            this.nodeTextBox5.DataPropertyName = "NodeControl1";
            this.nodeTextBox5.IncrementalSearchEnabled = true;
            this.nodeTextBox5.LeftMargin = 3;
            this.nodeTextBox5.ParentColumn = this.name_syncTreeColumn;
            // 
            // nodeTextBox6
            // 
            this.nodeTextBox6.DataPropertyName = "NodeControl2";
            this.nodeTextBox6.IncrementalSearchEnabled = true;
            this.nodeTextBox6.LeftMargin = 3;
            this.nodeTextBox6.ParentColumn = this.created_syncTreeColumn;
            // 
            // nodeTextBox7
            // 
            this.nodeTextBox7.DataPropertyName = "NodeControl3";
            this.nodeTextBox7.IncrementalSearchEnabled = true;
            this.nodeTextBox7.LeftMargin = 3;
            this.nodeTextBox7.ParentColumn = this.modified_syncTreeColumn;
            // 
            // nodeTextBox8
            // 
            this.nodeTextBox8.DataPropertyName = "NodeControl4";
            this.nodeTextBox8.IncrementalSearchEnabled = true;
            this.nodeTextBox8.LeftMargin = 3;
            this.nodeTextBox8.ParentColumn = this.size_syncTreeColumn;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(4, 3);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.publicFolderKey_textBox);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.loadPublicFolderKey_button);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(447, 23);
            this.splitContainer1.SplitterDistance = 362;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 8;
            // 
            // loadPublicFolderKey_button
            // 
            this.loadPublicFolderKey_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadPublicFolderKey_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.loadPublicFolderKey_button.Location = new System.Drawing.Point(0, 0);
            this.loadPublicFolderKey_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.loadPublicFolderKey_button.Name = "loadPublicFolderKey_button";
            this.loadPublicFolderKey_button.Size = new System.Drawing.Size(80, 23);
            this.loadPublicFolderKey_button.TabIndex = 9;
            this.loadPublicFolderKey_button.Text = "Load";
            this.loadPublicFolderKey_button.UseVisualStyleBackColor = true;
            this.loadPublicFolderKey_button.Click += new System.EventHandler(this.LoadPublicFolderKey_button_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.beforeDate_dateTimePicker);
            this.panel1.Controls.Add(this.checkedFiles_label);
            this.panel1.Controls.Add(this.afterDate_dateTimePicker);
            this.panel1.Controls.Add(this.filter_textBox);
            this.panel1.Controls.Add(this.flatList_checkBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 627);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 54);
            this.panel1.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(179, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "to";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-3, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Modified from";
            // 
            // beforeDate_dateTimePicker
            // 
            this.beforeDate_dateTimePicker.CustomFormat = "  dd.MM.yy";
            this.beforeDate_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.beforeDate_dateTimePicker.Location = new System.Drawing.Point(201, 29);
            this.beforeDate_dateTimePicker.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.beforeDate_dateTimePicker.MaxDate = new System.DateTime(2018, 11, 26, 0, 0, 0, 0);
            this.beforeDate_dateTimePicker.MinDate = new System.DateTime(2013, 11, 21, 0, 0, 0, 0);
            this.beforeDate_dateTimePicker.Name = "beforeDate_dateTimePicker";
            this.beforeDate_dateTimePicker.Size = new System.Drawing.Size(90, 23);
            this.beforeDate_dateTimePicker.TabIndex = 26;
            this.beforeDate_dateTimePicker.Value = new System.DateTime(2018, 11, 26, 0, 0, 0, 0);
            this.beforeDate_dateTimePicker.ValueChanged += new System.EventHandler(this.Filter_textBox_TextChanged);
            // 
            // checkedFiles_label
            // 
            this.checkedFiles_label.AutoSize = true;
            this.checkedFiles_label.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkedFiles_label.Location = new System.Drawing.Point(324, 0);
            this.checkedFiles_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkedFiles_label.Name = "checkedFiles_label";
            this.checkedFiles_label.Size = new System.Drawing.Size(123, 15);
            this.checkedFiles_label.TabIndex = 11;
            this.checkedFiles_label.Text = "Selected: 0 MB | 0 files";
            this.checkedFiles_label.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // afterDate_dateTimePicker
            // 
            this.afterDate_dateTimePicker.CustomFormat = "  dd.MM.yy";
            this.afterDate_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.afterDate_dateTimePicker.Location = new System.Drawing.Point(84, 29);
            this.afterDate_dateTimePicker.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.afterDate_dateTimePicker.MaxDate = new System.DateTime(2022, 7, 8, 0, 0, 0, 0);
            this.afterDate_dateTimePicker.MinDate = new System.DateTime(2011, 9, 11, 0, 0, 0, 0);
            this.afterDate_dateTimePicker.Name = "afterDate_dateTimePicker";
            this.afterDate_dateTimePicker.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.afterDate_dateTimePicker.Size = new System.Drawing.Size(90, 23);
            this.afterDate_dateTimePicker.TabIndex = 25;
            this.afterDate_dateTimePicker.Value = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
            this.afterDate_dateTimePicker.ValueChanged += new System.EventHandler(this.Filter_textBox_TextChanged);
            // 
            // filter_textBox
            // 
            this.filter_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.filter_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filter_textBox.Location = new System.Drawing.Point(0, 3);
            this.filter_textBox.Name = "filter_textBox";
            this.filter_textBox.Size = new System.Drawing.Size(121, 23);
            this.filter_textBox.TabIndex = 13;
            this.filter_textBox.TextChangedCompleteDelay = System.TimeSpan.Parse("00:00:00.6000000");
            // 
            // flatList_checkBox
            // 
            this.flatList_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flatList_checkBox.AutoSize = true;
            this.flatList_checkBox.Enabled = false;
            this.flatList_checkBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.flatList_checkBox.Location = new System.Drawing.Point(125, 5);
            this.flatList_checkBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.flatList_checkBox.Name = "flatList_checkBox";
            this.flatList_checkBox.Size = new System.Drawing.Size(60, 19);
            this.flatList_checkBox.TabIndex = 12;
            this.flatList_checkBox.Text = "Flat list";
            this.flatList_checkBox.UseVisualStyleBackColor = true;
            this.flatList_checkBox.CheckedChanged += new System.EventHandler(this.FlatList_checkBox_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.hideExistingFiles_checkBox);
            this.panel2.Controls.Add(this.syncFolders_button);
            this.panel2.Controls.Add(this.showSyncForm_button);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(459, 627);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(447, 54);
            this.panel2.TabIndex = 11;
            // 
            // hideExistingFiles_checkBox
            // 
            this.hideExistingFiles_checkBox.AutoSize = true;
            this.hideExistingFiles_checkBox.Checked = true;
            this.hideExistingFiles_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hideExistingFiles_checkBox.Location = new System.Drawing.Point(170, 5);
            this.hideExistingFiles_checkBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.hideExistingFiles_checkBox.Name = "hideExistingFiles_checkBox";
            this.hideExistingFiles_checkBox.Size = new System.Drawing.Size(119, 19);
            this.hideExistingFiles_checkBox.TabIndex = 26;
            this.hideExistingFiles_checkBox.Text = "Hide existing files";
            this.hideExistingFiles_checkBox.UseVisualStyleBackColor = true;
            // 
            // syncFolders_button
            // 
            this.syncFolders_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.syncFolders_button.Enabled = false;
            this.syncFolders_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.syncFolders_button.Location = new System.Drawing.Point(0, 1);
            this.syncFolders_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.syncFolders_button.Name = "syncFolders_button";
            this.syncFolders_button.Size = new System.Drawing.Size(165, 25);
            this.syncFolders_button.TabIndex = 10;
            this.syncFolders_button.Text = "Compare folders content";
            this.syncFolders_button.UseVisualStyleBackColor = true;
            this.syncFolders_button.Click += new System.EventHandler(this.syncFolders_button_Click);
            // 
            // showSyncForm_button
            // 
            this.showSyncForm_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.showSyncForm_button.Enabled = false;
            this.showSyncForm_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.showSyncForm_button.Location = new System.Drawing.Point(0, 28);
            this.showSyncForm_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.showSyncForm_button.Name = "showSyncForm_button";
            this.showSyncForm_button.Size = new System.Drawing.Size(165, 25);
            this.showSyncForm_button.TabIndex = 24;
            this.showSyncForm_button.Text = "Show sync form";
            this.showSyncForm_button.UseVisualStyleBackColor = true;
            this.showSyncForm_button.Click += new System.EventHandler(this.showSyncForm_button_Click);
            // 
            // editPublicFolderKey_button
            // 
            this.editPublicFolderKey_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.editPublicFolderKey_button.Location = new System.Drawing.Point(72, 99);
            this.editPublicFolderKey_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.editPublicFolderKey_button.Name = "editPublicFolderKey_button";
            this.editPublicFolderKey_button.Size = new System.Drawing.Size(50, 23);
            this.editPublicFolderKey_button.TabIndex = 25;
            this.editPublicFolderKey_button.Text = "Edit";
            this.editPublicFolderKey_button.UseVisualStyleBackColor = true;
            this.editPublicFolderKey_button.Click += new System.EventHandler(this.editPublicFolderKey_button_Click);
            // 
            // LoadFromFile_button
            // 
            this.LoadFromFile_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LoadFromFile_button.Location = new System.Drawing.Point(495, 99);
            this.LoadFromFile_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.LoadFromFile_button.Name = "LoadFromFile_button";
            this.LoadFromFile_button.Size = new System.Drawing.Size(100, 23);
            this.LoadFromFile_button.TabIndex = 14;
            this.LoadFromFile_button.Text = "Open from file";
            this.LoadFromFile_button.UseVisualStyleBackColor = true;
            this.LoadFromFile_button.Click += new System.EventHandler(this.LoadFromFile_button_Click);
            // 
            // SaveToFile_button
            // 
            this.SaveToFile_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveToFile_button.Location = new System.Drawing.Point(598, 99);
            this.SaveToFile_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SaveToFile_button.Name = "SaveToFile_button";
            this.SaveToFile_button.Size = new System.Drawing.Size(100, 23);
            this.SaveToFile_button.TabIndex = 15;
            this.SaveToFile_button.Text = "Save to file";
            this.SaveToFile_button.UseVisualStyleBackColor = true;
            this.SaveToFile_button.Click += new System.EventHandler(this.SaveToFile_button_Click);
            // 
            // publicFolders_comboBox
            // 
            this.publicFolders_comboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.publicFolders_comboBox.FormattingEnabled = true;
            this.publicFolders_comboBox.Location = new System.Drawing.Point(182, 99);
            this.publicFolders_comboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.publicFolders_comboBox.Name = "publicFolders_comboBox";
            this.publicFolders_comboBox.Size = new System.Drawing.Size(308, 23);
            this.publicFolders_comboBox.TabIndex = 16;
            this.publicFolders_comboBox.SelectedIndexChanged += new System.EventHandler(this.PublicFolders_comboBox_SelectedIndexChanged);
            this.publicFolders_comboBox.SelectedValueChanged += new System.EventHandler(this.PublicFolders_comboBox_SelectedIndexChanged);
            // 
            // addNewPublicFolder_button
            // 
            this.addNewPublicFolder_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addNewPublicFolder_button.Location = new System.Drawing.Point(18, 99);
            this.addNewPublicFolder_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.addNewPublicFolder_button.Name = "addNewPublicFolder_button";
            this.addNewPublicFolder_button.Size = new System.Drawing.Size(50, 23);
            this.addNewPublicFolder_button.TabIndex = 18;
            this.addNewPublicFolder_button.Text = "Add";
            this.addNewPublicFolder_button.UseVisualStyleBackColor = true;
            this.addNewPublicFolder_button.Click += new System.EventHandler(this.addPublicFolder_button_Click);
            // 
            // deletePublicFolder_button
            // 
            this.deletePublicFolder_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.deletePublicFolder_button.Location = new System.Drawing.Point(126, 99);
            this.deletePublicFolder_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.deletePublicFolder_button.Name = "deletePublicFolder_button";
            this.deletePublicFolder_button.Size = new System.Drawing.Size(50, 23);
            this.deletePublicFolder_button.TabIndex = 19;
            this.deletePublicFolder_button.Text = "Delete";
            this.deletePublicFolder_button.UseVisualStyleBackColor = true;
            this.deletePublicFolder_button.Click += new System.EventHandler(this.deletePublicFolder_button_Click);
            // 
            // loginYandex_button
            // 
            this.loginYandex_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loginYandex_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.loginYandex_button.Location = new System.Drawing.Point(964, 7);
            this.loginYandex_button.Name = "loginYandex_button";
            this.loginYandex_button.Size = new System.Drawing.Size(51, 36);
            this.loginYandex_button.TabIndex = 20;
            this.loginYandex_button.Text = "Log in Yadisk";
            this.loginYandex_button.UseVisualStyleBackColor = true;
            this.loginYandex_button.Visible = false;
            // 
            // loginMega_button
            // 
            this.loginMega_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loginMega_button.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.loginMega_button.Location = new System.Drawing.Point(862, 8);
            this.loginMega_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.loginMega_button.Name = "loginMega_button";
            this.loginMega_button.Size = new System.Drawing.Size(62, 42);
            this.loginMega_button.TabIndex = 20;
            this.loginMega_button.Text = "MEGA sing in";
            this.loginMega_button.UseVisualStyleBackColor = true;
            this.loginMega_button.Click += new System.EventHandler(this.loginMega_button_Click);
            // 
            // loadLink_progressBar
            // 
            this.loadLink_progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadLink_progressBar.Location = new System.Drawing.Point(18, 58);
            this.loadLink_progressBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.loadLink_progressBar.MarqueeAnimationSpeed = 0;
            this.loadLink_progressBar.Name = "loadLink_progressBar";
            this.loadLink_progressBar.Size = new System.Drawing.Size(906, 33);
            this.loadLink_progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.loadLink_progressBar.TabIndex = 21;
            // 
            // createArchive_button
            // 
            this.createArchive_button.Location = new System.Drawing.Point(1078, 97);
            this.createArchive_button.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.createArchive_button.Name = "createArchive_button";
            this.createArchive_button.Size = new System.Drawing.Size(106, 25);
            this.createArchive_button.TabIndex = 22;
            this.createArchive_button.Text = "Make Archive";
            this.createArchive_button.UseVisualStyleBackColor = true;
            this.createArchive_button.Visible = false;
            this.createArchive_button.Click += new System.EventHandler(this.createArchive_button_Click);
            // 
            // appVersion_linkLabel
            // 
            this.appVersion_linkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.appVersion_linkLabel.AutoSize = true;
            this.appVersion_linkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.appVersion_linkLabel.LinkColor = System.Drawing.SystemColors.Highlight;
            this.appVersion_linkLabel.Location = new System.Drawing.Point(853, 828);
            this.appVersion_linkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.appVersion_linkLabel.Name = "appVersion_linkLabel";
            this.appVersion_linkLabel.Size = new System.Drawing.Size(68, 13);
            this.appVersion_linkLabel.TabIndex = 23;
            this.appVersion_linkLabel.TabStop = true;
            this.appVersion_linkLabel.Text = "v 00.00.00";
            this.appVersion_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.appVersion_linkLabel_LinkClicked);
            // 
            // fogLink_button
            // 
            this.fogLink_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fogLink_button.Location = new System.Drawing.Point(826, 94);
            this.fogLink_button.Name = "fogLink_button";
            this.fogLink_button.Size = new System.Drawing.Size(98, 35);
            this.fogLink_button.TabIndex = 26;
            this.fogLink_button.Text = "FogLink";
            this.fogLink_button.UseVisualStyleBackColor = false;
            this.fogLink_button.Click += new System.EventHandler(this.fogLink_button_Click);
            // 
            // yadiskSpace_progressBar
            // 
            this.yadiskSpace_progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yadiskSpace_progressBar.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.yadiskSpace_progressBar.CustomText = "";
            this.yadiskSpace_progressBar.Location = new System.Drawing.Point(473, 8);
            this.yadiskSpace_progressBar.Name = "yadiskSpace_progressBar";
            this.yadiskSpace_progressBar.ProgressColor = System.Drawing.Color.LimeGreen;
            this.yadiskSpace_progressBar.Size = new System.Drawing.Size(382, 42);
            this.yadiskSpace_progressBar.TabIndex = 27;
            this.yadiskSpace_progressBar.TextColor = System.Drawing.Color.Black;
            this.yadiskSpace_progressBar.TextFont = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.yadiskSpace_progressBar.VisualMode = CloudFolderBrowser.ProgressBarDisplayMode.CustomText;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 850);
            this.Controls.Add(this.yadiskSpace_progressBar);
            this.Controls.Add(this.fogLink_button);
            this.Controls.Add(this.editPublicFolderKey_button);
            this.Controls.Add(this.appVersion_linkLabel);
            this.Controls.Add(this.createArchive_button);
            this.Controls.Add(this.loadLink_progressBar);
            this.Controls.Add(this.loginYandex_button);
            this.Controls.Add(this.loginMega_button);
            this.Controls.Add(this.deletePublicFolder_button);
            this.Controls.Add(this.addNewPublicFolder_button);
            this.Controls.Add(this.publicFolders_comboBox);
            this.Controls.Add(this.SaveToFile_button);
            this.Controls.Add(this.LoadFromFile_button);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(954, 571);
            this.Name = "MainForm";
            this.Text = "Cloud Folder Browser";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.syncFolderTree_contextMenuStrip.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private Aga.Controls.Tree.NodeControls.NodeCheckBox nodeCheckBox1;
        private Aga.Controls.Tree.NodeControls.NodeStateIcon nodeStateIcon1;

        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox1;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox2;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox3;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox4;

        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox5;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox6;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox7;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox8;


        private System.Windows.Forms.Button loadPublicFolderKey_button;
        private System.Windows.Forms.Button syncFolders_button;
        private System.Windows.Forms.Label checkedFiles_label;
        private System.Windows.Forms.CheckBox flatList_checkBox;
        private CloudFolderBrowser.TextBox filter_textBox;
        private System.Windows.Forms.Button LoadFromFile_button;
        private System.Windows.Forms.Button SaveToFile_button;
        private System.Windows.Forms.ComboBox publicFolders_comboBox;
        private System.Windows.Forms.Button addNewPublicFolder_button;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button deletePublicFolder_button;
        private System.Windows.Forms.Button loginYandex_button;
        private System.Windows.Forms.Button loginMega_button;
        private System.Windows.Forms.Button showSyncForm_button;
        private System.Windows.Forms.Button editPublicFolderKey_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker afterDate_dateTimePicker;
        private System.Windows.Forms.DateTimePicker beforeDate_dateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar loadLink_progressBar;
        private System.Windows.Forms.Button createArchive_button;
        private System.Windows.Forms.CheckBox hideExistingFiles_checkBox;
        private System.Windows.Forms.ContextMenuStrip syncFolderTree_contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem refreshFolder_menuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkNoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.LinkLabel appVersion_linkLabel;
        private ToolStripMenuItem openFolder_menuItem;
        private Button fogLink_button;
        private TextProgressBar yadiskSpace_progressBar;
    }
}
