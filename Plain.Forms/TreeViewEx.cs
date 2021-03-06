﻿/*
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
using System.Windows.Forms;
using Plain.Native;

namespace Plain.Forms {
	public class TreeViewEx : System.Windows.Forms.TreeView {
		public TreeViewEx()
			: base() {
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		protected override void OnHandleCreated(EventArgs e) {
			base.OnHandleCreated(e);
			NativeMethods.SendMessage(base.Handle, NativeMethods.TVM_SETEXTENDEDSTYLE,
			    (IntPtr)(NativeMethods.TVS_EX_DOUBLEBUFFER | NativeMethods.TVS_EX_NOINDENTSTATE | NativeMethods.TVS_EX_FADEINOUTEXPANDOS),
                (IntPtr)(NativeMethods.TVS_EX_DOUBLEBUFFER | NativeMethods.TVS_EX_NOINDENTSTATE | NativeMethods.TVS_EX_FADEINOUTEXPANDOS));
			NativeMethods.SetWindowTheme(base.Handle, "explorer", null);
			base.Indent = 0;
		}

		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
			TreeViewHitTestInfo htinfo = base.HitTest(e.Location);
			if (htinfo.Node != null) {
				base.SelectedNode = htinfo.Node;
			}
		}
	}
}
