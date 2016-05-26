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
		public FormConfig() {
			base.Name = typeof(Form).Name;
			WindowState = FormWindowState.Normal;
			Opacity = 1;
		}

		public override void SaveTo(INI ini) {
			ini.Section = base.Name;
			base.writeProp(ini, "DesktopBounds", m_DesktopBounds);
			base.writeProp(ini, "Size", Size);
			base.writeProp(ini, "WindowState", WindowState);
			base.writeProp(ini, "Opacity", Opacity);
		}

		public override void LoadFrom(INI ini) {
			object value;
			ini.Section = base.Name;
			if (base.readProp(ini, "DesktopBounds", out value)) {
				this.DesktopBounds = (Rectangle) value;
			}
			if (base.readProp(ini, "Size", out value)) {
				Size = (Size) value;
			}
			if (base.readProp(ini, "WindowState", out value)) {
				WindowState = (FormWindowState) value;
			}
			if (base.readProp(ini, "Opacity", out value)) {
				if (0.2 <= (double) value) {
					Opacity = (double) value;
				}
			}
		}

		public bool LocationSet { get; private set; }

	    public Size Size { set; get; }

	    public Rectangle DesktopBounds {
			set {
				m_DesktopBounds = value;
				LocationSet = true;
			}
			get { return m_DesktopBounds; }
		}

		public FormWindowState WindowState { set; get; }

	    public double Opacity { set; get; }

	    Rectangle m_DesktopBounds;
	}

	public class GameListFormConfig : FormConfig {
		public GameListFormConfig()
			: base() {
			base.Name = typeof(GameListForm).Name;
			base.Size = new Size(500, 400);
			SearchWidth = 128;
			Column0Width = 200;
			Column1Width = 120;
			Column2Width = 120;
		}

		public override void SaveTo(INI ini) {
			base.SaveTo(ini);
			base.writeProp(ini, "SearchWidth", SearchWidth);
			base.writeProp(ini, "Column0Width", Column0Width);
			base.writeProp(ini, "Column1Width", Column1Width);
			base.writeProp(ini, "Column2Width", Column2Width);
			base.writeProp(ini, "FEDPinned", FEDPinned);
			base.writeProp(ini, "SearchPhrase", SearchPhrase);
		}

		public override void LoadFrom(INI ini) {
			object value;
			base.LoadFrom(ini);
			if (base.readProp(ini, "SearchWidth", out value)) {
				if (64 <= (int) value) {
					SearchWidth = (int) value;
				}
			}
			if (base.readProp(ini, "Column0Width", out value)) {
				if (8 <= (int) value) {
					Column0Width = (int) value;
				}
				else {
					Column0Width = 8;
				}
			}
			if (base.readProp(ini, "Column1Width", out value)) {
				if (8 <= (int) value) {
					Column1Width = (int) value;
				}
				else {
					Column1Width = 8;
				}
			}
			if (base.readProp(ini, "Column2Width", out value)) {
				if (8 <= (int) value) {
					Column2Width = (int) value;
				}
				else {
					Column2Width = 8;
				}
			}
			if (base.readProp(ini, "FEDPinned", out value)) {
				FEDPinned = (bool) value;
			}
			if (base.readProp(ini, "SearchPhrase", out value)) {
				SearchPhrase = (string) value;
			}
		}

		public int SearchWidth { set; get; }

	    public int Column0Width { set; get; }

	    public int Column1Width { set; get; }

	    public int Column2Width { set; get; }

	    public bool FEDPinned { set; get; }

	    public string SearchPhrase { set; get; }
	}
}
