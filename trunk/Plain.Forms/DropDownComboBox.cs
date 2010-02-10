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

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Plain.Forms {
	[ToolboxItem(false)]
	public class DropDownComboBox : System.Windows.Forms.ComboBox {
		public DropDownComboBox()
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
