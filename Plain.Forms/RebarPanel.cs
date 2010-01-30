using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Plain.Forms {
	public class RebarPanel : System.Windows.Forms.Panel {
		protected override void OnPaintBackground(PaintEventArgs e) {
			base.OnPaintBackground(e);
			if (VisualStyleRenderer.IsElementDefined(VisualStyleElement.Rebar.Band.Normal)) {
				VisualStyleRenderer renderer = new VisualStyleRenderer(/*VisualStyleElement.Rebar.Band.Normal*/ "rebar", 0, 0);
				renderer.DrawBackground(e.Graphics, base.ClientRectangle);
			}
		}
	}
}
