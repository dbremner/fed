namespace DosboxApp {
    partial class GameListForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Search Results", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Dosbox Versions");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Software Update");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Getting Started");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Adding Games");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Running Games");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Dosbox Configurations");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Miscellaneous Options");
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Needing More Help");
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Help Topics", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Feedback");
			this.imlSmallList = new System.Windows.Forms.ImageList(this.components);
			this.mnuRun = new System.Windows.Forms.ContextMenu();
			this.mnuSepOtherLoc = new System.Windows.Forms.MenuItem();
			this.mnuOtherLoc = new System.Windows.Forms.MenuItem();
			this.mnuProp = new System.Windows.Forms.ContextMenu();
			this.mnuReset = new System.Windows.Forms.MenuItem();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.pcsDosbox = new System.Diagnostics.Process();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.mnuSoftwareUpdate = new System.Windows.Forms.ContextMenu();
			this.mnuUpdateStartup = new System.Windows.Forms.MenuItem();
			this.mnuUpdateManual = new System.Windows.Forms.MenuItem();
			this.tab = new Plain.Forms.TabControlEx();
			this.pageGames = new System.Windows.Forms.TabPage();
			this.lvwGame = new Plain.Forms.ListViewEx();
			this.hdrName = new System.Windows.Forms.ColumnHeader();
			this.hdrExe = new System.Windows.Forms.ColumnHeader();
			this.comboButton = new Plain.Forms.ComboButton();
			this.bmpImageList = new DosboxApp.BitmapImageList(this.components);
			this.pageConfig = new System.Windows.Forms.TabPage();
			this.gridConfig = new Plain.Forms.PropertyGridEx();
			this.pageOptions = new System.Windows.Forms.TabPage();
			this.tvwOptions = new Plain.Forms.TreeViewEx();
			this.pageHelp = new System.Windows.Forms.TabPage();
			this.pnlEditFeedback = new System.Windows.Forms.Panel();
			this.lblEditFeedback = new System.Windows.Forms.Label();
			this.btnPreviewFeedback = new System.Windows.Forms.Button();
			this.txtComment = new System.Windows.Forms.TextBox();
			this.pnlHelp = new System.Windows.Forms.Panel();
			this.pnlSendFeedback = new System.Windows.Forms.Panel();
			this.btnEditComment = new System.Windows.Forms.Button();
			this.btnSendFeedback = new System.Windows.Forms.Button();
			this.txtSendData = new System.Windows.Forms.TextBox();
			this.lblSendFeedback = new System.Windows.Forms.Label();
			this.tvwHelp = new Plain.Forms.TreeViewEx();
			this.sbr = new Plain.Forms.StatusBarEx();
			this.sbpInfo = new Plain.Forms.StatusBarPanelEx();
			this.pnlTop = new Plain.Forms.RebarPanel();
			this.pnlSearch = new System.Windows.Forms.Panel();
			this.txtSearch = new Plain.Forms.TextBoxEx();
			this.tbrTool = new Plain.Forms.ToolBarEx();
			this.btnOpenCapture = new System.Windows.Forms.ToolBarButton();
			this.sepSpan = new System.Windows.Forms.ToolBarButton();
			this.btnSpan = new Plain.Forms.ToolBarButtonEx();
			this.btnSoftwareUpdate = new System.Windows.Forms.ToolBarButton();
			this.btnDeleteUserFiles = new System.Windows.Forms.ToolBarButton();
			this.tbrProp = new Plain.Forms.ToolBarEx();
			this.btnSortCat = new System.Windows.Forms.ToolBarButton();
			this.btnSortAZ = new System.Windows.Forms.ToolBarButton();
			this.sepUndoConfig = new System.Windows.Forms.ToolBarButton();
			this.btnUndoConfig = new System.Windows.Forms.ToolBarButton();
			this.btnRedoConfig = new System.Windows.Forms.ToolBarButton();
			this.btnSaveConfig = new System.Windows.Forms.ToolBarButton();
			this.btnSpanOpenConfigExtern = new Plain.Forms.ToolBarButtonEx();
			this.btnViewConfigExtern = new System.Windows.Forms.ToolBarButton();
			this.btnViewTempConfigExtern = new System.Windows.Forms.ToolBarButton();
			this.tbrAction = new Plain.Forms.ToolBarEx();
			this.btnAddMasterGameFolder = new System.Windows.Forms.ToolBarButton();
			this.btnAddGameFolder = new System.Windows.Forms.ToolBarButton();
			this.btnDelete = new System.Windows.Forms.ToolBarButton();
			this.sepRun = new System.Windows.Forms.ToolBarButton();
			this.btnRun = new System.Windows.Forms.ToolBarButton();
			this.btnPin = new System.Windows.Forms.ToolBarButton();
			this.openFileFolderDialog = new Plain.Forms.OpenFileFolderDialog(this.components);
			this.pnlMain.SuspendLayout();
			this.tab.SuspendLayout();
			this.pageGames.SuspendLayout();
			this.lvwGame.SuspendLayout();
			this.pageConfig.SuspendLayout();
			this.pageOptions.SuspendLayout();
			this.pageHelp.SuspendLayout();
			this.pnlEditFeedback.SuspendLayout();
			this.pnlSendFeedback.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.sbpInfo)).BeginInit();
			this.pnlTop.SuspendLayout();
			this.pnlSearch.SuspendLayout();
			this.SuspendLayout();
			// 
			// imlSmallList
			// 
			this.imlSmallList.ColorDepth = System.Windows.Forms.ColorDepth.Depth4Bit;
			this.imlSmallList.ImageSize = new System.Drawing.Size(1, 20);
			this.imlSmallList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// mnuRun
			// 
			this.mnuRun.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuSepOtherLoc,
            this.mnuOtherLoc});
			// 
			// mnuSepOtherLoc
			// 
			this.mnuSepOtherLoc.Index = 0;
			this.mnuSepOtherLoc.Text = "-";
			// 
			// mnuOtherLoc
			// 
			this.mnuOtherLoc.Index = 1;
			this.mnuOtherLoc.Text = "(Select Location...)";
			this.mnuOtherLoc.Click += new System.EventHandler(this.mnuOtherLoc_Click);
			// 
			// mnuProp
			// 
			this.mnuProp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuReset});
			this.mnuProp.Popup += new System.EventHandler(this.mnuProp_Popup);
			// 
			// mnuReset
			// 
			this.mnuReset.Index = 0;
			this.mnuReset.Text = "&Reset";
			this.mnuReset.Click += new System.EventHandler(this.mnuReset_Click);
			// 
			// pcsDosbox
			// 
			this.pcsDosbox.StartInfo.Domain = "";
			this.pcsDosbox.StartInfo.LoadUserProfile = false;
			this.pcsDosbox.StartInfo.Password = null;
			this.pcsDosbox.StartInfo.StandardErrorEncoding = null;
			this.pcsDosbox.StartInfo.StandardOutputEncoding = null;
			this.pcsDosbox.StartInfo.UserName = "";
			this.pcsDosbox.SynchronizingObject = this;
			this.pcsDosbox.Exited += new System.EventHandler(this.pcsDosbox_Exited);
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.tab);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 149);
			this.pnlMain.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(422, 195);
			this.pnlMain.TabIndex = 3;
			// 
			// mnuSoftwareUpdate
			// 
			this.mnuSoftwareUpdate.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuUpdateStartup,
            this.mnuUpdateManual});
			// 
			// mnuUpdateStartup
			// 
			this.mnuUpdateStartup.Index = 0;
			this.mnuUpdateStartup.RadioCheck = true;
			this.mnuUpdateStartup.Text = "Check on Startup";
			// 
			// mnuUpdateManual
			// 
			this.mnuUpdateManual.Checked = true;
			this.mnuUpdateManual.Index = 1;
			this.mnuUpdateManual.RadioCheck = true;
			this.mnuUpdateManual.Text = "Manually";
			// 
			// tab
			// 
			this.tab.Controls.Add(this.pageGames);
			this.tab.Controls.Add(this.pageConfig);
			this.tab.Controls.Add(this.pageOptions);
			this.tab.Controls.Add(this.pageHelp);
			this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tab.Location = new System.Drawing.Point(0, 0);
			this.tab.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.tab.Multiline = true;
			this.tab.Name = "tab";
			this.tab.SelectedIndex = 0;
			this.tab.Size = new System.Drawing.Size(422, 195);
			this.tab.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
			this.tab.TabIndex = 2;
			this.tab.SelectedIndexChanged += new System.EventHandler(this.tab_SelectedIndexChanged);
			// 
			// pageGames
			// 
			this.pageGames.Controls.Add(this.lvwGame);
			this.pageGames.Location = new System.Drawing.Point(4, 24);
			this.pageGames.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.pageGames.Name = "pageGames";
			this.pageGames.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.pageGames.Size = new System.Drawing.Size(414, 167);
			this.pageGames.TabIndex = 0;
			this.pageGames.Text = "Games";
			this.pageGames.UseVisualStyleBackColor = true;
			// 
			// lvwGame
			// 
			this.lvwGame.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrName,
            this.hdrExe});
			this.lvwGame.Controls.Add(this.comboButton);
			this.lvwGame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwGame.EmptyText = "Click on \'Add Master Game Folder\' or \'Add Game Folder\'";
			this.lvwGame.FullRowSelect = true;
			listViewGroup1.Header = "Search Results";
			listViewGroup1.Name = null;
			this.lvwGame.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
			this.lvwGame.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvwGame.HideSelection = false;
			this.lvwGame.Location = new System.Drawing.Point(3, 5);
			this.lvwGame.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.lvwGame.Name = "lvwGame";
			this.lvwGame.ShowItemToolTips = true;
			this.lvwGame.Size = new System.Drawing.Size(408, 157);
			this.lvwGame.SmallImageList = this.bmpImageList.ImageList;
			this.lvwGame.StateImageList = this.imlSmallList;
			this.lvwGame.TabIndex = 1;
			this.lvwGame.UseCompatibleStateImageBehavior = false;
			this.lvwGame.View = System.Windows.Forms.View.Details;
			this.lvwGame.EndScroll += new System.EventHandler(this.lvwGame_EndScroll);
			this.lvwGame.ItemActivate += new System.EventHandler(this.lvwGame_ItemActivate);
			this.lvwGame.BeginScroll += new System.EventHandler(this.lvwGame_BeginScroll);
			this.lvwGame.Scroll += new System.Windows.Forms.ScrollEventHandler(this.lvwGame_Scroll);
			this.lvwGame.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvwGame_ColumnWidthChanged);
			this.lvwGame.SelectedIndexChanged += new System.EventHandler(this.lvwGame_SelectedIndexChanged);
			this.lvwGame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwGame_MouseDown);
			this.lvwGame.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvwGame_ColumnWidthChanging);
			this.lvwGame.LostFocus += new System.EventHandler(this.lvwGame_LostFocus);
			// 
			// hdrName
			// 
			this.hdrName.Text = "Name";
			this.hdrName.Width = 240;
			// 
			// hdrExe
			// 
			this.hdrExe.Text = "Executable";
			this.hdrExe.Width = 120;
			// 
			// comboButton
			// 
			this.comboButton.AutoSize = true;
			this.comboButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			// 
			// 
			// 
			this.comboButton.ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboButton.ComboBox.Location = new System.Drawing.Point(-104, -2);
			this.comboButton.ComboBox.Name = "comboBox";
			this.comboButton.ComboBox.TabIndex = 0;
			this.comboButton.ComboBox.SelectedIndexChanged += new System.EventHandler(this.comboButton_ComboBox_SelectedIndexChanged);
			this.comboButton.Location = new System.Drawing.Point(292, 67);
			this.comboButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.comboButton.Name = "comboButton";
			this.comboButton.Size = new System.Drawing.Size(17, 21);
			this.comboButton.TabIndex = 2;
			this.comboButton.Visible = false;
			this.comboButton.LostFocus += new System.EventHandler(this.comboButton_LostFocus);
			// 
			// pageConfig
			// 
			this.pageConfig.Controls.Add(this.gridConfig);
			this.pageConfig.Location = new System.Drawing.Point(4, 22);
			this.pageConfig.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.pageConfig.Name = "pageConfig";
			this.pageConfig.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.pageConfig.Size = new System.Drawing.Size(414, 169);
			this.pageConfig.TabIndex = 1;
			this.pageConfig.Text = "Dosbox Config";
			this.pageConfig.UseVisualStyleBackColor = true;
			// 
			// gridConfig
			// 
			this.gridConfig.CategoryForeColor = System.Drawing.SystemColors.GrayText;
			this.gridConfig.ContextMenu = this.mnuProp;
			this.gridConfig.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridConfig.Location = new System.Drawing.Point(3, 5);
			this.gridConfig.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.gridConfig.Name = "gridConfig";
			this.gridConfig.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.gridConfig.Size = new System.Drawing.Size(408, 159);
			this.gridConfig.TabIndex = 1;
			this.gridConfig.ToolbarVisible = false;
			// 
			// 
			// 
			this.gridConfig.ToolStrip.AccessibleName = "ToolBar";
			this.gridConfig.ToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
			this.gridConfig.ToolStrip.AllowMerge = false;
			this.gridConfig.ToolStrip.AutoSize = false;
			this.gridConfig.ToolStrip.CanOverflow = false;
			this.gridConfig.ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.gridConfig.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.gridConfig.ToolStrip.Location = new System.Drawing.Point(0, 0);
			this.gridConfig.ToolStrip.Name = "";
			this.gridConfig.ToolStrip.Padding = new System.Windows.Forms.Padding(2, 0, 1, 0);
			this.gridConfig.ToolStrip.TabIndex = 1;
			this.gridConfig.ToolStrip.TabStop = true;
			this.gridConfig.ToolStrip.Text = "PropertyGridToolBar";
			this.gridConfig.ToolStrip.Visible = false;
			this.gridConfig.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.gridConfig_SelectedGridItemChanged);
			this.gridConfig.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.gridConfig_PropertyValueChanged);
			this.gridConfig.ModifiedChanged += new System.EventHandler(this.gridConfig_ModifiedChanged);
			// 
			// pageOptions
			// 
			this.pageOptions.Controls.Add(this.tvwOptions);
			this.pageOptions.Location = new System.Drawing.Point(4, 22);
			this.pageOptions.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.pageOptions.Name = "pageOptions";
			this.pageOptions.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.pageOptions.Size = new System.Drawing.Size(414, 169);
			this.pageOptions.TabIndex = 2;
			this.pageOptions.Text = "FED Options";
			this.pageOptions.UseVisualStyleBackColor = true;
			// 
			// tvwOptions
			// 
			this.tvwOptions.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvwOptions.FullRowSelect = true;
			this.tvwOptions.HideSelection = false;
			this.tvwOptions.Indent = 15;
			this.tvwOptions.ItemHeight = 20;
			this.tvwOptions.Location = new System.Drawing.Point(3, 5);
			this.tvwOptions.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.tvwOptions.Name = "tvwOptions";
			treeNode1.ImageKey = "dosbox.ico";
			treeNode1.Name = "Node0";
			treeNode1.SelectedImageKey = "dosbox.ico";
			treeNode1.Text = "Dosbox Versions";
			treeNode2.ImageKey = "update.ico";
			treeNode2.Name = "Node1";
			treeNode2.SelectedImageKey = "update.ico";
			treeNode2.Text = "Software Update";
			this.tvwOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
			this.tvwOptions.ShowLines = false;
			this.tvwOptions.ShowRootLines = false;
			this.tvwOptions.Size = new System.Drawing.Size(121, 159);
			this.tvwOptions.TabIndex = 0;
			// 
			// pageHelp
			// 
			this.pageHelp.Controls.Add(this.pnlEditFeedback);
			this.pageHelp.Controls.Add(this.pnlHelp);
			this.pageHelp.Controls.Add(this.pnlSendFeedback);
			this.pageHelp.Controls.Add(this.tvwHelp);
			this.pageHelp.ImageKey = "(none)";
			this.pageHelp.Location = new System.Drawing.Point(4, 22);
			this.pageHelp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.pageHelp.Name = "pageHelp";
			this.pageHelp.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.pageHelp.Size = new System.Drawing.Size(414, 169);
			this.pageHelp.TabIndex = 3;
			this.pageHelp.Text = "Help";
			this.pageHelp.UseVisualStyleBackColor = true;
			// 
			// pnlEditFeedback
			// 
			this.pnlEditFeedback.Controls.Add(this.lblEditFeedback);
			this.pnlEditFeedback.Controls.Add(this.btnPreviewFeedback);
			this.pnlEditFeedback.Controls.Add(this.txtComment);
			this.pnlEditFeedback.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlEditFeedback.Location = new System.Drawing.Point(124, 5);
			this.pnlEditFeedback.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.pnlEditFeedback.Name = "pnlEditFeedback";
			this.pnlEditFeedback.Size = new System.Drawing.Size(287, 159);
			this.pnlEditFeedback.TabIndex = 5;
			this.pnlEditFeedback.Visible = false;
			// 
			// lblEditFeedback
			// 
			this.lblEditFeedback.AutoSize = true;
			this.lblEditFeedback.Location = new System.Drawing.Point(5, 5);
			this.lblEditFeedback.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblEditFeedback.Name = "lblEditFeedback";
			this.lblEditFeedback.Size = new System.Drawing.Size(131, 15);
			this.lblEditFeedback.TabIndex = 2;
			this.lblEditFeedback.Text = "Write your comment below:";
			// 
			// btnPreviewFeedback
			// 
			this.btnPreviewFeedback.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPreviewFeedback.Location = new System.Drawing.Point(217, 129);
			this.btnPreviewFeedback.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.btnPreviewFeedback.Name = "btnPreviewFeedback";
			this.btnPreviewFeedback.Size = new System.Drawing.Size(62, 27);
			this.btnPreviewFeedback.TabIndex = 1;
			this.btnPreviewFeedback.Text = "Preview";
			this.btnPreviewFeedback.UseVisualStyleBackColor = true;
			this.btnPreviewFeedback.Click += new System.EventHandler(this.btnPreviewFeedback_Click);
			// 
			// txtComment
			// 
			this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtComment.HideSelection = false;
			this.txtComment.Location = new System.Drawing.Point(5, 24);
			this.txtComment.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.txtComment.Multiline = true;
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(276, 97);
			this.txtComment.TabIndex = 0;
			// 
			// pnlHelp
			// 
			this.pnlHelp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlHelp.Location = new System.Drawing.Point(124, 5);
			this.pnlHelp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.pnlHelp.Name = "pnlHelp";
			this.pnlHelp.Size = new System.Drawing.Size(287, 159);
			this.pnlHelp.TabIndex = 4;
			// 
			// pnlSendFeedback
			// 
			this.pnlSendFeedback.Controls.Add(this.btnEditComment);
			this.pnlSendFeedback.Controls.Add(this.btnSendFeedback);
			this.pnlSendFeedback.Controls.Add(this.txtSendData);
			this.pnlSendFeedback.Controls.Add(this.lblSendFeedback);
			this.pnlSendFeedback.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlSendFeedback.Location = new System.Drawing.Point(124, 5);
			this.pnlSendFeedback.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.pnlSendFeedback.Name = "pnlSendFeedback";
			this.pnlSendFeedback.Size = new System.Drawing.Size(287, 159);
			this.pnlSendFeedback.TabIndex = 6;
			this.pnlSendFeedback.Visible = false;
			// 
			// btnEditComment
			// 
			this.btnEditComment.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEditComment.Location = new System.Drawing.Point(150, 129);
			this.btnEditComment.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.btnEditComment.Name = "btnEditComment";
			this.btnEditComment.Size = new System.Drawing.Size(62, 27);
			this.btnEditComment.TabIndex = 3;
			this.btnEditComment.Text = "Edit";
			this.btnEditComment.UseVisualStyleBackColor = true;
			this.btnEditComment.Click += new System.EventHandler(this.btnEditComment_Click);
			// 
			// btnSendFeedback
			// 
			this.btnSendFeedback.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSendFeedback.Location = new System.Drawing.Point(217, 129);
			this.btnSendFeedback.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.btnSendFeedback.Name = "btnSendFeedback";
			this.btnSendFeedback.Size = new System.Drawing.Size(62, 27);
			this.btnSendFeedback.TabIndex = 2;
			this.btnSendFeedback.Text = "Send";
			this.btnSendFeedback.UseVisualStyleBackColor = true;
			this.btnSendFeedback.Click += new System.EventHandler(this.btnSendFeedback_Click);
			// 
			// txtSendData
			// 
			this.txtSendData.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSendData.Location = new System.Drawing.Point(5, 24);
			this.txtSendData.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.txtSendData.Multiline = true;
			this.txtSendData.Name = "txtSendData";
			this.txtSendData.ReadOnly = true;
			this.txtSendData.Size = new System.Drawing.Size(276, 97);
			this.txtSendData.TabIndex = 1;
			// 
			// lblSendFeedback
			// 
			this.lblSendFeedback.AutoSize = true;
			this.lblSendFeedback.Location = new System.Drawing.Point(2, 5);
			this.lblSendFeedback.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblSendFeedback.Name = "lblSendFeedback";
			this.lblSendFeedback.Size = new System.Drawing.Size(229, 15);
			this.lblSendFeedback.TabIndex = 0;
			this.lblSendFeedback.Text = "The following data will be sent to the developer:";
			// 
			// tvwHelp
			// 
			this.tvwHelp.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvwHelp.FullRowSelect = true;
			this.tvwHelp.HideSelection = false;
			this.tvwHelp.HotTracking = true;
			this.tvwHelp.Indent = 15;
			this.tvwHelp.ItemHeight = 20;
			this.tvwHelp.Location = new System.Drawing.Point(3, 5);
			this.tvwHelp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.tvwHelp.Name = "tvwHelp";
			treeNode3.ImageKey = "helpfile.ico";
			treeNode3.Name = "Node3";
			treeNode3.SelectedImageKey = "helpfile.ico";
			treeNode3.Text = "Getting Started";
			treeNode4.ImageKey = "helpfile.ico";
			treeNode4.Name = "Node4";
			treeNode4.SelectedImageKey = "helpfile.ico";
			treeNode4.Text = "Adding Games";
			treeNode5.ImageKey = "helpfile.ico";
			treeNode5.Name = "Node5";
			treeNode5.SelectedImageKey = "helpfile.ico";
			treeNode5.Text = "Running Games";
			treeNode6.ImageKey = "helpfile.ico";
			treeNode6.Name = "Node6";
			treeNode6.SelectedImageKey = "helpfile.ico";
			treeNode6.Text = "Dosbox Configurations";
			treeNode7.ImageKey = "helpfile.ico";
			treeNode7.Name = "Node7";
			treeNode7.SelectedImageKey = "helpfile.ico";
			treeNode7.Text = "Miscellaneous Options";
			treeNode8.ImageKey = "helpfile.ico";
			treeNode8.Name = "Node8";
			treeNode8.SelectedImageKey = "helpfile.ico";
			treeNode8.Text = "Needing More Help";
			treeNode9.ImageKey = "helptoc.ico";
			treeNode9.Name = "nodHelp";
			treeNode9.SelectedImageKey = "helptoc.ico";
			treeNode9.Text = "Help Topics";
			treeNode10.ImageKey = "CommentHS0.ico";
			treeNode10.Name = "nodFeedback";
			treeNode10.SelectedImageKey = "CommentHS0.ico";
			treeNode10.Text = "Feedback";
			this.tvwHelp.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
			this.tvwHelp.ShowLines = false;
			this.tvwHelp.ShowRootLines = false;
			this.tvwHelp.Size = new System.Drawing.Size(121, 159);
			this.tvwHelp.TabIndex = 3;
			this.tvwHelp.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwHelp_BeforeCollapse);
			this.tvwHelp.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwHelp_AfterSelect);
			// 
			// sbr
			// 
			this.sbr.Location = new System.Drawing.Point(0, 344);
			this.sbr.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.sbr.Name = "sbr";
			this.sbr.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.sbpInfo});
			this.sbr.ResourceImageList = this.bmpImageList;
			this.sbr.ShowPanels = true;
			this.sbr.Size = new System.Drawing.Size(422, 25);
			this.sbr.TabIndex = 2;
			// 
			// sbpInfo
			// 
			this.sbpInfo.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.sbpInfo.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.sbpInfo.ImageKey = null;
			this.sbpInfo.Name = "sbpInfo";
			this.sbpInfo.Width = 405;
			// 
			// pnlTop
			// 
			this.pnlTop.Controls.Add(this.pnlSearch);
			this.pnlTop.Controls.Add(this.tbrTool);
			this.pnlTop.Controls.Add(this.tbrProp);
			this.pnlTop.Controls.Add(this.tbrAction);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(422, 149);
			this.pnlTop.TabIndex = 4;
			this.pnlTop.Resize += new System.EventHandler(this.pnlTop_Resize);
			// 
			// pnlSearch
			// 
			this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
			this.pnlSearch.Controls.Add(this.txtSearch);
			this.pnlSearch.Cursor = System.Windows.Forms.Cursors.SizeWE;
			this.pnlSearch.Location = new System.Drawing.Point(324, 9);
			this.pnlSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.pnlSearch.Name = "pnlSearch";
			this.pnlSearch.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
			this.pnlSearch.Size = new System.Drawing.Size(95, 25);
			this.pnlSearch.TabIndex = 3;
			this.pnlSearch.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlSearch_MouseMove);
			// 
			// txtSearch
			// 
			this.txtSearch.CueBanner = "Search";
			this.txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtSearch.HideSelection = false;
			this.txtSearch.InnerMargins = new Plain.Forms.EditMargins(0, 0);
			this.txtSearch.Location = new System.Drawing.Point(2, 0);
			this.txtSearch.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(93, 21);
			this.txtSearch.TabIndex = 2;
			this.txtSearch.Resize += new System.EventHandler(this.txtSearch_Resize);
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			// 
			// tbrTool
			// 
			this.tbrTool.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.tbrTool.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.btnOpenCapture,
            this.sepSpan,
            this.btnSpan,
            this.btnSoftwareUpdate,
            this.btnDeleteUserFiles});
			this.tbrTool.DropDownArrows = true;
			this.tbrTool.ImageList = this.bmpImageList.ImageList;
			this.tbrTool.Location = new System.Drawing.Point(0, 72);
			this.tbrTool.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.tbrTool.Name = "tbrTool";
			this.tbrTool.ShowToolTips = true;
			this.tbrTool.Size = new System.Drawing.Size(422, 36);
			this.tbrTool.TabIndex = 3;
			this.tbrTool.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.tbrTool.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbrTool_ButtonClick);
			// 
			// btnOpenCapture
			// 
			this.btnOpenCapture.ImageKey = "OpenCapture";
			this.btnOpenCapture.Name = "btnOpenCapture";
			this.btnOpenCapture.ToolTipText = "Open Capture Folder (F4)";
			// 
			// sepSpan
			// 
			this.sepSpan.Name = "sepSpan";
			this.sepSpan.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// btnSpan
			// 
			this.btnSpan.Enabled = false;
			this.btnSpan.Name = "btnSpan";
			this.btnSpan.Spring = true;
			this.btnSpan.Width = 290;
			// 
			// btnSoftwareUpdate
			// 
			this.btnSoftwareUpdate.DropDownMenu = this.mnuSoftwareUpdate;
			this.btnSoftwareUpdate.ImageKey = "SoftwareUpdate";
			this.btnSoftwareUpdate.Name = "btnSoftwareUpdate";
			this.btnSoftwareUpdate.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			this.btnSoftwareUpdate.Text = "Update";
			this.btnSoftwareUpdate.ToolTipText = "Check for Update";
			// 
			// btnDeleteUserFiles
			// 
			this.btnDeleteUserFiles.ImageKey = "DeleteUserFiles";
			this.btnDeleteUserFiles.Name = "btnDeleteUserFiles";
			this.btnDeleteUserFiles.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.btnDeleteUserFiles.ToolTipText = "Schedule User Files Deletion";
			// 
			// tbrProp
			// 
			this.tbrProp.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.tbrProp.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.btnSortCat,
            this.btnSortAZ,
            this.sepUndoConfig,
            this.btnUndoConfig,
            this.btnRedoConfig,
            this.btnSaveConfig,
            this.btnSpanOpenConfigExtern,
            this.btnViewConfigExtern,
            this.btnViewTempConfigExtern});
			this.tbrProp.DropDownArrows = true;
			this.tbrProp.ImageList = this.bmpImageList.ImageList;
			this.tbrProp.Location = new System.Drawing.Point(0, 36);
			this.tbrProp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.tbrProp.Name = "tbrProp";
			this.tbrProp.ShowToolTips = true;
			this.tbrProp.Size = new System.Drawing.Size(422, 36);
			this.tbrProp.TabIndex = 2;
			this.tbrProp.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.tbrProp.Visible = false;
			this.tbrProp.Wrappable = false;
			this.tbrProp.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbrProp_ButtonClick);
			// 
			// btnSortCat
			// 
			this.btnSortCat.ImageKey = "SortCat";
			this.btnSortCat.Name = "btnSortCat";
			this.btnSortCat.Pushed = true;
			this.btnSortCat.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.btnSortCat.ToolTipText = "Categorized";
			// 
			// btnSortAZ
			// 
			this.btnSortAZ.ImageKey = "SortAZ";
			this.btnSortAZ.Name = "btnSortAZ";
			this.btnSortAZ.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.btnSortAZ.ToolTipText = "Alphabetical";
			// 
			// sepUndoConfig
			// 
			this.sepUndoConfig.Name = "sepUndoConfig";
			this.sepUndoConfig.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// btnUndoConfig
			// 
			this.btnUndoConfig.Enabled = false;
			this.btnUndoConfig.ImageKey = "Undo";
			this.btnUndoConfig.Name = "btnUndoConfig";
			this.btnUndoConfig.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			this.btnUndoConfig.ToolTipText = "Undo Configuration Change (Ctrl+Z)";
			// 
			// btnRedoConfig
			// 
			this.btnRedoConfig.Enabled = false;
			this.btnRedoConfig.ImageKey = "Redo";
			this.btnRedoConfig.Name = "btnRedoConfig";
			this.btnRedoConfig.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			this.btnRedoConfig.ToolTipText = "Redo Configuration Change (Ctrl+Y)";
			// 
			// btnSaveConfig
			// 
			this.btnSaveConfig.ImageKey = "Save";
			this.btnSaveConfig.Name = "btnSaveConfig";
			this.btnSaveConfig.Text = "Save";
			this.btnSaveConfig.ToolTipText = "Save Current Configuration (Ctrl+S)";
			// 
			// btnSpanOpenConfigExtern
			// 
			this.btnSpanOpenConfigExtern.Enabled = false;
			this.btnSpanOpenConfigExtern.Name = "btnSpanOpenConfigExtern";
			this.btnSpanOpenConfigExtern.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			this.btnSpanOpenConfigExtern.Width = 4;
			// 
			// btnViewConfigExtern
			// 
			this.btnViewConfigExtern.ImageKey = "Config";
			this.btnViewConfigExtern.Name = "btnViewConfigExtern";
			this.btnViewConfigExtern.Tag = "View config in Notepad and copy path to clipboard.";
			this.btnViewConfigExtern.ToolTipText = "View Config";
			// 
			// btnViewTempConfigExtern
			// 
			this.btnViewTempConfigExtern.ImageKey = "ConfigTemp";
			this.btnViewTempConfigExtern.Name = "btnViewTempConfigExtern";
			this.btnViewTempConfigExtern.Tag = "View temporary config in Notepad and copy path to clipboard.";
			this.btnViewTempConfigExtern.ToolTipText = "View Temporary Config";
			this.btnViewTempConfigExtern.Visible = false;
			// 
			// tbrAction
			// 
			this.tbrAction.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.tbrAction.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.btnAddMasterGameFolder,
            this.btnAddGameFolder,
            this.btnDelete,
            this.sepRun,
            this.btnRun,
            this.btnPin});
			this.tbrAction.DropDownArrows = true;
			this.tbrAction.ImageList = this.bmpImageList.ImageList;
			this.tbrAction.Location = new System.Drawing.Point(0, 0);
			this.tbrAction.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.tbrAction.Name = "tbrAction";
			this.tbrAction.ShowToolTips = true;
			this.tbrAction.Size = new System.Drawing.Size(422, 36);
			this.tbrAction.TabIndex = 0;
			this.tbrAction.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.tbrAction.Wrappable = false;
			this.tbrAction.MouseLeave += new System.EventHandler(this.tbrAction_MouseLeave);
			this.tbrAction.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tbrAction_MouseMove);
			this.tbrAction.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbrAction_ButtonClick);
			this.tbrAction.Resize += new System.EventHandler(this.tbrAction_Resize);
			// 
			// btnAddMasterGameFolder
			// 
			this.btnAddMasterGameFolder.ImageKey = "AddMasterGameFolder";
			this.btnAddMasterGameFolder.Name = "btnAddMasterGameFolder";
			this.btnAddMasterGameFolder.ToolTipText = "Add Master Game Folder (F2)";
			// 
			// btnAddGameFolder
			// 
			this.btnAddGameFolder.ImageKey = "AddGameFolder";
			this.btnAddGameFolder.Name = "btnAddGameFolder";
			this.btnAddGameFolder.ToolTipText = "Add Game Folder (F3)";
			// 
			// btnDelete
			// 
			this.btnDelete.Enabled = false;
			this.btnDelete.ImageKey = "Delete";
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.ToolTipText = "Delete Selected Game (Delete)";
			// 
			// sepRun
			// 
			this.sepRun.Name = "sepRun";
			this.sepRun.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// btnRun
			// 
			this.btnRun.DropDownMenu = this.mnuRun;
			this.btnRun.ImageKey = "dosbox";
			this.btnRun.Name = "btnRun";
			this.btnRun.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
			this.btnRun.Tag = "Select a game to run.";
			this.btnRun.Text = "Run";
			this.btnRun.ToolTipText = "Run Selected Game (F5)";
			// 
			// btnPin
			// 
			this.btnPin.ImageKey = "Pin";
			this.btnPin.Name = "btnPin";
			this.btnPin.Pushed = true;
			this.btnPin.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.btnPin.Tag = "Minimize the GUI to notification area when DOSBox is running.";
			this.btnPin.ToolTipText = "Keep GUI in Memory";
			// 
			// openFileFolderDialog
			// 
			// 
			// 
			// 
			this.openFileFolderDialog.OpenFileDialog.AddExtension = false;
			this.openFileFolderDialog.OpenFileDialog.CheckFileExists = false;
			this.openFileFolderDialog.OpenFileDialog.FileName = " ";
			this.openFileFolderDialog.OpenFileDialog.Filter = "Folders|*.arbitrary-unique-{401D4D69-935B-452b-9C73-DEAA59526D7F}";
			// 
			// GameListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(422, 369);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.sbr);
			this.Controls.Add(this.pnlTop);
			this.Font = new System.Drawing.Font("Impact", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.Name = "GameListForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "FED - FrontEnd for DOSBox";
			this.pnlMain.ResumeLayout(false);
			this.tab.ResumeLayout(false);
			this.pageGames.ResumeLayout(false);
			this.lvwGame.ResumeLayout(false);
			this.lvwGame.PerformLayout();
			this.pageConfig.ResumeLayout(false);
			this.pageOptions.ResumeLayout(false);
			this.pageHelp.ResumeLayout(false);
			this.pnlEditFeedback.ResumeLayout(false);
			this.pnlEditFeedback.PerformLayout();
			this.pnlSendFeedback.ResumeLayout(false);
			this.pnlSendFeedback.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.sbpInfo)).EndInit();
			this.pnlTop.ResumeLayout(false);
			this.pnlTop.PerformLayout();
			this.pnlSearch.ResumeLayout(false);
			this.pnlSearch.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		private Plain.Forms.ToolBarEx tbrAction;
		private Plain.Forms.PropertyGridEx gridConfig;
        private Plain.Forms.StatusBarEx sbr;
		private System.Windows.Forms.ToolBarButton btnRun;
		internal Plain.Forms.ListViewEx lvwGame;
		private Plain.Forms.ToolBarEx tbrProp;
        private System.Windows.Forms.ToolBarButton btnSortCat;
        private System.Windows.Forms.ToolBarButton btnSortAZ;
		private Plain.Forms.TextBoxEx txtSearch;
        private System.Windows.Forms.ColumnHeader hdrName;
        private System.Windows.Forms.ColumnHeader hdrExe;
        private System.Windows.Forms.ImageList imlSmallList;
        private System.Windows.Forms.ContextMenu mnuRun;
        private System.Windows.Forms.MenuItem mnuSepOtherLoc;
        private System.Windows.Forms.MenuItem mnuOtherLoc;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ToolBarButton sepUndoConfig;
		private System.Windows.Forms.ToolBarButton btnUndoConfig;
		private System.Windows.Forms.ToolBarButton btnRedoConfig;
		private System.Windows.Forms.ContextMenu mnuProp;
		private System.Windows.Forms.MenuItem mnuReset;
		private System.Windows.Forms.ToolBarButton btnAddMasterGameFolder;
		private System.Windows.Forms.ToolBarButton btnAddGameFolder;
		private System.Windows.Forms.ToolBarButton sepRun;
		private System.Windows.Forms.ToolBarButton btnDelete;
		private System.Diagnostics.Process pcsDosbox;
		private Plain.Forms.OpenFileFolderDialog openFileFolderDialog;
		private System.Windows.Forms.Panel pnlMain;
		private Plain.Forms.TabControlEx tab;
		private System.Windows.Forms.TabPage pageGames;
		private System.Windows.Forms.TabPage pageConfig;
		private Plain.Forms.RebarPanel pnlTop;
		private System.Windows.Forms.ToolBarButton btnSaveConfig;
		private Plain.Forms.ComboButton comboButton;
		private System.Windows.Forms.TabPage pageOptions;
		private Plain.Forms.ToolBarEx tbrTool;
		private System.Windows.Forms.ToolBarButton btnOpenCapture;
		private System.Windows.Forms.TabPage pageHelp;
		private Plain.Forms.TreeViewEx tvwHelp;
		private System.Windows.Forms.Panel pnlEditFeedback;
		private System.Windows.Forms.Panel pnlHelp;
		private Plain.Forms.TreeViewEx tvwOptions;
		private System.Windows.Forms.ToolBarButton btnSoftwareUpdate;
		private System.Windows.Forms.Button btnPreviewFeedback;
		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.Panel pnlSendFeedback;
		private System.Windows.Forms.Button btnEditComment;
		private System.Windows.Forms.Button btnSendFeedback;
		private System.Windows.Forms.TextBox txtSendData;
		private System.Windows.Forms.Label lblSendFeedback;
		private System.Windows.Forms.Label lblEditFeedback;
		private BitmapImageList bmpImageList;
		private System.Windows.Forms.ToolBarButton btnDeleteUserFiles;
		private System.Windows.Forms.ToolBarButton sepSpan;
		private Plain.Forms.ToolBarButtonEx btnSpan;
		private System.Windows.Forms.ContextMenu mnuSoftwareUpdate;
		private System.Windows.Forms.MenuItem mnuUpdateStartup;
		private System.Windows.Forms.MenuItem mnuUpdateManual;
		internal System.Windows.Forms.Panel pnlSearch;
		private Plain.Forms.ToolBarButtonEx btnSpanOpenConfigExtern;
		private System.Windows.Forms.ToolBarButton btnViewConfigExtern;
		private System.Windows.Forms.ToolBarButton btnViewTempConfigExtern;
		private System.Windows.Forms.ToolBarButton btnPin;
		private Plain.Forms.StatusBarPanelEx sbpInfo;

    }
}

