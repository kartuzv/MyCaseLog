
namespace MyCaseLog
{
	partial class SelectArea
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectArea));
			this.panelDrag = new System.Windows.Forms.Panel();
			this.btnCaptureThis = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// panelDrag
			// 
			this.panelDrag.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelDrag.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelDrag.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.panelDrag.Location = new System.Drawing.Point(12, 12);
			this.panelDrag.Name = "panelDrag";
			this.panelDrag.Size = new System.Drawing.Size(394, 389);
			this.panelDrag.TabIndex = 0;
			this.panelDrag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelDrag_MouseDown);
			// 
			// btnCaptureThis
			// 
			this.btnCaptureThis.BackColor = System.Drawing.Color.Transparent;
			this.btnCaptureThis.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCaptureThis.BackgroundImage")));
			this.btnCaptureThis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btnCaptureThis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCaptureThis.ForeColor = System.Drawing.Color.Transparent;
			this.btnCaptureThis.Location = new System.Drawing.Point(-1, -4);
			this.btnCaptureThis.Name = "btnCaptureThis";
			this.btnCaptureThis.Size = new System.Drawing.Size(56, 57);
			this.btnCaptureThis.TabIndex = 0;
			this.btnCaptureThis.UseVisualStyleBackColor = false;
			this.btnCaptureThis.Click += new System.EventHandler(this.btnCaptureThis_Click);
			// 
			// SelectArea
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(418, 413);
			this.Controls.Add(this.btnCaptureThis);
			this.Controls.Add(this.panelDrag);
			this.ForeColor = System.Drawing.Color.Transparent;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "SelectArea";
			this.Text = "SelectArea";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panelDrag;
		private System.Windows.Forms.Button btnCaptureThis;
	}
}