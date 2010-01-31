using System;
using System.Windows.Forms;
using Plain.Native;

namespace Plain.Forms {
	public class TreeViewEx : System.Windows.Forms.TreeView {
		public TreeViewEx()
			: base() {
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		protected override void OnHandleCreated(EventArgs e) {
			base.OnHandleCreated(e);
			NativeMethods.SendMessage(base.Handle, NativeMethods.TVM_SETEXTENDEDSTYLE, NativeMethods.TVS_EX_DOUBLEBUFFER | NativeMethods.TVS_EX_NOINDENTSTATE | NativeMethods.TVS_EX_FADEINOUTEXPANDOS, NativeMethods.TVS_EX_DOUBLEBUFFER | NativeMethods.TVS_EX_NOINDENTSTATE | NativeMethods.TVS_EX_FADEINOUTEXPANDOS);
			NativeMethods.SetWindowTheme(base.Handle, "explorer", null);
			base.Indent = 0;
		}
	}
}
