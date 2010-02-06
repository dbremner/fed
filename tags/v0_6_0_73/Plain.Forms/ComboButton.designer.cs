namespace Plain.Forms {
	partial class ComboButton {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.comboBox = new Plain.Forms.ComboBoxDropDown();
			this.label = new System.Windows.Forms.Label();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// comboBox
			// 
			this.comboBox.Location = new System.Drawing.Point(20, 24);
			this.comboBox.Name = "comboBox";
			this.comboBox.Size = new System.Drawing.Size(121, 21);
			this.comboBox.TabIndex = 0;
			this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
			// 
			// label
			// 
			this.label.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label.Location = new System.Drawing.Point(0, 0);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(141, 48);
			this.label.TabIndex = 1;
			this.label.MouseLeave += new System.EventHandler(this.label_MouseLeave);
			this.label.Paint += new System.Windows.Forms.PaintEventHandler(this.label_Paint);
			this.label.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_MouseDown);
			this.label.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label_MouseUp);
			this.label.MouseEnter += new System.EventHandler(this.label_MouseEnter);
			// 
			// timer
			// 
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// ComboButton
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.label);
			this.Controls.Add(this.comboBox);
			this.Name = "ComboButton";
			this.Size = new System.Drawing.Size(141, 48);
			this.ResumeLayout(false);

		}

		#endregion

		private Plain.Forms.ComboBoxDropDown comboBox;
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.Timer timer;
	}
}
