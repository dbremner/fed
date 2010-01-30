using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Plain.Native {
	[ComImport, TypeLibType(TypeLibTypeFlags.FCanCreate), ClassInterface(ClassInterfaceType.None), Guid("00021401-0000-0000-C000-000000000046")]
	public class ShellLink {
		//[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		//public extern ShellLink();
	}

	[ComImport]
	[Guid("000214EE-0000-0000-C000-000000000046")]
	[CoClass(typeof(Plain.Native.ShellLink))]
	public interface NativeShellLinkA : IPersistFile, IShellLinkA {
	}

	[ComImport]
	[Guid("000214F9-0000-0000-C000-000000000046")]
	[CoClass(typeof(Plain.Native.ShellLink))]
	public interface NativeShellLinkW : IPersistFile, IShellLinkW {
	}
}
