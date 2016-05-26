/*
FED (Front-End for Dosbox), a gaming graphical user interface for the DOSBox emulator.
Copyright (C) 2010  Ka Ki Cheung

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
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;
using Plain.Native;

namespace Plain.IO {
	public class ShortcutFile : IDisposable {
		public ShortcutFile(string path) {
			m_Path = path;
			try {
				_comShellLinkClass = new ShellLink();
				_comPersistFile = (IPersistFile) _comShellLinkClass;
				_comShellLink = (IShellLinkW) _comShellLinkClass;

				if (File.Exists(path)) {
					_comPersistFile.Load(path, 0);
				}
				// INFOTIPSIZE 1024
				// MAX_PATH 260
				_sb = new StringBuilder(1024);
			} catch (Exception ex) {
				throw ex;
			}
		}

		~ShortcutFile() {
			Dispose();
		}

		public void Dispose() {
			if (_comPersistFile != null) {
				Marshal.ReleaseComObject(_comPersistFile);
				_comPersistFile = null;
			}
			if (_comShellLink != null) {
				Marshal.ReleaseComObject(_comShellLink);
				_comShellLink = null;
			}
			if (_comShellLinkClass != null) {
				Marshal.ReleaseComObject(_comShellLinkClass);
				_comShellLinkClass = null;
			}
		}

		public void Save() {
			if (_comPersistFile != null) {
				string filename;
				_comPersistFile.GetCurFile(out filename);
				if (filename == null) {
					_comPersistFile.Save(m_Path, true);
				} else {
					_comPersistFile.Save(null, true);
				}
				_comPersistFile.SaveCompleted(m_Path);
			}
		}

		public string Target {
			set
			{
			    _comShellLink?.SetPath(value);
			}
		    get {
				if (_comShellLink != null) {
					_comShellLink.GetPath(_sb, _sb.Capacity, IntPtr.Zero, (uint) NativeMethods.SLGP_FLAGS.SLGP_UNCPRIORITY);
					return _sb.ToString();
				}
				return null;
			}
		}

		public IntPtr IDList {
			set
			{
			    _comShellLink?.SetIDList(value);
			}
		    get {
				if (_comShellLink != null) {
					IntPtr value;
					_comShellLink.GetIDList(out value);
					return value;
				}
				return IntPtr.Zero;
			}
		}

		public string Description {
			set
			{
			    _comShellLink?.SetDescription(value);
			}
		    get {
				if (_comShellLink != null) {
					_comShellLink.GetDescription(_sb, _sb.Capacity);
					return _sb.ToString();
				}
				return null;
			}
		}

		public string WorkingDirectory {
			set
			{
			    _comShellLink?.SetWorkingDirectory(value);
			}
		    get {
				if (_comShellLink != null) {
					_comShellLink.GetWorkingDirectory(_sb, _sb.Capacity);
					return _sb.ToString();
				}
				return null;
			}
		}

		public string Arguments {
			set
			{
			    _comShellLink?.SetArguments(value);
			}
		    get {
				if (_comShellLink != null) {
					_comShellLink.GetArguments(_sb, _sb.Capacity);
					return _sb.ToString();
				}
				return null;
			}
		}

		// TODO: use bitwise operations...
		public Keys Hotkey {
			set {
				if (_comShellLink != null) {
					byte vk;
					byte mod = 0;
					if ((value & Keys.Shift) != 0) {
						value &= ~Keys.Shift;
						mod |= NativeMethods.HOTKEYF_SHIFT;
					}
					if ((value & Keys.Control) != 0) {
						value &= ~Keys.Control;
						mod |= NativeMethods.HOTKEYF_CONTROL;
					}
					if ((value & Keys.Alt) != 0) {
						value &= ~Keys.Alt;
						mod |= NativeMethods.HOTKEYF_ALT;
					}
					vk = (byte) value;
					_comShellLink.SetHotkey(NativeMethods.MAKEWORD(vk, mod));
				}
			}
			get {
				Keys value = Keys.None;
				if (_comShellLink != null) {
					ushort hotkey;
					_comShellLink.GetHotkey(out hotkey);
					byte vk = NativeMethods.LOBYTE(hotkey);
					byte mod = NativeMethods.HIBYTE(hotkey);
					value = (Keys) vk;
					if ((mod & NativeMethods.HOTKEYF_SHIFT) != 0) {
						value |= Keys.Shift;
					}
					if ((mod & NativeMethods.HOTKEYF_CONTROL) != 0) {
						value |= Keys.Control;
					}
					if ((mod & NativeMethods.HOTKEYF_ALT) != 0) {
						value |= Keys.Alt;
					}
				}
				return value;
			}
		}

		public FormWindowState ShowCmd {
			set {
				if (_comShellLink != null) {
					int nShow = NativeMethods.SW_NORMAL;
					switch (value) {
					case FormWindowState.Minimized:
						nShow = NativeMethods.SW_SHOWMINIMIZED;
						break;
					case FormWindowState.Maximized:
						nShow = NativeMethods.SW_SHOWMAXIMIZED;
						break;
					}
					_comShellLink.SetShowCmd(nShow);
				}
			}
			get {
				FormWindowState value = FormWindowState.Normal;
				if (_comShellLink != null) {
					int nShow;
					_comShellLink.GetShowCmd(out nShow);
					switch (nShow) {
					case NativeMethods.SW_SHOWMINIMIZED:
						value = FormWindowState.Minimized;
						break;
					case NativeMethods.SW_SHOWMAXIMIZED:
						value = FormWindowState.Maximized;
						break;
					}
				}
				return value;
			}
		}

		public IconLocation IconLocation {
			set
			{
			    _comShellLink?.SetIconLocation(value.Path, value.Index);
			}
		    get {
				IconLocation value = IconLocation.Empty;
				if (_comShellLink != null) {
					_comShellLink.GetIconLocation(_sb, _sb.Capacity, out value.Index);
					value.Path = _sb.ToString();
				}
				return value;
			}
		}

		string m_Path;
		ShellLink _comShellLinkClass;
		IPersistFile _comPersistFile;
		IShellLinkW _comShellLink;
		StringBuilder _sb;
	}

	public struct IconLocation {
		public string Path;
		public int Index;
		public static readonly IconLocation Empty = new IconLocation();
	}
}
