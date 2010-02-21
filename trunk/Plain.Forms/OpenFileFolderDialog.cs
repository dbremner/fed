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
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Plain.Forms {
	[Category("Dialogs")]
	public partial class OpenFileFolderDialog : Component {
		public OpenFileFolderDialog() {
			m_PickFolders = DEFAULT_PICKFOLDERS;
			_WindowsForms_ = new Reflector("System.Windows.Forms");

			InitializeComponent();
		}

		public OpenFileFolderDialog(IContainer container)
			: this() {
			container.Add(this);
		}

		public event CancelEventHandler FolderOK = delegate { };

		public DialogResult ShowDialog() {
			return ShowDialog(null);
		}

		public DialogResult ShowDialog(IWin32Window owner) {
			if (m_PickFolders) {
				configOpenFileDialogToSelectFolder();
				DialogResult answer;
				if ((bool) _WindowsForms_.Get(openFileDialog, "UseVistaDialogInternal") == false) {
					FileDialogParentForm frmMsg = new FileDialogParentForm(this);
					frmMsg.ShowFileDialog(owner, openFileDialog);
					frmMsg.Close();
					answer = frmMsg.DialogResult;
				}
				else {
					answer = _RunDialog(owner != null ? owner.Handle : IntPtr.Zero) ? DialogResult.OK : DialogResult.Cancel;
				}
				return answer;
			}
			else {
				return openFileDialog.ShowDialog(owner);
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public OpenFileDialog OpenFileDialog {
			get { return openFileDialog; }
		}

		[Category("Behavior")]
		[DefaultValue(DEFAULT_PICKFOLDERS)]
		[Description("Present the Open dialog offering a choice of folders rather than files.")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public bool PickFolders {
			set {
				m_PickFolders = value;
				configOpenFileDialogToSelectFolder();
			}
			get { return m_PickFolders; }
		}

		const bool DEFAULT_PICKFOLDERS = true;
		bool m_PickFolders;
		Reflector _WindowsForms_;

		protected virtual void OnFileOK(CancelEventArgs e) {
			FolderOK(this, e);
		}

		void configOpenFileDialogToSelectFolder() {
			if (m_PickFolders) {
				openFileDialog.AddExtension = false;
				openFileDialog.CheckFileExists = false;
				openFileDialog.DereferenceLinks = true;
				openFileDialog.Multiselect = false;
				if ((bool) _WindowsForms_.Get(openFileDialog, "UseVistaDialogInternal") == false) {
					openFileDialog.AddExtension = false;
					openFileDialog.Filter = "Folders|\n";

					if (Directory.Exists(openFileDialog.FileName)) {
						openFileDialog.InitialDirectory = openFileDialog.FileName;
					}
				}
			}
			openFileDialog.FileName = string.Empty;
		}

		bool _RunDialog(IntPtr hWndOwner) {
			if (hWndOwner == IntPtr.Zero) {
			}
			_WindowsForms_.Call(openFileDialog, "EnsureFileDialogPermission");
			return _RunDialogVista(hWndOwner);
		}

		bool _RunDialogVista(IntPtr hWndOwner) {
			uint num = 0;
			bool flag;
			Type typeIFileDialog = _WindowsForms_.GetType("FileDialogNative.IFileDialog");
			object dialog = _WindowsForms_.Call(openFileDialog, "CreateVistaDialog");
			_WindowsForms_.Call(openFileDialog, "OnBeforeVistaDialog", dialog);

			uint options = (uint) _WindowsForms_.CallAs(typeof(FileDialog), openFileDialog, "GetOptions");
			options |= (uint) _WindowsForms_.GetEnum("FileDialogNative.FOS", "FOS_PICKFOLDERS");
			_WindowsForms_.CallAs(typeIFileDialog, dialog, "SetOptions", options);

			object pfde = _WindowsForms_.New("FileDialog.VistaDialogEvents", openFileDialog);
			object[] parameters = new object[] { pfde, num };
			_WindowsForms_.CallAs2(typeIFileDialog, dialog, "Advise", parameters);
			num = (uint) parameters[1];
			try {
				int num2 = (int) _WindowsForms_.CallAs(typeIFileDialog, dialog, "Show", hWndOwner);
				flag = 0 == num2;
			}
			finally {
				_WindowsForms_.CallAs(typeIFileDialog, dialog, "Unadvise", num);
				GC.KeepAlive(pfde);
			}
			return flag;
		}
	}
}
