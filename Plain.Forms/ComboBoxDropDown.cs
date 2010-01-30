using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Plain.Forms {
	public class ComboBoxDropDown : System.Windows.Forms.ComboBox {
		public ComboBoxDropDown()
			: base() {
			base.SetStyle(ControlStyles.Selectable, false);
			base.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			base.TabStop = false;
		}

		[Browsable(false)]
		[DefaultValue(AnchorStyles.Bottom | AnchorStyles.Right)]
		public new AnchorStyles Anchor {
			internal set { base.Anchor = value; }
			get { return base.Anchor; }
		}

		[Browsable(false)]
		public new DockStyle Dock {
			internal set { base.Dock = value; }
			get { return base.Dock; }
		}

		[Browsable(false)]
		public new Point Location {
			internal set { base.Location = value; }
			get { return base.Location; }
		}

		[Browsable(false)]
		public new int Left {
			internal set { base.Left = value; }
			get { return base.Left; }
		}

		[Browsable(false)]
		public new int Top {
			internal set { base.Top = value; }
			get { return base.Top; }
		}

		[Browsable(false)]
		[DefaultValue(false)]
		public new bool TabStop {
			internal set { base.TabStop = value; }
			get { return base.TabStop; }
		}
	}
}
