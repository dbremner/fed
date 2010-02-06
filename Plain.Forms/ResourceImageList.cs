using System;
using System.ComponentModel;
using System.Windows.Forms;
using Plain.Design;

namespace Plain.Forms {
#if DEBUG
	[Designer(typeof(ResourceImageListDesigner))]
#endif
	public abstract partial class ResourceImageList : Component {
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

		public virtual void Refresh() {
			m_iml.Images.Clear();
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ImageList ImageList {
			get { return m_iml; }
		}

		ImageList m_iml;

		protected virtual ColorDepth ColorDepth {
			get { return ColorDepth.Depth32Bit; }
		}

		void create() {
			m_iml.ColorDepth = this.ColorDepth;
			Refresh();
		}
	}
}
