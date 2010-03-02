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
#if DEBUG
			Stopwatch.Mark("Real InitializeComponent");
#endif
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Dosbox Versions");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Software Update");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameListForm));
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
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("About");
			this.imlSmallList = new System.Windows.Forms.ImageList(this.components);
			this.mnuRun = new System.Windows.Forms.ContextMenu();
			this.mnuSepOtherLoc = new System.Windows.Forms.MenuItem();
			this.mnuOtherLoc = new System.Windows.Forms.MenuItem();
			this.mnuProp = new System.Windows.Forms.ContextMenu();
			this.mnuReset = new System.Windows.Forms.MenuItem();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.pnlMain = new System.Windows.Forms.Panel();
			this.tab = new Plain.Forms.TabControlEx();
			this.pageGames = new System.Windows.Forms.TabPage();
			this.lvwGame = new Plain.Forms.ListViewEx();
			this.hdrName = new System.Windows.Forms.ColumnHeader();
			this.hdrDir = new System.Windows.Forms.ColumnHeader();
			this.hdrExe = new System.Windows.Forms.ColumnHeader();
			this.comboButton = new Plain.Forms.ComboButton();
			this.bmpImageList = new DosboxApp.BitmapImageList(this.components);
			this.pageConfig = new System.Windows.Forms.TabPage();
			this.gridConfig = new Plain.Forms.PropertyGridEx();
			this.pageOptions = new System.Windows.Forms.TabPage();
			this.pnlDosboxVersions = new System.Windows.Forms.Panel();
			this.lblNotImplemented = new System.Windows.Forms.Label();
			this.pnlUpdate = new System.Windows.Forms.Panel();
			this.tvwOptions = new Plain.Forms.TreeViewEx();
			this.pageHelp = new System.Windows.Forms.TabPage();
			this.pnlFeedback = new System.Windows.Forms.Panel();
			this.txtComment = new Plain.Forms.TextBoxEx();
			this.txtSendData = new System.Windows.Forms.TextBox();
			this.pnlAbout = new System.Windows.Forms.Panel();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.pnlHelp = new System.Windows.Forms.Panel();
			this.tvwHelp = new Plain.Forms.TreeViewEx();
			this.tbrHelp = new System.Windows.Forms.ToolBar();
			this.sepPreviewFeedback = new System.Windows.Forms.ToolBarButton();
			this.btnPreviewFeedback = new System.Windows.Forms.ToolBarButton();
			this.btnSendFeedback = new System.Windows.Forms.ToolBarButton();
			this.btnEditFeedback = new System.Windows.Forms.ToolBarButton();
			this.sepVisitHomepage = new System.Windows.Forms.ToolBarButton();
			this.btnVisitHomepage = new System.Windows.Forms.ToolBarButton();
			this.mnuSoftwareUpdate = new System.Windows.Forms.ContextMenu();
			this.mnuUpdateManual = new System.Windows.Forms.MenuItem();
			this.mnuUpdateStartup = new System.Windows.Forms.MenuItem();
			this.mnuSepUpdateNotify = new System.Windows.Forms.MenuItem();
			this.mnuUpdateNotify = new System.Windows.Forms.MenuItem();
			this.mnuUpdateInstall = new System.Windows.Forms.MenuItem();
			this.sbr = new Plain.Forms.StatusBarEx();
			this.sbpInfo = new Plain.Forms.StatusBarPanelEx();
			this.pnlTop = new Plain.Forms.RebarPanel();
			this.pnlSearch = new System.Windows.Forms.Panel();
			this.txtSearch = new Plain.Forms.TextBoxEx();
			this.lblSearchAction = new System.Windows.Forms.Label();
			this.tbrTool = new Plain.Forms.ToolBarEx();
			this.btnSoftwareUpdate = new System.Windows.Forms.ToolBarButton();
			this.sepSpan = new System.Windows.Forms.ToolBarButton();
			this.btnSpan = new Plain.Forms.ToolBarButtonEx();
			this.btnDeleteUserFiles = new System.Windows.Forms.ToolBarButton();
			this.tbrProp = new Plain.Forms.ToolBarEx();
			this.btnSortCat = new System.Windows.Forms.ToolBarButton();
			this.btnSortAZ = new System.Windows.Forms.ToolBarButton();
			this.sepUndoConfig = new System.Windows.Forms.ToolBarButton();
			this.btnUndoConfig = new System.Windows.Forms.ToolBarButton();
			this.btnRedoConfig = new System.Windows.Forms.ToolBarButton();
			this.btnSaveConfig = new System.Windows.Forms.ToolBarButton();
			this.sepOpenCapture = new System.Windows.Forms.ToolBarButton();
			this.btnOpenCapture = new System.Windows.Forms.ToolBarButton();
			this.btnOpenKeyMap = new System.Windows.Forms.ToolBarButton();
			this.btnSpanOpenConfigExtern = new Plain.Forms.ToolBarButtonEx();
			this.btnViewConfigExtern = new System.Windows.Forms.ToolBarButton();
			this.btnViewTempConfigExtern = new System.Windows.Forms.ToolBarButton();
			this.tbrAction = new Plain.Forms.ToolBarEx();
			this.btnAddMasterGameFolder = new System.Windows.Forms.ToolBarButton();
			this.btnAddGameFolder = new System.Windows.Forms.ToolBarButton();
			this.btnRemove = new System.Windows.Forms.ToolBarButton();
			this.sepRun = new System.Windows.Forms.ToolBarButton();
			this.btnRun = new System.Windows.Forms.ToolBarButton();
			this.btnPin = new System.Windows.Forms.ToolBarButton();
			this.openFileFolderDialog = new Plain.Forms.OpenFileFolderDialog(this.components);
			this.mnuGame = new System.Windows.Forms.ContextMenu();
			this.mnuRunGame = new System.Windows.Forms.MenuItem();
			this.mnuOpenGameDirectory = new System.Windows.Forms.MenuItem();
			this.mnuSepRemove = new System.Windows.Forms.MenuItem();
			this.mnuRemove = new System.Windows.Forms.MenuItem();
			this.pnlMain.SuspendLayout();
			this.tab.SuspendLayout();
			this.pageGames.SuspendLayout();
			this.lvwGame.SuspendLayout();
			this.pageConfig.SuspendLayout();
			this.pageOptions.SuspendLayout();
			this.pnlDosboxVersions.SuspendLayout();
			this.pageHelp.SuspendLayout();
			this.pnlFeedback.SuspendLayout();
			this.pnlAbout.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.sbpInfo)).BeginInit();
			this.pnlTop.SuspendLayout();
			this.pnlSearch.SuspendLayout();
			this.txtSearch.SuspendLayout();
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
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.tab);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
#if DEBUG
			this.pnlMain.Location = new System.Drawing.Point(0, 149);
#endif
			this.pnlMain.Name = "pnlMain";
#if DEBUG
			this.pnlMain.Size = new System.Drawing.Size(422, 174);
#endif
			this.pnlMain.TabIndex = 3;
			// 
			// tab
			// 
			this.tab.Controls.Add(this.pageGames);
			this.tab.Controls.Add(this.pageConfig);
			this.tab.Controls.Add(this.pageOptions);
			this.tab.Controls.Add(this.pageHelp);
			this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
#if DEBUG
			this.tab.Location = new System.Drawing.Point(0, 0);
#endif
			this.tab.Multiline = true;
			this.tab.Name = "tab";
			this.tab.SelectedIndex = 0;
			this.tab.ShowToolTips = true;
#if DEBUG
			this.tab.Size = new System.Drawing.Size(422, 174);
#endif
			this.tab.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
			this.tab.TabIndex = 2;
			this.tab.SelectedIndexChanged += new System.EventHandler(this.tab_SelectedIndexChanged);
			// 
			// pageGames
			// 
			this.pageGames.Controls.Add(this.lvwGame);
#if DEBUG
			this.pageGames.Location = new System.Drawing.Point(4, 24);
#endif
			this.pageGames.Name = "pageGames";
			this.pageGames.Padding = new System.Windows.Forms.Padding(2);
#if DEBUG
			this.pageGames.Size = new System.Drawing.Size(414, 146);
#endif
			this.pageGames.TabIndex = 0;
			this.pageGames.Text = "Games";
			this.pageGames.ToolTipText = "(Alt+1)";
			this.pageGames.UseVisualStyleBackColor = true;
			// 
			// lvwGame
			// 
			this.lvwGame.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrName,
            this.hdrDir,
            this.hdrExe});
			this.lvwGame.Controls.Add(this.comboButton);
			this.lvwGame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvwGame.EmptyText = "Click on \'Add Master Game Folder\' or \'Add Game Folder\'";
			this.lvwGame.FullRowSelect = true;
			this.lvwGame.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvwGame.HideSelection = false;
#if DEBUG
			this.lvwGame.Location = new System.Drawing.Point(2, 2);
#endif
			this.lvwGame.Name = "lvwGame";
			this.lvwGame.ShowItemToolTips = true;
#if DEBUG
			this.lvwGame.Size = new System.Drawing.Size(410, 142);
#endif
			this.lvwGame.SmallImageList = this.bmpImageList.ImageList;
			this.lvwGame.StateImageList = this.imlSmallList;
			this.lvwGame.TabIndex = 1;
			this.lvwGame.UseCompatibleStateImageBehavior = false;
			this.lvwGame.View = System.Windows.Forms.View.Details;
			this.lvwGame.ItemActivate += new System.EventHandler(this.lvwGame_ItemActivate);
			this.lvwGame.ScrollBegin += new System.EventHandler(this.lvwGame_ScrollBegin);
			this.lvwGame.Scroll += new System.Windows.Forms.ScrollEventHandler(this.lvwGame_Scroll);
			this.lvwGame.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.lvwGame_ColumnWidthChanged);
			this.lvwGame.SelectedIndexChanged += new System.EventHandler(this.lvwGame_SelectedIndexChanged);
			this.lvwGame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvwGame_MouseDown);
			this.lvwGame.ScrollEnd += new System.EventHandler(this.lvwGame_ScrollEnd);
			this.lvwGame.ColumnReordered += new System.Windows.Forms.ColumnReorderedEventHandler(this.lvwGame_ColumnReordered);
			this.lvwGame.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvwGame_ColumnWidthChanging);
			// 
			// hdrName
			// 
			this.hdrName.Text = "Name";
#if DEBUG
			this.hdrName.Width = 200;
#endif
			// 
			// hdrDir
			// 
			this.hdrDir.Text = "Directory";
#if DEBUG
			this.hdrDir.Width = 120;
#endif
			// 
			// hdrExe
			// 
			this.hdrExe.Text = "Executable";
#if DEBUG
			this.hdrExe.Width = 120;
#endif
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
			this.comboButton.ComboBox.DropDown += new System.EventHandler(this.comboButton_ComboBox_DropDown);
			this.comboButton.ComboBox.SelectedIndexChanged += new System.EventHandler(this.comboButton_ComboBox_SelectedIndexChanged);
#if DEBUG
			this.comboButton.Location = new System.Drawing.Point(292, 67);
#endif
			this.comboButton.Name = "comboButton";
#if DEBUG
			this.comboButton.Size = new System.Drawing.Size(17, 21);
#endif
			this.comboButton.TabIndex = 2;
			this.comboButton.Visible = false;
			this.comboButton.LostFocus += new System.EventHandler(this.comboButton_LostFocus);
			// 
			// pageConfig
			// 
			this.pageConfig.Controls.Add(this.gridConfig);
#if DEBUG
			this.pageConfig.Location = new System.Drawing.Point(4, 24);
#endif
			this.pageConfig.Name = "pageConfig";
			this.pageConfig.Padding = new System.Windows.Forms.Padding(2);
#if DEBUG
			this.pageConfig.Size = new System.Drawing.Size(414, 146);
#endif
			this.pageConfig.TabIndex = 1;
			this.pageConfig.Text = "DOSBox Config";
			this.pageConfig.ToolTipText = "(Alt+2)";
			this.pageConfig.UseVisualStyleBackColor = true;
			// 
			// gridConfig
			// 
			this.gridConfig.CategoryForeColor = System.Drawing.SystemColors.GrayText;
			this.gridConfig.ContextMenu = this.mnuProp;
			this.gridConfig.Dock = System.Windows.Forms.DockStyle.Fill;
#if DEBUG
			this.gridConfig.Location = new System.Drawing.Point(2, 2);
#endif
			this.gridConfig.Name = "gridConfig";
			this.gridConfig.PropertySort = System.Windows.Forms.PropertySort.Categorized;
#if DEBUG
			this.gridConfig.Size = new System.Drawing.Size(410, 142);
#endif
			this.gridConfig.TabIndex = 1;
			this.gridConfig.ToolbarVisible = false;
			// 
			// 
			// 
#if DEBUG
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
#endif
			this.gridConfig.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.gridConfig_SelectedGridItemChanged);
			this.gridConfig.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.gridConfig_PropertyValueChanged);
			this.gridConfig.ModifiedChanged += new System.EventHandler(this.gridConfig_ModifiedChanged);
			// 
			// pageOptions
			// 
			this.pageOptions.Controls.Add(this.pnlDosboxVersions);
			this.pageOptions.Controls.Add(this.pnlUpdate);
			this.pageOptions.Controls.Add(this.tvwOptions);
#if DEBUG
			this.pageOptions.Location = new System.Drawing.Point(4, 24);
#endif
			this.pageOptions.Name = "pageOptions";
			this.pageOptions.Padding = new System.Windows.Forms.Padding(2);
#if DEBUG
			this.pageOptions.Size = new System.Drawing.Size(414, 146);
#endif
			this.pageOptions.TabIndex = 2;
			this.pageOptions.Text = "FED Options";
			this.pageOptions.ToolTipText = "(Alt+3)";
			this.pageOptions.UseVisualStyleBackColor = true;
			// 
			// pnlDosboxVersions
			// 
			this.pnlDosboxVersions.Controls.Add(this.lblNotImplemented);
			this.pnlDosboxVersions.Dock = System.Windows.Forms.DockStyle.Fill;
#if DEBUG
			this.pnlDosboxVersions.Location = new System.Drawing.Point(123, 2);
#endif
			this.pnlDosboxVersions.Name = "pnlDosboxVersions";
			this.pnlDosboxVersions.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
#if DEBUG
			this.pnlDosboxVersions.Size = new System.Drawing.Size(289, 142);
#endif
			this.pnlDosboxVersions.TabIndex = 2;
			// 
			// lblNotImplemented
			// 
			this.lblNotImplemented.Dock = System.Windows.Forms.DockStyle.Fill;
#if DEBUG
			this.lblNotImplemented.Location = new System.Drawing.Point(2, 0);
#endif
			this.lblNotImplemented.Name = "lblNotImplemented";
#if DEBUG
			this.lblNotImplemented.Size = new System.Drawing.Size(287, 142);
#endif
			this.lblNotImplemented.TabIndex = 0;
			this.lblNotImplemented.Text = "Not Yet Implemented";
			this.lblNotImplemented.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlUpdate
			// 
			this.pnlUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
#if DEBUG
			this.pnlUpdate.Location = new System.Drawing.Point(123, 2);
#endif
			this.pnlUpdate.Name = "pnlUpdate";
			this.pnlUpdate.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
#if DEBUG
			this.pnlUpdate.Size = new System.Drawing.Size(289, 142);
#endif
			this.pnlUpdate.TabIndex = 1;
			this.pnlUpdate.Visible = false;
			// 
			// tvwOptions
			// 
			this.tvwOptions.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvwOptions.FullRowSelect = true;
			this.tvwOptions.HideSelection = false;
			this.tvwOptions.Indent = 15;
			this.tvwOptions.ItemHeight = 20;
#if DEBUG
			this.tvwOptions.Location = new System.Drawing.Point(2, 2);
#endif
			this.tvwOptions.Name = "tvwOptions";
			treeNode1.ForeColor = System.Drawing.SystemColors.GrayText;
			treeNode1.Name = "nodDosboxVersions";
			treeNode1.Text = "Dosbox Versions";
			treeNode2.Name = "nodUpdate";
			treeNode2.Text = "Software Update";
			this.tvwOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
			this.tvwOptions.ShowLines = false;
			this.tvwOptions.ShowRootLines = false;
			this.tvwOptions.Size = new System.Drawing.Size(121, 142);
			this.tvwOptions.TabIndex = 0;
			this.tvwOptions.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwOptions_BeforeCollapse);
			this.tvwOptions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwOptions_AfterSelect);
			// 
			// pageHelp
			// 
			this.pageHelp.Controls.Add(this.pnlFeedback);
			this.pageHelp.Controls.Add(this.pnlAbout);
			this.pageHelp.Controls.Add(this.pnlHelp);
			this.pageHelp.Controls.Add(this.tvwHelp);
			this.pageHelp.ImageKey = "(none)";
#if DEBUG
			this.pageHelp.Location = new System.Drawing.Point(4, 24);
#endif
			this.pageHelp.Name = "pageHelp";
			this.pageHelp.Padding = new System.Windows.Forms.Padding(2);
#if DEBUG
			this.pageHelp.Size = new System.Drawing.Size(414, 146);
#endif
			this.pageHelp.TabIndex = 3;
			this.pageHelp.Text = "Help";
			this.pageHelp.ToolTipText = "(Alt+4)";
			this.pageHelp.UseVisualStyleBackColor = true;
			// 
			// pnlFeedback
			// 
			this.pnlFeedback.Controls.Add(this.txtComment);
			this.pnlFeedback.Controls.Add(this.txtSendData);
			this.pnlFeedback.Dock = System.Windows.Forms.DockStyle.Fill;
#if DEBUG
			this.pnlFeedback.Location = new System.Drawing.Point(123, 2);
#endif
			this.pnlFeedback.Name = "pnlFeedback";
			this.pnlFeedback.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
#if DEBUG
			this.pnlFeedback.Size = new System.Drawing.Size(289, 142);
#endif
			this.pnlFeedback.TabIndex = 6;
			this.pnlFeedback.Visible = false;
			// 
			// txtComment
			// 
			this.txtComment.CueBanner = "Write your comment here.";
			this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
#if DEBUG
			this.txtComment.Location = new System.Drawing.Point(2, 0);
#endif
			this.txtComment.Multiline = true;
			this.txtComment.Name = "txtComment";
#if DEBUG
			this.txtComment.Size = new System.Drawing.Size(287, 142);
#endif
			this.txtComment.TabIndex = 5;
			// 
			// txtSendData
			// 
			this.txtSendData.Dock = System.Windows.Forms.DockStyle.Fill;
#if DEBUG
			this.txtSendData.Location = new System.Drawing.Point(2, 0);
#endif
			this.txtSendData.Multiline = true;
			this.txtSendData.Name = "txtSendData";
			this.txtSendData.ReadOnly = true;
#if DEBUG
			this.txtSendData.Size = new System.Drawing.Size(287, 142);
#endif
			this.txtSendData.TabIndex = 1;
			this.txtSendData.Visible = false;
			// 
			// pnlAbout
			// 
			this.pnlAbout.Controls.Add(this.lblCopyright);
			this.pnlAbout.Dock = System.Windows.Forms.DockStyle.Fill;
#if DEBUG
			this.pnlAbout.Location = new System.Drawing.Point(123, 2);
#endif
			this.pnlAbout.Name = "pnlAbout";
			this.pnlAbout.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
#if DEBUG
			this.pnlAbout.Size = new System.Drawing.Size(289, 142);
#endif
			this.pnlAbout.TabIndex = 7;
			this.pnlAbout.Visible = false;
			// 
			// lblCopyright
			// 
			this.lblCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
#if DEBUG
			this.lblCopyright.Location = new System.Drawing.Point(2, 0);
#endif
			this.lblCopyright.Name = "lblCopyright";
#if DEBUG
			this.lblCopyright.Size = new System.Drawing.Size(287, 142);
#endif
			this.lblCopyright.TabIndex = 0;
			this.lblCopyright.Text = resources.GetString("lblCopyright.Text");
			// 
			// pnlHelp
			// 
			this.pnlHelp.Dock = System.Windows.Forms.DockStyle.Fill;
#if DEBUG
			this.pnlHelp.Location = new System.Drawing.Point(123, 2);
#endif
			this.pnlHelp.Name = "pnlHelp";
#if DEBUG
			this.pnlHelp.Size = new System.Drawing.Size(289, 142);
#endif
			this.pnlHelp.TabIndex = 4;
			// 
			// tvwHelp
			// 
			this.tvwHelp.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvwHelp.FullRowSelect = true;
			this.tvwHelp.HideSelection = false;
			this.tvwHelp.HotTracking = true;
			this.tvwHelp.Indent = 15;
			this.tvwHelp.ItemHeight = 20;
#if DEBUG
			this.tvwHelp.Location = new System.Drawing.Point(2, 2);
#endif
			this.tvwHelp.Name = "tvwHelp";
			treeNode3.Name = "nodStart";
			treeNode3.Text = "Getting Started";
			treeNode4.Name = "nodAddGame";
			treeNode4.Text = "Adding Games";
			treeNode5.Name = "nodRunGame";
			treeNode5.Text = "Running Games";
			treeNode6.Name = "nodDosboxConfig";
			treeNode6.Text = "Dosbox Configurations";
			treeNode7.Name = "nodMiscOptions";
			treeNode7.Text = "Miscellaneous Options";
			treeNode8.Name = "nodNeedMoreHelp";
			treeNode8.Text = "Needing More Help";
			treeNode9.Name = "nodHelp";
			treeNode9.Text = "Help Topics";
			treeNode10.Name = "nodFeedback";
			treeNode10.Text = "Feedback";
			treeNode11.Name = "nodAbout";
			treeNode11.Text = "About";
			this.tvwHelp.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11});
			this.tvwHelp.ShowLines = false;
			this.tvwHelp.ShowRootLines = false;
			this.tvwHelp.Size = new System.Drawing.Size(121, 142);
			this.tvwHelp.TabIndex = 3;
			this.tvwHelp.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwHelp_BeforeCollapse);
			this.tvwHelp.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwHelp_AfterSelect);
			// 
			// tbrHelp
			// 
			this.tbrHelp.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.tbrHelp.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.sepPreviewFeedback,
            this.btnPreviewFeedback,
            this.btnSendFeedback,
            this.btnEditFeedback,
            this.sepVisitHomepage,
            this.btnVisitHomepage});
			this.tbrHelp.Divider = false;
			this.tbrHelp.DropDownArrows = true;
			this.tbrHelp.ImageList = this.bmpImageList.ImageList;
			this.tbrHelp.Location = new System.Drawing.Point(0, 102);
			this.tbrHelp.Name = "tbrHelp";
			this.tbrHelp.ShowToolTips = true;
#if DEBUG
			this.tbrHelp.Size = new System.Drawing.Size(422, 26);
#endif
			this.tbrHelp.TabIndex = 3;
			this.tbrHelp.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.tbrHelp.Visible = false;
			this.tbrHelp.Wrappable = false;
			this.tbrHelp.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbrHelp_ButtonClick);
			// 
			// sepPreviewFeedback
			// 
			this.sepPreviewFeedback.Name = "sepPreviewFeedback";
			this.sepPreviewFeedback.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// btnPreviewFeedback
			// 
			this.btnPreviewFeedback.ImageKey = "Preview";
			this.btnPreviewFeedback.Name = "btnPreviewFeedback";
			this.btnPreviewFeedback.Text = "Preview";
			this.btnPreviewFeedback.ToolTipText = "Preview outgoing message";
			this.btnPreviewFeedback.Visible = false;
			// 
			// btnSendFeedback
			// 
			this.btnSendFeedback.ImageKey = "Send";
			this.btnSendFeedback.Name = "btnSendFeedback";
			this.btnSendFeedback.ToolTipText = "Send";
			this.btnSendFeedback.Visible = false;
			// 
			// btnEditFeedback
			// 
			this.btnEditFeedback.ImageKey = "Comment";
			this.btnEditFeedback.Name = "btnEditFeedback";
			this.btnEditFeedback.ToolTipText = "Edit";
			// 
			// sepVisitHomepage
			// 
			this.sepVisitHomepage.ImageKey = "(none)";
			this.sepVisitHomepage.Name = "sepVisitHomepage";
			this.sepVisitHomepage.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// btnVisitHomepage
			// 
			this.btnVisitHomepage.ImageKey = "ViewWeb";
			this.btnVisitHomepage.Name = "btnVisitHomepage";
			this.btnVisitHomepage.ToolTipText = "Visit FED Homepage";
			// 
			// mnuSoftwareUpdate
			// 
			this.mnuSoftwareUpdate.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuUpdateManual,
            this.mnuUpdateStartup,
            this.mnuSepUpdateNotify,
            this.mnuUpdateNotify,
            this.mnuUpdateInstall});
			// 
			// mnuUpdateManual
			// 
			this.mnuUpdateManual.Checked = true;
			this.mnuUpdateManual.Index = 0;
			this.mnuUpdateManual.RadioCheck = true;
			this.mnuUpdateManual.Text = "Manually";
			this.mnuUpdateManual.Click += new System.EventHandler(this.mnuUpdateWhen_Click);
			// 
			// mnuUpdateStartup
			// 
			this.mnuUpdateStartup.Index = 1;
			this.mnuUpdateStartup.RadioCheck = true;
			this.mnuUpdateStartup.Text = "Check on Startup";
			this.mnuUpdateStartup.Click += new System.EventHandler(this.mnuUpdateWhen_Click);
			// 
			// mnuSepUpdateNotify
			// 
			this.mnuSepUpdateNotify.Index = 2;
			this.mnuSepUpdateNotify.Text = "-";
			// 
			// mnuUpdateNotify
			// 
			this.mnuUpdateNotify.Checked = true;
			this.mnuUpdateNotify.Index = 3;
			this.mnuUpdateNotify.RadioCheck = true;
			this.mnuUpdateNotify.Text = "When Available, Notify";
			this.mnuUpdateNotify.Click += new System.EventHandler(this.mnuUpdateHow_Click);
			// 
			// mnuUpdateInstall
			// 
			this.mnuUpdateInstall.Index = 4;
			this.mnuUpdateInstall.RadioCheck = true;
			this.mnuUpdateInstall.Text = "When Available, Install";
			this.mnuUpdateInstall.Click += new System.EventHandler(this.mnuUpdateHow_Click);
			// 
			// sbr
			// 
			this.sbr.Location = new System.Drawing.Point(0, 323);
			this.sbr.Name = "sbr";
			this.sbr.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.sbpInfo});
			this.sbr.ResourceImageList = this.bmpImageList;
			this.sbr.ShowPanels = true;
#if DEBUG
			this.sbr.Size = new System.Drawing.Size(422, 25);
#endif
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
			this.pnlTop.Controls.Add(this.tbrHelp);
			this.pnlTop.Controls.Add(this.pnlSearch);
			this.pnlTop.Controls.Add(this.tbrTool);
			this.pnlTop.Controls.Add(this.tbrProp);
			this.pnlTop.Controls.Add(this.tbrAction);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Name = "pnlTop";
#if DEBUG
			this.pnlTop.Size = new System.Drawing.Size(422, 149);
#endif
			this.pnlTop.TabIndex = 4;
			this.pnlTop.Resize += new System.EventHandler(this.pnlTop_Resize);
			// 
			// pnlSearch
			// 
			this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
			this.pnlSearch.Controls.Add(this.txtSearch);
			this.pnlSearch.Cursor = System.Windows.Forms.Cursors.SizeWE;
#if DEBUG
			this.pnlSearch.Location = new System.Drawing.Point(324, 9);
#endif
			this.pnlSearch.Name = "pnlSearch";
			this.pnlSearch.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
			this.pnlSearch.Size = new System.Drawing.Size(95, 35);
			this.pnlSearch.TabIndex = 3;
			this.pnlSearch.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlSearch_MouseMove);
			// 
			// txtSearch
			// 
			this.txtSearch.Controls.Add(this.lblSearchAction);
			this.txtSearch.CueBanner = "Search";
			this.txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtSearch.HideSelection = false;
			this.txtSearch.InnerMargin = new System.Windows.Forms.Padding(2, 0, 22, 0);
			this.txtSearch.Location = new System.Drawing.Point(2, 0);
			this.txtSearch.Name = "txtSearch";
#if DEBUG
			this.txtSearch.Size = new System.Drawing.Size(93, 21);
#endif
			this.txtSearch.TabIndex = 2;
			this.txtSearch.Resize += new System.EventHandler(this.txtSearch_Resize);
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			// 
			// lblSearchAction
			// 
			this.lblSearchAction.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblSearchAction.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblSearchAction.Image = global::DosboxApp.Properties.Resources.Search;
			this.lblSearchAction.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lblSearchAction.Location = new System.Drawing.Point(73, 0);
			this.lblSearchAction.Name = "lblSearchAction";
			this.lblSearchAction.Size = new System.Drawing.Size(16, 17);
			this.lblSearchAction.TabIndex = 3;
			this.lblSearchAction.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblSearchAction_MouseMove);
			this.lblSearchAction.Click += new System.EventHandler(this.lblSearchAction_Click);
			this.lblSearchAction.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblSearchAction_MouseDown);
			this.lblSearchAction.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblSearchAction_MouseUp);
			// 
			// tbrTool
			// 
			this.tbrTool.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.tbrTool.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.btnSoftwareUpdate,
            this.sepSpan,
            this.btnSpan,
            this.btnDeleteUserFiles});
			this.tbrTool.Divider = false;
			this.tbrTool.DropDownArrows = true;
			this.tbrTool.ImageList = this.bmpImageList.ImageList;
			this.tbrTool.Location = new System.Drawing.Point(0, 68);
			this.tbrTool.Name = "tbrTool";
			this.tbrTool.ShowToolTips = true;
#if DEBUG
			this.tbrTool.Size = new System.Drawing.Size(422, 34);
#endif
			this.tbrTool.TabIndex = 3;
			this.tbrTool.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.tbrTool.Visible = false;
			this.tbrTool.Wrappable = false;
			this.tbrTool.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbrTool_ButtonClick);
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
#if DEBUG
			this.btnSpan.Width = 24;
#endif
			// 
			// btnDeleteUserFiles
			// 
			this.btnDeleteUserFiles.Enabled = false;
			this.btnDeleteUserFiles.ImageKey = "DeleteUserFiles";
			this.btnDeleteUserFiles.Name = "btnDeleteUserFiles";
			this.btnDeleteUserFiles.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.btnDeleteUserFiles.Tag = "Delete FED settings file when program exits.";
			this.btnDeleteUserFiles.ToolTipText = "Schedule User File Deletion";
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
            this.sepOpenCapture,
            this.btnOpenCapture,
            this.btnOpenKeyMap,
            this.btnSpanOpenConfigExtern,
            this.btnViewConfigExtern,
            this.btnViewTempConfigExtern});
			this.tbrProp.Divider = false;
			this.tbrProp.DropDownArrows = true;
			this.tbrProp.ImageList = this.bmpImageList.ImageList;
#if DEBUG
			this.tbrProp.Location = new System.Drawing.Point(0, 34);
#endif
			this.tbrProp.Name = "tbrProp";
			this.tbrProp.ShowToolTips = true;
#if DEBUG
			this.tbrProp.Size = new System.Drawing.Size(422, 34);
#endif
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
			this.btnUndoConfig.Text = "Undo";
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
			this.btnSaveConfig.ToolTipText = "Save Configuration (Ctrl+S)";
			// 
			// sepOpenCapture
			// 
			this.sepOpenCapture.Name = "sepOpenCapture";
			this.sepOpenCapture.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// btnOpenCapture
			// 
			this.btnOpenCapture.ImageKey = "OpenCapture";
			this.btnOpenCapture.Name = "btnOpenCapture";
			this.btnOpenCapture.ToolTipText = "Open Capture Folder (Ctrl+P)";
			// 
			// btnOpenKeyMap
			// 
			this.btnOpenKeyMap.ImageKey = "Keyboard";
			this.btnOpenKeyMap.Name = "btnOpenKeyMap";
			this.btnOpenKeyMap.ToolTipText = "Open Key Mapper File (Ctrl+K)";
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
			this.btnViewConfigExtern.ToolTipText = "View Config (Ctrl+E)";
			// 
			// btnViewTempConfigExtern
			// 
			this.btnViewTempConfigExtern.ImageKey = "ConfigTemp";
			this.btnViewTempConfigExtern.Name = "btnViewTempConfigExtern";
			this.btnViewTempConfigExtern.Tag = "View temporary config in Notepad and copy path to clipboard.";
			this.btnViewTempConfigExtern.ToolTipText = "View Temporary Config (Ctrl+T)";
			this.btnViewTempConfigExtern.Visible = false;
			// 
			// tbrAction
			// 
			this.tbrAction.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.tbrAction.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.btnAddMasterGameFolder,
            this.btnAddGameFolder,
            this.btnRemove,
            this.sepRun,
            this.btnRun,
            this.btnPin});
			this.tbrAction.Divider = false;
			this.tbrAction.DropDownArrows = true;
			this.tbrAction.ImageList = this.bmpImageList.ImageList;
			this.tbrAction.Location = new System.Drawing.Point(0, 0);
			this.tbrAction.Name = "tbrAction";
			this.tbrAction.ShowToolTips = true;
#if DEBUG
			this.tbrAction.Size = new System.Drawing.Size(422, 34);
#endif
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
			this.btnAddGameFolder.Text = "Add Game";
			this.btnAddGameFolder.ToolTipText = "Add Game Folder (F3)";
			// 
			// btnRemove
			// 
			this.btnRemove.Enabled = false;
			this.btnRemove.ImageKey = "Delete";
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.ToolTipText = "Remove Selected Game (Delete)";
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
			this.btnPin.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.btnPin.Tag = "Minimize FED to notification area when DOSBox is running.";
			this.btnPin.ToolTipText = "Keep FED in Memory (Scroll Lock)";
#if DEBUG
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
#endif
			// 
			// mnuGame
			// 
			this.mnuGame.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuRunGame,
            this.mnuOpenGameDirectory,
            this.mnuSepRemove,
            this.mnuRemove});
			// 
			// mnuRunGame
			// 
			this.mnuRunGame.DefaultItem = true;
			this.mnuRunGame.Index = 0;
			this.mnuRunGame.Text = "&Run";
			this.mnuRunGame.Click += new System.EventHandler(this.mnuRunGame_Click);
			// 
			// mnuOpenGameDirectory
			// 
			this.mnuOpenGameDirectory.Index = 1;
			this.mnuOpenGameDirectory.Text = "&Open Directory";
			this.mnuOpenGameDirectory.Click += new System.EventHandler(this.mnuOpenGameDirectory_Click);
			// 
			// mnuSepRemove
			// 
			this.mnuSepRemove.Index = 2;
			this.mnuSepRemove.Text = "-";
			// 
			// mnuRemove
			// 
			this.mnuRemove.Index = 3;
			this.mnuRemove.Text = "Re&move";
			// 
			// GameListForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(422, 348);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.sbr);
			this.Controls.Add(this.pnlTop);
#if DEBUG
			this.Font = new System.Drawing.Font("Impact", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
#endif
			this.KeyPreview = true;
			this.Name = "GameListForm";
			this.OpacityOnSizeMove = 0.5;
			this.pnlMain.ResumeLayout(false);
			this.tab.ResumeLayout(false);
			this.pageGames.ResumeLayout(false);
			this.lvwGame.ResumeLayout(false);
			this.lvwGame.PerformLayout();
			this.pageConfig.ResumeLayout(false);
			this.pageOptions.ResumeLayout(false);
			this.pnlDosboxVersions.ResumeLayout(false);
			this.pageHelp.ResumeLayout(false);
			this.pnlFeedback.ResumeLayout(false);
			this.pnlFeedback.PerformLayout();
			this.pnlAbout.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) (this.sbpInfo)).EndInit();
			this.pnlTop.ResumeLayout(false);
			this.pnlTop.PerformLayout();
			this.pnlSearch.ResumeLayout(false);
			this.pnlSearch.PerformLayout();
			this.txtSearch.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private Plain.Forms.ToolBarEx tbrAction;
		private Plain.Forms.PropertyGridEx gridConfig;
        private Plain.Forms.StatusBarEx sbr;
		private System.Windows.Forms.ToolBarButton btnRun;
		private Plain.Forms.ListViewEx lvwGame;
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
		private System.Windows.Forms.Panel pnlHelp;
		private Plain.Forms.TreeViewEx tvwOptions;
		private System.Windows.Forms.ToolBarButton btnSoftwareUpdate;
		private System.Windows.Forms.Panel pnlFeedback;
		private System.Windows.Forms.TextBox txtSendData;
		private BitmapImageList bmpImageList;
		private System.Windows.Forms.ToolBarButton btnDeleteUserFiles;
		private System.Windows.Forms.ToolBarButton sepSpan;
		private Plain.Forms.ToolBarButtonEx btnSpan;
		private System.Windows.Forms.ContextMenu mnuSoftwareUpdate;
		private System.Windows.Forms.MenuItem mnuUpdateStartup;
		private System.Windows.Forms.MenuItem mnuUpdateManual;
		private System.Windows.Forms.Panel pnlSearch;
		private Plain.Forms.ToolBarButtonEx btnSpanOpenConfigExtern;
		private System.Windows.Forms.ToolBarButton btnViewConfigExtern;
		private System.Windows.Forms.ToolBarButton btnViewTempConfigExtern;
		private System.Windows.Forms.ToolBarButton btnPin;
		private Plain.Forms.StatusBarPanelEx sbpInfo;
		private System.Windows.Forms.MenuItem mnuSepUpdateNotify;
		private System.Windows.Forms.MenuItem mnuUpdateNotify;
		private System.Windows.Forms.MenuItem mnuUpdateInstall;
		private System.Windows.Forms.Panel pnlAbout;
		private System.Windows.Forms.Label lblCopyright;
		private Plain.Forms.TextBoxEx txtComment;
		private System.Windows.Forms.ToolBar tbrHelp;
		private System.Windows.Forms.ToolBarButton btnPreviewFeedback;
		private System.Windows.Forms.ToolBarButton btnSendFeedback;
		private System.Windows.Forms.ToolBarButton btnEditFeedback;
		private System.Windows.Forms.ToolBarButton sepPreviewFeedback;
		private System.Windows.Forms.ToolBarButton sepVisitHomepage;
		private System.Windows.Forms.ToolBarButton btnVisitHomepage;
		private System.Windows.Forms.ContextMenu mnuGame;
		private System.Windows.Forms.MenuItem mnuRunGame;
		private System.Windows.Forms.MenuItem mnuOpenGameDirectory;
		private System.Windows.Forms.MenuItem mnuSepRemove;
		private System.Windows.Forms.MenuItem mnuRemove;
		private System.Windows.Forms.ToolBarButton btnRemove;
		private System.Windows.Forms.ColumnHeader hdrDir;
		private System.Windows.Forms.Panel pnlUpdate;
		private System.Windows.Forms.ToolBarButton btnOpenKeyMap;
		private System.Windows.Forms.ToolBarButton sepOpenCapture;
		private System.Windows.Forms.Panel pnlDosboxVersions;
		private System.Windows.Forms.Label lblNotImplemented;
		private System.Windows.Forms.Label lblSearchAction;

    }
}

