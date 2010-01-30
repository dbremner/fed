using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Plain.IO;
using Plain.Native;

namespace Plain.Forms {
	partial class OpenFileFolderDialog {
		private class FileDialogWindow : NativeWindow {
			public FileDialogWindow(IntPtr handle) {
				AssignHandle(handle);
				m_FakedOK = false;
				m_SelectedFilename = string.Empty;
			}

			public bool FakedOK {
				get { return m_FakedOK; }
			}

			public string SelectedFilename {
				get { return m_SelectedFilename; }
			}

			bool m_FakedOK;
			string m_SelectedFilename;

			protected override void WndProc(ref Message m) {
				switch (m.Msg) {
				case NativeMethods.WM_SHOWWINDOW:
					IntPtr hwndCommonDialogNotify = getCommonDialogNotifyHandle(m.HWnd);
					if (hwndCommonDialogNotify != IntPtr.Zero) {
						System.Diagnostics.Debug.WriteLine("Found CommonDialog 0x" + hwndCommonDialogNotify.ToString("X"), "FileDialogWindow.WndProc");
						new CommonDialogNotifyWindow(hwndCommonDialogNotify);
					}
					NativeMethods.SendMessage(m.HWnd, NativeMethods.CDM_HIDECONTROL, NativeMethods.cmb1, 0);
					NativeMethods.SendMessage(m.HWnd, NativeMethods.CDM_HIDECONTROL, NativeMethods.stc2, 0);
					// TODO: localize
					NativeMethods.SendMessage(m.HWnd, NativeMethods.CDM_SETCONTROLTEXT, NativeMethods.IDOK, "Select");
					NativeMethods.SendMessage(m.HWnd, NativeMethods.CDM_SETCONTROLTEXT, NativeMethods.stc3, "Folder:");
					break;
				case NativeMethods.WM_COMMAND:
					switch (NativeMethods.LOWORD(m.WParam.ToInt32())) {
					case NativeMethods.IDOK:
						switch (NativeMethods.HIWORD(m.WParam.ToInt32())) {
						case NativeMethods.BN_CLICKED:
							bool bFakeOK = true;
							IntPtr hwndFocus = NativeMethods.GetFocus();
							IntPtr hwndComboBoxEx32 = NativeMethods.GetDlgItem(m.HWnd, NativeMethods.cmb13);
							IntPtr hwndComboBox = NativeMethods.FindWindowEx(hwndComboBoxEx32, IntPtr.Zero, null, IntPtr.Zero);
							IntPtr hwndEdit = NativeMethods.FindWindowEx(hwndComboBox, IntPtr.Zero, null, IntPtr.Zero);
							if (hwndFocus == hwndEdit) {
								bFakeOK = false;
							}
							if (bFakeOK) {
								int bufSize = NativeMethods.SendMessage(m.HWnd, NativeMethods.CDM_GETFILEPATH, 0, (StringBuilder) null);
								if (bufSize >= 0) {
									StringBuilder sb = new StringBuilder(bufSize);
									bufSize = NativeMethods.SendMessage(m.HWnd, NativeMethods.CDM_GETFILEPATH, sb.Capacity, sb);
									if (bufSize >= 0) {
										m_SelectedFilename = sb.ToString();
										if (fake_CDN_FILEOK(m.HWnd, m_SelectedFilename)) {
											m_FakedOK = true;
											NativeMethods.PostMessage(m.HWnd, NativeMethods.WM_CLOSE, 0, 0);
											m.Result = IntPtr.Zero;
										}
									}
								}
							}
							break;
						}
						break;
					}
					break;
				case NativeMethods.WM_NCDESTROY:
					this.ReleaseHandle();
					System.Diagnostics.Debug.WriteLine("Freed FileDialogWindow", "FileDialogWindow.WndProc");
					break;
				}
				base.WndProc(ref m);
			}

			IntPtr getCommonDialogNotifyHandle(IntPtr hwndFileDialog) {
				return NativeMethods.FindWindowEx(hwndFileDialog, IntPtr.Zero, "#32770", IntPtr.Zero);
			}

			bool fake_CDN_FILEOK(IntPtr hwndFileDialog, string path) {
				bool rtn = false;
				System.Diagnostics.Debug.WriteLine("Checking " + path, "FileDialogWindow.fake_CDN_FILEOK");
				if (Path.GetExtension(path).ToLowerInvariant() == ".lnk") {
					using (ShortcutFile link = new ShortcutFile(path)) {
						path = link.Target;
					}
					System.Diagnostics.Debug.WriteLine("Accepting " + path, "FileDialogWindow.fake_CDN_FILEOK");
				}
				if (Directory.Exists(path)) {
					IntPtr hwndCommonDialogNotify = getCommonDialogNotifyHandle(hwndFileDialog);
					if (hwndCommonDialogNotify != IntPtr.Zero) {
						NativeMethods.OFNOTIFY ofnotify = new NativeMethods.OFNOTIFY();

						ofnotify.hdr.hwndFrom = hwndFileDialog;
						ofnotify.hdr.idFrom = (UIntPtr) NativeMethods.IDOK;
						ofnotify.hdr.code = NativeMethods.CDN_FILEOK;

						NativeMethods.OPENFILENAME ofn = new NativeMethods.OPENFILENAME();
						OperatingSystem os = System.Environment.OSVersion;
						if (os.Platform == PlatformID.Win32NT && os.Version >= NativeMethods.Windows2000) {
							ofn.lStructSize = (uint) Marshal.SizeOf(typeof(NativeMethods.OPENFILENAME));
						}
						else if (os.Platform == PlatformID.Win32Windows) {
							ofn.lStructSize = (uint) Marshal.SizeOf(typeof(NativeMethods.OPENFILENAME_9x));
						}
						ofn.hwndOwner = NativeMethods.GetAncestor(hwndFileDialog, NativeMethods.GA_ROOTOWNER);
						ofn.lpstrFilter = "Folders\0*.*\0\0";
						// MSDN: "The buffer should be at least 256 characters long."
						ofn.lpstrFile = path + new string('\0', 256);
						ofn.nMaxFile = (uint) ofn.lpstrFile.Length;
						// FIXME: lpfnHook may not be cleaned up.
						ofnotify.lpOFN = Marshal.AllocCoTaskMem((int) ofn.lStructSize);
						if (ofnotify.lpOFN != IntPtr.Zero) {
							Marshal.StructureToPtr(ofn, ofnotify.lpOFN, false);
							System.Diagnostics.Debug.WriteLine("Faking CDN_FILEOK", "FileDialogWindow.fake_CDN_FILEOK");
							rtn = (0 == NativeMethods.SendMessage(hwndCommonDialogNotify, NativeMethods.WM_NOTIFY, ofnotify.hdr.idFrom, ref ofnotify));
							Marshal.FreeCoTaskMem(ofnotify.lpOFN);
						}
					}
				}
				return rtn;
			}
		}
	}
}
