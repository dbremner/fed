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

//#define USE_FAKEOK

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Plain.IO;
using Plain.Native;

namespace Plain.Forms {
	partial class OpenFileFolderDialog {
		private class FileDialogWindow : NativeWindow {
			public FileDialogWindow(IntPtr handle, OpenFileFolderDialog openFileFolderDialog) {
				AssignHandle(handle);
				m_OpenFileFolderDialog = openFileFolderDialog;
				m_AcceptedFolderOK = false;
			}

			public bool AcceptedFolderOK {
				get { return m_AcceptedFolderOK; }
			}

			OpenFileFolderDialog m_OpenFileFolderDialog;
			bool m_AcceptedFolderOK;
#if USE_FAKEOK
			CommonDialogNotifyWindow m_CDNWindow;
#endif

			protected override void WndProc(ref Message m) {
				switch (m.Msg) {
				case NativeMethods.WM_SHOWWINDOW:
#if USE_FAKEOK
					IntPtr hwndCommonDialogNotify = getCommonDialogNotifyHandle(m.HWnd);
					if (hwndCommonDialogNotify != IntPtr.Zero) {
						System.Diagnostics.Debug.WriteLine("Found CommonDialog 0x" + hwndCommonDialogNotify.ToString("X"), "FileDialogWindow.WndProc");
						m_CDNWindow = new CommonDialogNotifyWindow(hwndCommonDialogNotify);
					}
#endif
					NativeMethods.SendMessage(m.HWnd, NativeMethods.CDM_HIDECONTROL, (IntPtr)NativeMethods.cmb1, IntPtr.Zero);
					NativeMethods.SendMessage(m.HWnd, NativeMethods.CDM_HIDECONTROL, (IntPtr)NativeMethods.stc2, IntPtr.Zero);
					// TODO: localize
					NativeMethods.SendMessage(m.HWnd, NativeMethods.CDM_SETCONTROLTEXT, (IntPtr)NativeMethods.IDOK, "Select");
					NativeMethods.SendMessage(m.HWnd, NativeMethods.CDM_SETCONTROLTEXT, (IntPtr)NativeMethods.stc3, "Folder:");
					break;
				case NativeMethods.WM_COMMAND:
					switch (NativeMethods.LOWORD(m.WParam.ToInt32())) {
					case NativeMethods.IDOK:
						switch (NativeMethods.HIWORD(m.WParam.ToInt32())) {
						case NativeMethods.BN_CLICKED:
							bool bAcceptOK = true;
							IntPtr hwndFocus = NativeMethods.GetFocus();
							IntPtr hwndComboBoxEx32 = NativeMethods.GetDlgItem(m.HWnd, NativeMethods.cmb13);
							IntPtr hwndComboBox = PInvoke.User32.FindWindowEx(hwndComboBoxEx32, IntPtr.Zero, null, null);
							IntPtr hwndEdit = PInvoke.User32.FindWindowEx(hwndComboBox, IntPtr.Zero, null, null);
							if (hwndFocus == hwndEdit) {
								bAcceptOK = false;
							}
							if (bAcceptOK) {
								int bufSize = NativeMethods.SendMessage(m.HWnd, NativeMethods.CDM_GETFILEPATH, IntPtr.Zero, (StringBuilder) null);
								if (bufSize >= 0) {
									StringBuilder sb = new StringBuilder(bufSize);
									bufSize = NativeMethods.SendMessage(m.HWnd, NativeMethods.CDM_GETFILEPATH, (IntPtr)sb.Capacity, sb);
									if (bufSize >= 0) {
										string path = sb.ToString();
										System.Diagnostics.Debug.WriteLine("Checking " + path, "FileDialogWindow.WndProc");
										if (Path.GetExtension(path).ToLowerInvariant() == ".lnk") {
											using (ShortcutFile link = new ShortcutFile(path)) {
												path = link.Target;
											}
											System.Diagnostics.Debug.WriteLine("Accepting " + path, "FileDialogWindow.WndProc");
										}
#if USE_FAKEOK
										if (fake_CDN_FILEOK(m.HWnd, path)) {
											m_AcceptedFolderOK = true;
											NativeMethods.PostMessage(m.HWnd, NativeMethods.WM_CLOSE, 0, 0);
											m.Result = IntPtr.Zero;
										}
#else
										m_OpenFileFolderDialog.openFileDialog.FileName = path;
										CancelEventArgs e = new CancelEventArgs();
										m_OpenFileFolderDialog.OnFileOK(e);
										if (e.Cancel == false) {
											m_AcceptedFolderOK = true;
											NativeMethods.PostMessage(m.HWnd, NativeMethods.WM_CLOSE, (IntPtr) 0, (IntPtr) 0);
											m.Result = IntPtr.Zero;
										}
#endif
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

#if USE_FAKEOK
			IntPtr getCommonDialogNotifyHandle(IntPtr hwndFileDialog) {
				return NativeMethods.FindWindowEx(hwndFileDialog, IntPtr.Zero, "#32770", IntPtr.Zero);
			}

			bool fake_CDN_FILEOK(IntPtr hwndFileDialog, string path) {
				bool rtn = false;
				if (Directory.Exists(path)) {
					if (m_CDNWindow != null){
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
							rtn = (0 == NativeMethods.SendMessage(m_CDNWindow.Handle, NativeMethods.WM_NOTIFY, ofnotify.hdr.idFrom, ref ofnotify));
							Marshal.FreeCoTaskMem(ofnotify.lpOFN);
						}
					}
				}
				return rtn;
			}
#endif
		}
	}
}
