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
		public ComboBoxDropDown ComboBox {
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

		Size m_PreferredSize;
		bool _IsMouseDown;
		bool _HasMouse;

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
			Control ctrl = base.Parent;
			while (ctrl != null) {
				if (ctrl.CanSelect) {
					ctrl.Select();
					break;
				}
				ctrl = ctrl.Parent;
			}
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
			if (ComboBoxRenderer.IsSupported) {
				ComboBoxRenderer.DrawDropDownButton(e.Graphics, label.Bounds, cboState);
			}
			else {
				ControlPaint.DrawComboButton(e.Graphics, label.Bounds, btnState);
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
			VScrollBar vsb = new VScrollBar();
			int width = vsb.Width;
			vsb.Dispose();
			vsb = null;
			return width;
		}

		Size getPreferredSize() {
			return new Size(getComboButtonWidth(), comboBox.Height);
		}
	}
}
