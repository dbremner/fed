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
