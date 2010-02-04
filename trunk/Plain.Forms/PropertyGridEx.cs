using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;

namespace Plain.Forms {
	public class PropertyGridEx : System.Windows.Forms.PropertyGrid {
		/// <summary>
		/// Initializes a new instance of the PropertyGridEx class.
		/// </summary>
		public PropertyGridEx()
			: base() {
			m_Changes = new LinkedList<ChangeInfo>();
			initChangeQueue();
			m_Unsaved = new LinkedList<CommitInfo>();
			initUnsavedQueue();
		}

		/// <summary>
		/// Occurs when the value of the Modified property has changed.
		/// </summary>
		[Description("Occurs when the value of the Modified property has changed.")]
		public event EventHandler ModifiedChanged = delegate { };

		/// <summary>
		/// Undoes the last edit operation in the PropertyGridEx.
		/// </summary>
		public void Undo() {
			if (CanUndo) {
				_Undoing = true;
				m_Current.Value.ChangedItem.PropertyDescriptor.SetValue(base.SelectedObject, m_Current.Value.OldValue);
				m_Current.Value.ChangedItem.Select();
				if (m_Unsaved.Count > 0) {
					m_Unsaved.RemoveLast();
				}
				else {
					m_Unsaved.AddLast(new CommitInfo(m_Current.Value.ChangedItem, m_Current.Value.OldValue, true));
				}
				OnModifiedChanged(EventArgs.Empty);
				m_Current = m_Current.Previous;
				base.OnPropertyValueChanged(new PropertyValueChangedEventArgs(m_Current.Next.Value.ChangedItem, m_Current.Next.Value.NewValue));
				_Undoing = false;
			}
		}

		/// <summary>
		/// Reapplies the last operation that was undone in the control.
		/// </summary>
		public void Redo() {
			if (CanRedo) {
				_Redoing = true;
				m_Current = m_Current.Next;
				m_Current.Value.ChangedItem.PropertyDescriptor.SetValue(base.SelectedObject, m_Current.Value.NewValue);
				m_Current.Value.ChangedItem.Select();
				if (m_Unsaved.Count > 0 && m_Unsaved.Last.Value.NegativeChange) {
					m_Unsaved.RemoveLast();
				}
				else {
					m_Unsaved.AddLast(new CommitInfo(m_Current.Value.ChangedItem, m_Current.Value.NewValue));
				}
				OnModifiedChanged(EventArgs.Empty);
				base.OnPropertyValueChanged(new PropertyValueChangedEventArgs(m_Current.Value.ChangedItem, m_Current.Value.OldValue));
				_Redoing = false;
			}
		}

		/// <summary>
		/// Clears information about the most recent operation from the undo buffer of the PropertyGridEx.
		/// </summary>
		public void ClearUndo() {
			initChangeQueue();
		}

		/// <summary>
		/// 
		/// </summary>
		public void SetSavePoint() {
			initUnsavedQueue();
			OnModifiedChanged(EventArgs.Empty);
		}

		/// <summary>
		/// 
		/// </summary>
		public CommitInfo[] GetUnsavedChanges() {
			Dictionary<PropertyDescriptor, CommitInfo> uniqueCommits = new Dictionary<PropertyDescriptor, CommitInfo>(m_Unsaved.Count);
			foreach (CommitInfo cinfo in m_Unsaved) {
				if (uniqueCommits.ContainsKey(cinfo.ChangedItem.PropertyDescriptor)) {
					uniqueCommits[cinfo.ChangedItem.PropertyDescriptor] = cinfo;
				}
				else {
					uniqueCommits.Add(cinfo.ChangedItem.PropertyDescriptor, cinfo);
				}
			}
			CommitInfo[] unsaved = new CommitInfo[uniqueCommits.Count];
			uniqueCommits.Values.CopyTo(unsaved, 0);
			return unsaved;
		}

		/// <summary>
		/// Resets the selected property to its default value.
		/// </summary>
		public new void ResetSelectedProperty() {
			GridItem gi = base.SelectedGridItem;
			if (gi.PropertyDescriptor != null) {
				if (gi.PropertyDescriptor.CanResetValue(base.SelectedObject)) {
					PropertyValueChangedEventArgs e = new PropertyValueChangedEventArgs(gi, gi.Value);
					base.ResetSelectedProperty();
					OnPropertyValueChanged(e);
				}
			}
		}

		public void SetComment(string title, string description) {
			Control comment = getControl(TYPE_DOCCOMMENT);
			if (comment != null) {
				MethodInfo func = null;
				try {
					func = comment.GetType().GetMethod("SetComment", BindingFlags.Instance | BindingFlags.Public);
					func.Invoke(comment, new object[] { title, description });
				}
				catch { }
			}
		}

		/// <summary>
		/// Gets the internal ToolStrip.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStrip ToolStrip {
			get { return getControl(TYPE_TOOLSTRIP) as ToolStrip; }
		}

		/// <summary>
		/// Gets a value indicating whether the user can undo the previous operation in a PropertyGridEx control.
		/// </summary>
		[Browsable(false)]
		public bool CanUndo {
			get { return m_Current != m_Changes.First; }
		}

		/// <summary>
		/// Gets a value indicating whether there are actions that have occurred within the PropertyGridEx that can be reapplied.
		/// </summary>
		[Browsable(false)]
		public bool CanRedo {
			get { return m_Current != m_Changes.Last; }
		}

		/// <summary>
		/// Gets the number of actions stored in the undo queue.
		/// </summary>
		[Browsable(false)]
		public int CanUndoMore {
			get {
				int level = 0;
				LinkedListNode<ChangeInfo> node = m_Current;
				while (node != m_Changes.First) {
					node = node.Previous;
					++level;
				}
				return level;
			}
		}

		/// <summary>
		/// Gets the number of actions stored in the redo queue.
		/// </summary>
		[Browsable(false)]
		public int CanRedoMore {
			get {
				int level = 0;
				LinkedListNode<ChangeInfo> node = m_Current;
				while (node != m_Changes.Last) {
					node = node.Next;
					++level;
				}
				return level;
			}
		}

		/// <summary>
		/// Gets the info of the action that can be undone in the control when the Undo method is called.
		/// </summary>
		[Browsable(false)]
		public ChangeInfo UndoInfo {
			get {
				if (CanUndo) {
					return m_Current.Value;
				}
				return null;
			}
		}

		/// <summary>
		/// Gets the info of the action that can be reapplied to the control when the Redo method is called.
		/// </summary>
		[Browsable(false)]
		public ChangeInfo RedoInfo {
			get {
				if (CanRedo) {
					return m_Current.Next.Value;
				}
				return null;
			}
		}

		/// <summary>
		/// Gets a value that indicates that the currently selected object has been modified by the user since the object was displayed or the save point was last set.
		/// </summary>
		[Browsable(false)]
		public bool Modified {
			get { return m_Unsaved.Count > 0; }
		}

		const string TYPE_DOCCOMMENT = "DocComment";
		const string TYPE_HOTCOMMANDS = "HotCommands";
		const string TYPE_PROPERTYGRIDVIEW = "PropertyGridView";
		const string TYPE_TOOLSTRIP = "ToolStrip";
		bool _Undoing;
		bool _Redoing;
		LinkedList<ChangeInfo> m_Changes;
		LinkedListNode<ChangeInfo> m_Current;
		LinkedList<CommitInfo> m_Unsaved;

		protected override void OnSystemColorsChanged(EventArgs e) {
			base.OnSystemColorsChanged(e);
			fixVScrollbarWidth();
		}

		protected override void OnPropertyValueChanged(PropertyValueChangedEventArgs e) {
			if (_Undoing || _Redoing) {
				return;
			}
			removeAfter(m_Current);
			m_Changes.AddLast(new ChangeInfo(e.ChangedItem, e.OldValue, e.ChangedItem.Value));
			m_Current = m_Current.Next;
			m_Unsaved.AddLast(new CommitInfo(m_Current.Value.ChangedItem, m_Current.Value.NewValue));
			OnModifiedChanged(EventArgs.Empty);
			base.OnPropertyValueChanged(e);
		}

		protected override void OnSelectedObjectsChanged(EventArgs e) {
			base.OnSelectedObjectsChanged(e);
			initChangeQueue();
			initUnsavedQueue();
			OnModifiedChanged(EventArgs.Empty);
		}

		protected virtual void OnModifiedChanged(EventArgs e) {
			ModifiedChanged(this, e);
		}

		void initChangeQueue() {
			m_Changes.Clear();
			m_Changes.AddLast(new ChangeInfo());
			m_Current = m_Changes.First;
		}

		void initUnsavedQueue() {
			m_Unsaved.Clear();
		}

		void removeAfter(LinkedListNode<ChangeInfo> node) {
			while (m_Changes.Last != node) {
				m_Changes.RemoveLast();
			}
		}

		Control getControl(string typeName) {
			foreach (Control ctrl in base.Controls) {
				if (ctrl.GetType().Name == typeName) {
					return ctrl;
				}
			}
			return null;
		}

		Control getChildControl(Control parent, string typeName) {
			foreach (Control ctrl in parent.Controls) {
				Control c = getChildControl(ctrl, typeName);
				if (c != null) {
					return c;
				}
				if (ctrl.GetType().Name == typeName) {
					return ctrl;
				}
			}
			return null;
		}

		void fixVScrollbarWidth() {
			VScrollBar vsb = getChildControl(this, "VScrollBar") as VScrollBar;
			if (vsb != null) {
				vsb.Width = SystemInformation.VerticalScrollBarWidth;
			}
		}



		public class ChangeInfo {
			internal ChangeInfo() { }
			/// <summary>
			/// Initializes a new instance of the ChangeInfo class.
			/// </summary>
			/// <param name="item">The item in the grid that changed.</param>
			/// <param name="oldValue">The old property value.</param>
			/// <param name="newValue">The new property value.</param>
			public ChangeInfo(GridItem item, object oldValue, object newValue) {
				m_ChangedItem = item;
				m_OldValue = oldValue;
				m_NewValue = newValue;
			}
			/// <summary>
			/// Gets the GridItem that was changed.
			/// </summary>
			public GridItem ChangedItem {
				get { return m_ChangedItem; }
			}
			/// <summary>
			/// The value of the grid item before it was changed.
			/// </summary>
			public object OldValue {
				get { return m_OldValue; }
			}
			/// <summary>
			/// The value of the grid item after it was changed.
			/// </summary>
			public object NewValue {
				get { return m_NewValue; }
			}
			GridItem m_ChangedItem;
			object m_OldValue;
			object m_NewValue;
		}

		public class CommitInfo {
			public CommitInfo(GridItem item, object value)
				: this(item, value, false) {
			}
			public CommitInfo(GridItem item, object value, bool negativeChange) {
				m_ChangedItem = item;
				m_Value = value;
				m_NegativeChange = negativeChange;
			}
			/// <summary>
			/// Gets the GridItem that was changed.
			/// </summary>
			public GridItem ChangedItem {
				get { return m_ChangedItem; }
			}
			/// <summary>
			/// The value of the grid item.
			/// </summary>
			public object Value {
				get { return m_Value; }
			}
			public bool NegativeChange {
				get { return m_NegativeChange; }
			}
			GridItem m_ChangedItem;
			object m_Value;
			bool m_NegativeChange;
		}
	}
}
