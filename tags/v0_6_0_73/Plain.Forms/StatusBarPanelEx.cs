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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Plain.Native;

namespace Plain.Forms {
	public class StatusBarPanelEx : System.Windows.Forms.StatusBarPanel {
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[RelatedImageList("ParentEx.ResourceImageList.ImageList")]
		[TypeConverterAttribute(typeof(ImageKeyConverter))]
		public string ImageKey {
			set {
				m_ImageKey = value;
				StatusBarEx sbrEx = this.ParentEx;
				if (sbrEx != null) {
					if (sbrEx.ResourceImageList != null) {
						if (sbrEx.ResourceImageList.ImageList.Images.ContainsKey(m_ImageKey)) {
							Image image = sbrEx.ResourceImageList.ImageList.Images[m_ImageKey];
							if (image is Bitmap) {
								base.Icon = Icon.FromHandle((image as Bitmap).GetHicon());
							}
						}
					}
				}
			}
			get { return m_ImageKey; }
		}

		[Browsable(false)]
		public StatusBarEx ParentEx {
			get { return base.Parent as StatusBarEx; }
		}

		string m_ImageKey;
	}
}
