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

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DosboxApp.Properties;
using Plain.Forms;

namespace DosboxApp {
	public class BitmapImageList : Plain.Forms.ResourceImageList {
		public BitmapImageList() : base() { }

		public BitmapImageList(IContainer container) : base(container) { }

		public override void Refresh() {
			base.Refresh();
			foreach (DictionaryEntry entry in Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true)) {
				Bitmap png = entry.Value as Bitmap;
				if (png != null) {
					base.ImageList.Images.Add(entry.Key as string, png);
				}
			}
		}
	}
}
