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

			DateTime d0;
			DateTime d1;
			d0 = DateTime.Now;

			themeToMatchFonts();
			themeToMatchUserPref();
			styleToMatchVista();
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
#if true
			tab.TabPages.Remove(pageOptions);
			tab.TabPages.Remove(pageHelp);
#endif
			m_IsInitializationDone = true;
			txtSearch.InnerMargins = new EditMargins(2, 0);
		}

		bool m_IsInitializationDone;
		bool m_IsSelectionScrolling;
		string m_TemporaryConfigFile;

		protected override void OnHandleCreated(EventArgs e) {
			base.OnHandleCreated(e);
			base.Icon = Resources.Game;
			notifyIcon.Icon = Resources.Game;
		}

		protected override void OnSystemColorsChanged(EventArgs e) {
			base.OnSystemColorsChanged(e);
			themeToMatchFonts();
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
			// TODO: save grid splitter width.
			Program.AppConfig.GameListFormConfig.SaveFrom(this);
			if (m_TemporaryConfigFile != null) {
				try {
					File.Delete(m_TemporaryConfigFile);
				}
				catch { }
			}
		}

		void themeToMatchFonts() {
			this.Font = SystemFonts.IconTitleFont;
			sbr.Font = SystemFonts.StatusFont;
			imlSmallList.ImageSize = new Size(1, comboButton.Height - 1);
			tvwOptions.ItemHeight = comboButton.Height - 1;
			tvwHelp.ItemHeight = comboButton.Height - 1;
		}

		void themeToMatchUserPref() {
			tvwOptions.HotTracking = SystemInformation.IsHotTrackingEnabled;
			tvwHelp.HotTracking = SystemInformation.IsHotTrackingEnabled;
		}

		void styleToMatchVista() {
			if (Environment.OSVersion.Version >= NativeMethods.WindowsVista) {
				tbrAction.Divider = false;
				tbrProp.Divider = false;
				tbrTool.Divider = false;
				tvwOptions.Indent = 0;
				tvwHelp.Indent = 0;
			}
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

		DosboxInfo getSelectedDosboxVersion() {
			foreach (MenuItem mi in btnRun.DropDownMenu.MenuItems) {
				if (mi.Checked) {
					return mi.Tag as DosboxInfo;
				}
			}
			return null;
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
			list.BeginUpdate();
			foreach (GameObject gobj in hash.Values) {
				addGameToList(list, gobj);
			}
			list.EndUpdate();
		}

		void addGameToList(ListView list, GameObject gobj) {
			if (gobj != null) {
				ListViewItem item = new ListViewItem(Path.GetFileName(gobj.Directory));
				item.Group = getGameGroup(list, gobj.Directory);
				item.UseItemStyleForSubItems = false;
				item.SubItems.Add(gobj.Executable, SystemColors.GrayText, list.BackColor, list.Font);
				item.Tag = gobj;
				list.Items.Add(item);
				item.Group.Header = Path.GetDirectoryName(gobj.Directory) + " (" + item.Group.Items.Count + ")";
			}
		}

		GameObject addGameToHash(Dictionary<string, GameObject> hash, string path, string exe) {
			path = Path.GetFullPath(path);
			if (hash.ContainsKey(path) == false) {
				GameObject gobj = new GameObject(path);
				if (exe == null) {
					gobj.Executable = GameObject.FindPreferredExecutable(GameObject.GetExecutables(path), Path.GetFileName(path).ToLowerInvariant());
				}
				else {
					gobj.Executable = exe;
				}
				hash.Add(path, gobj);
				return gobj;
			}
			return null;
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

		void addMasterGameFolder() {
			btnAddGameFolder.Enabled = false;
			btnAddMasterGameFolder.Enabled = false;
			if (openFileFolderDialog.ShowDialog(this) == DialogResult.OK) {
				lvwGame.BeginUpdate();
				foreach (DirectoryInfo diGame in new DirectoryInfo(openFileFolderDialog.OpenFileDialog.FileName).GetDirectories()) {
					addGameToList(lvwGame, addGameToHash(Program.AppConfig.GameConfig.Games, diGame.FullName, null));
				}
				lvwGame.EndUpdate();
			}
			btnAddGameFolder.Enabled = true;
			btnAddMasterGameFolder.Enabled = true;
		}

		void addGameFolder() {
			btnAddGameFolder.Enabled = false;
			btnAddMasterGameFolder.Enabled = false;
			DialogResult ans = openFileFolderDialog.ShowDialog(this);
			if (ans == DialogResult.OK) {
				addGameToList(lvwGame, addGameToHash(Program.AppConfig.GameConfig.Games, openFileFolderDialog.OpenFileDialog.FileName, null));
			}
			btnAddGameFolder.Enabled = true;
			btnAddMasterGameFolder.Enabled = true;
		}

		void deleteGames() {
			lvwGame.BeginUpdate();
			foreach (ListViewItem item in lvwGame.SelectedItems) {
				GameObject gobj = item.Tag as GameObject;
				Program.AppConfig.GameConfig.Games.Remove(gobj.Directory);
				item.Group.Header = Path.GetDirectoryName(gobj.Directory) + " (" + (item.Group.Items.Count - 1) + ")";
				lvwGame.Items.Remove(item);
			}
			lvwGame.EndUpdate();
		}

		bool prepareTempConfig(DosboxInfo dbinfo) {
			if (gridConfig.Modified) {
				try {
					if (m_TemporaryConfigFile == null) {
						m_TemporaryConfigFile = Path.GetTempFileName();
						File.Copy(dbinfo.GetUserConfigFileName(), m_TemporaryConfigFile);
					}
					INI ini = new INI(m_TemporaryConfigFile);
					foreach (PropertyGridEx.CommitInfo ci in gridConfig.GetUnsavedChanges()) {
						CategoryOrderAttribute cat = DosboxConfig.GetCategoryFromDescriptor(ci.ChangedItem.PropertyDescriptor);
						if (cat != null) {
							DosboxConfig.SaveProperty(ini, cat.Category, ci.ChangedItem.PropertyDescriptor.Name, ci.Value);
						}
					}
					return true;
				}
				catch { }
			}
			return false;
		}

		void runDosbox() {
			if (lvwGame.SelectedItems.Count > 0) {
				DosboxInfo dbinfo = getSelectedDosboxVersion();
				if (dbinfo != null) {
					// TODO: run button runs dosbox alone; double click runs the game.
					GameObject gobj = lvwGame.SelectedItems[0].Tag as GameObject;
					string args = "\"" + gobj.Directory + "\" -noconsole -c " + gobj.Executable;
					if (prepareTempConfig(dbinfo)) {
						args += " -conf " + m_TemporaryConfigFile;
					}
					pcsDosbox.StartInfo.FileName = dbinfo.FileName;
					pcsDosbox.StartInfo.Arguments = args;
					bool bRemain = (Control.MouseButtons & MouseButtons.Middle) != 0;
					pcsDosbox.EnableRaisingEvents = bRemain;
					try {
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
				}
			}
		}

		void saveConfig() {
			DosboxInfo dbinfo = getSelectedDosboxVersion();
			if (dbinfo != null) {
				INI ini = dbinfo.GetUserConfigINI();
				foreach (PropertyGridEx.CommitInfo ci in gridConfig.GetUnsavedChanges()) {
					CategoryOrderAttribute cat = DosboxConfig.GetCategoryFromDescriptor(ci.ChangedItem.PropertyDescriptor);
					if (cat != null) {
						DosboxConfig.SaveProperty(ini, cat.Category, ci.ChangedItem.PropertyDescriptor.Name, ci.Value);
					}
				}
				gridConfig.SetSavePoint();
			}
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
				pnlSearch.Visible = true;
				tbrAction.Visible = true;
				tbrProp.Visible = false;
				tbrTool.Visible = false;
				break;
			case 1:
				pnlSearch.Visible = false;
				tbrAction.Visible = false;
				tbrProp.Visible = true;
				tbrTool.Visible = false;
				break;
			case 2:
				pnlSearch.Visible = false;
				tbrAction.Visible = false;
				tbrProp.Visible = false;
				tbrTool.Visible = true;
				break;
			case 3:
				pnlSearch.Visible = false;
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

		private void pnlTop_Resize(object sender, EventArgs e) {
			if (base.WindowState == FormWindowState.Minimized) {
				pnlSearch.Visible = false;
				return;
			}
			else {
				pnlSearch.Visible = true;
			}
			if (m_IsInitializationDone) {
				Rectangle rect = pnlSearch.Bounds;
				Rectangle rectRef = tbrAction.ClientRectangle;
				rect.Y = rectRef.Top + (rectRef.Height - rect.Height) / 2;

				int xLeftBound = (tbrAction.Buttons.Count > 0) ? tbrAction.Buttons[tbrAction.Buttons.Count - 1].Rectangle.Right + 4 : 0;
				int xRightBound = tbrAction.ClientSize.Width - 4;
				rect.X = xRightBound - rect.Width;
				if (rect.X < xLeftBound) {
					rect.X = xLeftBound;
				}
				if (rect.Right > xRightBound) {
					rect.Width = xRightBound - rect.X;
					if (rect.Width < 64) {
						rect.Width = 64;
					}
				}
				pnlSearch.SetBounds(rect.Left, rect.Top, rect.Width, 0, BoundsSpecified.Location | BoundsSpecified.Width);
			}
		}

		private void pnlSearch_MouseMove(object sender, MouseEventArgs e) {
			if (e.Button == MouseButtons.Left) {
				Form form = pnlSearch.FindForm();
				int xTo = Control.MousePosition.X - form.Left - (form.Width - form.ClientSize.Width) / 2;
				int xLeftBound = (tbrAction.Buttons.Count > 0) ? tbrAction.Buttons[tbrAction.Buttons.Count - 1].Rectangle.Right + 4 : 0;
				if (xTo < xLeftBound) {
					xTo = xLeftBound;
				}
				int xRightBound = tbrAction.ClientSize.Width - 4;
				if (xTo > xRightBound - 64) {
					xTo = xRightBound - 64;
				}
				xTo -= txtSearch.Left / 2;
				pnlSearch.SetBounds(xTo, 0, xRightBound - xTo, 0, BoundsSpecified.X | BoundsSpecified.Width);
			}
		}

		private void txtSearch_Resize(object sender, EventArgs e) {
			if (true || m_IsInitializationDone) {
				pnlSearch.Height = txtSearch.Height;
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e) {
			string filter = txtSearch.Text.ToLowerInvariant();
			lvwGame.Items.Clear();
			if (filter == string.Empty) {
				listFromHash(lvwGame, Program.AppConfig.GameConfig.Games);
			}
			else {
				lvwGame.BeginUpdate();
				foreach (GameObject gobj in Program.AppConfig.GameConfig.Games.Values) {
					if (Path.GetFileName(gobj.Directory).ToLowerInvariant().Contains(filter)) {
						addGameToList(lvwGame, gobj);
					}
				}
				lvwGame.EndUpdate();
			}
		}

		private void tbrAction_ButtonClick(object sender, ToolBarButtonClickEventArgs e) {
			if (e.Button.Enabled == false) {
				return;
			}
			if (e.Button == btnAddMasterGameFolder) {
				addMasterGameFolder();
			}
			else if (e.Button == btnAddGameFolder) {
				addGameFolder();
			}
			else if (e.Button == btnDelete) {
				deleteGames();
			}
			else if (e.Button == btnRun) {
				runDosbox();
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
			if (m_IsInitializationDone) {
				// TODO: find out why this is called many times.
				pnlTop.Height = Math.Max(tbrAction.Height, pnlSearch.Height);
			}
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
				GameObject gobj = lvwGame.SelectedItems[0].Tag as GameObject;
				if (comboButton.ComboBox.SelectedIndex == comboButton.ComboBox.Items.Count - 1) {
					openFileFolderDialog.PickFolders = false;
					openFileFolderDialog.OpenFileDialog.Filter = "Executables|*.bat;*.exe;*.com";
					if (openFileFolderDialog.OpenFileDialog.ShowDialog(this) == DialogResult.OK) {
						gobj.Executable = PathEx.GetRelativePath(gobj.Directory, openFileFolderDialog.OpenFileDialog.FileName);
					}
					openFileFolderDialog.PickFolders = true;
				}
				else {
					gobj.Executable = comboButton.ComboBox.Text;
				}
				lvwGame.SelectedItems[0].SubItems[1].Text = gobj.Executable;
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
				saveConfig();
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
			if (e.Button.Enabled == false) {
				return;
			}
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
