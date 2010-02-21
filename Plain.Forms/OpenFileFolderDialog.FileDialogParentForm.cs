/*
FED (Front-End for Dosbox), a gaming graphical user interface for the DOSBox emulator.
Copyright (C) 2009  Ka Ki Cheung

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Plain.Native;

namespace Plain.Forms {
	partial class OpenFileFolderDialog {
		private class FileDialogParentForm : System.Windows.Forms.Form {
			public FileDialogParentForm(OpenFileFolderDialog openFileFolderDialog) {
				this.ControlBox = false;
				this.DialogResult = DialogResult.Cancel;
				this.FormBorderStyle = FormBorderStyle.None;
				this.MaximizeBox = false;
				this.MinimizeBox = false;
				this.Opacity = 0;
				this.ShowIcon = false;
				this.ShowInTaskbar = false;
				this.Size = Size.Empty;
				this.StartPosition = FormStartPosition.CenterParent;

				m_OpenFileFolderDialog = openFileFolderDialog;
				m_FileDialogWindow = null;
			}

			public DialogResult ShowFileDialog(IWin32Window owner, FileDialog fileDialog) {
				this.Show(owner);
				return fileDialog.ShowDialog(this);
			}

			OpenFileFolderDialog m_OpenFileFolderDialog;
			FileDialogWindow m_FileDialogWindow;

			protected override void WndProc(ref Message m) {
				switch (m.Msg) {
				case NativeMethods.WM_ACTIVATE:
					if ((int) m.WParam == NativeMethods.WA_INACTIVE) {
						StringBuilder sb = new StringBuilder(7);
						if (NativeMethods.GetClassName(m.LParam, sb, sb.Capacity) != 0) {
							if (sb.ToString() == "#32770") {
								m_FileDialogWindow = new FileDialogWindow(m.LParam, m_OpenFileFolderDialog);
								System.Diagnostics.Debug.WriteLine("Found FileDialog 0x" + m.LParam.ToString("X"), "FileDialogParentForm.WndProc");
							}
						}
					}
					break;
				}
				base.WndProc(ref m);
			}

			protected override void OnClosed(EventArgs e) {
				base.OnClosed(e);
				if (m_FileDialogWindow != null) {
					if (m_FileDialogWindow.AcceptedFolderOK) {
						base.DialogResult = DialogResult.OK;
					}
				}
			}
		}
	}
}
