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

#if DEBUG

using System;
using System.ComponentModel.Design;
using Plain.Forms;

namespace Plain.Design {
	public class StatusBarPanelExCollectionEditor : CollectionEditor {
		public StatusBarPanelExCollectionEditor(Type type)
			: base(type) {
		}

		protected override CollectionEditor.CollectionForm CreateCollectionForm() {
			CollectionForm form = base.CreateCollectionForm();
			form.Text = typeof(StatusBarPanelEx).Name + " Collection Editor";
			return form;
		}

		protected override Type[] CreateNewItemTypes() {
			Type[] types = base.CreateNewItemTypes();
			Type[] types_ex = new Type[types.Length + 1];
			int i = 0;
			for (; i < types.Length; ++i) {
				types_ex[i] = types[i];
			}
			types_ex[i] = typeof(StatusBarPanelEx);
			return types_ex;
		}
	}
}

#endif
