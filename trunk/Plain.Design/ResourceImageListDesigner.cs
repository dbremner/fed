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
