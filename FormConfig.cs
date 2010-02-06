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

using System.Drawing;
using System.Windows.Forms;
using Plain.IO;

namespace DosboxApp {
	public class FormConfig : BaseConfig {
		public static Point CenterInScreen(Size size) {
			Rectangle rect = Screen.FromPoint(Cursor.Position).WorkingArea;
			return new Point((rect.Left + rect.Right - size.Width) / 2, (rect.Top + rect.Bottom - size.Height) / 2);
		}

		public FormConfig() {
			m_WindowState = FormWindowState.Normal;
			m_Opacity = 1;
		}

		// TODO: move these form functions to form.
		public virtual void SaveFrom(Form form) {
			m_WindowState = form.WindowState;
			if (form.WindowState != FormWindowState.Normal) {
				form.WindowState = FormWindowState.Normal;
			}
			m_Location = form.Location;
			m_Size = form.ClientSize;
			m_Opacity = form.Opacity;
		}

		public override void SaveTo(INI ini) {
			ini.Section = base.Name;
			base.writeProp(ini, "Location", m_Location);
			base.writeProp(ini, "Opacity", m_Opacity);
			base.writeProp(ini, "Size", m_Size);
			base.writeProp(ini, "WindowState", m_WindowState);
		}

		public override void LoadFrom(INI ini) {
			object value;
			ini.Section = base.Name;
			if (base.readProp(ini, "Location", out value)) {
				this.Location = (Point) value;
			}
			if (base.readProp(ini, "Opacity", out value)) {
				if (0.2 <= (double) value) {
					m_Opacity = (double) value;
				}
			}
			if (base.readProp(ini, "Size", out value)) {
				m_Size = (Size) value;
			}
			if (base.readProp(ini, "WindowState", out value)) {
				m_WindowState = (FormWindowState) value;
			}
		}

		public virtual void LoadTo(Form form) {
			form.ClientSize = m_Size;
			if (m_LocationSet) {
				form.Location = m_Location;
				bool bVisible = false;
				foreach (Screen sc in Screen.AllScreens) {
					if (sc.WorkingArea.IntersectsWith(form.Bounds)) {
						bVisible = true;
						break;
					}
				}
				if (bVisible == false) {
					form.Location = FormConfig.CenterInScreen(form.Size);
				}
			}
			else {
				form.Location = FormConfig.CenterInScreen(form.Size);
			}
			form.WindowState = m_WindowState;
			form.Opacity = m_Opacity;
		}

		public bool LocationSet {
			get { return m_LocationSet; }
		}

		public Point Location {
			set {
				m_Location = value;
				m_LocationSet = true;
			}
			get { return m_Location; }
		}

		public Size Size {
			set { m_Size = value; }
			get { return m_Size; }
		}

		public FormWindowState WindowState {
			set { m_WindowState = value; }
			get { return m_WindowState; }
		}

		public double Opacity {
			set { m_Opacity = value; }
			get { return m_Opacity; }
		}

		bool m_LocationSet;
		Point m_Location;
		protected Size m_Size;
		FormWindowState m_WindowState;
		double m_Opacity;
	}

	public class GameListFormConfig : FormConfig {
		public GameListFormConfig()
			: base() {
			base.Name = typeof(GameListForm).Name;
			m_Size = new Size(500, 400);
			m_SearchWidth = 128;
			m_Column0Width = 240;
			m_Column1Width = 120;
		}

		public override void SaveFrom(Form form) {
			SaveFrom(form as GameListForm);
		}

		public void SaveFrom(GameListForm form) {
			base.SaveFrom(form);
			m_SearchWidth = form.pnlSearch.Width;
			m_Column0Width = form.lvwGame.Columns[0].Width;
			m_Column1Width = form.lvwGame.Columns[1].Width;

		}

		public override void SaveTo(INI ini) {
			base.SaveTo(ini);
			base.writeProp(ini, "SearchWidth", m_SearchWidth);
			base.writeProp(ini, "Column0Width", m_Column0Width);
			base.writeProp(ini, "Column1Width", m_Column1Width);
		}

		public override void LoadFrom(INI ini) {
			object value;
			base.LoadFrom(ini);
			if (base.readProp(ini, "SearchWidth", out value)) {
				if (64 <= (int) value) {
					m_SearchWidth = (int) value;
				}
			}
			if (base.readProp(ini, "Column0Width", out value)) {
				if (32 <= (int) value) {
					m_Column0Width = (int) value;
				}
			}
			if (base.readProp(ini, "Column1Width", out value)) {
				if (32 <= (int) value) {
					m_Column1Width = (int) value;
				}
			}
		}

		public override void LoadTo(Form form) {
			LoadTo(form as GameListForm);
		}

		public void LoadTo(GameListForm form) {
			base.LoadTo(form);
			form.pnlSearch.SetBounds(form.pnlSearch.Parent.ClientSize.Width - m_SearchWidth - 4, 0, m_SearchWidth, 0, BoundsSpecified.X | BoundsSpecified.Width);
			form.lvwGame.Columns[0].Width = m_Column0Width;
			form.lvwGame.Columns[1].Width = m_Column1Width;
		}

		public int SearchWidth {
			set { m_SearchWidth = value; }
			get { return m_SearchWidth; }
		}

		public int Column0Width {
			set { m_Column0Width = value; }
			get { return m_Column0Width; }
		}

		public int Column1Width {
			set { m_Column1Width = value; }
			get { return m_Column1Width; }
		}

		int m_SearchWidth;
		int m_Column0Width;
		int m_Column1Width;
	}
}
