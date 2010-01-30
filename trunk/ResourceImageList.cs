using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DosboxApp.Properties;

namespace DosboxApp {
	[Designer(typeof(ResourceImageListDesigner))]
	public partial class ResourceImageList : Component {
		public static implicit operator ImageList(ResourceImageList resiml) {
			return resiml.ImageList;
		}

		public ResourceImageList() {
			InitializeComponent();
			m_iml = new ImageList();
			create();
		}

		public ResourceImageList(IContainer container) {
			container.Add(this);

			InitializeComponent();
			m_iml = new ImageList(container);
			create();
		}

		public void Refresh() {
			m_iml.Images.Clear();
			foreach (DictionaryEntry entry in Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true)) {
				Bitmap png = entry.Value as Bitmap;
				if (png != null) {
					m_iml.Images.Add(entry.Key as string, png);
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ImageList ImageList {
			get { return m_iml; }
		}

		ImageList m_iml;

		void create() {
			m_iml.ColorDepth = ColorDepth.Depth32Bit;
			Refresh();
		}
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
			if (this.Component is ResourceImageList) {
				ResourceImageList resiml = this.Component as ResourceImageList;
				resiml.Refresh();
			}
		}

		DesignerActionItemCollection m_ActionItems;
	}

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
}
