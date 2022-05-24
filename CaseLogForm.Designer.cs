
namespace MyCaseLog
{
	partial class CaseLogForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaseLogForm));
			this.btnSaveToLog = new System.Windows.Forms.Button();
			this.txtViewerTitle = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOpenLogDir = new System.Windows.Forms.Button();
			this.cboHosp = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnPPTEST = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnSaveToLog
			// 
			this.btnSaveToLog.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnSaveToLog.Location = new System.Drawing.Point(12, 12);
			this.btnSaveToLog.Name = "btnSaveToLog";
			this.btnSaveToLog.Size = new System.Drawing.Size(170, 41);
			this.btnSaveToLog.TabIndex = 0;
			this.btnSaveToLog.Text = "Add To CaseLog";
			this.btnSaveToLog.UseVisualStyleBackColor = true;
			this.btnSaveToLog.Click += new System.EventHandler(this.btnSaveToLog_Click);
			// 
			// txtViewerTitle
			// 
			this.txtViewerTitle.Location = new System.Drawing.Point(77, 69);
			this.txtViewerTitle.Name = "txtViewerTitle";
			this.txtViewerTitle.Size = new System.Drawing.Size(176, 23);
			this.txtViewerTitle.TabIndex = 1;
			this.txtViewerTitle.Text = "Philips iSite Radiology";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 72);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "Viewer";
			// 
			// btnOpenLogDir
			// 
			this.btnOpenLogDir.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOpenLogDir.BackgroundImage")));
			this.btnOpenLogDir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnOpenLogDir.Location = new System.Drawing.Point(312, 12);
			this.btnOpenLogDir.Name = "btnOpenLogDir";
			this.btnOpenLogDir.Size = new System.Drawing.Size(46, 47);
			this.btnOpenLogDir.TabIndex = 3;
			this.btnOpenLogDir.Text = "Log";
			this.btnOpenLogDir.UseVisualStyleBackColor = true;
			this.btnOpenLogDir.Click += new System.EventHandler(this.btnOpenLogDir_Click);
			// 
			// cboHosp
			// 
			this.cboHosp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboHosp.FormattingEnabled = true;
			this.cboHosp.Location = new System.Drawing.Point(67, 98);
			this.cboHosp.Name = "cboHosp";
			this.cboHosp.Size = new System.Drawing.Size(186, 23);
			this.cboHosp.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 106);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 15);
			this.label2.TabIndex = 5;
			this.label2.Text = "Hospital";
			// 
			// btnPPTEST
			// 
			this.btnPPTEST.Location = new System.Drawing.Point(288, 86);
			this.btnPPTEST.Name = "btnPPTEST";
			this.btnPPTEST.Size = new System.Drawing.Size(75, 23);
			this.btnPPTEST.TabIndex = 6;
			this.btnPPTEST.Text = "PP-TEST";
			this.btnPPTEST.UseVisualStyleBackColor = true;
			this.btnPPTEST.Click += new System.EventHandler(this.btnPPTEST_Click);
			// 
			// CaseLogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(370, 136);
			this.Controls.Add(this.btnPPTEST);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cboHosp);
			this.Controls.Add(this.btnOpenLogDir);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtViewerTitle);
			this.Controls.Add(this.btnSaveToLog);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "CaseLogForm";
			this.Text = "MyCaseLog";
			this.Load += new System.EventHandler(this.CaseLogForm_Load);
			this.Shown += new System.EventHandler(this.CaseLogForm_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSaveToLog;
		private System.Windows.Forms.TextBox txtViewerTitle;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOpenLogDir;
		private System.Windows.Forms.ComboBox cboHosp;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnPPTEST;
	}
}