using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DosboxApp {
	public partial class FEDFormEx : Form {
		public static Point CenterInScreen(Size size) {
			Rectangle rect = Screen.FromPoint(Cursor.Position).WorkingArea;
			return new Point((rect.Left + rect.Right - size.Width) / 2, (rect.Top + rect.Bottom - size.Height) / 2);
		}

		public FEDFormEx() {
			InitializeComponent();
			m_OpacityNormal = 1;
			m_OpacityOnSizeMove = 1;
		}

		public virtual void SaveTo(FormConfig config) {
			config.WindowState = base.WindowState;
			if (base.WindowState != FormWindowState.Normal) {
				base.WindowState = FormWindowState.Normal;
			}
			config.DesktopBounds = base.DesktopBounds;
			config.Size = base.Size;
			config.Opacity = base.Opacity;
		}

		public virtual void LoadFrom(FormConfig config) {
			if (config.LocationSet) {
				base.DesktopBounds = config.DesktopBounds;
				base.Size = config.Size;

				bool bVisible = false;
				foreach (Screen sc in Screen.AllScreens) {
					if (sc.WorkingArea.IntersectsWith(base.Bounds)) {
						bVisible = true;
						break;
					}
				}
				if (bVisible == false) {
					base.Location = FEDFormEx.CenterInScreen(base.Size);
				}
			}
			else {
				base.Size = config.Size;
				base.Location = FEDFormEx.CenterInScreen(base.Size);
			}
			base.WindowState = config.WindowState;
			base.Opacity = config.Opacity;
		}

		[DefaultValue(1.0)]
		[TypeConverter(typeof(OpacityConverter))]
		public double OpacityNormal {
			set {
				m_OpacityNormal = value;
				if (m_SizingMoving == false) {
					base.Opacity = m_OpacityNormal;
				}
			}
			get { return m_OpacityNormal; }
		}

		[DefaultValue(1.0)]
		[TypeConverter(typeof(OpacityConverter))]
		public double OpacityOnSizeMove {
			set {
				m_OpacityOnSizeMove = value;
				if (m_SizingMoving) {
					base.Opacity = m_OpacityOnSizeMove;
				}
			}
			get { return m_OpacityOnSizeMove; }
		}

		double m_OpacityNormal;
		double m_OpacityOnSizeMove;
		bool m_SizingMoving;

		private void FEDFormEx_ResizeBegin(object sender, EventArgs e) {
			m_SizingMoving = true;
			base.Opacity = m_OpacityOnSizeMove;
		}

		private void FEDFormEx_ResizeEnd(object sender, EventArgs e) {
			base.Opacity = m_OpacityNormal;
			m_SizingMoving = false;
		}
	}
}
