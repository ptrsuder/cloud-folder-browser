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
        private System.Windows.Forms.ProgressBar yadiskSpace_progressBar;
        private System.Windows.Forms.Label yadiskSpace_label;
        private System.Windows.Forms.TextBox yadiskPublicFolderKey_textBox;
        private System.Windows.Forms.Button browseSyncFolder_button;
        private System.Windows.Forms.TextBox syncFolderPath_textBox;
        private Aga.Controls.Tree.TreeViewAdv yadiskPublicFolder_treeViewAdv;
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
            this.yadiskSpace_progressBar = new System.Windows.Forms.ProgressBar();
            this.yadiskSpace_label = new System.Windows.Forms.Label();
            this.yadiskPublicFolderKey_textBox = new System.Windows.Forms.TextBox();
            this.browseSyncFolder_button = new System.Windows.Forms.Button();
            this.syncFolderPath_textBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.yadiskPublicFolder_treeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
            this.name_treeColumn = new Aga.Controls.Tree.TreeColumn();
            this.created_treeColumn = new Aga.Controls.Tree.TreeColumn();
            this.modified_treeColumn = new Aga.Controls.Tree.TreeColumn();
            this.size_treeColumn = new Aga.Controls.Tree.TreeColumn();
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
            this.nodeTextBox5 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox6 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox7 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeTextBox8 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.editPublicFolderKey_button = new System.Windows.Forms.Button();
            this.savePublicFolderKey_button = new System.Windows.Forms.Button();
            this.loadPublicFolderKey_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.beforeDate_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.afterDate_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.filter_textBox = new System.Windows.Forms.TextBox();
            this.flatList_checkBox = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.syncFolders_button = new System.Windows.Forms.Button();
            this.showSyncForm_button = new System.Windows.Forms.Button();
            this.LoadFromFile_button = new System.Windows.Forms.Button();
            this.SaveToFile_button = new System.Windows.Forms.Button();
            this.publicFolders_comboBox = new System.Windows.Forms.ComboBox();
            this.nodeItem_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkAllSubfoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkFolderOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewPublicFolder_button = new System.Windows.Forms.Button();
            this.deletePublicFolder_button = new System.Windows.Forms.Button();
            this.loginYandex_button = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.nodeItem_contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // yadiskSpace_progressBar
            // 
            this.yadiskSpace_progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yadiskSpace_progressBar.Location = new System.Drawing.Point(739, 23);
            this.yadiskSpace_progressBar.Name = "yadiskSpace_progressBar";
            this.yadiskSpace_progressBar.Size = new System.Drawing.Size(369, 36);
            this.yadiskSpace_progressBar.Step = 1;
            this.yadiskSpace_progressBar.TabIndex = 0;
            this.yadiskSpace_progressBar.Visible = false;
            // 
            // yadiskSpace_label
            // 
            this.yadiskSpace_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yadiskSpace_label.AutoSize = true;
            this.yadiskSpace_label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.yadiskSpace_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.yadiskSpace_label.Location = new System.Drawing.Point(745, 31);
            this.yadiskSpace_label.Name = "yadiskSpace_label";
            this.yadiskSpace_label.Size = new System.Drawing.Size(49, 22);
            this.yadiskSpace_label.TabIndex = 1;
            this.yadiskSpace_label.Text = "Used: ";
            this.yadiskSpace_label.UseCompatibleTextRendering = true;
            this.yadiskSpace_label.Visible = false;
            // 
            // yadiskPublicFolderKey_textBox
            // 
            this.yadiskPublicFolderKey_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yadiskPublicFolderKey_textBox.Location = new System.Drawing.Point(0, 0);
            this.yadiskPublicFolderKey_textBox.Name = "yadiskPublicFolderKey_textBox";
            this.yadiskPublicFolderKey_textBox.ReadOnly = true;
            this.yadiskPublicFolderKey_textBox.Size = new System.Drawing.Size(419, 20);
            this.yadiskPublicFolderKey_textBox.TabIndex = 3;
            this.yadiskPublicFolderKey_textBox.Text = "https://yadi.sk/d/df_jk-ih3Z9TfM";
            // 
            // browseSyncFolder_button
            // 
            this.browseSyncFolder_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseSyncFolder_button.Location = new System.Drawing.Point(1, 0);
            this.browseSyncFolder_button.Name = "browseSyncFolder_button";
            this.browseSyncFolder_button.Size = new System.Drawing.Size(47, 21);
            this.browseSyncFolder_button.TabIndex = 4;
            this.browseSyncFolder_button.Text = "...";
            this.browseSyncFolder_button.UseVisualStyleBackColor = true;
            this.browseSyncFolder_button.Click += new System.EventHandler(this.browseSyncFolder_button_Click);
            // 
            // syncFolderPath_textBox
            // 
            this.syncFolderPath_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syncFolderPath_textBox.Location = new System.Drawing.Point(0, 0);
            this.syncFolderPath_textBox.Name = "syncFolderPath_textBox";
            this.syncFolderPath_textBox.ReadOnly = true;
            this.syncFolderPath_textBox.Size = new System.Drawing.Size(517, 20);
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
            this.tableLayoutPanel1.Controls.Add(this.yadiskPublicFolder_treeViewAdv, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.syncFolder_treeViewAdv, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 2);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 114);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1153, 597);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(579, 3);
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
            this.splitContainer2.Size = new System.Drawing.Size(571, 21);
            this.splitContainer2.SplitterDistance = 517;
            this.splitContainer2.TabIndex = 9;
            // 
            // yadiskPublicFolder_treeViewAdv
            // 
            this.yadiskPublicFolder_treeViewAdv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.yadiskPublicFolder_treeViewAdv.BackColor = System.Drawing.SystemColors.Window;
            this.yadiskPublicFolder_treeViewAdv.ColumnHeaderHeight = 17;
            this.yadiskPublicFolder_treeViewAdv.Columns.Add(this.name_treeColumn);
            this.yadiskPublicFolder_treeViewAdv.Columns.Add(this.created_treeColumn);
            this.yadiskPublicFolder_treeViewAdv.Columns.Add(this.modified_treeColumn);
            this.yadiskPublicFolder_treeViewAdv.Columns.Add(this.size_treeColumn);
            this.yadiskPublicFolder_treeViewAdv.DefaultToolTipProvider = null;
            this.yadiskPublicFolder_treeViewAdv.DragDropMarkColor = System.Drawing.Color.Black;
            this.yadiskPublicFolder_treeViewAdv.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.yadiskPublicFolder_treeViewAdv.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.yadiskPublicFolder_treeViewAdv.LineColor = System.Drawing.SystemColors.ControlDark;
            this.yadiskPublicFolder_treeViewAdv.Location = new System.Drawing.Point(3, 30);
            this.yadiskPublicFolder_treeViewAdv.Model = null;
            this.yadiskPublicFolder_treeViewAdv.Name = "yadiskPublicFolder_treeViewAdv";
            this.yadiskPublicFolder_treeViewAdv.NodeControls.Add(this.nodeCheckBox1);
            this.yadiskPublicFolder_treeViewAdv.NodeControls.Add(this.nodeStateIcon1);
            this.yadiskPublicFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox1);
            this.yadiskPublicFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox2);
            this.yadiskPublicFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox3);
            this.yadiskPublicFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox4);
            this.yadiskPublicFolder_treeViewAdv.NodeFilter = null;
            this.yadiskPublicFolder_treeViewAdv.SelectedNode = null;
            this.yadiskPublicFolder_treeViewAdv.Size = new System.Drawing.Size(570, 504);
            this.yadiskPublicFolder_treeViewAdv.TabIndex = 7;
            this.yadiskPublicFolder_treeViewAdv.UseColumns = true;
            this.yadiskPublicFolder_treeViewAdv.ColumnClicked += new System.EventHandler<Aga.Controls.Tree.TreeColumnEventArgs>(this.treeViewAdv_ColumnClicked);
            this.yadiskPublicFolder_treeViewAdv.Collapsed += new System.EventHandler<Aga.Controls.Tree.TreeViewAdvEventArgs>(this.treeViewAdv_Expanded);
            this.yadiskPublicFolder_treeViewAdv.Expanded += new System.EventHandler<Aga.Controls.Tree.TreeViewAdvEventArgs>(this.treeViewAdv_Expanded);
            // 
            // name_treeColumn
            // 
            this.name_treeColumn.Header = "Name";
            this.name_treeColumn.Sortable = true;
            this.name_treeColumn.SortOrder = System.Windows.Forms.SortOrder.None;
            this.name_treeColumn.TooltipText = null;
            this.name_treeColumn.Width = 20;
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
            this.syncFolder_treeViewAdv.ColumnHeaderHeight = 17;
            this.syncFolder_treeViewAdv.Columns.Add(this.name_syncTreeColumn);
            this.syncFolder_treeViewAdv.Columns.Add(this.created_syncTreeColumn);
            this.syncFolder_treeViewAdv.Columns.Add(this.modified_syncTreeColumn);
            this.syncFolder_treeViewAdv.Columns.Add(this.size_syncTreeColumn);
            this.syncFolder_treeViewAdv.DefaultToolTipProvider = null;
            this.syncFolder_treeViewAdv.DragDropMarkColor = System.Drawing.Color.Black;
            this.syncFolder_treeViewAdv.FullRowSelectActiveColor = System.Drawing.Color.Empty;
            this.syncFolder_treeViewAdv.FullRowSelectInactiveColor = System.Drawing.Color.Empty;
            this.syncFolder_treeViewAdv.LineColor = System.Drawing.SystemColors.ControlDark;
            this.syncFolder_treeViewAdv.Location = new System.Drawing.Point(579, 30);
            this.syncFolder_treeViewAdv.Model = null;
            this.syncFolder_treeViewAdv.Name = "syncFolder_treeViewAdv";
            this.syncFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox5);
            this.syncFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox6);
            this.syncFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox7);
            this.syncFolder_treeViewAdv.NodeControls.Add(this.nodeTextBox8);
            this.syncFolder_treeViewAdv.NodeFilter = null;
            this.syncFolder_treeViewAdv.SelectedNode = null;
            this.syncFolder_treeViewAdv.Size = new System.Drawing.Size(571, 504);
            this.syncFolder_treeViewAdv.TabIndex = 6;
            this.syncFolder_treeViewAdv.UseColumns = true;
            this.syncFolder_treeViewAdv.ColumnClicked += new System.EventHandler<Aga.Controls.Tree.TreeColumnEventArgs>(this.treeViewAdv_ColumnClicked);
            this.syncFolder_treeViewAdv.Collapsed += new System.EventHandler<Aga.Controls.Tree.TreeViewAdvEventArgs>(this.treeViewAdv_Expanded);
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
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.yadiskPublicFolderKey_textBox);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.editPublicFolderKey_button);
            this.splitContainer1.Panel2.Controls.Add(this.savePublicFolderKey_button);
            this.splitContainer1.Panel2.Controls.Add(this.loadPublicFolderKey_button);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(570, 21);
            this.splitContainer1.SplitterDistance = 419;
            this.splitContainer1.TabIndex = 8;
            // 
            // editPublicFolderKey_button
            // 
            this.editPublicFolderKey_button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editPublicFolderKey_button.Location = new System.Drawing.Point(50, 1);
            this.editPublicFolderKey_button.Name = "editPublicFolderKey_button";
            this.editPublicFolderKey_button.Size = new System.Drawing.Size(46, 20);
            this.editPublicFolderKey_button.TabIndex = 25;
            this.editPublicFolderKey_button.Text = "Edit";
            this.editPublicFolderKey_button.UseVisualStyleBackColor = true;
            this.editPublicFolderKey_button.Click += new System.EventHandler(this.editPublicFolderKey_button_Click);
            // 
            // savePublicFolderKey_button
            // 
            this.savePublicFolderKey_button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.savePublicFolderKey_button.Enabled = false;
            this.savePublicFolderKey_button.Location = new System.Drawing.Point(97, 1);
            this.savePublicFolderKey_button.Name = "savePublicFolderKey_button";
            this.savePublicFolderKey_button.Size = new System.Drawing.Size(50, 20);
            this.savePublicFolderKey_button.TabIndex = 17;
            this.savePublicFolderKey_button.Text = "Save";
            this.savePublicFolderKey_button.UseVisualStyleBackColor = true;
            this.savePublicFolderKey_button.Click += new System.EventHandler(this.SavePublicFolderKey_button_Click);
            // 
            // loadPublicFolderKey_button
            // 
            this.loadPublicFolderKey_button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadPublicFolderKey_button.Location = new System.Drawing.Point(3, 1);
            this.loadPublicFolderKey_button.Name = "loadPublicFolderKey_button";
            this.loadPublicFolderKey_button.Size = new System.Drawing.Size(46, 20);
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
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.afterDate_dateTimePicker);
            this.panel1.Controls.Add(this.filter_textBox);
            this.panel1.Controls.Add(this.flatList_checkBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 540);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 54);
            this.panel1.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(199, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "and before";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Show only modified after";
            // 
            // beforeDate_dateTimePicker
            // 
            this.beforeDate_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.beforeDate_dateTimePicker.Location = new System.Drawing.Point(258, 30);
            this.beforeDate_dateTimePicker.MaxDate = new System.DateTime(2018, 11, 26, 0, 0, 0, 0);
            this.beforeDate_dateTimePicker.MinDate = new System.DateTime(2013, 11, 21, 0, 0, 0, 0);
            this.beforeDate_dateTimePicker.Name = "beforeDate_dateTimePicker";
            this.beforeDate_dateTimePicker.Size = new System.Drawing.Size(81, 20);
            this.beforeDate_dateTimePicker.TabIndex = 26;
            this.beforeDate_dateTimePicker.Value = new System.DateTime(2018, 11, 26, 0, 0, 0, 0);
            this.beforeDate_dateTimePicker.ValueChanged += new System.EventHandler(this.Filter_textBox_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(194, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Selected: 0 MB";
            // 
            // afterDate_dateTimePicker
            // 
            this.afterDate_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.afterDate_dateTimePicker.Location = new System.Drawing.Point(120, 29);
            this.afterDate_dateTimePicker.MaxDate = new System.DateTime(2018, 11, 26, 0, 0, 0, 0);
            this.afterDate_dateTimePicker.MinDate = new System.DateTime(2011, 9, 11, 0, 0, 0, 0);
            this.afterDate_dateTimePicker.Name = "afterDate_dateTimePicker";
            this.afterDate_dateTimePicker.Size = new System.Drawing.Size(80, 20);
            this.afterDate_dateTimePicker.TabIndex = 25;
            this.afterDate_dateTimePicker.Value = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
            this.afterDate_dateTimePicker.ValueChanged += new System.EventHandler(this.Filter_textBox_TextChanged);
            // 
            // filter_textBox
            // 
            this.filter_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.filter_textBox.Location = new System.Drawing.Point(0, 4);
            this.filter_textBox.Name = "filter_textBox";
            this.filter_textBox.Size = new System.Drawing.Size(121, 20);
            this.filter_textBox.TabIndex = 13;
            this.filter_textBox.TextChanged += new System.EventHandler(this.Filter_textBox_TextChanged);
            // 
            // flatList_checkBox
            // 
            this.flatList_checkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flatList_checkBox.AutoSize = true;
            this.flatList_checkBox.Location = new System.Drawing.Point(130, 6);
            this.flatList_checkBox.Name = "flatList_checkBox";
            this.flatList_checkBox.Size = new System.Drawing.Size(58, 17);
            this.flatList_checkBox.TabIndex = 12;
            this.flatList_checkBox.Text = "Flat list";
            this.flatList_checkBox.UseVisualStyleBackColor = true;
            this.flatList_checkBox.CheckedChanged += new System.EventHandler(this.FlatList_checkBox_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.syncFolders_button);
            this.panel2.Controls.Add(this.showSyncForm_button);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(579, 540);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(571, 54);
            this.panel2.TabIndex = 11;
            // 
            // syncFolders_button
            // 
            this.syncFolders_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.syncFolders_button.Enabled = false;
            this.syncFolders_button.Location = new System.Drawing.Point(3, 2);
            this.syncFolders_button.Name = "syncFolders_button";
            this.syncFolders_button.Size = new System.Drawing.Size(141, 22);
            this.syncFolders_button.TabIndex = 10;
            this.syncFolders_button.Text = "Compare folders content";
            this.syncFolders_button.UseVisualStyleBackColor = true;
            this.syncFolders_button.Click += new System.EventHandler(this.syncFolders_button_Click);
            // 
            // showSyncForm_button
            // 
            this.showSyncForm_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.showSyncForm_button.Enabled = false;
            this.showSyncForm_button.Location = new System.Drawing.Point(3, 27);
            this.showSyncForm_button.Name = "showSyncForm_button";
            this.showSyncForm_button.Size = new System.Drawing.Size(92, 22);
            this.showSyncForm_button.TabIndex = 24;
            this.showSyncForm_button.Text = "Show sync form";
            this.showSyncForm_button.UseVisualStyleBackColor = true;
            this.showSyncForm_button.Click += new System.EventHandler(this.showSyncForm_button_Click);
            // 
            // LoadFromFile_button
            // 
            this.LoadFromFile_button.Location = new System.Drawing.Point(426, 86);
            this.LoadFromFile_button.Name = "LoadFromFile_button";
            this.LoadFromFile_button.Size = new System.Drawing.Size(82, 22);
            this.LoadFromFile_button.TabIndex = 14;
            this.LoadFromFile_button.Text = "Open from file";
            this.LoadFromFile_button.UseVisualStyleBackColor = true;
            this.LoadFromFile_button.Click += new System.EventHandler(this.LoadFromFile_button_Click);
            // 
            // SaveToFile_button
            // 
            this.SaveToFile_button.Location = new System.Drawing.Point(510, 86);
            this.SaveToFile_button.Name = "SaveToFile_button";
            this.SaveToFile_button.Size = new System.Drawing.Size(77, 22);
            this.SaveToFile_button.TabIndex = 15;
            this.SaveToFile_button.Text = "Save to file";
            this.SaveToFile_button.UseVisualStyleBackColor = true;
            this.SaveToFile_button.Click += new System.EventHandler(this.SaveToFile_button_Click);
            // 
            // publicFolders_comboBox
            // 
            this.publicFolders_comboBox.FormattingEnabled = true;
            this.publicFolders_comboBox.Location = new System.Drawing.Point(135, 86);
            this.publicFolders_comboBox.Name = "publicFolders_comboBox";
            this.publicFolders_comboBox.Size = new System.Drawing.Size(285, 21);
            this.publicFolders_comboBox.TabIndex = 16;
            this.publicFolders_comboBox.SelectedValueChanged += new System.EventHandler(this.PublicFolders_comboBox_SelectedIndexChanged);
            // 
            // nodeItem_contextMenuStrip
            // 
            this.nodeItem_contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllSubfoldersToolStripMenuItem,
            this.checkFolderOnlyToolStripMenuItem});
            this.nodeItem_contextMenuStrip.Name = "nodeItem_contextMenuStrip";
            this.nodeItem_contextMenuStrip.Size = new System.Drawing.Size(181, 48);
            // 
            // checkAllSubfoldersToolStripMenuItem
            // 
            this.checkAllSubfoldersToolStripMenuItem.Name = "checkAllSubfoldersToolStripMenuItem";
            this.checkAllSubfoldersToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.checkAllSubfoldersToolStripMenuItem.Text = "Check all subfolders";
            // 
            // checkFolderOnlyToolStripMenuItem
            // 
            this.checkFolderOnlyToolStripMenuItem.Name = "checkFolderOnlyToolStripMenuItem";
            this.checkFolderOnlyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.checkFolderOnlyToolStripMenuItem.Text = "Check folder only";
            // 
            // addNewPublicFolder_button
            // 
            this.addNewPublicFolder_button.Location = new System.Drawing.Point(13, 85);
            this.addNewPublicFolder_button.Name = "addNewPublicFolder_button";
            this.addNewPublicFolder_button.Size = new System.Drawing.Size(60, 22);
            this.addNewPublicFolder_button.TabIndex = 18;
            this.addNewPublicFolder_button.Text = "Add new";
            this.addNewPublicFolder_button.UseVisualStyleBackColor = true;
            this.addNewPublicFolder_button.Click += new System.EventHandler(this.AddNewPublicFolder_button_Click);
            // 
            // deletePublicFolder_button
            // 
            this.deletePublicFolder_button.Location = new System.Drawing.Point(76, 85);
            this.deletePublicFolder_button.Name = "deletePublicFolder_button";
            this.deletePublicFolder_button.Size = new System.Drawing.Size(54, 22);
            this.deletePublicFolder_button.TabIndex = 19;
            this.deletePublicFolder_button.Text = "Delete";
            this.deletePublicFolder_button.UseVisualStyleBackColor = true;
            this.deletePublicFolder_button.Click += new System.EventHandler(this.deletePublicFolder_button_Click);
            // 
            // loginYandex_button
            // 
            this.loginYandex_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loginYandex_button.Location = new System.Drawing.Point(1114, 23);
            this.loginYandex_button.Name = "loginYandex_button";
            this.loginYandex_button.Size = new System.Drawing.Size(51, 36);
            this.loginYandex_button.TabIndex = 20;
            this.loginYandex_button.Text = "Log in Yadisk";
            this.loginYandex_button.UseVisualStyleBackColor = true;
            this.loginYandex_button.Click += new System.EventHandler(this.loginYandex_button_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label4.Location = new System.Drawing.Point(936, 719);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(228, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Bugs and suggestions - honheim@yandex.com";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 741);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.loginYandex_button);
            this.Controls.Add(this.deletePublicFolder_button);
            this.Controls.Add(this.addNewPublicFolder_button);
            this.Controls.Add(this.publicFolders_comboBox);
            this.Controls.Add(this.SaveToFile_button);
            this.Controls.Add(this.LoadFromFile_button);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.yadiskSpace_label);
            this.Controls.Add(this.yadiskSpace_progressBar);
            this.MinimumSize = new System.Drawing.Size(612, 500);
            this.Name = "MainForm";
            this.Text = "CloudFolderBrowser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.nodeItem_contextMenuStrip.ResumeLayout(false);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox flatList_checkBox;
        private System.Windows.Forms.TextBox filter_textBox;
        private System.Windows.Forms.Button LoadFromFile_button;
        private System.Windows.Forms.Button SaveToFile_button;
        private System.Windows.Forms.ComboBox publicFolders_comboBox;
        private System.Windows.Forms.ContextMenuStrip nodeItem_contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem checkAllSubfoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkFolderOnlyToolStripMenuItem;
        private System.Windows.Forms.Button savePublicFolderKey_button;
        private System.Windows.Forms.Button addNewPublicFolder_button;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button deletePublicFolder_button;
        private System.Windows.Forms.Button loginYandex_button;
        private System.Windows.Forms.Button showSyncForm_button;
        private System.Windows.Forms.Button editPublicFolderKey_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker afterDate_dateTimePicker;
        private System.Windows.Forms.DateTimePicker beforeDate_dateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

