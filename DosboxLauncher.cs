﻿using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Plain.Native;
using System.Threading;

namespace DosboxApp {
	public class DosboxLauncher {
		public static event EventHandler ProcessStarted;

		public static void Starting(DosboxInfo dbinfo, GameObject gobj, string pathConf) {
			var shortDirName = new StringBuilder(1024);
			var shortExeName = new StringBuilder(1024);
			NativeMethods.GetShortPathName(gobj.Directory, shortDirName, shortDirName.Capacity);
			NativeMethods.GetShortPathName(gobj.FileName, shortExeName, shortExeName.Capacity);
			string args = "-noconsole";
			args += " -c \"mount d " + "'" + gobj.Directory + "' -t cdrom\"";
			args += " -c \"mount c " + "'" + Path.GetDirectoryName(gobj.Directory) + "'\"";
			args += " -c \"c:\"";
			args += " -c \"cd " + Path.GetFileName(shortDirName.ToString()) + "\"";
			args += " -c \"" + Path.GetFileName(shortExeName.ToString()) + "\"";
			if (pathConf != null) {
				args += " -conf \"" + pathConf + "\"";
			}

			Process = new Process();
			Process.EnableRaisingEvents = true;
			Process.StartInfo.FileName = dbinfo.FileName;
			Process.StartInfo.Arguments = args;
			Process.StartInfo.WorkingDirectory = Path.GetDirectoryName(dbinfo.FileName);
			//m_Process.StartInfo.WindowStyle = m_ProcessWindowStyle.Hidden;
			//m_Process.StartInfo.ErrorDialog = true;
			WindowHandle = IntPtr.Zero;
		}

		public static bool Start() {
			if (Process != null) {
				try {
					Process.Start();
					Process.WaitForInputIdle(3000);
					for (int i = 0; i < 10; ++i) {
						WindowHandle = getDosboxWindowHandle();
						if (WindowHandle != IntPtr.Zero) {
							OnProcessStarted(EventArgs.Empty);
							break;
						}
						Thread.CurrentThread.Join(500);
					}
					return true;
				}
				catch (Exception ex) {
					MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + Process.StartInfo.FileName + Environment.NewLine + Process.StartInfo.Arguments, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			return false;
		}

		public static Process Process { get; private set; }

	    public static IntPtr WindowHandle { get; private set; }

	    static protected void OnProcessStarted(EventArgs e) {
			if (ProcessStarted.GetInvocationList().Length > 0) {
				ProcessStarted(Process, e);
			}
		}

		static IntPtr getDosboxWindowHandle() {
			// FIXME: use EnumWindows.
			// FIXME: m_Process may have exited.
			IntPtr hwnd = PInvoke.User32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "SDL_app", null);
			while (hwnd != IntPtr.Zero) {
				uint pid;
				NativeMethods.GetWindowThreadProcessId(hwnd, out pid);
				if (pid == Process.Id) {
					break;
				}
				hwnd = PInvoke.User32.FindWindowEx(IntPtr.Zero, hwnd, "SDL_app", null);
			}
			return hwnd;
		}
	}
}
