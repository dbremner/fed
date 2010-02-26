/*
FED (Front-End for Dosbox), a gaming graphical user interface for the DOSBox emulator.
Copyright (C) 2010  Ka Ki Cheung

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

//#define USE_OWNERDRAWCUEBANNER
//#define USE_ITALICFONTCUEBANNER
//#define USE_NATIVECUEBANNER

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Plain.Native;

namespace Plain.Forms {
	public class TextBoxEx : System.Windows.Forms.TextBox {
		public TextBoxEx() {
			m_CueBannerItalicLabel = new ClickThroughLabel();
			m_CueBannerItalicLabel.AutoSize = false;
			m_CueBannerItalicLabel.Dock = DockStyle.Fill;
			m_CueBannerItalicLabel.ForeColor = SystemColors.GrayText;
			m_CueBannerItalicLabel.TextAlign = ContentAlignment.MiddleLeft;
			m_CueBanner = string.Empty;
#if USE_ITALICFONTCUEBANNER
			setFontForCueBanner();
#endif
		}

		public event EventHandler CueBannerShown = delegate { };
		public event EventHandler CueBannerHidden = delegate { };

#if USE_ITALICFONTCUEBANNER
		public override void ResetFont() {
			base.ResetFont();
		}

		public override Font Font {
			set {
				m_Font = value;
				m_IsFontChanging = true;
				if (shouldShowCueBanner() == false || setFontForCueBanner() == false) {
					base.Font = m_Font;
				}
				m_IsFontChanging = false;
			}
			get { return base.Font; }
		}
#endif

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
				m_CueBannerItalicLabel.Text = m_CueBanner;
#if USE_OWNERDRAWCUEBANNER
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
#elif USE_NATIVECUEBANNER
				NativeMethods.SendMessage(base.Handle, NativeMethods.EM_SETCUEBANNER, 0, m_CueBanner);
#endif
				signalCueBannerVisibility();
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
		/// 
		/// </summary>
		[DefaultValue(typeof(Padding), "0, 0, 0, 0")]
		public Padding InnerMargin {
			set {
				m_InnerMargins = value;
#if USE_ITALICFONTCUEBANNER
				if (m_InnerMargins.IsEmpty == false && shouldShowCueBanner()) {
					return;
				}
#endif
				setInnerMargins(m_InnerMargins);
				m_CueBannerItalicLabel.Padding = new Padding(m_InnerMargins.Left, 0, m_InnerMargins.Right, 2);
			}
			get {
#if false
				int ret = NativeMethods.SendMessage(base.Handle, NativeMethods.EM_GETMARGINS, 0, 0);
				return new Padding(NativeMethods.LOWORD(ret), 0, NativeMethods.HIWORD(ret), 0);
#endif
				return m_InnerMargins;
			}
		}

#if USE_ITALICFONTCUEBANNER
		Font m_Font;
		bool m_IsFontChanging;
#endif
		string m_CueBanner;
		Padding m_InnerMargins;
		bool m_CueBannerVisible;
		ClickThroughLabel m_CueBannerItalicLabel;

		protected virtual void OnCueBannerShown(EventArgs e) {
			base.Controls.Add(m_CueBannerItalicLabel);
			CueBannerShown(this, e);
		}

		protected virtual void OnCueBannerHidden(EventArgs e) {
			base.Controls.Remove(m_CueBannerItalicLabel);
			CueBannerHidden(this, e);
		}

		protected override void OnMultilineChanged(EventArgs e) {
			base.OnMultilineChanged(e);
			m_CueBannerItalicLabel.TextAlign = base.Multiline ? ContentAlignment.TopLeft : ContentAlignment.MiddleLeft;
		}

		protected override void OnHandleCreated(EventArgs e) {
			base.OnHandleCreated(e);
			this.CueBanner = m_CueBanner;
			setInnerMargins(m_InnerMargins);
		}

		protected override void OnVisibleChanged(EventArgs e) {
			base.OnVisibleChanged(e);
			setInnerMargins(m_InnerMargins);	// Needed, or margins applied but not shown.
		}

		protected override void OnParentFontChanged(EventArgs e) {
#if USE_ITALICFONTCUEBANNER
			this.Font = base.Parent.Font;
#endif
			base.OnParentFontChanged(e);
			m_CueBannerItalicLabel.Font = new Font(base.Font, base.Font.Style | FontStyle.Italic);
		}

		protected override void OnFontChanged(EventArgs e) {
#if USE_ITALICFONTCUEBANNER
			if (m_IsFontChanging == false) {
				m_Font = base.Font;
			}
			if (shouldShowCueBanner() == false) {
				setInnerMargins(m_InnerMargins);
			}
#endif
			base.OnFontChanged(e);
			m_CueBannerItalicLabel.Font = new Font(base.Font, base.Font.Style | FontStyle.Italic);
		}

		protected override void OnSystemColorsChanged(EventArgs e) {
			base.OnSystemColorsChanged(e);
			m_CueBannerItalicLabel.ForeColor = SystemColors.GrayText;
		}

		protected override void OnBackColorChanged(EventArgs e) {
			base.OnBackColorChanged(e);
			m_CueBannerItalicLabel.BackColor = base.BackColor;
		}

		protected override void OnGotFocus(EventArgs e) {
#if USE_OWNERDRAWCUEBANNER
			if (base.GetStyle(ControlStyles.UserPaint)) {
				base.SetStyle(ControlStyles.UserPaint, false);
				base.UpdateStyles();
				changeFromOwnerDrawToNormal();
			}
#elif USE_ITALICFONTCUEBANNER
			if (setFontForNormal()) {
				setInnerMargins(m_InnerMargins);
			}
#endif
			signalCueBannerVisibility(false);
			base.OnGotFocus(e);
		}

		protected override void OnLostFocus(EventArgs e) {
			if (shouldShowCueBannerWhenNotFocused()) {
#if USE_OWNERDRAWCUEBANNER
				base.SetStyle(ControlStyles.UserPaint, true);
				base.UpdateStyles();
#elif USE_ITALICFONTCUEBANNER
				setFontForCueBanner();
				setInnerMargins(Padding.Empty);
#endif
				signalCueBannerVisibility(true);
			}
			base.OnLostFocus(e);
		}

		protected override void OnTextChanged(EventArgs e) {
			if (shouldShowCueBanner()) {
#if USE_OWNERDRAWCUEBANNER
				if (base.GetStyle(ControlStyles.UserPaint) == false) {
					base.SetStyle(ControlStyles.UserPaint, true);
					base.UpdateStyles();
				}
#elif USE_ITALICFONTCUEBANNER
				setFontForCueBanner();
				setInnerMargins(Padding.Empty);
#endif
				signalCueBannerVisibility(true);
			}
			else {
#if USE_OWNERDRAWCUEBANNER
				if (base.GetStyle(ControlStyles.UserPaint)) {
					base.SetStyle(ControlStyles.UserPaint, false);
					base.UpdateStyles();
					changeFromOwnerDrawToNormal();
				}
#elif USE_ITALICFONTCUEBANNER
				if (setFontForNormal()) {
					setInnerMargins(m_InnerMargins);
				}
#endif
				signalCueBannerVisibility(false);
			}
			base.OnTextChanged(e);
		}

#if USE_OWNERDRAWCUEBANNER
		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			using (Font font = new Font(base.Font, FontStyle.Italic)) {
				Rectangle rect = base.ClientRectangle;
				rect.X += m_InnerMargins.Left;
				rect.Width -= (m_InnerMargins.Left + m_InnerMargins.Right);
				StringFormat sf = StringFormat.GenericDefault;
				sf.LineAlignment = StringAlignment.Center;
				e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
				e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
				e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
				e.Graphics.DrawString(m_CueBanner, font, SystemBrushes.GrayText, rect, sf);
			}
		}

		void changeFromOwnerDrawToNormal() {
			Font font = base.Font;
			base.Font = null;
			base.Font = font;
			this.InnerMargins = m_InnerMargins;
		}
#endif

		bool shouldShowCueBannerWhenNotFocused() {
			return m_CueBanner.Length > 0 && base.TextLength == 0;
		}

		bool shouldShowCueBanner() {
			return shouldShowCueBannerWhenNotFocused() && base.Focused == false;
		}

		void signalCueBannerVisibility() {
			signalCueBannerVisibility(shouldShowCueBanner());
		}

		void signalCueBannerVisibility(bool shouldShow) {
			if (shouldShow) {
				if (m_CueBannerVisible == false) {
					m_CueBannerVisible = true;
					OnCueBannerShown(EventArgs.Empty);
				}
			}
			else {
				if (m_CueBannerVisible) {
					m_CueBannerVisible = false;
					OnCueBannerHidden(EventArgs.Empty);
				}
			}
		}

#if USE_ITALICFONTCUEBANNER
		bool setFontForCueBanner() {
			// We need an italic font, so even if null is to be set (i.e. use parent), we need to see if parent font needs to be modified.
			Font font = (m_Font != null) ? m_Font : base.Font;
			if ((font.Style & FontStyle.Italic) == 0) {
				base.Font = new Font(font, font.Style | FontStyle.Italic);
				return true;
			}
			return false;
		}

		bool setFontForNormal() {
			if (m_Font != null) {
				if ((base.Font.Style & FontStyle.Italic) != 0) {
					base.Font = new Font(m_Font, m_Font.Style & ~FontStyle.Italic);
					return true;
				}
				return false;
			}
			base.Font = m_Font;
			return true;
		}
#endif

		void setInnerMargins(Padding margins) {
			NativeMethods.SendMessage(base.Handle, NativeMethods.EM_SETMARGINS,
				NativeMethods.EC_LEFTMARGIN | NativeMethods.EC_RIGHTMARGIN,
				(int) NativeMethods.MAKELONG((ushort) margins.Left, (ushort) margins.Right));
		}
	}
}
