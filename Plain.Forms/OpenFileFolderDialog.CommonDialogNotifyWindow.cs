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
