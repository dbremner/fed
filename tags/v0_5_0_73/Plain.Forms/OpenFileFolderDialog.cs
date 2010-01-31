using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Plain.Forms {
	[Category("Dialogs")]
	public partial class OpenFileFolderDialog : Component {
		public OpenFileFolderDialog() {
			_WindowsForms_ = new Reflector("System.Windows.Forms");

			InitializeComponent();
		}

		public OpenFileFolderDialog(IContainer container)
			: this() {
			container.Add(this);
		}

		public DialogResult ShowDialog() {
			return ShowDialog(null);
		}

		public DialogResult ShowDialog(IWin32Window owner) {
			if (m_PickFolders) {
				configOpenFileDialogToSelectFolder();
				if ((bool) _WindowsForms_.Get(openFileDialog, "UseVistaDialogInternal") == false) {
					FileDialogParentForm frmMsg = new FileDialogParentForm();
					DialogResult ans = frmMsg.ShowFileDialog(owner, openFileDialog);
					frmMsg.Close();
					if (frmMsg.DialogResult == DialogResult.OK) {
						if (string.IsNullOrEmpty( openFileDialog.FileName)){
							openFileDialog.FileName = frmMsg.SelectedFilename;
						}
					}
					return frmMsg.DialogResult;
				}
				else {
					return _RunDialog(owner != null ? owner.Handle : IntPtr.Zero) ? DialogResult.OK : DialogResult.Cancel;
				}
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

		void configOpenFileDialogToSelectFolder() {
			if ((bool) _WindowsForms_.Get(openFileDialog, "UseVistaDialogInternal") == false) {
				if (m_PickFolders) {
					openFileDialog.AddExtension = false;
					openFileDialog.CheckFileExists = false;
					openFileDialog.CheckPathExists = true;
					openFileDialog.DereferenceLinks = true;
					openFileDialog.Filter = "Folders|\n";
					openFileDialog.Multiselect = false;

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
