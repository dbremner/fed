using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Plain.Forms {
	[ToolboxItemFilter("System.Windows.Forms")]
	[Designer("System.Windows.Forms.Design.ImageListDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Images")]
	[TypeConverter(typeof(ImageListConverterEx))]
	[DesignerSerializer("System.Windows.Forms.Design.ImageListCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public partial class ImageListEx : Component {
		public static implicit operator ImageList(ImageListEx imlEx) {
			return imlEx.Base;
		}

		public ImageListEx() {
			InitializeComponent();
			m_Base = new ImageList();
			create();
		}

		public ImageListEx(IContainer container) {
			container.Add(this);

			InitializeComponent();
			m_Base = new ImageList(container);
			create();
		}

		[TypeConverter(typeof(ExpandableObjectConverter))]
		public ImageList Base {
			get { return m_Base; }
		}

		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor(typeof(ImageCollectionEditorEx), typeof(UITypeEditor))]
		[MergableProperty(false)]
		public ImageList.ImageCollection Images {
			get { return m_Base.Images; }
		}

		ImageList m_Base;

		void create() {
			m_Base.Tag = this;
		}
	}

	public class ImageListConverterEx : ComponentConverter {
		public ImageListConverterEx()
			: base(typeof(ImageListEx)) {
		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context) {
			return true;
		}
	}

	public class ImageCollectionEditorEx : System.ComponentModel.Design.CollectionEditor {
		public ImageCollectionEditorEx(Type type)
			: base(type) {
			_WindowsFormsDesign_ = new Reflector("System.Design", "System.Windows.Forms.Design");
		}

		Reflector _WindowsFormsDesign_;

		protected override CollectionEditor.CollectionForm CreateCollectionForm() {
			CollectionEditor.CollectionForm form = base.CreateCollectionForm();
			form.Text = "Images Collection Editor";
			return form;
		}

		protected override object CreateInstance(Type itemType) {
			UITypeEditor editor = new ImageListImageEditorEx();
			return editor.EditValue(base.Context, null);
		}

		protected override string GetDisplayText(object value) {
			string str;
			if (value == null) {
				return string.Empty;
			}
			PropertyDescriptor descriptor = TypeDescriptor.GetProperties(value)["Name"];
			if (descriptor != null) {
				str = (string) descriptor.GetValue(value);
				if ((str != null) && (str.Length > 0)) {
					return str;
				}
			}
			Type typeImageListImage = _WindowsFormsDesign_.GetType("ImageListImage");
			if (value.GetType() == typeImageListImage) {
				value = _WindowsFormsDesign_.GetAs(typeImageListImage, value, "Image");
			}
			str = TypeDescriptor.GetConverter(value).ConvertToString(value);
			if ((str != null) && (str.Length != 0)) {
				return str;
			}
			return value.GetType().Name;
		}

		protected override IList GetObjectsFromInstance(object instance) {
			ArrayList list = instance as ArrayList;
			if (list != null) {
				return list;
			}
			return null;
		}

		protected override object SetItems(object editValue, object[] value) {
			/*Type typeImageListImage = _WindowsFormsDesign_.GetType("ImageListImage");
			for (int i = 0; i < value.Length; ++i) {
				if (value[i].GetType() == typeImageListImage) {
					value[i] = _WindowsFormsDesign_.GetAs(typeImageListImage, value, "Image");
				}
			}*/
			{
				if (editValue != null) {
					MessageBox.Show(editValue.GetType().FullName);
					int length = this.GetItems(editValue).Length;
					int num2 = value.Length;
					if (!(editValue is IList)) {
						return editValue;
					}
					IList list = (IList) editValue;
					list.Clear();
					for (int i = 0; i < value.Length; i++) {
						try {
							list.Add(value[i]);
						} catch (Exception ex) {
							MessageBox.Show(ex.Message, "SetItems");
						}
					}
				}
				return editValue;
			}
		}

		protected override object[] GetItems(object editValue) {
			try {
				return base.GetItems(editValue);
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "GetItems");
				return null;
			}
		}

		protected override Type CreateCollectionItemType() {
			Type type = base.CreateCollectionItemType();
			MessageBox.Show(type.FullName, "CreateCollectionItemType");
			return type;
		}

		protected override Type[] CreateNewItemTypes() {
			Type[] types = base.CreateNewItemTypes();
			foreach (Type type in types) {
				MessageBox.Show(type.FullName, "CreateNewItemTypes");
			}
			return types;
		}
	}

	public class ImageListImageEditorEx : System.Windows.Forms.Design.ImageListImageEditor {
	}
}
