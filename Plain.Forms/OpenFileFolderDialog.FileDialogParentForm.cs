using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Plain.Native;

namespace Plain.Forms {
	partial class OpenFileFolderDialog {
		private class FileDialogParentForm : System.Windows.Forms.Form {
			public FileDialogParentForm() {
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

				m_FileDialogWindow = null;
				m_SelectedFilename = string.Empty;
			}

			public DialogResult ShowFileDialog(IWin32Window owner, FileDialog fileDialog) {
				this.Show(owner);
				return fileDialog.ShowDialog(this);
			}

			public string SelectedFilename {
				get { return m_SelectedFilename; }
			}

			FileDialogWindow m_FileDialogWindow;
			string m_SelectedFilename;

			protected override void WndProc(ref Message m) {
				switch (m.Msg) {
				case NativeMethods.WM_ACTIVATE:
					if ((int) m.WParam == NativeMethods.WA_INACTIVE) {
						StringBuilder sb = new StringBuilder(7);
						if (NativeMethods.GetClassName(m.LParam, sb, sb.Capacity) != 0) {
							if (sb.ToString() == "#32770") {
								m_FileDialogWindow = new FileDialogWindow(m.LParam);
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
					if (m_FileDialogWindow.FakedOK) {
						this.DialogResult = DialogResult.OK;
						m_SelectedFilename = m_FileDialogWindow.SelectedFilename;
					}
				}
			}
		}
	}
}
