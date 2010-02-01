using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Plain.Native;

namespace Plain.Forms {
	public class TextBoxEx : System.Windows.Forms.TextBox {
		public TextBoxEx() {
			m_CueBanner = string.Empty;
		}

		/// <summary>
		/// Gets or sets the textual cue, or tip, that is displayed by the TextBoxEx control to prompt the user for information.
		/// </summary>
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("The textual cue, or tip, that is displayed by the TextBoxEx control to prompt the user for information.")]
		public string CueBanner {
			set {
				if (value == null) {
					m_CueBanner = string.Empty;
				}
				else {
					m_CueBanner = value;
				}
#if false
				NativeMethods.SendMessage(base.Handle, NativeMethods.EM_SETCUEBANNER, 0, m_CueBanner);
#endif
				if (m_CueBanner.Length > 0 && base.TextLength == 0 && base.Focused == false) {
					if (base.GetStyle(ControlStyles.UserPaint) == false) {
						base.SetStyle(ControlStyles.UserPaint, true);
						base.UpdateStyles();
					}
				}
				else {
					if (base.GetStyle(ControlStyles.UserPaint)) {
						base.SetStyle(ControlStyles.UserPaint, false);
						base.UpdateStyles();
						changeFromOwnerDrawToNormal();
					}
				}
			}
			get {
#if false
				StringBuilder sb = new StringBuilder(1024);
				NativeMethods.SendMessage(base.Handle, NativeMethods.EM_GETCUEBANNER, sb, sb.Capacity);
				return sb.ToString();
#endif
				return m_CueBanner;
			}
		}

		/// <summary>
		/// Gets or sets the widths of the left and right margins for a TextBoxEx control. 
		/// </summary>
		[Category("Layout")]
		[Description("The widths of the left and right margins for a TextBoxEx control.")]
		public EditMargins InnerMargins {
			set {
				m_InnerMargins = value;
				NativeMethods.SendMessage(base.Handle, NativeMethods.EM_SETMARGINS,
					NativeMethods.EC_LEFTMARGIN | NativeMethods.EC_RIGHTMARGIN,
					(int) NativeMethods.MAKELONG((ushort) value.Left, (ushort) value.Right));
			}
			get {
#if false
				int ret = NativeMethods.SendMessage(base.Handle, NativeMethods.EM_GETMARGINS, 0, 0);
				return new Margins(NativeMethods.LOWORD(ret), NativeMethods.HIWORD(ret));
#endif
				return m_InnerMargins;
			}
		}

		string m_CueBanner;
		EditMargins m_InnerMargins;

		protected override void OnHandleCreated(EventArgs e) {
			base.OnHandleCreated(e);
			this.CueBanner = m_CueBanner;
		}

		protected override void OnFontChanged(EventArgs e) {
			base.OnFontChanged(e);
			this.InnerMargins = m_InnerMargins;
		}

		protected override void OnGotFocus(EventArgs e) {
			if (base.GetStyle(ControlStyles.UserPaint)) {
				base.SetStyle(ControlStyles.UserPaint, false);
				base.UpdateStyles();
				changeFromOwnerDrawToNormal();
			}
			base.OnGotFocus(e);
		}

		protected override void OnLostFocus(EventArgs e) {
			if (base.TextLength == 0) {
				base.SetStyle(ControlStyles.UserPaint, true);
				base.UpdateStyles();
			}
			base.OnLostFocus(e);
		}

		protected override void OnTextChanged(EventArgs e) {
			if (m_CueBanner.Length > 0 && base.TextLength == 0 && base.Focused == false) {
				if (base.GetStyle(ControlStyles.UserPaint) == false) {
					base.SetStyle(ControlStyles.UserPaint, true);
					base.UpdateStyles();
				}
			}
			else {
				if (base.GetStyle(ControlStyles.UserPaint)) {
					base.SetStyle(ControlStyles.UserPaint, false);
					base.UpdateStyles();
					changeFromOwnerDrawToNormal();
				}
			}
			base.OnTextChanged(e);
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			using (Font font = new Font(base.Font, FontStyle.Italic)) {
				Rectangle rect = base.ClientRectangle;
				rect.X += m_InnerMargins.Left;
				rect.Width -= (m_InnerMargins.Left + m_InnerMargins.Right);
				StringFormat sf = StringFormat.GenericDefault;
				sf.LineAlignment = StringAlignment.Center;
				e.Graphics.DrawString(m_CueBanner, font, SystemBrushes.GrayText, rect, sf);
			}
		}

		void changeFromOwnerDrawToNormal() {
			Font font = base.Font;
			base.Font = null;
			base.Font = font;
			this.InnerMargins = m_InnerMargins;
		}
	}

#if DEBUG
	[Serializable]
	[TypeConverter(typeof(MarginsConverter))]
#endif
	public struct EditMargins {
		static EditMargins() {
			Empty = new EditMargins();
		}
		public static readonly EditMargins Empty;
		public EditMargins(int left, int right) {
			m_Left = left;
			m_Right = right;
		}
		/// <summary>
		/// The width of the left margin.
		/// </summary>
		[Description("Gets or sets the width of the left margin.")]
		public int Left {
			set { m_Left = value; }
			get { return m_Left; }
		}
		/// <summary>
		/// The width of the right margin.
		/// </summary>
		[Description("Gets or sets the width of the right margin.")]
		public int Right {
			set { m_Right = value; }
			get { return m_Right; }
		}
		int m_Left;
		int m_Right;
	}
}
