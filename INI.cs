using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Plain;
using Plain.Native;

namespace DosboxApp {
	public class INI {
		public INI(string path) {
			if (string.IsNullOrEmpty(path)) {
				throw new ArgumentNullException("path");
			}
			m_FileName = path;
		}

		public void Write(string key, string value) {
			check_section();
			check_key(key);
			NativeMethods.WritePrivateProfileString(m_Section, key, value, m_FileName);
		}

		public string Read(string key) {
			check_section();
			check_key(key);
			StringBuilder sb = new StringBuilder(256);
			uint ret = NativeMethods.GetPrivateProfileString(m_Section, key, string.Empty, sb, (uint) sb.Capacity, m_FileName);
			if (ret == 0) {
				int err = Marshal.GetLastWin32Error();
				if (err != 0) {
#if USE_THROW
					throw new Win32Exception(err);
#endif
				}
				return string.Empty;
			}
			return sb.ToString();
		}

		public uint ReadInt(string key) {
			check_section();
			check_key(key);
			uint value = NativeMethods.GetPrivateProfileInt(m_Section, key, 0, m_FileName);
			int err = Marshal.GetLastWin32Error();
			if (err != 0) {
#if USE_THROW
				throw new Win32Exception(err);
#endif
			}
			return value;
		}

		public void Delete(string key) {
			Write(key, null);
		}

		public void WriteSection(string data) {
			DeleteSection();
			if (data != null) {
				data = data.Replace(Environment.NewLine, "\0");
				bool ret = NativeMethods.WritePrivateProfileSection(m_Section, data, m_FileName);
				if (ret == false) {
					int err = Marshal.GetLastWin32Error();
					if (err != 0) {
#if USE_THROW
						throw new Win32Exception(err);
#endif
					}
				}
			}
		}

		public string ReadSection() {
			check_section();
			string s = string.Empty;
			IntPtr lpsz = Marshal.AllocHGlobal(MAX_SECTIONDATA);
			if (lpsz != IntPtr.Zero) {
				uint num = NativeMethods.GetPrivateProfileSection(m_Section, lpsz, MAX_SECTIONDATA, m_FileName);
				int err = Marshal.GetLastWin32Error();
				if (err != 0) {
					Marshal.FreeHGlobal(lpsz);
#if USE_THROW
					throw new Win32Exception(err);
#endif
				}
				s = Marshal.PtrToStringAnsi(lpsz, (int) num);
				string[] lines = s.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
				s = string.Empty;
				for (int i = 0; i < lines.Length; ++i) {
					if (lines[i].Trim().StartsWith("#") == false) {
						s += lines[i] + Environment.NewLine;
					}
				}
				Marshal.FreeHGlobal(lpsz);
			}
			return s;
		}

		public void DeleteSection() {
			check_section();
			bool ret = NativeMethods.WritePrivateProfileSection(m_Section, null, m_FileName);
			if (ret == false) {
				int err = Marshal.GetLastWin32Error();
				if (err != 0) {
#if USE_THROW
					throw new Win32Exception(err);
#endif
				}
			}
		}

		public string FileName {
			get { return m_FileName; }
		}

		public string Section {
			set {
				if (string.IsNullOrEmpty(value)) {
					throw new ArgumentNullException("Section");
				}
				m_Section = value;
			}
			get { return m_Section; }
		}

		const int MAX_SECTIONDATA = 65535;
		string m_FileName;
		string m_Section;

		void check_section() {
			if (string.IsNullOrEmpty(m_Section)) {
				throw new ArgumentNullException("Section");
			}
		}

		void check_key(string key) {
			if (string.IsNullOrEmpty(key)) {
				throw new ArgumentNullException("key");
			}
		}
	}
}
