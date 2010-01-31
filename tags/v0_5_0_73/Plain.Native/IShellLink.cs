using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Plain.Native {
	[ComImport]
	[Guid("000214EE-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IShellLinkA {
		void GetPath(
			[Out]
			[MarshalAs(UnmanagedType.LPStr)] 
			/* [size_is][string][out] */ StringBuilder pszFile,
			/* [in] */ Int32 cch,
			/* [unique][out][in] */ IntPtr pfd,
			/* [in] */ UInt32 fFlags
		);

		void GetIDList(
			/* [out] */ out IntPtr ppidl
		);

		void SetIDList(
			/* [in] */ IntPtr pidl
		);

		void GetDescription(
			[Out]
			[MarshalAs(UnmanagedType.LPStr)] 
			/* [size_is][string][out] */ StringBuilder pszName,
			/* [in] */ Int32 cch
		);

		void SetDescription(
			[MarshalAs(UnmanagedType.LPStr)] 
			/* [string][in] */ String pszName
		);

		void GetWorkingDirectory(
			[Out]
			[MarshalAs(UnmanagedType.LPStr)] 
			/* [size_is][string][out] */ StringBuilder pszDir,
			/* [in] */ Int32 cch
		);

		void SetWorkingDirectory(
			[MarshalAs(UnmanagedType.LPStr)] 
			/* [string][in] */ String pszDir
		);

		void GetArguments(
			[Out]
			[MarshalAs(UnmanagedType.LPStr)] 
			/* [size_is][string][out] */ StringBuilder pszArgs,
			/* [in] */ Int32 cch
		);

		void SetArguments(
			[MarshalAs(UnmanagedType.LPStr)] 
			/* [string][in] */ String pszArgs
		);

		void GetHotkey(
			/* [out] */ out UInt16 pwHotkey
		);

		void SetHotkey(
			/* [in] */ UInt16 wHotkey
		);

		void GetShowCmd(
			/* [out] */ out Int32 piShowCmd
		);

		void SetShowCmd(
			/* [in] */ Int32 iShowCmd
		);

		void GetIconLocation(
			[Out]
			[MarshalAs(UnmanagedType.LPStr)] 
			/* [size_is][string][out] */ StringBuilder pszIconPath,
			/* [in] */ Int32 cch,
			/* [out] */ out Int32 piIcon
		);

		void SetIconLocation(
			[MarshalAs(UnmanagedType.LPStr)] 
			/* [string][in] */ String pszIconPath,
			/* [in] */ Int32 iIcon
		);

		void SetRelativePath(
			[MarshalAs(UnmanagedType.LPStr)] 
			/* [string][in] */ String pszPathRel,
			/* [in] */ UInt32 dwReserved
		);

		void Resolve(
			/* [unique][in] */ IntPtr hwnd,
			/* [in] */ UInt32 fFlags
		);

		void SetPath(
			[MarshalAs(UnmanagedType.LPStr)] 
			/* [string][in] */ String pszFile
		);
	}

	[ComImport]
	[Guid("000214F9-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IShellLinkW {
		void GetPath(
			[Out]
			[MarshalAs(UnmanagedType.LPWStr)] 
			/* [size_is][string][out] */ StringBuilder pszFile,
			/* [in] */ Int32 cch,
			/* [unique][out][in] */ IntPtr pfd,
			/* [in] */ UInt32 fFlags
		);

		void GetIDList(
			/* [out] */ out IntPtr ppidl
		);

		void SetIDList(
			/* [in] */ IntPtr pidl
		);

		void GetDescription(
			[Out]
			[MarshalAs(UnmanagedType.LPWStr)] 
			/* [size_is][string][out] */ StringBuilder pszName,
			/* [in] */ Int32 cch
		);

		void SetDescription(
			[MarshalAs(UnmanagedType.LPWStr)] 
			/* [string][in] */ String pszName
		);

		void GetWorkingDirectory(
			[Out]
			[MarshalAs(UnmanagedType.LPWStr)] 
			/* [size_is][string][out] */ StringBuilder pszDir,
			/* [in] */ Int32 cch
		);

		void SetWorkingDirectory(
			[MarshalAs(UnmanagedType.LPWStr)] 
			/* [string][in] */ String pszDir
		);

		void GetArguments(
			[Out]
			[MarshalAs(UnmanagedType.LPWStr)] 
			/* [size_is][string][out] */ StringBuilder pszArgs,
			/* [in] */ Int32 cch
		);

		void SetArguments(
			[MarshalAs(UnmanagedType.LPWStr)] 
			/* [string][in] */ String pszArgs
		);

		void GetHotkey(
			/* [out] */ out UInt16 pwHotkey
		);

		void SetHotkey(
			/* [in] */ UInt16 wHotkey
		);

		void GetShowCmd(
			/* [out] */ out Int32 piShowCmd
		);

		void SetShowCmd(
			/* [in] */ Int32 iShowCmd
		);

		void GetIconLocation(
			[Out]
			[MarshalAs(UnmanagedType.LPWStr)] 
			/* [size_is][string][out] */ StringBuilder pszIconPath,
			/* [in] */ Int32 cch,
			/* [out] */ out Int32 piIcon
		);

		void SetIconLocation(
			[MarshalAs(UnmanagedType.LPWStr)] 
			/* [string][in] */ String pszIconPath,
			/* [in] */ Int32 iIcon
		);

		void SetRelativePath(
			[MarshalAs(UnmanagedType.LPWStr)] 
			/* [string][in] */ String pszPathRel,
			/* [in] */ UInt32 dwReserved
		);

		void Resolve(
			/* [unique][in] */ IntPtr hwnd,
			/* [in] */ UInt32 fFlags
		);

		void SetPath(
			[MarshalAs(UnmanagedType.LPWStr)] 
			/* [string][in] */ String pszFile
		);
	}
}
