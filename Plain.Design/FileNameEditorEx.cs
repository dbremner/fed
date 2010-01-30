using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Plain.Design {
	public class FileNameEditorEx : System.Windows.Forms.Design.FileNameEditor {
		public FileNameEditorEx(string title)
			: base() {
			m_Title = title;
		}

		protected override void InitializeDialog(OpenFileDialog openFileDialog) {
			base.InitializeDialog(openFileDialog);
			openFileDialog.Title = m_Title;
		}

		string m_Title;
	}
}
