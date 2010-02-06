using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using Plain.Design;

namespace Plain.Forms {
	public class StatusBarEx : System.Windows.Forms.StatusBar {

#if DEBUG
		[Editor(typeof(StatusBarPanelExCollectionEditor), typeof(UITypeEditor))]
		public new StatusBarPanelCollection Panels {
			get { return base.Panels; }
		}
#endif

		public ResourceImageList ResourceImageList {
			set { m_ResourceImageList = value; }
			get { return m_ResourceImageList; }
		}

		ResourceImageList m_ResourceImageList;
	}
}
