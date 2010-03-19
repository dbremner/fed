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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Plain.Forms {
	public partial class ComboButton : UserControl {
		public ComboButton() {
			InitializeComponent();
			m_PreferredSize = getPreferredSize();
			base.Size = m_PreferredSize;
			base.SetStyle(ControlStyles.Selectable, false);
		}

		public override Size GetPreferredSize(Size proposedSize) {
			return m_PreferredSize;
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DropDownComboBox ComboBox {
			get { return comboBox; }
		}

		//[Browsable(false)]
		public new Size PreferredSize {
			get { return m_PreferredSize; }
		}

		public new Size Size {
			set {
				if (base.AutoSize == false) {
					base.Size = value;
				}
			}
			get { return base.Size; }
		}

		public new int Width {
			set {
				if (base.AutoSize == false) {
					base.Width = value;
				}
			}
			get { return base.Width; }
		}

		public new int Height {
			set {
				if (base.AutoSize == false) {
					base.Height = value;
				}
			}
			get { return base.Height; }
		}

		public Rectangle VirtualRectangle {
			set { m_VirtualRectangle = value; }
			get { return m_VirtualRectangle; }
		}

		Size m_PreferredSize;
		Rectangle m_VirtualRectangle;
		bool _IsMouseDown;
		bool _HasMouse;

		protected override void OnAutoSizeChanged(EventArgs e) {
			base.OnAutoSizeChanged(e);
			if (base.AutoSize) {
				base.Size = m_PreferredSize;
			}
		}

		protected override void OnLayout(LayoutEventArgs e) {
			base.OnLayout(e);
			comboBox.Location = new Point(base.Width - comboBox.Width, base.Height - comboBox.Height);
		}

		protected override void OnSystemColorsChanged(EventArgs e) {
			base.OnSystemColorsChanged(e);
			m_PreferredSize = getPreferredSize();
			if (base.AutoSize) {
				base.Size = m_PreferredSize;
			}
		}

		private void comboBox_SelectedIndexChanged(object sender, EventArgs e) {
			/*Control ctrl = base.Parent;
			while (ctrl != null) {
				if (ctrl.CanSelect) {
					ctrl.Select();
					break;
				}
				ctrl = ctrl.Parent;
			}*/
		}

		private void label_MouseEnter(object sender, EventArgs e) {
			_HasMouse = true;
			label.Refresh();
		}

		private void label_MouseDown(object sender, MouseEventArgs e) {
			_IsMouseDown = (e.Button & MouseButtons.Left) != 0;
			label.Refresh();
			if (comboBox.DroppedDown) {
				comboBox.DroppedDown = false;
				timer.Enabled = false;
			}
			else {
				comboBox.DroppedDown = true;
				timer.Enabled = true;
			}
		}

		private void label_MouseUp(object sender, MouseEventArgs e) {
			if (_IsMouseDown) {
				if ((e.Button & MouseButtons.Left) == 0) {
					_IsMouseDown = false;
					label.Refresh();
				}
			}
		}

		private void label_MouseLeave(object sender, EventArgs e) {
			_HasMouse = false;
			label.Refresh();
		}

		private void label_Paint(object sender, PaintEventArgs e) {
			ComboBoxState cboState;
			ButtonState btnState;
			if (_IsMouseDown) {
				cboState = ComboBoxState.Pressed;
				btnState = ButtonState.Pushed;
			}
			else if (_HasMouse) {
				cboState = ComboBoxState.Hot;
				btnState = ButtonState.Normal;
			}
			else {
				cboState = ComboBoxState.Normal;
				btnState = ButtonState.Normal;
			}
			Rectangle r = m_VirtualRectangle.IsEmpty ? label.Bounds : m_VirtualRectangle;
			if (ComboBoxRenderer.IsSupported) {
				ComboBoxRenderer.DrawDropDownButton(e.Graphics, r, cboState);
			}
			else {
				ControlPaint.DrawComboButton(e.Graphics, r, btnState);
			}
		}

		private void timer_Tick(object sender, EventArgs e) {
			bool needPaint = false;
			if (_IsMouseDown) {
				bool isMouseDown = (Control.MouseButtons & MouseButtons.Left) != 0;
				if (isMouseDown == false) {
					_IsMouseDown = false;
					timer.Enabled = false;
					needPaint = true;
				}
			}
			/*
			bool hasMouse = label.Bounds.Contains(label.PointToClient(Control.MousePosition));
			if (_HasMouse != hasMouse) {
				_HasMouse = hasMouse;
				needPaint = true;
			}
			*/
			if (needPaint) {
				label.Refresh();
			}
		}

		int getComboButtonWidth() {
			return SystemInformation.VerticalScrollBarArrowHeight;
		}

		Size getPreferredSize() {
			return new Size(getComboButtonWidth(), comboBox.Height);
		}
	}
}
