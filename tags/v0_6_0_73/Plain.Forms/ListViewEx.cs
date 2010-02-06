/*
FED (Front-End for Dosbox), a gaming graphical user interface for the DOSBox emulator.
Copyright (C) 2009  Ka Ki Cheung

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Plain.Design;
using Plain.Native;

namespace Plain.Forms {
	public class ListViewEx : System.Windows.Forms.ListView {
		public ListViewEx()
			: base() {
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			if (Environment.OSVersion.Version < NativeMethods.WindowsVista) {
				m_LabelEmptyText = new Label();
				m_LabelEmptyText.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
				m_LabelEmptyText.AutoSize = false;
				m_LabelEmptyText.ForeColor = SystemColors.GrayText;
				m_LabelEmptyText.TextAlign = ContentAlignment.MiddleCenter;
				m_LabelEmptyText.Visible = false;
				m_LabelEmptyText.MouseDown += new MouseEventHandler(LabelEmptyText_MouseDown);
				base.Controls.Add(m_LabelEmptyText);
			}
			m_ColumnWidthChangingIndex = -1;
		}

		/// <summary>
		/// Occurs when the user or code scrolls through the client area.
		/// </summary>
		[Description("Occurs when the user or code scrolls through the client area.")]
		public event ScrollEventHandler Scroll = delegate { };

		public event EventHandler BeginScroll = delegate { };
		public event EventHandler EndScroll = delegate { };
		
#if DEBUG
		[Editor(typeof(ListViewGroupExCollectionEditor), typeof(UITypeEditor))]
		public new ListViewGroupCollection Groups {
			get { return base.Groups; }
		}
#endif

		/// <summary>
		/// Gets or sets the text meant for display when the list-view control appears empty.
		/// </summary>
		[Description("Gets or sets the text meant for display when the list-view control appears empty.")]
		public string EmptyText {
			set {
				m_EmptyText = value;
				if (m_LabelEmptyText != null) {
					m_LabelEmptyText.Text = m_EmptyText;
					m_LabelEmptyText.Visible = string.IsNullOrEmpty(m_EmptyText) == false;
				}
			}
			get {
#if false
				if (base.Created) {
					StringBuilder sb = new StringBuilder(NativeMethods.L_MAX_URL_LENGTH);
					NativeMethods.SendMessage(base.Handle, NativeMethods.LVM_GETEMPTYTEXT, sb.Capacity, sb);
					return sb.ToString();
				}
#endif
				return m_EmptyText;
			}
		}

		[Browsable(false)]
		public int HeaderHeight {
			get {
				// May not be "created" when recreating, while header is there...
				//if (base.Created) {
					NativeMethods.RECT r = new NativeMethods.RECT();
					NativeMethods.GetWindowRect((IntPtr) NativeMethods.SendMessage(base.Handle, NativeMethods.LVM_GETHEADER, 0, 0), ref r);
					return r.bottom - r.top;
				//}
				//return 0;
			}
		}

		string m_EmptyText;
		Label m_LabelEmptyText;
		int m_ColumnWidthChangingIndex;

		protected virtual void OnScroll(ScrollEventArgs e) {
			Scroll(this, e);
		}

		protected virtual void OnBeginScroll(EventArgs e) {
			BeginScroll(this, e);
		}

		protected virtual void OnEndScroll(EventArgs e) {
			EndScroll(this, e);
		}

		protected override void OnHandleCreated(EventArgs e) {
			base.OnHandleCreated(e);
			NativeMethods.SendMessage(base.Handle, NativeMethods.LVM_SETEXTENDEDLISTVIEWSTYLE, NativeMethods.LVS_EX_DOUBLEBUFFER, NativeMethods.LVS_EX_DOUBLEBUFFER);
			NativeMethods.SetWindowTheme(base.Handle, "explorer", null);
			updateEmptyText();
		}

		protected override void OnStyleChanged(EventArgs e) {
			base.OnStyleChanged(e);
			updateEmptyText();
		}

		protected override void OnFontChanged(EventArgs e) {
			base.OnFontChanged(e);
			if (m_LabelEmptyText != null) {
				m_LabelEmptyText.Font = new Font(base.Font, FontStyle.Italic);
			}
		}

		protected override void OnResize(EventArgs e) {
			base.OnResize(e);
			updateEmptyText();
		}

		protected override void OnKeyDown(KeyEventArgs e) {
			base.OnKeyDown(e);
			switch (e.KeyCode) {
			case Keys.Down:
			case Keys.Up:
			case Keys.PageDown:
			case Keys.PageUp:
			case Keys.Home:
			case Keys.End:
				OnBeginScroll(EventArgs.Empty);
				break;
			case Keys.Escape:
				if (m_ColumnWidthChangingIndex != -1) {
					base.OnColumnWidthChanging(new ColumnWidthChangingEventArgs(m_ColumnWidthChangingIndex, base.Columns[m_ColumnWidthChangingIndex].Width));
					m_ColumnWidthChangingIndex = -1;
				}
				break;
			}
		}

		protected override void OnKeyUp(KeyEventArgs e) {
			base.OnKeyUp(e);
			switch (e.KeyData) {
			case Keys.Down:
			case Keys.Up:
			case Keys.PageDown:
			case Keys.PageUp:
			case Keys.Home:
			case Keys.End:
				OnEndScroll(EventArgs.Empty);
				break;
			}
		}

		protected override void OnColumnWidthChanging(ColumnWidthChangingEventArgs e) {
			base.OnColumnWidthChanging(e);
			m_ColumnWidthChangingIndex = e.ColumnIndex;
		}

		protected override void OnColumnWidthChanged(ColumnWidthChangedEventArgs e) {
			base.OnColumnWidthChanged(e);
			m_ColumnWidthChangingIndex = -1;
		}

		protected override void WndProc(ref Message m) {
			switch (m.Msg) {
			case NativeMethods.WM_REFLECT | NativeMethods.WM_NOTIFY:
				NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR) m.GetLParam(typeof(NativeMethods.NMHDR));
				switch (nmhdr.code) {
				case NativeMethods.LVN_GETEMPTYMARKUP:
					if (m_LabelEmptyText == null) {
						NativeMethods.NMLVEMPTYMARKUP nmMarkup = (NativeMethods.NMLVEMPTYMARKUP) m.GetLParam(typeof(NativeMethods.NMLVEMPTYMARKUP));
						nmMarkup.dwFlags = NativeMethods.EMF_CENTERED;
						nmMarkup.szMarkup = m_EmptyText;
						Marshal.StructureToPtr(nmMarkup, m.LParam, true);
						m.Result = (IntPtr) 1;
						base.WndProc(ref m);
						base.Refresh();
						return;
					}
					break;
				case NativeMethods.LVN_INSERTITEM:
					if (m_LabelEmptyText != null) {
						m_LabelEmptyText.Visible = false;
					}
					break;
				case NativeMethods.LVN_DELETEITEM:
					if (m_LabelEmptyText != null) {
						m_LabelEmptyText.Visible = (base.Items.Count == 1);
					}
					break;
				case NativeMethods.LVN_DELETEALLITEMS:
					if (m_LabelEmptyText != null) {
						m_LabelEmptyText.Visible = true;
					}
					break;
				case NativeMethods.LVN_BEGINSCROLL:
					OnBeginScroll(EventArgs.Empty);
					break;
				case NativeMethods.LVN_ENDSCROLL:
					OnEndScroll(EventArgs.Empty);
					break;
				}
				break;
			case NativeMethods.WM_HSCROLL: {
					ScrollEventType set;
					int pos = 0;
					switch (NativeMethods.LOWORD(m.WParam.ToInt32())) {
					case NativeMethods.SB_ENDSCROLL:
						set = ScrollEventType.EndScroll;
						break;
					case NativeMethods.SB_LEFT:
						set = ScrollEventType.First;
						break;
					case NativeMethods.SB_RIGHT:
						set = ScrollEventType.Last;
						break;
					case NativeMethods.SB_LINELEFT:
						set = ScrollEventType.SmallDecrement;
						break;
					case NativeMethods.SB_LINERIGHT:
						set = ScrollEventType.SmallIncrement;
						break;
					case NativeMethods.SB_PAGELEFT:
						set = ScrollEventType.LargeDecrement;
						break;
					case NativeMethods.SB_PAGERIGHT:
						set = ScrollEventType.LargeIncrement;
						break;
					case NativeMethods.SB_THUMBPOSITION:
						set = ScrollEventType.ThumbPosition;
						pos = NativeMethods.HIWORD(m.WParam.ToInt32());
						break;
					case NativeMethods.SB_THUMBTRACK:
						set = ScrollEventType.ThumbTrack;
						pos = NativeMethods.HIWORD(m.WParam.ToInt32());
						break;
					default:
						set = ScrollEventType.ThumbTrack;
						break;
					}
					// TODO: GetScrollInfo
					ScrollEventArgs e = new ScrollEventArgs(set, pos, ScrollOrientation.HorizontalScroll);
					OnScroll(e);
				}
				break;
			case NativeMethods.WM_VSCROLL: {
					ScrollEventType set;
					int pos = 0;
					switch (NativeMethods.LOWORD(m.WParam.ToInt32())) {
					case NativeMethods.SB_ENDSCROLL:
						set = ScrollEventType.EndScroll;
						break;
					case NativeMethods.SB_LINEDOWN:
						set = ScrollEventType.SmallIncrement;
						break;
					case NativeMethods.SB_LINEUP:
						set = ScrollEventType.SmallDecrement;
						break;
					case NativeMethods.SB_PAGEDOWN:
						set = ScrollEventType.LargeIncrement;
						break;
					case NativeMethods.SB_PAGEUP:
						set = ScrollEventType.LargeDecrement;
						break;
					case NativeMethods.SB_THUMBPOSITION:
						set = ScrollEventType.ThumbPosition;
						pos = NativeMethods.HIWORD(m.WParam.ToInt32());
						break;
					case NativeMethods.SB_THUMBTRACK:
						set = ScrollEventType.ThumbTrack;
						pos = NativeMethods.HIWORD(m.WParam.ToInt32());
						break;
					case NativeMethods.SB_BOTTOM:
						set = ScrollEventType.Last;
						break;
					case NativeMethods.SB_TOP:
						set = ScrollEventType.First;
						break;
					default:
						set = ScrollEventType.ThumbTrack;
						break;
					}
					// TODO: GetScrollInfo
					ScrollEventArgs e = new ScrollEventArgs(set, pos, ScrollOrientation.VerticalScroll);
					OnScroll(e);
				}
				break;
			case NativeMethods.LVM_SETVIEW:
				base.WndProc(ref m);
				updateEmptyText();
				return;
			// TODO: Check LVM_INSERTGROUPSORTED
			case NativeMethods.LVM_INSERTGROUP:
				NativeMethods.LVGROUP_xp lvgxp = (NativeMethods.LVGROUP_xp) m.GetLParam(typeof(NativeMethods.LVGROUP_xp));
				foreach (ListViewGroup group in base.Groups) {
					ListViewGroupEx groupEx = group.Tag as ListViewGroupEx;
					if (groupEx != null && groupEx.ID == lvgxp.iGroupId) {
						base.WndProc(ref m);
						groupEx.Collapsed = groupEx.ShouldBeCollapsed;
						groupEx.Collapsible = groupEx.ShouldBeCollapsible;
						return;
					}
				}
				break;
			case NativeMethods.WM_LBUTTONUP:
				//case NativeMethods.WM_LBUTTONDOWN:
				//case NativeMethods.WM_LBUTTONDBLCLK:
				base.DefWndProc(ref m);
				break;
			}
			base.WndProc(ref m);
		}

		void updateEmptyText() {
			if (m_LabelEmptyText != null) {
				if (base.View != View.Details || base.HeaderStyle == ColumnHeaderStyle.None) {
					m_LabelEmptyText.SetBounds(0, 0, base.ClientSize.Width, base.ClientSize.Height);
				}
				else {
					m_LabelEmptyText.SetBounds(0, this.HeaderHeight, base.ClientSize.Width, base.ClientSize.Height - this.HeaderHeight);
				}
				m_LabelEmptyText.SendToBack();
			}
		}

		private void LabelEmptyText_MouseDown(object sender, MouseEventArgs e) {
			base.Focus();
		}
	}
}
