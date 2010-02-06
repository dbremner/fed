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
using System.Text;

namespace Plain.Native {
	public static class NativeMethods {
		public static readonly Guid CLSID_ShellLink = new Guid("00021401-0000-0000-C000-000000000046");

		public static readonly Version WindowsVista = new Version(6, 0);
		public static readonly Version Windows2000 = new Version(5, 0);

		public static Int16 LOWORD(Int32 dword) {
			return (Int16) dword;
		}
		public static Int16 HIWORD(Int32 dword) {
			return (Int16) ((dword >> 16) & 0xFFFF);
		}
		public static UInt32 MAKELONG(UInt16 a, UInt16 b) {
			return ((UInt32) (((UInt16) (((UInt64) (a)) & 0xffff)) | ((UInt32) ((UInt16) (((UInt64) (b)) & 0xffff))) << 16));
		}
		public static byte LOBYTE(UInt16 w) {
			return ((Byte) (((UInt64) (w)) & 0xff));
		}
		public static byte HIBYTE(UInt16 w) {
			return ((Byte) ((((UInt64) (w)) >> 8) & 0xff));
		}
		public static UInt16 MAKEWORD(Byte a, Byte b) {
			return ((UInt16) (((Byte) (((UInt64) (a)) & 0xff)) | ((UInt16) ((Byte) (((UInt64) (b)) & 0xff))) << 8));
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, Int32 nMaxCount);

		[DllImport("user32.dll")]
		public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, String lpszClass, IntPtr lpszWindow);

		[DllImport("user32.dll")]
		public static extern IntPtr GetDlgItem(IntPtr hDlg, Int32 nIDDlgItem);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, String lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, StringBuilder lParam);

		// Must not be OFNOTIFY or my fake CDN_FILEOK WM_NOTIFY gets everything 0. (Use ref?)
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, UIntPtr wParam, ref OFNOTIFY lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, Int32 Msg, Int32 wParam, ref LVGROUP lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, ref NMLVEMPTYMARKUP lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, ref TBBUTTONINFO lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, StringBuilder wParam, Int32 lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int PostMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr GetAncestor(IntPtr hwnd, UInt32 gaFlags);

		[DllImport("user32.dll")]
		public static extern IntPtr GetFocus();

		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern Int32 SetWindowTheme(IntPtr hwnd, String pszSubAppName, String pszSubIdList);

		[DllImport("comdlg32.dll")]
		public static extern int CommDlgExtendedError();

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern bool WritePrivateProfileString(String lpAppName, String lpKeyName, String lpString, String lpFileName);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern uint GetPrivateProfileString(String lpAppName, String lpKeyName, String lpDefault, StringBuilder lpReturnedString, UInt32 nSize, String lpFileName);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern bool WritePrivateProfileSection(String lpAppName, String lpString, String lpFileName);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern uint GetPrivateProfileSection(String lpAppName, IntPtr lpReturnedString, UInt32 nSize, String lpFileName);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetPrivateProfileInt(String lpAppName, String lpKeyName, Int32 nDefault, String lpFileName);

		[DllImport("dwmapi.dll")]
		public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int GetWindowLong(IntPtr hWnd, Int32 nIndex);

		[DllImport("user32.dll", SetLastError = true, EntryPoint = "GetWindowLongPtr")]
		public static extern IntPtr GetWindowLong64(IntPtr hWnd, Int32 nIndex);

		public static IntPtr GetWindowLongPtr(IntPtr hWnd, Int32 nIndex) {
			if (IntPtr.Size == 4) {
				return (IntPtr) GetWindowLong(hWnd, nIndex);
			}
			else {
				return GetWindowLong64(hWnd, nIndex);
			}
		}

		[DllImport("user32.dll", SetLastError = true)]
		public static extern int SetWindowLong(IntPtr hWnd, Int32 nIndex, Int32 dwNewLong);

		[DllImport("user32.dll", SetLastError = true, EntryPoint = "SetWindowLongPtr")]
		public static extern IntPtr SetWindowLong64(IntPtr hWnd, Int32 nIndex, IntPtr dwNewLong);

		public static IntPtr SetWindowLongPtr(IntPtr hWnd, Int32 nIndex, IntPtr dwNewLong) {
			if (IntPtr.Size == 4) {
				return (IntPtr) SetWindowLong(hWnd, nIndex, dwNewLong.ToInt32());
			}
			else {
				return SetWindowLong64(hWnd, nIndex, dwNewLong);
			}
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern Int32 GetShortPathName(String lpszLongPath, StringBuilder lpszShortPath, Int32 cchBuffer);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern Int32 GetLongPathName(String lpszShortPath, StringBuilder lpszLongPath, Int32 cchBuffer);

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
			public String lpstrFilter;
			public IntPtr lpstrCustomFilter;
			public UInt32 nMaxCustFilter;
			public UInt32 nFilterIndex;
			public String lpstrFile;
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
			public String pszHeader;
			public Int32 cchHeader;
			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszFooter;
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
			public String pszHeader;
			public Int32 cchHeader;
			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszFooter;
			public Int32 cchFooter;
			public Int32 iGroupId;
			public UInt32 stateMask;
			public UInt32 state;
			public UInt32 uAlign;
			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszSubtitle;
			public UInt32 cchSubtitle;
			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszTask;
			public UInt32 cchTask;
			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszDescriptionTop;
			public UInt32 cchDescriptionTop;
			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszDescriptionBottom;
			public UInt32 cchDescriptionBottom;
			public Int32 iTitleImage;
			public Int32 iExtendedImage;
			public Int32 iFirstItem;		// Read only
			public UInt32 cItems;			// Read only
			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszSubsetTitle;	// NULL if group is not subset
			public UInt32 cchSubsetTitle;
			public LVGROUP(LVGROUP_xp lvgxp) {
				this.cbSize = (uint) Marshal.SizeOf(typeof(LVGROUP));
				this.mask = lvgxp.mask;
				this.pszHeader = lvgxp.pszHeader;
				this.cchHeader = lvgxp.cchHeader;
				this.pszFooter = lvgxp.pszFooter;
				this.cchFooter = lvgxp.cchFooter;
				this.iGroupId = lvgxp.iGroupId;
				this.stateMask = lvgxp.stateMask;
				this.state = lvgxp.state;
				this.uAlign = lvgxp.uAlign;
				this.pszSubtitle = null;
				this.cchSubtitle = 0;
				this.pszTask = null;
				this.cchTask = 0;
				this.pszDescriptionTop = null;
				this.cchDescriptionTop = 0;
				this.pszDescriptionBottom = null;
				this.cchDescriptionBottom = 0;
				this.iTitleImage = 0;
				this.iExtendedImage = 0;
				this.iFirstItem = 0;
				this.cItems = 0;
				this.pszSubsetTitle = null;
				this.cchSubsetTitle = 0;
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct NMLVEMPTYMARKUP {
			public NMHDR hdr;
			public UInt32 dwFlags;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = L_MAX_URL_LENGTH)]
			public String szMarkup;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT {
			public Int32 left;
			public Int32 top;
			public Int32 right;
			public Int32 bottom;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct TBBUTTONINFO {
			public Int32 cbSize;
			public UInt32 dwMask;
			public Int32 idCommand;
			public Int32 iImage;
			public Byte fsState;
			public Byte fsStyle;
			public UInt16 cx;
			public IntPtr lParam;
			public String pszText;
			public Int32 cchTest;
		}

		[Flags]
		public enum SLGP_FLAGS {
			SLGP_SHORTPATH = 0x1,
			SLGP_UNCPRIORITY = 0x2,
			SLGP_RAWPATH = 0x4,
			SLGP_RELATIVEPRIORITY = 0x8
		};

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
		public const int LVM_GETITEMCOUNT = (LVM_FIRST + 4);
		public const int LVM_INSERTITEMA = (LVM_FIRST + 7);
		public const int LVM_INSERTITEMW = (LVM_FIRST + 77);
		public const int LVM_DELETEITEM = (LVM_FIRST + 8);
		public const int LVM_DELETEALLITEMS = (LVM_FIRST + 9);
		public const int LVM_GETHEADER = (LVM_FIRST + 31);
		public const int LVM_SETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 54);
		public const int LVM_GETEXTENDEDLISTVIEWSTYLE = (LVM_FIRST + 55);
		public const int LVM_SETVIEW = (LVM_FIRST + 142);
		public const int LVM_INSERTGROUP = (LVM_FIRST + 145);
		public const int LVM_SETGROUPINFO = (LVM_FIRST + 147);
		public const int LVM_GETGROUPINFO = (LVM_FIRST + 149);
		public const int LVM_GETEMPTYTEXT = (LVM_FIRST + 204);

		public const uint LVN_FIRST = unchecked(0U - 100U);
		public const uint LVN_INSERTITEM = (LVN_FIRST - 2);
		public const uint LVN_DELETEITEM = (LVN_FIRST - 3);
		public const uint LVN_DELETEALLITEMS = (LVN_FIRST - 4);
		public const uint LVN_BEGINSCROLL = (LVN_FIRST - 80);
		public const uint LVN_ENDSCROLL = (LVN_FIRST - 81);
		public const uint LVN_GETEMPTYMARKUP = (LVN_FIRST - 87);

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

		public const byte HOTKEYF_SHIFT = 0x01;
		public const byte HOTKEYF_CONTROL = 0x02;
		public const byte HOTKEYF_ALT = 0x04;
		public const byte HOTKEYF_EXT = 0x08;

		public const int SW_NORMAL = 1;
		public const int SW_SHOWMINIMIZED = 2;
		public const int SW_SHOWMAXIMIZED = 3;

		public const int L_MAX_URL_LENGTH = (2048 + 32 + 4 /*sizeof("://")*/);	// Need to include NULL?
		public const uint EMF_CENTERED = 0x00000001;

		public const int WM_REFLECT = WM_USER + 0x1c00;

		public const int WS_EX_TRANSPARENT = 0x00000020;
		public const int GWL_EXSTYLE = (-20);

		public const int TV_FIRST = 0x1100;
		public const int TVM_SETEXTENDEDSTYLE = (TV_FIRST + 44);
		public const int TVM_GETEXTENDEDSTYLE = (TV_FIRST + 45);
		public const int TVS_EX_DOUBLEBUFFER = 0x0004;
		public const int TVS_EX_NOINDENTSTATE = 0x0008;
		public const int TVS_EX_FADEINOUTEXPANDOS = 0x0040;

		public const int LVS_EX_DOUBLEBUFFER = 0x00010000;

		public const int WM_HSCROLL = 0x0114;
		public const int WM_VSCROLL = 0x0115;
		public const int SB_LINEUP = 0;
		public const int SB_LINELEFT = 0;
		public const int SB_LINEDOWN = 1;
		public const int SB_LINERIGHT = 1;
		public const int SB_PAGEUP = 2;
		public const int SB_PAGELEFT = 2;
		public const int SB_PAGEDOWN = 3;
		public const int SB_PAGERIGHT = 3;
		public const int SB_THUMBPOSITION = 4;
		public const int SB_THUMBTRACK = 5;
		public const int SB_TOP = 6;
		public const int SB_LEFT = 6;
		public const int SB_BOTTOM = 7;
		public const int SB_RIGHT = 7;
		public const int SB_ENDSCROLL = 8;

		public const int CCM_FIRST = 0x2000;
		public const int CCM_SETWINDOWTHEME = (CCM_FIRST + 0xb);
		public const int TB_SETWINDOWTHEME = CCM_SETWINDOWTHEME;

		public const int TBIF_SIZE = 0x00000040;
		public const int TB_GETBUTTONINFOW = (WM_USER + 63);
		public const int TB_SETBUTTONINFOW = (WM_USER + 64);
		public const int TB_GETBUTTONINFOA = (WM_USER + 65);
		public const int TB_SETBUTTONINFOA = (WM_USER + 66);

		public const int EM_SETMARGINS = 0x00D3;
		public const int EM_GETMARGINS = 0x00D4;
		public const int ECM_FIRST = 0x1500;
		public const int EM_SETCUEBANNER = (ECM_FIRST + 1);
		public const int EM_GETCUEBANNER = (ECM_FIRST + 2);

		public const int EC_LEFTMARGIN = 0x0001;
		public const int EC_RIGHTMARGIN = 0x0002;
		public const int EC_USEFONTINFO = 0xffff;
	}
}
