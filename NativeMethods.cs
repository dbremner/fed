using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Plain {
	public static class NativeMethods {
		public static Int16 LOWORD(Int32 dword) {
			return (Int16) dword;
		}

		public static Int16 HIWORD(Int32 dword) {
			return (Int16) ((dword >> 16) & 0xFFFF);
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

		[DllImport("user32.dll")]
		public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, IntPtr lpszWindow);

		[DllImport("user32.dll")]
		public static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, string lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, StringBuilder lParam);

		// Must not be OFNOTIFY or my fake CDN_FILEOK WM_NOTIFY gets everything 0.
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, UIntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, Int32 Msg, int wParam, ref LVGROUP lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr GetAncestor(IntPtr hwnd, uint gaFlags);

		[DllImport("user32.dll")]
		public static extern IntPtr GetFocus();

		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern Int32 SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

		[DllImport("comdlg32.dll")]
		public static extern int CommDlgExtendedError();

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern bool WritePrivateProfileSection(string lpAppName, string lpString, string lpFileName);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

		[DllImport("dwmapi.dll")]
		public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

		[StructLayout(LayoutKind.Sequential)]
		public struct NMHDR {
			public IntPtr hwndFrom;
			public UIntPtr idFrom;
			public UInt32 code;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct OFNOTIFY {
			[MarshalAs(UnmanagedType.Struct)]
			public NMHDR hdr;
			// Must not be OPENFILENAME or my fake CDN_FILEOK fails in base.WndProc with ExecutionEngineException.
			public IntPtr lpOFN;
			public IntPtr pszFile;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct OPENFILENAME_9x {
			public UInt32 lStructSize;
			public IntPtr hwndOwner;
			public IntPtr hInstance;
			public IntPtr lpstrFilter;
			public IntPtr lpstrCustomFilter;
			public UInt32 nMaxCustFilter;
			public UInt32 nFilterIndex;
			public IntPtr lpstrFile;
			public UInt32 nMaxFile;
			public IntPtr lpstrFileTitle;
			public UInt32 nMaxFileTitle;
			public IntPtr lpstrInitialDir;
			public IntPtr lpstrTitle;
			public UInt32 Flags;
			public UInt16 nFileOffset;
			public UInt16 nFileExtension;
			public IntPtr lpstrDefExt;
			public IntPtr lCustData;
			public IntPtr lpfnHook;
			public IntPtr lpTemplateName;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct OPENFILENAME {
			public UInt32 lStructSize;
			public IntPtr hwndOwner;
			public IntPtr hInstance;
			public string lpstrFilter;
			public IntPtr lpstrCustomFilter;
			public UInt32 nMaxCustFilter;
			public UInt32 nFilterIndex;
			public string lpstrFile;
			public UInt32 nMaxFile;
			public IntPtr lpstrFileTitle;
			public UInt32 nMaxFileTitle;
			public IntPtr lpstrInitialDir;
			public IntPtr lpstrTitle;
			public UInt32 Flags;
			public UInt16 nFileOffset;
			public UInt16 nFileExtension;
			public IntPtr lpstrDefExt;
			public IntPtr lCustData;
			//[MarshalAs(UnmanagedType.FunctionPtr)] fails Marshal.SizeOf
			public IntPtr lpfnHook;
			public IntPtr lpTemplateName;
			public IntPtr pvReserved;
			public UInt32 dwReserved;
			public UInt32 FlagsEx;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MARGINS {
			public Int32 cxLeftWidth;
			public Int32 cxRightWidth;
			public Int32 cyTopHeight;
			public Int32 cyBottomHeight;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct LVGROUP_xp {
			public UInt32 cbSize;
			public UInt32 mask;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszHeader;
			public Int32 cchHeader;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszFooter;
			public Int32 cchFooter;
			public Int32 iGroupId;
			public UInt32 stateMask;
			public UInt32 state;
			public UInt32 uAlign;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct LVGROUP {
			public UInt32 cbSize;
			public UInt32 mask;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszHeader;
			public Int32 cchHeader;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszFooter;
			public Int32 cchFooter;
			public Int32 iGroupId;
			public UInt32 stateMask;
			public UInt32 state;
			public UInt32 uAlign;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszSubtitle;
			public UInt32 cchSubtitle;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszTask;
			public UInt32 cchTask;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszDescriptionTop;
			public UInt32 cchDescriptionTop;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszDescriptionBottom;
			public UInt32 cchDescriptionBottom;
			public Int32 iTitleImage;
			public Int32 iExtendedImage;
			public Int32 iFirstItem;		// Read only
			public UInt32 cItems;			// Read only
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszSubsetTitle;	// NULL if group is not subset
			public UInt32 cchSubsetTitle;
		}

		public const uint CDN_FIRST = unchecked(0U - 601U);
		public const uint CDN_INITDONE = (CDN_FIRST - 0x0000);
		public const uint CDN_SELCHANGE = (CDN_FIRST - 0x0001);
		public const uint CDN_FOLDERCHANGE = (CDN_FIRST - 0x0002);
		public const uint CDN_SHAREVIOLATION = (CDN_FIRST - 0x0003);
		public const uint CDN_HELP = (CDN_FIRST - 0x0004);
		public const uint CDN_FILEOK = (CDN_FIRST - 0x0005);
		public const uint CDN_TYPECHANGE = (CDN_FIRST - 0x0006);

		public const int WM_USER = 0x0400;
		public const int CDM_FIRST = (WM_USER + 100);
		public const int CDM_LAST = (WM_USER + 200);
		public const int CDM_GETSPEC = (CDM_FIRST + 0x0000);
		public const int CDM_GETFILEPATH = (CDM_FIRST + 0x0001);
		public const int CDM_GETFOLDERPATH = (CDM_FIRST + 0x0002);
		public const int CDM_GETFOLDERIDLIST = (CDM_FIRST + 0x0003);
		public const int CDM_SETCONTROLTEXT = (CDM_FIRST + 0x0004);
		public const int CDM_HIDECONTROL = (CDM_FIRST + 0x0005);
		public const int CDM_SETDEFEXT = (CDM_FIRST + 0x0006);

		public const int WM_COMMAND = 0x0111;
		public const int BN_CLICKED = 0;
		// http://msdn.microsoft.com/en-us/library/ms646960%28VS.85%29.aspx#_win32_Explorer_Style_Control_Identifiers
		public const int IDOK = 1;
		public const int stc2 = 0x0441;
		public const int stc3 = 0x0442;
		public const int cmb1 = 0x0470;
		public const int cmb13 = 0x047c;
		public const int edt1 = 0x0480;

		public const int WM_NOTIFY = 0x004E;
		public const int WM_ACTIVATE = 0x0006;
		public const int WA_INACTIVE = 0;
		public const int WA_ACTIVE = 1;
		public const int WA_CLICKACTIVE = 2;

		public const int WM_SHOWWINDOW = 0x0018;
		public const int WM_KEYDOWN = 0x0100;
		public const int VK_RETURN = 0x0D;

		public const int WM_GETDLGCODE = 0x0087;

		public const int WM_CLOSE = 0x0010;
		public const int WM_DESTROY = 0x0002;
		public const int WM_NCDESTROY = 0x0082;

		public const uint GA_PARENT = 1;
		public const uint GA_ROOT = 2;
		public const uint GA_ROOTOWNER = 3;

		public const int LVM_FIRST = 0x1000;
		public const int LVM_INSERTGROUP = (LVM_FIRST + 145);
		public const int LVM_SETGROUPINFO = (LVM_FIRST + 147);
		public const int LVM_GETGROUPINFO = (LVM_FIRST + 149);

		public const uint LVGF_NONE = 0x00000000;
		public const uint LVGF_HEADER = 0x00000001;
		public const uint LVGF_FOOTER = 0x00000002;
		public const uint LVGF_STATE = 0x00000004;
		public const uint LVGF_ALIGN = 0x00000008;
		public const uint LVGF_GROUPID = 0x00000010;
		public const uint LVGF_SUBTITLE = 0x00000100;
		public const uint LVGF_TASK = 0x00000200;
		public const uint LVGF_DESCRIPTIONTOP = 0x00000400;
		public const uint LVGF_DESCRIPTIONBOTTOM = 0x00000800;
		public const uint LVGF_TITLEIMAGE = 0x00001000;
		public const uint LVGF_EXTENDEDIMAGE = 0x00002000;
		public const uint LVGF_ITEMS = 0x00004000;
		public const uint LVGF_SUBSET = 0x00008000;
		public const uint LVGF_SUBSETITEMS = 0x00010000;

		public const uint LVGS_COLLAPSED = 0x00000001;
		public const uint LVGS_COLLAPSIBLE = 0x00000008;

		public const int WM_LBUTTONDOWN = 0x0201;
		public const int WM_LBUTTONUP = 0x0202;
		public const int WM_LBUTTONDBLCLK = 0x0203;

	}
}
