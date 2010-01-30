using System;
using System.Windows.Forms;
using Plain.Native;

namespace Plain.Forms {
	public class TabControlEx : System.Windows.Forms.TabControl {
		public TabControlEx()
			: base() {
			//SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			//SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		protected override void OnHandleCreated(EventArgs e) {
			base.OnHandleCreated(e);
			//NativeMethods.SetWindowTheme(base.Handle, "BrowserTab", null);
			//NativeMethods.SendMessage(base.Handle, NativeMethods.CCM_SETWINDOWTHEME, 0, "BrowserTab");
		}
	}
}
