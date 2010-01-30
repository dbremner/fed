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
