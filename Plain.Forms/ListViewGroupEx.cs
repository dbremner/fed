using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms;
using Plain.Native;

namespace Plain.Forms {
	[SerializableAttribute]
	public class ListViewGroupBase : ISerializable {
		public ListViewGroupBase() {
			m_Group = new ListViewGroup();
		}
		public ListViewGroupBase(string header) {
			m_Group = new ListViewGroup(header);
		}
		public ListViewGroupBase(string header, HorizontalAlignment headerAlignment) {
			m_Group = new ListViewGroup(header, headerAlignment);
		}
		public ListViewGroupBase(string key, string headerText) {
			m_Group = new ListViewGroup(key, headerText);
		}
		public ListViewGroupBase(ListViewGroup group) {
			m_Group = group;
		}
		protected ListViewGroupBase(SerializationInfo info, StreamingContext context)
			: this() {
			this.deserialize(info, context);
		}

		[TypeConverter(typeof(ExpandableObjectConverter))]
		public ListViewGroup Base {
			protected set { m_Group = value; }
			get { return m_Group; }
		}

		public int ID {
			get {
				PropertyInfo prop = null;
				try {
					prop = m_Group.GetType().GetProperty("ID", BindingFlags.Instance | BindingFlags.NonPublic);
					return (int) prop.GetValue(m_Group, null);
				} catch { }
				return 0;
			}
		}

		ListViewGroup m_Group;

		void deserialize(SerializationInfo info, StreamingContext context) {
			MethodInfo func = null;
			try {
				func = m_Group.GetType().GetMethod("Deserialize", BindingFlags.Instance | BindingFlags.NonPublic);
				func.Invoke(m_Group, new object[] { info, context });
			} catch {
				Deserialize(info, context);
			}
		}

		protected void getObjectData(SerializationInfo info, StreamingContext context) {
			MethodInfo func = null;
			try {
				func = m_Group.GetType().GetMethod("GetObjectData", BindingFlags.Instance | BindingFlags.NonPublic);
				func.Invoke(m_Group, new object[] { info, context });
			} catch {
				(this as ISerializable).GetObjectData(info, context);
			}
		}

		private void Deserialize(SerializationInfo info, StreamingContext context) {
			int num = 0;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext()) {
				SerializationEntry current = enumerator.Current;
				if (current.Name == "Header") {
					m_Group.Header = (string) current.Value;
				} else {
					if (current.Name == "HeaderAlignment") {
						m_Group.HeaderAlignment = (HorizontalAlignment) current.Value;
						continue;
					}
					if (current.Name == "Tag") {
						m_Group.Tag = current.Value;
						continue;
					}
					if (current.Name == "ItemsCount") {
						num = (int) current.Value;
						continue;
					}
					if (current.Name == "Name") {
						m_Group.Name = (string) current.Value;
					}
				}
			}
			if (num > 0) {
				ListViewItem[] items = new ListViewItem[num];
				for (int i = 0; i < num; i++) {
					items[i] = (ListViewItem) info.GetValue("Item" + i, typeof(ListViewItem));
				}
				m_Group.Items.AddRange(items);
			}
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("Header", m_Group.Header);
			info.AddValue("HeaderAlignment", m_Group.HeaderAlignment);
			info.AddValue("Tag", m_Group.Tag);
			if (!string.IsNullOrEmpty(m_Group.Name)) {
				info.AddValue("Name", m_Group.Name);
			}
			if ((m_Group.Items != null) && (m_Group.Items.Count > 0)) {
				info.AddValue("ItemsCount", m_Group.Items.Count);
				for (int i = 0; i < m_Group.Items.Count; i++) {
					info.AddValue("Item" + i.ToString(CultureInfo.InvariantCulture), m_Group.Items[i], typeof(ListViewItem));
				}
			}
		}
	}

	[SerializableAttribute]
	public class ListViewGroupEx : ListViewGroupBase, ISerializable {
		public static implicit operator ListViewGroup(ListViewGroupEx groupEx) {
			return groupEx.Base;
		}

		public ListViewGroupEx()
			: base() {
			create();
		}
		public ListViewGroupEx(string header)
			: base(header) {
			create();
		}
		public ListViewGroupEx(string header, HorizontalAlignment headerAlignment)
			: base(header, headerAlignment) {
			create();
		}
		public ListViewGroupEx(string key, string headerText)
			: base(key, headerText) {
			create();
		}
		public ListViewGroupEx(ListViewGroup group) {
			base.Base = group;
			create();
		}
		private ListViewGroupEx(SerializationInfo info, StreamingContext context)
			: base(info, context) {
			create();
			this.deserialize(info, context);
		}

		/// <summary>
		/// Common Controls Version 6.00 and Windows Vista. The group can be collapsed.
		/// </summary>
		[DefaultValue(false)]
		[Description("Common Controls Version 6.00 and Windows Vista. The group can be collapsed.")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public bool Collapsible {
			set {
				m_Collapsible = value;
				if (m_Collapsible == false) {
					m_Collapsed = false;
				}
				updateState();
			}
			get {
				if (base.Base.ListView != null && base.Base.ListView.Created) {
					NativeMethods.LVGROUP lvg = new NativeMethods.LVGROUP();
					lvg.cbSize = (uint) Marshal.SizeOf(lvg);
					lvg.mask = NativeMethods.LVGF_STATE;
					lvg.stateMask = NativeMethods.LVGS_COLLAPSED | NativeMethods.LVGS_COLLAPSIBLE;
					int id = NativeMethods.SendMessage(base.Base.ListView.Handle, NativeMethods.LVM_GETGROUPINFO, base.ID, ref lvg);
					if (id == base.ID) {
						return (lvg.state & NativeMethods.LVGS_COLLAPSIBLE) != 0;
					}
				}
				return m_Collapsible;
			}
		}

		/// <summary>
		/// Common Controls Version 6.00 and Windows Vista. The group is collapsed.
		/// </summary>
		[DefaultValue(false)]
		[Description("Common Controls Version 6.00 and Windows Vista. The group is collapsed.")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public bool Collapsed {
			set {
				m_Collapsed = value;
				if (m_Collapsed == true) {
					m_Collapsible = true;
				}
				updateState();
			}
			get {
				if (base.Base.ListView != null && base.Base.ListView.Created) {
					NativeMethods.LVGROUP lvg = new NativeMethods.LVGROUP();
					lvg.cbSize = (uint) Marshal.SizeOf(lvg);
					lvg.mask = NativeMethods.LVGF_STATE;
					lvg.stateMask = NativeMethods.LVGS_COLLAPSED | NativeMethods.LVGS_COLLAPSIBLE;
					int id = NativeMethods.SendMessage(base.Base.ListView.Handle, NativeMethods.LVM_GETGROUPINFO, base.ID, ref lvg);
					if (id == base.ID) {
						return (lvg.state & NativeMethods.LVGS_COLLAPSED) != 0;
					}
				}
				return m_Collapsed;
			}
		}

		[Browsable(false)]
		internal bool ShouldBeCollapsible {
			get { return m_Collapsible; }
		}

		[Browsable(false)]
		internal bool ShouldBeCollapsed {
			get { return m_Collapsed; }
		}

		bool m_Collapsible;
		bool m_Collapsed;

		void create() {
			base.Base.Tag = this;
		}

		void updateState() {
			if (base.Base.ListView != null && base.Base.ListView.Created) {
				NativeMethods.LVGROUP lvg = new NativeMethods.LVGROUP();
				lvg.cbSize = (uint) Marshal.SizeOf(lvg);
				lvg.mask = NativeMethods.LVGF_STATE;
				lvg.stateMask = NativeMethods.LVGS_COLLAPSIBLE | NativeMethods.LVGS_COLLAPSED;
				lvg.state = 0;
				if (m_Collapsible) {
					lvg.state |= NativeMethods.LVGS_COLLAPSIBLE;
				}
				if (m_Collapsed) {
					lvg.state |= NativeMethods.LVGS_COLLAPSED;
				}
				NativeMethods.SendMessage(base.Base.ListView.Handle, NativeMethods.LVM_SETGROUPINFO, base.ID, ref lvg);
			}
		}

		void deserialize(SerializationInfo info, StreamingContext context) {
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext()) {
				SerializationEntry current = enumerator.Current;
				if (current.Name == "Collapsible") {
					this.Collapsible = (bool) current.Value;
				} else {
					if (current.Name == "Collapsed") {
						this.Collapsed = (bool) current.Value;
						continue;
					}
				}
			}
		}

		#region ISerializable Members

		void GetObjectData(SerializationInfo info, StreamingContext context) {
			base.getObjectData(info, context);
			info.AddValue("Collapsible", this.Collapsible);
			info.AddValue("Collapsed", this.Collapsed);
		}

		#endregion
	}
}
