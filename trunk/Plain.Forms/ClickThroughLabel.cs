/*
FED (Front-End for Dosbox), a gaming graphical user interface for the DOSBox emulator.
Copyright (C) 2010  Ka Ki Cheung

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
using System.Windows.Forms;
using Plain.Native;

namespace Plain.Forms {
	public class ClickThroughLabel : System.Windows.Forms.Label {
		protected override void WndProc(ref Message m) {
			switch (m.Msg) {
			case NativeMethods.WM_NCHITTEST:
				m.Result = new IntPtr(NativeMethods.HTTRANSPARENT);
				return;
			}
			base.WndProc(ref m);
		}
	}
}
