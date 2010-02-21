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
