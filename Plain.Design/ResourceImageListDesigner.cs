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
using System.ComponentModel;
using System.ComponentModel.Design;
using Plain.Forms;

namespace Plain.Design {
	public class ResourceImageListDesigner : ComponentDesigner {
		public ResourceImageListDesigner() {
			m_ActionList = new DesignerActionListCollection();
			m_ActionList.Add(new ResourceDependentDesignerActionList(base.Component));
		}

		public override DesignerActionListCollection ActionLists {
			get { return m_ActionList; }
		}

		DesignerActionListCollection m_ActionList;
	}

	public class ResourceDependentDesignerActionList : DesignerActionList {
		public ResourceDependentDesignerActionList(IComponent component)
			: base(component) {
			m_ActionItems = new DesignerActionItemCollection();
			m_ActionItems.Add(new DesignerActionMethodItem(this, "RefreshImageList", "Refresh", "Behavior", "Refresh the resources in the ImageList", true));
		}

		public override DesignerActionItemCollection GetSortedActionItems() {
			return m_ActionItems;
		}

		void RefreshImageList() {
			ResourceImageList resiml = this.Component as ResourceImageList;
			if (resiml != null) {
				resiml.Refresh();
			}
		}

		DesignerActionItemCollection m_ActionItems;
	}
}

#endif
