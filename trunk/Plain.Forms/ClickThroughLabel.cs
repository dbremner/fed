using System;
using System.Windows.Forms;
using Plain.Native;

namespace Plain.Forms {
	public class ClickThroughLabel : System.Windows.Forms.Label {
		protected override void WndProc(ref Message m) {
			switch (m.Msg) {
			case NativeMethods.WM_NCHITTEST:
				m.Result = new IntPtr(NativeMethods.HTTRANSPARENT);
				return;
			}
			base.WndProc(ref m);
		}
	}
}
