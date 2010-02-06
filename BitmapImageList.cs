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
