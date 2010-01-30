using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Plain.Native;

namespace Plain.Wasted {
	public interface ShellLinkInterface : IDisposable {
		void GetPath(StringBuilder pszFile, Int32 cch, IntPtr pfd, UInt32 fFlags);
		void GetIDList(out IntPtr ppidl);
		void SetIDList(IntPtr pidl);
		void GetDescription(StringBuilder pszName, Int32 cch);
		void SetDescription(String pszName);
		void GetWorkingDirectory(StringBuilder pszDir, Int32 cch);
		void SetWorkingDirectory(String pszDir);
		void GetArguments(StringBuilder pszArgs, Int32 cch);
		void SetArguments(String pszArgs);
		void GetHotkey(out UInt16 pwHotkey);
		void SetHotkey(UInt16 wHotkey);
		void GetShowCmd(out Int32 piShowCmd);
		void SetShowCmd(Int32 iShowCmd);
		void GetIconLocation(StringBuilder pszIconPath, Int32 cch, out Int32 piIcon);
		void SetIconLocation(String pszIconPath, Int32 iIcon);
		void SetRelativePath(String pszPathRel, UInt32 dwReserved);
		void Resolve(IntPtr hwnd, UInt32 fFlags);
		void SetPath(String pszFile);
	}

	public class ShellLinkAWrapper : ShellLinkInterface {
		public ShellLinkAWrapper(IShellLinkA shellLinkA) {
			_shellLinkA = shellLinkA;
		}
		~ShellLinkAWrapper() {
			Dispose();
		}
		public void Dispose() {
			Marshal.ReleaseComObject(_shellLinkA);
		}

		public void GetPath(StringBuilder pszFile, int cch, IntPtr pfd, uint fFlags) {
			_shellLinkA.GetPath(pszFile, cch, pfd, fFlags);
		}
		public void GetIDList(out IntPtr ppidl) {
			_shellLinkA.GetIDList(out ppidl);
		}
		public void SetIDList(IntPtr pidl) {
			_shellLinkA.SetIDList(pidl);
		}
		public void GetDescription(StringBuilder pszName, int cch) {
			_shellLinkA.GetDescription(pszName, cch);
		}
		public void SetDescription(string pszName) {
			_shellLinkA.SetDescription(pszName);
		}
		public void GetWorkingDirectory(StringBuilder pszDir, int cch) {
			_shellLinkA.GetWorkingDirectory(pszDir, cch);
		}
		public void SetWorkingDirectory(string pszDir) {
			_shellLinkA.SetWorkingDirectory(pszDir);
		}
		public void GetArguments(StringBuilder pszArgs, int cch) {
			_shellLinkA.GetArguments(pszArgs, cch);
		}
		public void SetArguments(string pszArgs) {
			_shellLinkA.SetArguments(pszArgs);
		}
		public void GetHotkey(out ushort pwHotkey) {
			_shellLinkA.GetHotkey(out pwHotkey);
		}
		public void SetHotkey(ushort wHotkey) {
			_shellLinkA.SetHotkey(wHotkey);
		}
		public void GetShowCmd(out int piShowCmd) {
			_shellLinkA.GetShowCmd(out piShowCmd);
		}
		public void SetShowCmd(int iShowCmd) {
			_shellLinkA.SetShowCmd(iShowCmd);
		}
		public void GetIconLocation(StringBuilder pszIconPath, int cch, out int piIcon) {
			_shellLinkA.GetIconLocation(pszIconPath, cch, out piIcon);
		}
		public void SetIconLocation(string pszIconPath, int iIcon) {
			_shellLinkA.SetIconLocation(pszIconPath, iIcon);
		}
		public void SetRelativePath(string pszPathRel, uint dwReserved) {
			_shellLinkA.SetRelativePath(pszPathRel, dwReserved);
		}
		public void Resolve(IntPtr hwnd, uint fFlags) {
			_shellLinkA.Resolve(hwnd, fFlags);
		}
		public void SetPath(string pszFile) {
			_shellLinkA.SetPath(pszFile);
		}

		IShellLinkA _shellLinkA;
	}

	public class ShellLinkWWrapper : ShellLinkInterface {
		public ShellLinkWWrapper(IShellLinkW shellLinkW) {
			_shellLinkW = shellLinkW;
		}
		~ShellLinkWWrapper() {
			Dispose();
		}
		public void Dispose() {
			Marshal.ReleaseComObject(_shellLinkW);
		}

		public void GetPath(StringBuilder pszFile, int cch, IntPtr pfd, uint fFlags) {
			_shellLinkW.GetPath(pszFile, cch, pfd, fFlags);
		}
		public void GetIDList(out IntPtr ppidl) {
			_shellLinkW.GetIDList(out ppidl);
		}
		public void SetIDList(IntPtr pidl) {
			_shellLinkW.SetIDList(pidl);
		}
		public void GetDescription(StringBuilder pszName, int cch) {
			_shellLinkW.GetDescription(pszName, cch);
		}
		public void SetDescription(string pszName) {
			_shellLinkW.SetDescription(pszName);
		}
		public void GetWorkingDirectory(StringBuilder pszDir, int cch) {
			_shellLinkW.GetWorkingDirectory(pszDir, cch);
		}
		public void SetWorkingDirectory(string pszDir) {
			_shellLinkW.SetWorkingDirectory(pszDir);
		}
		public void GetArguments(StringBuilder pszArgs, int cch) {
			_shellLinkW.GetArguments(pszArgs, cch);
		}
		public void SetArguments(string pszArgs) {
			_shellLinkW.SetArguments(pszArgs);
		}
		public void GetHotkey(out ushort pwHotkey) {
			_shellLinkW.GetHotkey(out pwHotkey);
		}
		public void SetHotkey(ushort wHotkey) {
			_shellLinkW.SetHotkey(wHotkey);
		}
		public void GetShowCmd(out int piShowCmd) {
			_shellLinkW.GetShowCmd(out piShowCmd);
		}
		public void SetShowCmd(int iShowCmd) {
			_shellLinkW.SetShowCmd(iShowCmd);
		}
		public void GetIconLocation(StringBuilder pszIconPath, int cch, out int piIcon) {
			_shellLinkW.GetIconLocation(pszIconPath, cch, out piIcon);
		}
		public void SetIconLocation(string pszIconPath, int iIcon) {
			_shellLinkW.SetIconLocation(pszIconPath, iIcon);
		}
		public void SetRelativePath(string pszPathRel, uint dwReserved) {
			_shellLinkW.SetRelativePath(pszPathRel, dwReserved);
		}
		public void Resolve(IntPtr hwnd, uint fFlags) {
			_shellLinkW.Resolve(hwnd, fFlags);
		}
		public void SetPath(string pszFile) {
			_shellLinkW.SetPath(pszFile);
		}

		IShellLinkW _shellLinkW;
	}
}
