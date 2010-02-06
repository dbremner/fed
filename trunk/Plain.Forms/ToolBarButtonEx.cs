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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Plain.Native;

namespace Plain.Forms {
	public class ToolBarButtonEx : System.Windows.Forms.ToolBarButton {
		/// <summary>
		/// Gets or sets a value indicating whether the ToolBarButtonEx automatically fills the available space on the ToolBarEx as the form is resized.
		/// </summary>
		[DefaultValue(false)]
		[Description("Gets or sets a value indicating whether the ToolBarButtonEx automatically fills the available space on the ToolBarEx as the form is resized.")]
		public bool Spring {
			set {
				m_Spring = value;
				if (base.Parent != null) {
					lock (base.Parent) {
						ToolBarEx barEx = base.Parent as ToolBarEx;
						if (barEx != null) {
							barEx.UpdateButtonsWidth(this);
						}
					}
				}
			}
			get { return m_Spring; }
		}

		/// <summary>
		/// Gets or sets the width in pixels of a ToolBarButtonEx.
		/// </summary>
		[Browsable(false)]
		public int Width {
			set {
				if (base.Parent != null && base.Parent.IsHandleCreated) {
					NativeMethods.TBBUTTONINFO tbinfo = new NativeMethods.TBBUTTONINFO();
					tbinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.TBBUTTONINFO));
					tbinfo.dwMask = NativeMethods.TBIF_SIZE;
					tbinfo.cx = (ushort) value;
					Reflector _WindowsForms_ = new Reflector("System.Windows.Forms");
					try {
						int index = (int) _WindowsForms_.CallAs(typeof(ToolBarButton), this, "FindButtonIndex", null);
						NativeMethods.SendMessage(base.Parent.Handle, NativeMethods.TB_SETBUTTONINFOW, index, ref tbinfo);
					}
					catch { }
				}
			}
			get {
				if (base.Parent != null && base.Parent.IsHandleCreated) {
					NativeMethods.TBBUTTONINFO tbinfo = new NativeMethods.TBBUTTONINFO();
					tbinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.TBBUTTONINFO));
					tbinfo.dwMask = NativeMethods.TBIF_SIZE;
					Reflector _WindowsForms_ = new Reflector("System.Windows.Forms");
					try {
						int index = (int) _WindowsForms_.CallAs(typeof(ToolBarButton), this, "FindButtonIndex", null);
						NativeMethods.SendMessage(base.Parent.Handle, NativeMethods.TB_GETBUTTONINFOW, index, ref tbinfo);
						return tbinfo.cx;
					}
					catch { }
				}
				return base.Rectangle.Width;
			}
		}

		bool m_Spring;
	}
}
