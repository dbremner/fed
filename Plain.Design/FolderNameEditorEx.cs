using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Plain.Forms;

namespace Plain.Design {
	public class FolderNameEditorEx : UITypeEditor {
		public FolderNameEditorEx() : this(string.Empty) { }
		public FolderNameEditorEx(string title)
			: base() {
			m_Title = title;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
			if ((provider != null) && (((IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService))) != null)) {
				if (this.openFileFolderDialog == null) {
					this.openFileFolderDialog = new OpenFileFolderDialog();
					this.InitializeDialog(this.openFileFolderDialog);
				}
				if (value is string) {
					this.openFileFolderDialog.OpenFileDialog.FileName = (string) value;
				}
				if (this.openFileFolderDialog.ShowDialog() == DialogResult.OK) {
					value = this.openFileFolderDialog.OpenFileDialog.FileName;
				}
			}
			return value;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
			return UITypeEditorEditStyle.Modal;
		}

		OpenFileFolderDialog openFileFolderDialog;
		string m_Title;

		protected virtual void InitializeDialog(OpenFileFolderDialog openFileFolderDialog) {
			openFileFolderDialog.PickFolders = true;
			openFileFolderDialog.OpenFileDialog.Title = m_Title;
		}
	}
}
