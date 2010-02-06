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
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Plain.Design;

namespace Plain.Forms {
	[Designer(typeof(ControlDesigner))]
	public class ToolBarEx : System.Windows.Forms.ToolBar {
		/*
		public ToolBarEx()
			: base() {
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		protected override void OnHandleCreated(EventArgs e) {
			base.OnHandleCreated(e);
			NativeMethods.SetWindowTheme(this.Handle, "explorer", null);
			NativeMethods.SendMessage(base.Handle, NativeMethods.TB_SETWINDOWTHEME, 0, "SearchButton");
		}
		*/

		protected override void OnResize(EventArgs e) {
			base.OnResize(e);
			UpdateButtonsWidth(null);
		}

#if DEBUG
		[Editor(typeof(ToolbarButtonExCollectionEditor), typeof(UITypeEditor))]
		public new ToolBarButtonCollection Buttons {
			get { return base.Buttons; }
		}
#endif

		public void UpdateButtonsWidth(ToolBarButtonEx changedButton) {
			if (base.IsHandleCreated) {
				int cxEmpty = base.ClientSize.Width;
				List<ToolBarButtonEx> springs = new List<ToolBarButtonEx>(base.Buttons.Count);
				foreach (ToolBarButton button in base.Buttons) {
					if (button.Visible) {
						bool include = true;
						ToolBarButtonEx buttonEx = button as ToolBarButtonEx;
						if (buttonEx != null) {
							if (buttonEx.Spring) {
								springs.Add(buttonEx);
								include = false;
							}
							else if (buttonEx == changedButton) {
								buttonEx.Width = base.ButtonSize.Width;
							}
						}
						if (include) {
							cxEmpty -= button.Rectangle.Width;
						}
					}
				}
				if (springs.Count > 0) {
					cxEmpty /= springs.Count;
				}
				foreach (ToolBarButtonEx buttonEx in springs) {
					if (buttonEx.Visible) {
						buttonEx.Width = cxEmpty;
					}
				}
			}
		}
	}
}
