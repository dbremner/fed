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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Plain.Native;

namespace Plain.Forms {
	partial class OpenFileFolderDialog {
		private class CommonDialogNotifyWindow : System.Windows.Forms.NativeWindow {
			public CommonDialogNotifyWindow(IntPtr handle) {
				AssignHandle(handle);
			}

			protected override void WndProc(ref Message m) {
				switch (m.Msg) {
				case NativeMethods.WM_NOTIFY:
					NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR) m.GetLParam(typeof(NativeMethods.NMHDR));
					switch (nmhdr.code) {
					case NativeMethods.CDN_INITDONE:
						System.Diagnostics.Debug.WriteLine("CDN_INITDONE");
						break;
					case NativeMethods.CDN_FOLDERCHANGE:
						System.Diagnostics.Debug.WriteLine("CDN_FOLDERCHANGE");
						break;
					case NativeMethods.CDN_TYPECHANGE:
						System.Diagnostics.Debug.WriteLine("CDN_TYPECHANGE");
						break;
					case NativeMethods.CDN_FILEOK:
						System.Diagnostics.Debug.WriteLine("CDN_FILEOK");
						break;
					}
					break;
				case NativeMethods.WM_NCDESTROY:
					this.ReleaseHandle();
					System.Diagnostics.Debug.WriteLine("Freed CommonDialogNotifyWindow", "CommonDialogNotifyWindow.WndProc");
					break;
				}
				// FIXME: find out what causes the exception.
				try {
					base.WndProc(ref m);
				}
				catch (AccessViolationException ex) {
					System.Diagnostics.Debug.WriteLine(ex.Message, "CommonDialogNotifyWindow.WndProc");
				}
			}
		}
	}
}
