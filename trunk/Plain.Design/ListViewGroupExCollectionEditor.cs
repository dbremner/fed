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

#define USE_EX
#if DEBUG

using System;
using System.ComponentModel.Design;
using System.Windows.Forms;
using Plain.Forms;

namespace Plain.Design {
	public class ListViewGroupExCollectionEditor : CollectionEditor {
		public ListViewGroupExCollectionEditor(Type type)
			: base(type) {
		}

		protected override string GetDisplayText(object value) {
			string prefix = string.Empty;
			ListViewGroupEx groupEx = (value as ListViewGroup).Tag as ListViewGroupEx;
			if (groupEx != null) {
				prefix = "(" + groupEx.GetType().Name + ")";
			}
			return prefix + base.GetDisplayText(value);
		}

		protected override object CreateInstance(Type itemType) {
			object value = base.CreateInstance(itemType);
#if USE_EX
			value = new ListViewGroupEx(value as ListViewGroup);
#endif
			return value;
		}

		protected override object[] GetItems(object editValue) {
			object[] value = base.GetItems(editValue);
#if USE_EX
			for (int i = 0; i < value.Length; ++i) {
				ListViewGroupEx groupEx = (value[i] as ListViewGroup).Tag as ListViewGroupEx;
				if (groupEx != null) {
					value[i] = groupEx;
				}
			}
#endif
			return value;
		}

		protected override object SetItems(object editValue, object[] value) {
#if USE_EX
			for (int i = 0; i < value.Length; ++i) {
				ListViewGroupEx groupEx = value[i] as ListViewGroupEx;
				if (groupEx != null) {
					value[i] = groupEx.Base;
				}
			}
#endif
			return base.SetItems(editValue, value);
		}
	}
}

#endif
