
namespace MyCaseLog
{
	partial class ScreenshotForm
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
			this.pbCapture = new System.Windows.Forms.PictureBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnRetry = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pbCapture)).BeginInit();
			this.SuspendLayout();
			// 
			// pbCapture
			// 
			this.pbCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbCapture.Location = new System.Drawing.Point(1, 33);
			this.pbCapture.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.pbCapture.Name = "pbCapture";
			this.pbCapture.Size = new System.Drawing.Size(429, 219);
			this.pbCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbCapture.TabIndex = 0;
			this.pbCapture.TabStop = false;
			// 
			// btnSave
			// 
			this.btnSave.BackColor = System.Drawing.Color.LimeGreen;
			this.btnSave.Location = new System.Drawing.Point(1, 0);
			this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(88, 27);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = false;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.BackColor = System.Drawing.Color.OrangeRed;
			this.btnDelete.Location = new System.Drawing.Point(354, 0);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(76, 30);
			this.btnDelete.TabIndex = 2;
			this.btnDelete.Text = "Discard";
			this.btnDelete.UseVisualStyleBackColor = false;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnRetry
			// 
			this.btnRetry.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnRetry.BackColor = System.Drawing.Color.Yellow;
			this.btnRetry.Location = new System.Drawing.Point(180, 2);
			this.btnRetry.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.btnRetry.Name = "btnRetry";
			this.btnRetry.Size = new System.Drawing.Size(88, 27);
			this.btnRetry.TabIndex = 3;
			this.btnRetry.Text = "Retry";
			this.btnRetry.UseVisualStyleBackColor = false;
			this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
			// 
			// ScreenshotForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(435, 255);
			this.Controls.Add(this.btnRetry);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.pbCapture);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "ScreenshotForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Save";
			((System.ComponentModel.ISupportInitialize)(this.pbCapture)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCapture;
        private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnRetry;
	}
}