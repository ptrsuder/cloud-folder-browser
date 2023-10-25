namespace CloudFolderBrowser
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
        private TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox publicFolderKey_textBox;
        private Button browseSyncFolder_button;
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            publicFolderKey_textBox = new System.Windows.Forms.TextBox();
            browseSyncFolder_button = new Button();
            syncFolderPath_textBox = new System.Windows.Forms.TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            splitContainer2 = new SplitContainer();
            cloudPublicFolder_treeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
            name_treeColumn = new Aga.Controls.Tree.TreeColumn();
            created_treeColumn = new Aga.Controls.Tree.TreeColumn();
            modified_treeColumn = new Aga.Controls.Tree.TreeColumn();
            size_treeColumn = new Aga.Controls.Tree.TreeColumn();
            contextMenuStrip1 = new ContextMenuStrip(components);
            checkAllToolStripMenuItem = new ToolStripMenuItem();
            checkNoneToolStripMenuItem = new ToolStripMenuItem();
            expandAllToolStripMenuItem = new ToolStripMenuItem();
            collapseAllToolStripMenuItem = new ToolStripMenuItem();
            nodeCheckBox1 = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            nodeStateIcon1 = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
            nodeTextBox1 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            nodeTextBox2 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            nodeTextBox3 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            nodeTextBox4 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            splitContainer1 = new SplitContainer();
            loadPublicFolderKey_button = new Button();
            panel1 = new Panel();
            label3 = new Label();
            label2 = new Label();
            beforeDate_dateTimePicker = new DateTimePicker();
            checkedFiles_label = new Label();
            afterDate_dateTimePicker = new DateTimePicker();
            filter_textBox = new TextBox();
            flatList_checkBox = new CheckBox();
            panel2 = new Panel();
            hideExistingFiles_checkBox = new CheckBox();
            syncFolders_button = new Button();
            showSyncForm_button = new Button();
            syncFolder_treeViewAdv = new Aga.Controls.Tree.TreeViewAdv();
            name_syncTreeColumn = new Aga.Controls.Tree.TreeColumn();
            created_syncTreeColumn = new Aga.Controls.Tree.TreeColumn();
            modified_syncTreeColumn = new Aga.Controls.Tree.TreeColumn();
            size_syncTreeColumn = new Aga.Controls.Tree.TreeColumn();
            syncFolderTree_contextMenuStrip = new ContextMenuStrip(components);
            refreshFolder_menuItem = new ToolStripMenuItem();
            openFolder_menuItem = new ToolStripMenuItem();
            nodeTextBox5 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            nodeTextBox6 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            nodeTextBox7 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            nodeTextBox8 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            editPublicFolderKey_button = new Button();
            LoadFromFile_button = new Button();
            SaveToFile_button = new Button();
            publicFolders_comboBox = new ComboBox();
            addNewPublicFolder_button = new Button();
            deletePublicFolder_button = new Button();
            loginYandex_button = new Button();
            loginMega_button = new Button();
            createArchive_button = new Button();
            appVersion_linkLabel = new LinkLabel();
            fogLink_button = new Button();
            yadiskSpace_progressBar = new TextProgressBar();
            ProgressLoading_panel = new Panel();
            MainProgressBar = new CircularProgressBar.CircularProgressBar();
            enableProgressPanel_checkBox = new CheckBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            syncFolderTree_contextMenuStrip.SuspendLayout();
            ProgressLoading_panel.SuspendLayout();
            SuspendLayout();
            // 
            // publicFolderKey_textBox
            // 
            publicFolderKey_textBox.BorderStyle = BorderStyle.FixedSingle;
            publicFolderKey_textBox.Dock = DockStyle.Fill;
            publicFolderKey_textBox.Location = new Point(0, 0);
            publicFolderKey_textBox.Margin = new Padding(4, 3, 4, 3);
            publicFolderKey_textBox.Name = "publicFolderKey_textBox";
            publicFolderKey_textBox.ReadOnly = true;
            publicFolderKey_textBox.Size = new Size(335, 23);
            publicFolderKey_textBox.TabIndex = 3;
            // 
            // browseSyncFolder_button
            // 
            browseSyncFolder_button.Dock = DockStyle.Fill;
            browseSyncFolder_button.FlatStyle = FlatStyle.Popup;
            browseSyncFolder_button.Location = new Point(0, 0);
            browseSyncFolder_button.Margin = new Padding(4, 3, 4, 3);
            browseSyncFolder_button.Name = "browseSyncFolder_button";
            browseSyncFolder_button.Size = new Size(76, 23);
            browseSyncFolder_button.TabIndex = 4;
            browseSyncFolder_button.Text = "...";
            browseSyncFolder_button.UseVisualStyleBackColor = true;
            browseSyncFolder_button.Click += browseSyncFolder_button_Click;
            // 
            // syncFolderPath_textBox
            // 
            syncFolderPath_textBox.BorderStyle = BorderStyle.FixedSingle;
            syncFolderPath_textBox.Dock = DockStyle.Fill;
            syncFolderPath_textBox.Location = new Point(0, 0);
            syncFolderPath_textBox.Margin = new Padding(4, 3, 4, 3);
            syncFolderPath_textBox.Name = "syncFolderPath_textBox";
            syncFolderPath_textBox.ReadOnly = true;
            syncFolderPath_textBox.Size = new Size(358, 23);
            syncFolderPath_textBox.TabIndex = 5;
            syncFolderPath_textBox.TextChanged += syncFolderPath_textBox_TextChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(splitContainer2, 1, 0);
            tableLayoutPanel1.Controls.Add(cloudPublicFolder_treeViewAdv, 0, 1);
            tableLayoutPanel1.Controls.Add(splitContainer1, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 2);
            tableLayoutPanel1.Controls.Add(panel2, 1, 2);
            tableLayoutPanel1.Controls.Add(syncFolder_treeViewAdv, 1, 1);
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel1.Location = new Point(15, 132);
            tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel1.Size = new Size(894, 684);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel2;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(451, 3);
            splitContainer2.Margin = new Padding(4, 3, 4, 3);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(syncFolderPath_textBox);
            splitContainer2.Panel1.RightToLeft = RightToLeft.No;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(browseSyncFolder_button);
            splitContainer2.Panel2.RightToLeft = RightToLeft.No;
            splitContainer2.RightToLeft = RightToLeft.No;
            splitContainer2.Size = new Size(439, 23);
            splitContainer2.SplitterDistance = 358;
            splitContainer2.SplitterWidth = 5;
            splitContainer2.TabIndex = 9;
            // 
            // cloudPublicFolder_treeViewAdv
            // 
            cloudPublicFolder_treeViewAdv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cloudPublicFolder_treeViewAdv.BackColor = SystemColors.Window;
            cloudPublicFolder_treeViewAdv.BorderStyle = BorderStyle.FixedSingle;
            cloudPublicFolder_treeViewAdv.ColumnHeaderHeight = 17;
            cloudPublicFolder_treeViewAdv.Columns.Add(name_treeColumn);
            cloudPublicFolder_treeViewAdv.Columns.Add(created_treeColumn);
            cloudPublicFolder_treeViewAdv.Columns.Add(modified_treeColumn);
            cloudPublicFolder_treeViewAdv.Columns.Add(size_treeColumn);
            cloudPublicFolder_treeViewAdv.ContextMenuStrip = contextMenuStrip1;
            cloudPublicFolder_treeViewAdv.DefaultToolTipProvider = null;
            cloudPublicFolder_treeViewAdv.DragDropMarkColor = Color.Black;
            cloudPublicFolder_treeViewAdv.FullRowSelectActiveColor = Color.Empty;
            cloudPublicFolder_treeViewAdv.FullRowSelectInactiveColor = Color.Empty;
            cloudPublicFolder_treeViewAdv.LineColor = SystemColors.ControlDark;
            cloudPublicFolder_treeViewAdv.LoadOnDemand = true;
            cloudPublicFolder_treeViewAdv.Location = new Point(4, 33);
            cloudPublicFolder_treeViewAdv.Margin = new Padding(4);
            cloudPublicFolder_treeViewAdv.Model = null;
            cloudPublicFolder_treeViewAdv.Name = "cloudPublicFolder_treeViewAdv";
            cloudPublicFolder_treeViewAdv.NodeControls.Add(nodeCheckBox1);
            cloudPublicFolder_treeViewAdv.NodeControls.Add(nodeStateIcon1);
            cloudPublicFolder_treeViewAdv.NodeControls.Add(nodeTextBox1);
            cloudPublicFolder_treeViewAdv.NodeControls.Add(nodeTextBox2);
            cloudPublicFolder_treeViewAdv.NodeControls.Add(nodeTextBox3);
            cloudPublicFolder_treeViewAdv.NodeControls.Add(nodeTextBox4);
            cloudPublicFolder_treeViewAdv.NodeFilter = null;
            cloudPublicFolder_treeViewAdv.SelectedNode = null;
            cloudPublicFolder_treeViewAdv.Size = new Size(439, 587);
            cloudPublicFolder_treeViewAdv.TabIndex = 7;
            cloudPublicFolder_treeViewAdv.UseColumns = true;
            cloudPublicFolder_treeViewAdv.ColumnClicked += treeViewAdv_ColumnClicked;
            cloudPublicFolder_treeViewAdv.Collapsed += treeViewAdv_Collapsed;
            cloudPublicFolder_treeViewAdv.Expanded += treeViewAdv_Expanded;
            // 
            // name_treeColumn
            // 
            name_treeColumn.Header = "Name";
            name_treeColumn.Sortable = true;
            name_treeColumn.SortOrder = SortOrder.None;
            name_treeColumn.TooltipText = null;
            name_treeColumn.Width = 120;
            // 
            // created_treeColumn
            // 
            created_treeColumn.Header = "Created";
            created_treeColumn.Sortable = true;
            created_treeColumn.SortOrder = SortOrder.None;
            created_treeColumn.TooltipText = null;
            created_treeColumn.Width = 65;
            // 
            // modified_treeColumn
            // 
            modified_treeColumn.Header = "Modified";
            modified_treeColumn.Sortable = true;
            modified_treeColumn.SortOrder = SortOrder.None;
            modified_treeColumn.TooltipText = null;
            modified_treeColumn.Width = 65;
            // 
            // size_treeColumn
            // 
            size_treeColumn.Header = "Size";
            size_treeColumn.Sortable = true;
            size_treeColumn.SortOrder = SortOrder.None;
            size_treeColumn.TooltipText = null;
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
            // nodeCheckBox1
            // 
            nodeCheckBox1.DataPropertyName = "CheckState";
            nodeCheckBox1.EditEnabled = true;
            nodeCheckBox1.LeftMargin = 3;
            nodeCheckBox1.ParentColumn = name_treeColumn;
            nodeCheckBox1.ReverseCheckOrder = true;
            nodeCheckBox1.ThreeState = true;
            // 
            // nodeStateIcon1
            // 
            nodeStateIcon1.DataPropertyName = "StateIcon";
            nodeStateIcon1.LeftMargin = 2;
            nodeStateIcon1.ParentColumn = name_treeColumn;
            nodeStateIcon1.ScaleMode = Aga.Controls.Tree.ImageScaleMode.Clip;
            // 
            // nodeTextBox1
            // 
            nodeTextBox1.DataPropertyName = "NodeControl1";
            nodeTextBox1.IncrementalSearchEnabled = true;
            nodeTextBox1.LeftMargin = 3;
            nodeTextBox1.ParentColumn = name_treeColumn;
            // 
            // nodeTextBox2
            // 
            nodeTextBox2.DataPropertyName = "NodeControl2";
            nodeTextBox2.IncrementalSearchEnabled = true;
            nodeTextBox2.LeftMargin = 3;
            nodeTextBox2.ParentColumn = created_treeColumn;
            // 
            // nodeTextBox3
            // 
            nodeTextBox3.DataPropertyName = "NodeControl3";
            nodeTextBox3.IncrementalSearchEnabled = true;
            nodeTextBox3.LeftMargin = 3;
            nodeTextBox3.ParentColumn = modified_treeColumn;
            // 
            // nodeTextBox4
            // 
            nodeTextBox4.DataPropertyName = "NodeControl4";
            nodeTextBox4.IncrementalSearchEnabled = true;
            nodeTextBox4.LeftMargin = 3;
            nodeTextBox4.ParentColumn = size_treeColumn;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel2;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(4, 3);
            splitContainer1.Margin = new Padding(4, 3, 4, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(publicFolderKey_textBox);
            splitContainer1.Panel1.RightToLeft = RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(loadPublicFolderKey_button);
            splitContainer1.Panel2.RightToLeft = RightToLeft.No;
            splitContainer1.RightToLeft = RightToLeft.No;
            splitContainer1.Size = new Size(439, 23);
            splitContainer1.SplitterDistance = 335;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 8;
            // 
            // loadPublicFolderKey_button
            // 
            loadPublicFolderKey_button.Dock = DockStyle.Fill;
            loadPublicFolderKey_button.FlatStyle = FlatStyle.Popup;
            loadPublicFolderKey_button.Location = new Point(0, 0);
            loadPublicFolderKey_button.Margin = new Padding(4, 3, 4, 3);
            loadPublicFolderKey_button.Name = "loadPublicFolderKey_button";
            loadPublicFolderKey_button.Size = new Size(99, 23);
            loadPublicFolderKey_button.TabIndex = 9;
            loadPublicFolderKey_button.Text = "Load";
            loadPublicFolderKey_button.UseVisualStyleBackColor = true;
            loadPublicFolderKey_button.Click += LoadPublicFolderKey_button_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(beforeDate_dateTimePicker);
            panel1.Controls.Add(checkedFiles_label);
            panel1.Controls.Add(afterDate_dateTimePicker);
            panel1.Controls.Add(filter_textBox);
            panel1.Controls.Add(flatList_checkBox);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(4, 627);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(439, 54);
            panel1.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(179, 33);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(18, 15);
            label3.TabIndex = 27;
            label3.Text = "to";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(-3, 33);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(84, 15);
            label2.TabIndex = 14;
            label2.Text = "Modified from";
            // 
            // beforeDate_dateTimePicker
            // 
            beforeDate_dateTimePicker.CustomFormat = "  dd.MM.yy";
            beforeDate_dateTimePicker.Format = DateTimePickerFormat.Custom;
            beforeDate_dateTimePicker.Location = new Point(201, 29);
            beforeDate_dateTimePicker.Margin = new Padding(4, 3, 4, 3);
            beforeDate_dateTimePicker.MaxDate = new DateTime(2018, 11, 26, 0, 0, 0, 0);
            beforeDate_dateTimePicker.MinDate = new DateTime(2013, 11, 21, 0, 0, 0, 0);
            beforeDate_dateTimePicker.Name = "beforeDate_dateTimePicker";
            beforeDate_dateTimePicker.Size = new Size(90, 23);
            beforeDate_dateTimePicker.TabIndex = 26;
            beforeDate_dateTimePicker.Value = new DateTime(2018, 11, 26, 0, 0, 0, 0);
            beforeDate_dateTimePicker.ValueChanged += Filter_textBox_TextChanged;
            // 
            // checkedFiles_label
            // 
            checkedFiles_label.AutoSize = true;
            checkedFiles_label.Dock = DockStyle.Right;
            checkedFiles_label.Location = new Point(316, 0);
            checkedFiles_label.Margin = new Padding(4, 0, 4, 0);
            checkedFiles_label.Name = "checkedFiles_label";
            checkedFiles_label.Size = new Size(123, 15);
            checkedFiles_label.TabIndex = 11;
            checkedFiles_label.Text = "Selected: 0 MB | 0 files";
            checkedFiles_label.TextAlign = ContentAlignment.BottomCenter;
            // 
            // afterDate_dateTimePicker
            // 
            afterDate_dateTimePicker.CustomFormat = "  dd.MM.yy";
            afterDate_dateTimePicker.Format = DateTimePickerFormat.Custom;
            afterDate_dateTimePicker.Location = new Point(84, 29);
            afterDate_dateTimePicker.Margin = new Padding(4, 3, 4, 3);
            afterDate_dateTimePicker.MaxDate = new DateTime(2022, 7, 8, 0, 0, 0, 0);
            afterDate_dateTimePicker.MinDate = new DateTime(2011, 9, 11, 0, 0, 0, 0);
            afterDate_dateTimePicker.Name = "afterDate_dateTimePicker";
            afterDate_dateTimePicker.RightToLeft = RightToLeft.No;
            afterDate_dateTimePicker.Size = new Size(90, 23);
            afterDate_dateTimePicker.TabIndex = 25;
            afterDate_dateTimePicker.Value = new DateTime(2012, 1, 1, 0, 0, 0, 0);
            afterDate_dateTimePicker.ValueChanged += Filter_textBox_TextChanged;
            // 
            // filter_textBox
            // 
            filter_textBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            filter_textBox.BorderStyle = BorderStyle.FixedSingle;
            filter_textBox.Location = new Point(0, 3);
            filter_textBox.Name = "filter_textBox";
            filter_textBox.Size = new Size(174, 23);
            filter_textBox.TabIndex = 13;
            filter_textBox.TextChangedCompleteDelay = TimeSpan.Parse("00:00:00.6000000");
            // 
            // flatList_checkBox
            // 
            flatList_checkBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            flatList_checkBox.AutoSize = true;
            flatList_checkBox.Enabled = false;
            flatList_checkBox.FlatStyle = FlatStyle.Flat;
            flatList_checkBox.Location = new Point(178, 5);
            flatList_checkBox.Margin = new Padding(4, 3, 4, 3);
            flatList_checkBox.Name = "flatList_checkBox";
            flatList_checkBox.Size = new Size(60, 19);
            flatList_checkBox.TabIndex = 12;
            flatList_checkBox.Text = "Flat list";
            flatList_checkBox.UseVisualStyleBackColor = true;
            flatList_checkBox.CheckedChanged += FlatList_checkBox_CheckedChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(hideExistingFiles_checkBox);
            panel2.Controls.Add(syncFolders_button);
            panel2.Controls.Add(showSyncForm_button);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(451, 627);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(439, 54);
            panel2.TabIndex = 11;
            // 
            // hideExistingFiles_checkBox
            // 
            hideExistingFiles_checkBox.AutoSize = true;
            hideExistingFiles_checkBox.Checked = true;
            hideExistingFiles_checkBox.CheckState = CheckState.Checked;
            hideExistingFiles_checkBox.Location = new Point(170, 5);
            hideExistingFiles_checkBox.Margin = new Padding(4, 3, 4, 3);
            hideExistingFiles_checkBox.Name = "hideExistingFiles_checkBox";
            hideExistingFiles_checkBox.Size = new Size(119, 19);
            hideExistingFiles_checkBox.TabIndex = 26;
            hideExistingFiles_checkBox.Text = "Hide existing files";
            hideExistingFiles_checkBox.UseVisualStyleBackColor = true;
            // 
            // syncFolders_button
            // 
            syncFolders_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            syncFolders_button.Enabled = false;
            syncFolders_button.FlatStyle = FlatStyle.Popup;
            syncFolders_button.Location = new Point(0, 1);
            syncFolders_button.Margin = new Padding(4, 3, 4, 3);
            syncFolders_button.Name = "syncFolders_button";
            syncFolders_button.Size = new Size(165, 25);
            syncFolders_button.TabIndex = 10;
            syncFolders_button.Text = "Compare folders content";
            syncFolders_button.UseVisualStyleBackColor = true;
            syncFolders_button.Click += syncFolders_button_Click;
            // 
            // showSyncForm_button
            // 
            showSyncForm_button.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            showSyncForm_button.Enabled = false;
            showSyncForm_button.FlatStyle = FlatStyle.Popup;
            showSyncForm_button.Location = new Point(0, 28);
            showSyncForm_button.Margin = new Padding(4, 3, 4, 3);
            showSyncForm_button.Name = "showSyncForm_button";
            showSyncForm_button.Size = new Size(165, 25);
            showSyncForm_button.TabIndex = 24;
            showSyncForm_button.Text = "Show sync form";
            showSyncForm_button.UseVisualStyleBackColor = true;
            showSyncForm_button.Click += showSyncForm_button_Click;
            // 
            // syncFolder_treeViewAdv
            // 
            syncFolder_treeViewAdv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            syncFolder_treeViewAdv.BackColor = SystemColors.Window;
            syncFolder_treeViewAdv.BorderStyle = BorderStyle.FixedSingle;
            syncFolder_treeViewAdv.ColumnHeaderHeight = 17;
            syncFolder_treeViewAdv.Columns.Add(name_syncTreeColumn);
            syncFolder_treeViewAdv.Columns.Add(created_syncTreeColumn);
            syncFolder_treeViewAdv.Columns.Add(modified_syncTreeColumn);
            syncFolder_treeViewAdv.Columns.Add(size_syncTreeColumn);
            syncFolder_treeViewAdv.ContextMenuStrip = syncFolderTree_contextMenuStrip;
            syncFolder_treeViewAdv.DefaultToolTipProvider = null;
            syncFolder_treeViewAdv.DragDropMarkColor = Color.Black;
            syncFolder_treeViewAdv.FullRowSelectActiveColor = Color.Empty;
            syncFolder_treeViewAdv.FullRowSelectInactiveColor = Color.Empty;
            syncFolder_treeViewAdv.LineColor = SystemColors.ControlDark;
            syncFolder_treeViewAdv.Location = new Point(451, 33);
            syncFolder_treeViewAdv.Margin = new Padding(4);
            syncFolder_treeViewAdv.Model = null;
            syncFolder_treeViewAdv.Name = "syncFolder_treeViewAdv";
            syncFolder_treeViewAdv.NodeControls.Add(nodeTextBox5);
            syncFolder_treeViewAdv.NodeControls.Add(nodeTextBox6);
            syncFolder_treeViewAdv.NodeControls.Add(nodeTextBox7);
            syncFolder_treeViewAdv.NodeControls.Add(nodeTextBox8);
            syncFolder_treeViewAdv.NodeFilter = null;
            syncFolder_treeViewAdv.SelectedNode = null;
            syncFolder_treeViewAdv.Size = new Size(439, 587);
            syncFolder_treeViewAdv.TabIndex = 6;
            syncFolder_treeViewAdv.UseColumns = true;
            syncFolder_treeViewAdv.ColumnClicked += treeViewAdv_ColumnClicked;
            syncFolder_treeViewAdv.Collapsed += treeViewAdv_Collapsed;
            syncFolder_treeViewAdv.Expanded += treeViewAdv_Expanded;
            // 
            // name_syncTreeColumn
            // 
            name_syncTreeColumn.Header = "Name";
            name_syncTreeColumn.Sortable = true;
            name_syncTreeColumn.SortOrder = SortOrder.None;
            name_syncTreeColumn.TooltipText = null;
            // 
            // created_syncTreeColumn
            // 
            created_syncTreeColumn.Header = "Created";
            created_syncTreeColumn.SortOrder = SortOrder.None;
            created_syncTreeColumn.TooltipText = null;
            created_syncTreeColumn.Width = 65;
            // 
            // modified_syncTreeColumn
            // 
            modified_syncTreeColumn.Header = "Modified";
            modified_syncTreeColumn.Sortable = true;
            modified_syncTreeColumn.SortOrder = SortOrder.None;
            modified_syncTreeColumn.TooltipText = null;
            modified_syncTreeColumn.Width = 65;
            // 
            // size_syncTreeColumn
            // 
            size_syncTreeColumn.Header = "Size";
            size_syncTreeColumn.Sortable = true;
            size_syncTreeColumn.SortOrder = SortOrder.None;
            size_syncTreeColumn.TooltipText = null;
            // 
            // syncFolderTree_contextMenuStrip
            // 
            syncFolderTree_contextMenuStrip.Items.AddRange(new ToolStripItem[] { refreshFolder_menuItem, openFolder_menuItem });
            syncFolderTree_contextMenuStrip.Name = "syncFolderTree_contextMenuStrip";
            syncFolderTree_contextMenuStrip.Size = new Size(160, 48);
            // 
            // refreshFolder_menuItem
            // 
            refreshFolder_menuItem.Enabled = false;
            refreshFolder_menuItem.Name = "refreshFolder_menuItem";
            refreshFolder_menuItem.Size = new Size(159, 22);
            refreshFolder_menuItem.Text = "Refresh folder";
            // 
            // openFolder_menuItem
            // 
            openFolder_menuItem.Enabled = false;
            openFolder_menuItem.Name = "openFolder_menuItem";
            openFolder_menuItem.Size = new Size(159, 22);
            openFolder_menuItem.Text = "Open in exporer";
            // 
            // nodeTextBox5
            // 
            nodeTextBox5.DataPropertyName = "NodeControl1";
            nodeTextBox5.IncrementalSearchEnabled = true;
            nodeTextBox5.LeftMargin = 3;
            nodeTextBox5.ParentColumn = name_syncTreeColumn;
            // 
            // nodeTextBox6
            // 
            nodeTextBox6.DataPropertyName = "NodeControl2";
            nodeTextBox6.IncrementalSearchEnabled = true;
            nodeTextBox6.LeftMargin = 3;
            nodeTextBox6.ParentColumn = created_syncTreeColumn;
            // 
            // nodeTextBox7
            // 
            nodeTextBox7.DataPropertyName = "NodeControl3";
            nodeTextBox7.IncrementalSearchEnabled = true;
            nodeTextBox7.LeftMargin = 3;
            nodeTextBox7.ParentColumn = modified_syncTreeColumn;
            // 
            // nodeTextBox8
            // 
            nodeTextBox8.DataPropertyName = "NodeControl4";
            nodeTextBox8.IncrementalSearchEnabled = true;
            nodeTextBox8.LeftMargin = 3;
            nodeTextBox8.ParentColumn = size_syncTreeColumn;
            // 
            // editPublicFolderKey_button
            // 
            editPublicFolderKey_button.FlatStyle = FlatStyle.Popup;
            editPublicFolderKey_button.Location = new Point(72, 99);
            editPublicFolderKey_button.Margin = new Padding(4, 3, 4, 3);
            editPublicFolderKey_button.Name = "editPublicFolderKey_button";
            editPublicFolderKey_button.Size = new Size(50, 23);
            editPublicFolderKey_button.TabIndex = 25;
            editPublicFolderKey_button.Text = "Edit";
            editPublicFolderKey_button.UseVisualStyleBackColor = true;
            editPublicFolderKey_button.Click += editPublicFolderKey_button_Click;
            // 
            // LoadFromFile_button
            // 
            LoadFromFile_button.FlatStyle = FlatStyle.Popup;
            LoadFromFile_button.Location = new Point(495, 99);
            LoadFromFile_button.Margin = new Padding(4, 3, 4, 3);
            LoadFromFile_button.Name = "LoadFromFile_button";
            LoadFromFile_button.Size = new Size(100, 23);
            LoadFromFile_button.TabIndex = 14;
            LoadFromFile_button.Text = "Open from file";
            LoadFromFile_button.UseVisualStyleBackColor = true;
            LoadFromFile_button.Click += LoadFromFile_button_Click;
            // 
            // SaveToFile_button
            // 
            SaveToFile_button.FlatStyle = FlatStyle.Popup;
            SaveToFile_button.Location = new Point(598, 99);
            SaveToFile_button.Margin = new Padding(4, 3, 4, 3);
            SaveToFile_button.Name = "SaveToFile_button";
            SaveToFile_button.Size = new Size(100, 23);
            SaveToFile_button.TabIndex = 15;
            SaveToFile_button.Text = "Save to file";
            SaveToFile_button.UseVisualStyleBackColor = true;
            SaveToFile_button.Click += SaveToFile_button_Click;
            // 
            // publicFolders_comboBox
            // 
            publicFolders_comboBox.FlatStyle = FlatStyle.Popup;
            publicFolders_comboBox.FormattingEnabled = true;
            publicFolders_comboBox.Location = new Point(182, 99);
            publicFolders_comboBox.Margin = new Padding(4, 3, 4, 3);
            publicFolders_comboBox.Name = "publicFolders_comboBox";
            publicFolders_comboBox.Size = new Size(308, 23);
            publicFolders_comboBox.TabIndex = 16;
            publicFolders_comboBox.SelectedIndexChanged += PublicFolders_comboBox_SelectedIndexChanged;
            publicFolders_comboBox.SelectedValueChanged += PublicFolders_comboBox_SelectedIndexChanged;
            // 
            // addNewPublicFolder_button
            // 
            addNewPublicFolder_button.FlatStyle = FlatStyle.Popup;
            addNewPublicFolder_button.Location = new Point(18, 99);
            addNewPublicFolder_button.Margin = new Padding(4, 3, 4, 3);
            addNewPublicFolder_button.Name = "addNewPublicFolder_button";
            addNewPublicFolder_button.Size = new Size(50, 23);
            addNewPublicFolder_button.TabIndex = 18;
            addNewPublicFolder_button.Text = "Add";
            addNewPublicFolder_button.UseVisualStyleBackColor = true;
            addNewPublicFolder_button.Click += addPublicFolder_button_Click;
            // 
            // deletePublicFolder_button
            // 
            deletePublicFolder_button.FlatStyle = FlatStyle.Popup;
            deletePublicFolder_button.Location = new Point(126, 99);
            deletePublicFolder_button.Margin = new Padding(4, 3, 4, 3);
            deletePublicFolder_button.Name = "deletePublicFolder_button";
            deletePublicFolder_button.Size = new Size(50, 23);
            deletePublicFolder_button.TabIndex = 19;
            deletePublicFolder_button.Text = "Delete";
            deletePublicFolder_button.UseVisualStyleBackColor = true;
            deletePublicFolder_button.Click += deletePublicFolder_button_Click;
            // 
            // loginYandex_button
            // 
            loginYandex_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            loginYandex_button.FlatStyle = FlatStyle.Popup;
            loginYandex_button.Location = new Point(950, 7);
            loginYandex_button.Name = "loginYandex_button";
            loginYandex_button.Size = new Size(51, 36);
            loginYandex_button.TabIndex = 20;
            loginYandex_button.Text = "Log in Yadisk";
            loginYandex_button.UseVisualStyleBackColor = true;
            loginYandex_button.Visible = false;
            // 
            // loginMega_button
            // 
            loginMega_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            loginMega_button.FlatStyle = FlatStyle.Popup;
            loginMega_button.Location = new Point(847, 8);
            loginMega_button.Margin = new Padding(4, 3, 4, 3);
            loginMega_button.Name = "loginMega_button";
            loginMega_button.Size = new Size(62, 42);
            loginMega_button.TabIndex = 20;
            loginMega_button.Text = "MEGA sign in";
            loginMega_button.UseVisualStyleBackColor = true;
            loginMega_button.Click += loginMega_button_Click;
            // 
            // createArchive_button
            // 
            createArchive_button.Location = new Point(1078, 97);
            createArchive_button.Margin = new Padding(4, 3, 4, 3);
            createArchive_button.Name = "createArchive_button";
            createArchive_button.Size = new Size(106, 25);
            createArchive_button.TabIndex = 22;
            createArchive_button.Text = "Make Archive";
            createArchive_button.UseVisualStyleBackColor = true;
            createArchive_button.Visible = false;
            createArchive_button.Click += createArchive_button_Click;
            // 
            // appVersion_linkLabel
            // 
            appVersion_linkLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            appVersion_linkLabel.AutoSize = true;
            appVersion_linkLabel.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            appVersion_linkLabel.LinkColor = SystemColors.Highlight;
            appVersion_linkLabel.Location = new Point(839, 828);
            appVersion_linkLabel.Margin = new Padding(4, 0, 4, 0);
            appVersion_linkLabel.Name = "appVersion_linkLabel";
            appVersion_linkLabel.Size = new Size(68, 13);
            appVersion_linkLabel.TabIndex = 23;
            appVersion_linkLabel.TabStop = true;
            appVersion_linkLabel.Text = "v 00.00.00";
            appVersion_linkLabel.LinkClicked += appVersion_linkLabel_LinkClicked;
            // 
            // fogLink_button
            // 
            fogLink_button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            fogLink_button.Location = new Point(812, 94);
            fogLink_button.Name = "fogLink_button";
            fogLink_button.Size = new Size(98, 35);
            fogLink_button.TabIndex = 26;
            fogLink_button.Text = "FogLink";
            fogLink_button.UseVisualStyleBackColor = false;
            fogLink_button.Click += fogLink_button_Click;
            // 
            // yadiskSpace_progressBar
            // 
            yadiskSpace_progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            yadiskSpace_progressBar.BackColor = SystemColors.ControlDarkDark;
            yadiskSpace_progressBar.CustomText = "";
            yadiskSpace_progressBar.Location = new Point(459, 8);
            yadiskSpace_progressBar.Name = "yadiskSpace_progressBar";
            yadiskSpace_progressBar.ProgressColor = Color.LimeGreen;
            yadiskSpace_progressBar.Size = new Size(382, 42);
            yadiskSpace_progressBar.TabIndex = 27;
            yadiskSpace_progressBar.TextColor = Color.Black;
            yadiskSpace_progressBar.TextFont = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            yadiskSpace_progressBar.Visible = false;
            yadiskSpace_progressBar.VisualMode = ProgressBarDisplayMode.CustomText;
            // 
            // ProgressLoading_panel
            // 
            ProgressLoading_panel.Controls.Add(MainProgressBar);
            ProgressLoading_panel.Location = new Point(0, 0);
            ProgressLoading_panel.Name = "ProgressLoading_panel";
            ProgressLoading_panel.Size = new Size(500, 500);
            ProgressLoading_panel.TabIndex = 30;
            ProgressLoading_panel.Visible = false;
            // 
            // MainProgressBar
            // 
            MainProgressBar.Anchor = AnchorStyles.None;
            MainProgressBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            MainProgressBar.AnimationSpeed = 500;
            MainProgressBar.BackColor = Color.Transparent;
            MainProgressBar.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            MainProgressBar.ForeColor = Color.FromArgb(64, 64, 64);
            MainProgressBar.InnerColor = Color.Transparent;
            MainProgressBar.InnerMargin = 5;
            MainProgressBar.InnerWidth = 0;
            MainProgressBar.Location = new Point(125, 125);
            MainProgressBar.MarqueeAnimationSpeed = 2000;
            MainProgressBar.Name = "MainProgressBar";
            MainProgressBar.OuterColor = Color.Gray;
            MainProgressBar.OuterMargin = -25;
            MainProgressBar.OuterWidth = 0;
            MainProgressBar.ProgressColor = Color.FromArgb(255, 128, 0);
            MainProgressBar.ProgressWidth = 30;
            MainProgressBar.SecondaryFont = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point);
            MainProgressBar.Size = new Size(250, 250);
            MainProgressBar.StartAngle = 270;
            MainProgressBar.SubscriptColor = Color.FromArgb(166, 166, 166);
            MainProgressBar.SubscriptMargin = new Padding(10, -35, 0, 0);
            MainProgressBar.SubscriptText = ".23";
            MainProgressBar.SuperscriptColor = Color.FromArgb(166, 166, 166);
            MainProgressBar.SuperscriptMargin = new Padding(10, 35, 0, 0);
            MainProgressBar.SuperscriptText = "°C";
            MainProgressBar.TabIndex = 31;
            MainProgressBar.TextMargin = new Padding(8, 8, 0, 0);
            MainProgressBar.Value = 68;
            // 
            // enableProgressPanel_checkBox
            // 
            enableProgressPanel_checkBox.AutoSize = true;
            enableProgressPanel_checkBox.Location = new Point(719, 826);
            enableProgressPanel_checkBox.Name = "enableProgressPanel_checkBox";
            enableProgressPanel_checkBox.Size = new Size(113, 19);
            enableProgressPanel_checkBox.TabIndex = 31;
            enableProgressPanel_checkBox.Text = "Use progress bar";
            enableProgressPanel_checkBox.UseVisualStyleBackColor = true;
            enableProgressPanel_checkBox.CheckedChanged += enableProgressPanel_checkBox_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(924, 850);
            Controls.Add(enableProgressPanel_checkBox);
            Controls.Add(yadiskSpace_progressBar);
            Controls.Add(fogLink_button);
            Controls.Add(editPublicFolderKey_button);
            Controls.Add(appVersion_linkLabel);
            Controls.Add(createArchive_button);
            Controls.Add(loginYandex_button);
            Controls.Add(loginMega_button);
            Controls.Add(deletePublicFolder_button);
            Controls.Add(addNewPublicFolder_button);
            Controls.Add(publicFolders_comboBox);
            Controls.Add(SaveToFile_button);
            Controls.Add(LoadFromFile_button);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(ProgressLoading_panel);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(940, 570);
            Name = "MainForm";
            Text = "Cloud Folder Browser";
            tableLayoutPanel1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            contextMenuStrip1.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            syncFolderTree_contextMenuStrip.ResumeLayout(false);
            ProgressLoading_panel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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


        private Button loadPublicFolderKey_button;
        private Button syncFolders_button;
        private Label checkedFiles_label;
        private CheckBox flatList_checkBox;
        private CloudFolderBrowser.TextBox filter_textBox;
        private Button LoadFromFile_button;
        private Button SaveToFile_button;
        private ComboBox publicFolders_comboBox;
        private Button addNewPublicFolder_button;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private Button deletePublicFolder_button;
        private Button loginYandex_button;
        private Button loginMega_button;
        private Button showSyncForm_button;
        private Button editPublicFolderKey_button;
        private Panel panel1;
        private Panel panel2;
        private DateTimePicker afterDate_dateTimePicker;
        private DateTimePicker beforeDate_dateTimePicker;
        private Label label2;
        private Label label3;
        private Button createArchive_button;
        private CheckBox hideExistingFiles_checkBox;
        private ContextMenuStrip syncFolderTree_contextMenuStrip;
        private ToolStripMenuItem refreshFolder_menuItem;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem checkAllToolStripMenuItem;
        private ToolStripMenuItem checkNoneToolStripMenuItem;
        private ToolStripMenuItem expandAllToolStripMenuItem;
        private ToolStripMenuItem collapseAllToolStripMenuItem;
        private LinkLabel appVersion_linkLabel;
        private ToolStripMenuItem openFolder_menuItem;
        private Button fogLink_button;
        private TextProgressBar yadiskSpace_progressBar;
        private Panel ProgressLoading_panel;
        public CircularProgressBar.CircularProgressBar MainProgressBar;
        private CheckBox enableProgressPanel_checkBox;
    }
}
