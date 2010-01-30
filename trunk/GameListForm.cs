using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Plain.Forms;
using Plain.IO;
using Plain.Native;
using Plain.Design;
using DosboxApp.Properties;

namespace DosboxApp {
	public partial class GameListForm : Form {
		public GameListForm() {
			InitializeComponent();
		}

		bool m_IsSelectionScrolling;

		protected override void OnHandleCreated(EventArgs e) {
			base.OnHandleCreated(e);
			base.Icon = Resources.Game;
			notifyIcon.Icon = Resources.Game;
		}

		protected override void OnSystemColorsChanged(EventArgs e) {
			base.OnSystemColorsChanged(e);
			themeToMatchFonts();
			themeToMatchDropDown();
			themeToMatchUserPref();
		}

		protected override void OnActivated(EventArgs e) {
			base.OnActivated(e);
#if false
			if (Environment.OSVersion.Version >= NativeMethods.WindowsVista) {
				if (VisualStyleInformation.IsSupportedByOS && VisualStyleInformation.IsEnabledByUser) {
					try {
						NativeMethods.MARGINS margins = new NativeMethods.MARGINS();
						margins.cxLeftWidth = 0;
						margins.cxRightWidth = 0;
						margins.cyBottomHeight = 0;
						margins.cyTopHeight = tbrAction.Height;
						if (0 <= NativeMethods.DwmExtendFrameIntoClientArea(this.Handle, ref margins)) {
							pnlTop.BackColor = Color.Black;
						}
					}
					catch (DllNotFoundException) { }
				}
			}
#endif
		}

		protected override void OnLoad(EventArgs e) {
			DateTime d0 = DateTime.Now;
			base.OnLoad(e);
			DateTime d1 = DateTime.Now;
			System.Diagnostics.Debug.WriteLine(d1 - d0);
			d0 = DateTime.Now;

			themeToMatchFonts();
			themeToMatchDropDown();
			themeToMatchUserPref();
			Program.AppConfig.GameListFormConfig.LoadTo(this);
			listFromHash(lvwGame, Program.AppConfig.GameConfig.Games);

			d1 = DateTime.Now;
			System.Diagnostics.Debug.WriteLine(d1 - d0);
			d0 = DateTime.Now;

			DosboxInfo dinfo = loadDosboxVersions();
			if (dinfo != null) {
				gridConfig.SelectedObject = dinfo.LoadConfig();
			}

			d1 = DateTime.Now;
			System.Diagnostics.Debug.WriteLine(d1 - d0);

			tvwHelp.ExpandAll();
#if false
			tab.TabPages.Remove(pageOptions);
			tab.TabPages.Remove(pageHelp);
#endif
		}

		protected override void OnKeyDown(KeyEventArgs e) {
			base.OnKeyDown(e);
			switch (tab.SelectedIndex) {
			case 0:
				switch (e.KeyData) {
				case Keys.F2:
				case Keys.Control | Keys.Shift | Keys.O:
					tbrAction_ButtonClick(tbrAction, new ToolBarButtonClickEventArgs(btnAddMasterGameFolder));
					break;
				case Keys.F3:
				case Keys.Control | Keys.O:
					tbrAction_ButtonClick(tbrAction, new ToolBarButtonClickEventArgs(btnAddGameFolder));
					break;
				case Keys.Delete:
					tbrAction_ButtonClick(tbrAction, new ToolBarButtonClickEventArgs(btnDelete));
					break;
				case Keys.F5:
					tbrAction_ButtonClick(tbrAction, new ToolBarButtonClickEventArgs(btnRun));
					break;
				}
				break;
			case 1:
				switch (e.KeyData) {
				case Keys.Control | Keys.Z:
					tbrProp_ButtonClick(tbrProp, new ToolBarButtonClickEventArgs(btnUndoConfig));
					break;
				case Keys.Control | Keys.Y:
					tbrProp_ButtonClick(tbrProp, new ToolBarButtonClickEventArgs(btnRedoConfig));
					break;
				case Keys.Control | Keys.S:
					tbrProp_ButtonClick(tbrProp, new ToolBarButtonClickEventArgs(btnSaveConfig));
					break;
				}
				break;
			case 2:
				break;
			case 3:
				break;
			}
		}

		protected override void OnFormClosed(FormClosedEventArgs e) {
			base.OnFormClosed(e);
			this.Visible = false;
			Program.AppConfig.GameListFormConfig.SaveFrom(this);
		}

		void themeToMatchFonts() {
			this.Font = SystemFonts.IconTitleFont;
			sbr.Font = SystemFonts.StatusFont;
			imlSmallList.ImageSize = new Size(1, comboButton.Height - 1);
			tvwOptions.ItemHeight = comboButton.Height - 1;
			tvwHelp.ItemHeight = comboButton.Height - 1;
		}

		void themeToMatchDropDown() {
			if (Environment.OSVersion.Version >= NativeMethods.WindowsVista) {
				tbrAction.Divider = false;
				tbrProp.Divider = false;
				tbrTool.Divider = false;
				tvwOptions.Indent = 0;
				tvwHelp.Indent = 0;
			}
			if (gridConfig.SelectedObject != null) {
				PropertyGridEx grid = new PropertyGridEx();
				grid.CategoryForeColor = gridConfig.CategoryForeColor;
				grid.Dock = gridConfig.Dock;
				grid.PropertySort = gridConfig.PropertySort;
				grid.SelectedObject = gridConfig.SelectedObject;
				grid.SelectedGridItem = gridConfig.SelectedGridItem;
				grid.ToolbarVisible = gridConfig.ToolbarVisible;
				grid.ModifiedChanged += new EventHandler(gridConfig_ModifiedChanged);
				grid.PropertyValueChanged += new PropertyValueChangedEventHandler(gridConfig_PropertyValueChanged);
				grid.SelectedGridItemChanged += new SelectedGridItemChangedEventHandler(gridConfig_SelectedGridItemChanged);
				gridConfig.Dispose();
				gridConfig = grid;
				pageConfig.Controls.Add(gridConfig);
				gridConfig.BringToFront();
			}
		}

		void themeToMatchUserPref() {
			tvwOptions.HotTracking = SystemInformation.IsHotTrackingEnabled;
			tvwHelp.HotTracking = SystemInformation.IsHotTrackingEnabled;
		}

		DosboxInfo loadDosboxVersions() {
			DosboxInfo rtn = null;
			bool bDefaultSet = false;
			foreach (string path in DosboxInfo.Installations) {
				MenuItem mi = addDosboxMenuItem(path);
				if (bDefaultSet == false) {
					bDefaultSet = true;
					mi.Checked = true;
					rtn = mi.Tag as DosboxInfo;
				}
			}
			return rtn;
		}

		MenuItem addDosboxMenuItem(string path) {
			DosboxInfo info = new DosboxInfo(path);
			MenuItem mi = new MenuItem(info.VersionString);
			mi.Tag = info;
			mi.RadioCheck = true;
			mi.Click += new EventHandler(mnuDosboxVersion_Click);
			mnuRun.MenuItems.Add(mnuRun.MenuItems.Count - 2, mi);
			return mi;
		}

		void listFromHash(ListView list, Dictionary<string, GameObject> hash) {
			foreach (GameObject ginfo in hash.Values) {
				addGameToList(list, ginfo);
			}
		}

		GameObject addGameToHash(Dictionary<string, GameObject> hash, string path, string exe) {
			path = Path.GetFullPath(path);
			if (hash.ContainsKey(path) == false) {
				GameObject ginfo = new GameObject(path);
				if (exe == null) {
					ginfo.Executable = GameObject.FindPreferredExecutable(GameObject.GetExecutables(path), Path.GetFileName(path).ToLowerInvariant());
				}
				else {
					ginfo.Executable = exe;
				}
				hash.Add(path, ginfo);
				return ginfo;
			}
			return null;
		}

		void addGameToList(ListView list, GameObject ginfo) {
			if (ginfo != null) {
				ListViewItem item = new ListViewItem(Path.GetFileName(ginfo.Directory));
				item.Group = getGameGroup(list, ginfo.Directory);
				item.UseItemStyleForSubItems = false;
				item.SubItems.Add(ginfo.Executable, SystemColors.GrayText, list.BackColor, list.Font);
				item.Tag = ginfo;
				list.Items.Add(item);
				item.Group.Header = Path.GetDirectoryName(ginfo.Directory) + " (" + item.Group.Items.Count + ")";
			}
		}

		ListViewGroup getGameGroup(ListView list, string path) {
			string parent = Path.GetDirectoryName(path).ToLowerInvariant();
			foreach (ListViewGroup g in list.Groups) {
				string hdr = string.Empty;
				if (g.Items.Count > 0) {
					hdr = Path.GetDirectoryName((g.Items[0].Tag as GameObject).Directory).ToLowerInvariant();
				}
				if (hdr == parent) {
					return g;
				}
			}
			ListViewGroupEx groupEx = new ListViewGroupEx(Path.GetDirectoryName(path));
			list.Groups.Add(groupEx);
			groupEx.Collapsible = true;
			return groupEx;
		}

		void moveCombo() {
			if (lvwGame.SelectedIndices.Count == 1) {
				ListViewItem item = lvwGame.SelectedItems[0];
				if (item.SubItems[1].Bounds.Top >= lvwGame.HeaderHeight && item.SubItems[1].Bounds.Width >= comboButton.Width) {
					comboButton.ComboBox.Width = item.SubItems[1].Bounds.Width - 2;
					comboButton.Location = new Point(item.SubItems[1].Bounds.Right - comboButton.Width, item.SubItems[1].Bounds.Top);
					comboButton.Visible = true;
				}
				else {
					comboButton.Visible = false;
				}
			}
		}

		void popuCombo() {
			if (lvwGame.SelectedIndices.Count == 1) {
				ListViewItem item = lvwGame.SelectedItems[0];
				comboButton.ComboBox.Items.Clear();
				comboButton.ComboBox.Items.AddRange(GameObject.GetExecutables((item.Tag as GameObject).Directory).ToArray());
				if (comboButton.ComboBox.Items.Contains(item.SubItems[1].Text) == false) {
					comboButton.ComboBox.Items.Add(item.SubItems[1].Text);
				}
				comboButton.ComboBox.Items.Add("(Select...)");
				comboButton.ComboBox.Text = item.SubItems[1].Text;
				moveCombo();
			}
			else {
				comboButton.Visible = false;
			}
		}

		private void tab_SelectedIndexChanged(object sender, EventArgs e) {
			pnlTop.SuspendLayout();
			switch (tab.SelectedIndex) {
			case 0:
				tbrAction.Visible = true;
				tbrProp.Visible = false;
				tbrTool.Visible = false;
				break;
			case 1:
				tbrAction.Visible = false;
				tbrProp.Visible = true;
				tbrTool.Visible = false;
				break;
			case 2:
				tbrAction.Visible = false;
				tbrProp.Visible = false;
				tbrTool.Visible = true;
				break;
			case 3:
				tbrAction.Visible = false;
				tbrProp.Visible = false;
				tbrTool.Visible = false;
				break;
			}
			pnlTop.ResumeLayout();
			if (tab.SelectedTab.Controls.Count > 0) {
				tab.SelectedTab.Controls[0].Focus();
			}
		}

		private void tbrAction_ButtonClick(object sender, ToolBarButtonClickEventArgs e) {
			if (e.Button.Enabled == false) {
				return;
			}
			if (e.Button == btnAddMasterGameFolder) {
				btnAddGameFolder.Enabled = false;
				btnAddMasterGameFolder.Enabled = false;
				if (openFileFolderDialog.ShowDialog(this) == DialogResult.OK) {
					foreach (DirectoryInfo diGame in new DirectoryInfo(openFileFolderDialog.OpenFileDialog.FileName).GetDirectories()) {
						addGameToList(lvwGame, addGameToHash(Program.AppConfig.GameConfig.Games, diGame.FullName, null));
					}
				}
				btnAddGameFolder.Enabled = true;
				btnAddMasterGameFolder.Enabled = true;
			}
			else if (e.Button == btnAddGameFolder) {
				btnAddGameFolder.Enabled = false;
				btnAddMasterGameFolder.Enabled = false;
				DialogResult ans = openFileFolderDialog.ShowDialog(this);
				if (ans == DialogResult.OK) {
					addGameToList(lvwGame, addGameToHash(Program.AppConfig.GameConfig.Games, openFileFolderDialog.OpenFileDialog.FileName, null));
				}
				btnAddGameFolder.Enabled = true;
				btnAddMasterGameFolder.Enabled = true;
			}
			else if (e.Button == btnDelete) {
				foreach (ListViewItem item in lvwGame.SelectedItems) {
					GameObject ginfo = item.Tag as GameObject;
					Program.AppConfig.GameConfig.Games.Remove(ginfo.Directory);
					item.Group.Header = Path.GetDirectoryName(ginfo.Directory) + " (" + (item.Group.Items.Count - 1) + ")";
					lvwGame.Items.Remove(item);
				}
			}
			else if (e.Button == btnRun) {
				if (lvwGame.SelectedItems.Count > 0) {
					GameObject ginfo = lvwGame.SelectedItems[0].Tag as GameObject;
					foreach (MenuItem mi in btnRun.DropDownMenu.MenuItems) {
						if (mi.Checked) {
							DosboxInfo dbinfo = (DosboxInfo) mi.Tag;
							pcsDosbox.StartInfo.FileName = dbinfo.FileName;
							pcsDosbox.StartInfo.Arguments = "\"" + ginfo.FileName + "\" -noconsole";
							try {
								bool bRemain = (Control.MouseButtons & MouseButtons.Middle) != 0;
								pcsDosbox.EnableRaisingEvents = bRemain;
								pcsDosbox.Start();
								if (bRemain) {
									notifyIcon.Visible = true;
									this.Visible = false;
								}
								else {
									this.Close();
								}
							}
							catch (Exception ex) {
								MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + pcsDosbox.StartInfo.FileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							break;
						}
					}
				}
			}
		}

		private void tbrAction_MouseMove(object sender, MouseEventArgs e) {
			foreach (ToolBarButton btn in tbrAction.Buttons) {
				if (btn.Rectangle.Contains(e.Location)) {
					sbpInfo.Text = btn.Tag as string;
					break;
				}
			}
		}

		private void tbrAction_MouseLeave(object sender, EventArgs e) {
			sbpInfo.Text = string.Empty;
		}

		private void tbrAction_Resize(object sender, EventArgs e) {
			pnlTop.Height = tbrAction.Height;
			// TODO: find out why this is called many times.
			Rectangle rectRef;
			if (tbrAction.Buttons.Count > 0) {
				rectRef = tbrAction.Buttons[0].Rectangle;
				rectRef.Offset(0, 1);
			}
			else {
				rectRef = tbrAction.ClientRectangle;
				rectRef.Offset(0, -1);
			}
			Rectangle rect = new Rectangle();
			rect.Y = rectRef.Top + (rectRef.Height - pnlSearch.Height) / 2;
			int xClipped = pnlSearch.Bounds.Right - (tbrAction.ClientSize.Width - 4);
			int xTo = tbrAction.Buttons[tbrAction.Buttons.Count - 1].Rectangle.Right + 4;
			if (pnlSearch.Left < xTo) {
				pnlSearch.SetBounds(xTo, 0, pnlSearch.Parent.ClientSize.Width - xTo - 4, 0, BoundsSpecified.X | BoundsSpecified.Width);
			}
			pnlSearch.SetBounds(rect.Left, rect.Top, rect.Width, 0, BoundsSpecified.Location | BoundsSpecified.Width);
		}

		private void mnuDosboxVersion_Click(object sender, EventArgs e) {
			MenuItem miSel = sender as MenuItem;
			if (miSel.Checked == false) {
				foreach (MenuItem mi in btnRun.DropDownMenu.MenuItems) {
					mi.Checked = false;
				}
				miSel.Checked = true;
			}
		}

		private void mnuOtherLoc_Click(object sender, EventArgs e) {
			DialogResult ans = openFileFolderDialog.ShowDialog(this);
			if (ans == DialogResult.OK) {
				string path = openFileFolderDialog.OpenFileDialog.FileName;
				if (File.Exists(path + Path.DirectorySeparatorChar + DosboxInfo.EXECUTABLE)) {
					for (int i = 0; i < btnRun.DropDownMenu.MenuItems.Count - 2; ++i) {
						if ((btnRun.DropDownMenu.MenuItems[i].Tag as DosboxInfo).Directory.ToLowerInvariant() == path.ToLowerInvariant()) {
							return;
						}
					}
					foreach (MenuItem mi in btnRun.DropDownMenu.MenuItems) {
						mi.Checked = false;
					}
					addDosboxMenuItem(path).Checked = true;
				}
			}
		}

		private void pnlSearch_MouseMove(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				Form form = pnlSearch.FindForm();
				int xTo = Control.MousePosition.X - form.Left - (form.Width - form.ClientSize.Width) / 2;
				if (xTo < tbrAction.Buttons[tbrAction.Buttons.Count - 1].Rectangle.Right + 4) {
					xTo = tbrAction.Buttons[tbrAction.Buttons.Count - 1].Rectangle.Right + 4;
				}
				if (xTo > pnlSearch.Parent.ClientSize.Width - 4 - 64 - txtSearch.Left) {
					xTo = pnlSearch.Parent.ClientSize.Width - 4 - 64 - txtSearch.Left;
				}
				xTo -= txtSearch.Left / 2;
				pnlSearch.SetBounds(xTo, 0, pnlSearch.Parent.ClientSize.Width - xTo - 4, 0, BoundsSpecified.X | BoundsSpecified.Width);
			}
		}

		private void txtSearch_Resize(object sender, EventArgs e) {
			pnlSearch.Height = txtSearch.Height;
		}

		private void lvwGame_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e) {
			moveCombo();
		}

		private void lvwGame_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e) {
			moveCombo();
		}

		private void lvwGame_ItemActivate(object sender, EventArgs e) {
			tbrAction_ButtonClick(sender, new ToolBarButtonClickEventArgs(btnRun));
		}

		private void lvwGame_LostFocus(object sender, EventArgs e) {
			if (comboButton.ContainsFocus == false) {
				comboButton.Visible = false;
			}
		}

		private void lvwGame_Scroll(object sender, ScrollEventArgs e) {
			moveCombo();
		}

		private void lvwGame_BeginScroll(object sender, EventArgs e) {
			m_IsSelectionScrolling = true;
			comboButton.Visible = false;
		}

		private void lvwGame_EndScroll(object sender, EventArgs e) {
			popuCombo();
			m_IsSelectionScrolling = false;
		}

		private void lvwGame_MouseDown(object sender, MouseEventArgs e) {
			lvwGame_SelectedIndexChanged(sender, e);
		}

		private void lvwGame_SelectedIndexChanged(object sender, EventArgs e) {
			if (lvwGame.SelectedIndices.Count > 0) {
				btnDelete.Enabled = true;
			}
			else {
				btnDelete.Enabled = false;
			}
			if (m_IsSelectionScrolling == false) {
				popuCombo();
			}
		}

		private void comboButton_LostFocus(object sender, EventArgs e) {
			comboButton.Visible = false;
		}

		private void comboButton_ComboBox_SelectedIndexChanged(object sender, EventArgs e) {
			if (lvwGame.SelectedItems.Count > 0) {
				GameObject ginfo = lvwGame.SelectedItems[0].Tag as GameObject;
				if (comboButton.ComboBox.SelectedIndex == comboButton.ComboBox.Items.Count - 1) {
					openFileFolderDialog.PickFolders = false;
					openFileFolderDialog.OpenFileDialog.Filter = "Executables|*.bat;*.exe;*.com";
					if (openFileFolderDialog.OpenFileDialog.ShowDialog(this) == DialogResult.OK) {
						ginfo.Executable = PathEx.GetRelativePath(ginfo.Directory, openFileFolderDialog.OpenFileDialog.FileName);
					}
					openFileFolderDialog.PickFolders = true;
				}
				else {
					ginfo.Executable = comboButton.ComboBox.Text;
				}
				lvwGame.SelectedItems[0].SubItems[1].Text = ginfo.Executable;
			}
		}

		private void pcsDosbox_Exited(object sender, EventArgs e) {
			this.Show();
			this.Focus();
			notifyIcon.Visible = false;
		}

		private void tbrProp_ButtonClick(object sender, ToolBarButtonClickEventArgs e) {
			if (e.Button.Enabled == false) {
				return;
			}
			if (e.Button == btnUndoConfig) {
				gridConfig.Undo();
			}
			else if (e.Button == btnRedoConfig) {
				gridConfig.Redo();
			}
			else if (e.Button == btnSaveConfig) {
				foreach (MenuItem mi in btnRun.DropDownMenu.MenuItems) {
					if (mi.Checked) {
						DosboxInfo dinfo = mi.Tag as DosboxInfo;
						INI ini = dinfo.GetUserConfigINI();
						foreach (PropertyGridEx.CommitInfo ci in gridConfig.GetUnsavedChanges()) {
							CategoryOrderAttribute cat = DosboxConfig.GetCategoryFromDescriptor(ci.ChangedItem.PropertyDescriptor);
							if (cat != null) {
								DosboxConfig.SaveProperty(ini, cat.Category, ci.ChangedItem.PropertyDescriptor.Name, ci.Value);
							}
						}
						gridConfig.SetSavePoint();
						break;
					}
				}
			}
			else if (e.Button == btnSortCat) {
				btnSortCat.Pushed = true;
				btnSortAZ.Pushed = false;
				gridConfig.PropertySort = PropertySort.Categorized;
			}
			else if (e.Button == btnSortAZ) {
				btnSortCat.Pushed = false;
				btnSortAZ.Pushed = true;
				gridConfig.PropertySort = PropertySort.Alphabetical;
			}
		}

		private void gridConfig_ModifiedChanged(object sender, EventArgs e) {
			pageConfig.Text = gridConfig.Modified ? "Config*" : "Config";
		}

		private void gridConfig_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
			btnUndoConfig.Enabled = gridConfig.CanUndo;
			btnRedoConfig.Enabled = gridConfig.CanRedo;
		}

		private void gridConfig_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e) {
			// Fix artificially malformed category text from ordering.
			if (e.NewSelection.GridItemType == GridItemType.Category) {
				if (e.NewSelection.GridItems.Count > 0) {
					CategoryOrderAttribute cat = DosboxConfig.GetCategoryFromDescriptor(e.NewSelection.GridItems[0].PropertyDescriptor);
					if (cat != null) {
						gridConfig.SetComment(cat.Category, cat.Description);
					}
				}
			}
		}

		private void mnuProp_Popup(object sender, EventArgs e) {
			GridItem gi = gridConfig.SelectedGridItem;
			if (gi.GridItemType == GridItemType.Property) {
				if (gi.PropertyDescriptor.ComponentType == gridConfig.SelectedObject.GetType()) {
					if (gi.PropertyDescriptor.CanResetValue(gridConfig.SelectedObject)) {
						mnuReset.Enabled = true;
						return;
					}
				}
			}
			mnuReset.Enabled = false;
		}

		private void mnuReset_Click(object sender, EventArgs e) {
			gridConfig.ResetSelectedProperty();
		}

		private void tbrTool_ButtonClick(object sender, ToolBarButtonClickEventArgs e) {
			if (e.Button == btnOpenCapture) {
			}
		}

		private void tvwHelp_AfterSelect(object sender, TreeViewEventArgs e) {
			TreeNode node = e.Node;
			while (node.Parent != null) {
				node = node.Parent;
			}
			if (node.Name == "nodHelp") {
				if (pnlHelp.Visible == false) {
					pnlHelp.Visible = true;
					pnlEditFeedback.Visible = false;
					pnlSendFeedback.Visible = false;
				}
			}
			else if (node.Name == "nodFeedback") {
				if (pnlEditFeedback.Visible == false && pnlSendFeedback.Visible == false) {
					pnlEditFeedback.Visible = true;
					pnlSendFeedback.Visible = false;
					pnlHelp.Visible = false;
				}
			}
		}

		private void tvwHelp_BeforeCollapse(object sender, TreeViewCancelEventArgs e) {
			e.Cancel = true;
		}

		private void btnPreviewFeedback_Click(object sender, EventArgs e) {
			pnlSendFeedback.Visible = true;
			pnlEditFeedback.Visible = false;
		}

		private void btnEditComment_Click(object sender, EventArgs e) {
			pnlEditFeedback.Visible = true;
			pnlSendFeedback.Visible = false;

		}

		private void btnSendFeedback_Click(object sender, EventArgs e) {

		}
	}
}
