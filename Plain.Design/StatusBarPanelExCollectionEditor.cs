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
