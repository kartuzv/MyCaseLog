
namespace MyCaseLog
{
	partial class CaseLogForm2
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaseLogForm2));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cboSpecialty = new System.Windows.Forms.ComboBox();
			this.cboBodyPart = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cboPIDType = new System.Windows.Forms.ComboBox();
			this.txtPTID = new System.Windows.Forms.TextBox();
			this.chkKeepBodyPart = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtNotes = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnAddScreenshot = new System.Windows.Forms.Button();
			this.btnCaptureScreen = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.chkPPTX = new System.Windows.Forms.CheckBox();
			this.chkXLSX = new System.Windows.Forms.CheckBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.txtTags = new System.Windows.Forms.TextBox();
			this.btnEditListSpeciality = new System.Windows.Forms.Button();
			this.btnViewSpecialtyCases = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(11, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Sub-Specialty:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(11, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Body Part:";
			// 
			// cboSpecialty
			// 
			this.cboSpecialty.BackColor = System.Drawing.Color.Black;
			this.cboSpecialty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSpecialty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cboSpecialty.ForeColor = System.Drawing.Color.White;
			this.cboSpecialty.FormattingEnabled = true;
			this.cboSpecialty.Items.AddRange(new object[] {
            "Abdominal",
            "Chest",
            "Body",
            "Nuclear",
            "Mammo",
            "Neuro",
            "Pediatrics",
            "MSK"});
			this.cboSpecialty.Location = new System.Drawing.Point(118, 13);
			this.cboSpecialty.Name = "cboSpecialty";
			this.cboSpecialty.Size = new System.Drawing.Size(299, 23);
			this.cboSpecialty.TabIndex = 2;
			// 
			// cboBodyPart
			// 
			this.cboBodyPart.BackColor = System.Drawing.Color.Black;
			this.cboBodyPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboBodyPart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cboBodyPart.ForeColor = System.Drawing.Color.White;
			this.cboBodyPart.FormattingEnabled = true;
			this.cboBodyPart.Items.AddRange(new object[] {
            "Liver",
            "GB/Biliary",
            "Pancreas",
            "Kidneys/Ureters/Bladder",
            "Adrenals",
            "Lymph nodes",
            "Bowel",
            "Vasculature",
            "Female Gyn",
            "Male GU",
            "Genitourinary",
            "Bones",
            "Other"});
			this.cboBodyPart.Location = new System.Drawing.Point(118, 53);
			this.cboBodyPart.Name = "cboBodyPart";
			this.cboBodyPart.Size = new System.Drawing.Size(299, 23);
			this.cboBodyPart.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(11, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 15);
			this.label3.TabIndex = 4;
			this.label3.Text = "Patient ID:";
			// 
			// cboPIDType
			// 
			this.cboPIDType.BackColor = System.Drawing.Color.Black;
			this.cboPIDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPIDType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cboPIDType.ForeColor = System.Drawing.Color.White;
			this.cboPIDType.FormattingEnabled = true;
			this.cboPIDType.Items.AddRange(new object[] {
            "MRN",
            "Accession"});
			this.cboPIDType.Location = new System.Drawing.Point(99, 101);
			this.cboPIDType.Name = "cboPIDType";
			this.cboPIDType.Size = new System.Drawing.Size(129, 23);
			this.cboPIDType.TabIndex = 5;
			// 
			// txtPTID
			// 
			this.txtPTID.BackColor = System.Drawing.Color.Black;
			this.txtPTID.ForeColor = System.Drawing.Color.White;
			this.txtPTID.Location = new System.Drawing.Point(243, 101);
			this.txtPTID.Name = "txtPTID";
			this.txtPTID.Size = new System.Drawing.Size(155, 23);
			this.txtPTID.TabIndex = 6;
			// 
			// chkKeepBodyPart
			// 
			this.chkKeepBodyPart.AutoSize = true;
			this.chkKeepBodyPart.ForeColor = System.Drawing.Color.White;
			this.chkKeepBodyPart.Location = new System.Drawing.Point(428, 57);
			this.chkKeepBodyPart.Name = "chkKeepBodyPart";
			this.chkKeepBodyPart.Size = new System.Drawing.Size(52, 19);
			this.chkKeepBodyPart.TabIndex = 8;
			this.chkKeepBodyPart.Text = "Keep";
			this.chkKeepBodyPart.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.ForeColor = System.Drawing.Color.White;
			this.label4.Location = new System.Drawing.Point(11, 146);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 15);
			this.label4.TabIndex = 9;
			this.label4.Text = "Notes:";
			// 
			// txtNotes
			// 
			this.txtNotes.BackColor = System.Drawing.SystemColors.ControlText;
			this.txtNotes.ForeColor = System.Drawing.Color.White;
			this.txtNotes.Location = new System.Drawing.Point(11, 164);
			this.txtNotes.Multiline = true;
			this.txtNotes.Name = "txtNotes";
			this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtNotes.Size = new System.Drawing.Size(469, 70);
			this.txtNotes.TabIndex = 10;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.White;
			this.label5.Location = new System.Drawing.Point(12, 250);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(34, 15);
			this.label5.TabIndex = 11;
			this.label5.Text = "Tags:";
			// 
			// btnAddScreenshot
			// 
			this.btnAddScreenshot.BackColor = System.Drawing.Color.Black;
			this.btnAddScreenshot.ForeColor = System.Drawing.Color.White;
			this.btnAddScreenshot.Location = new System.Drawing.Point(157, 320);
			this.btnAddScreenshot.Name = "btnAddScreenshot";
			this.btnAddScreenshot.Size = new System.Drawing.Size(175, 38);
			this.btnAddScreenshot.TabIndex = 13;
			this.btnAddScreenshot.TabStop = false;
			this.btnAddScreenshot.Text = "Add Screenshot";
			this.btnAddScreenshot.UseVisualStyleBackColor = false;
			this.btnAddScreenshot.Click += new System.EventHandler(this.btnAddScreenshot_Click);
			// 
			// btnCaptureScreen
			// 
			this.btnCaptureScreen.BackColor = System.Drawing.Color.Black;
			this.btnCaptureScreen.ForeColor = System.Drawing.Color.White;
			this.btnCaptureScreen.Location = new System.Drawing.Point(417, 333);
			this.btnCaptureScreen.Name = "btnCaptureScreen";
			this.btnCaptureScreen.Size = new System.Drawing.Size(64, 25);
			this.btnCaptureScreen.TabIndex = 14;
			this.btnCaptureScreen.Text = "+Video";
			this.btnCaptureScreen.UseVisualStyleBackColor = false;
			this.btnCaptureScreen.Click += new System.EventHandler(this.btnCaptureScreen_Click);
			// 
			// listView1
			// 
			this.listView1.Activation = System.Windows.Forms.ItemActivation.TwoClick;
			this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.Left;
			this.listView1.BackColor = System.Drawing.SystemColors.InfoText;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listView1.HideSelection = false;
			this.listView1.LargeImageList = this.imageList1;
			this.listView1.Location = new System.Drawing.Point(13, 364);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(468, 109);
			this.listView1.TabIndex = 15;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(100, 100);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// chkPPTX
			// 
			this.chkPPTX.AutoSize = true;
			this.chkPPTX.Checked = true;
			this.chkPPTX.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkPPTX.ForeColor = System.Drawing.Color.White;
			this.chkPPTX.Location = new System.Drawing.Point(12, 479);
			this.chkPPTX.Name = "chkPPTX";
			this.chkPPTX.Size = new System.Drawing.Size(54, 19);
			this.chkPPTX.TabIndex = 16;
			this.chkPPTX.Text = "PPTX";
			this.chkPPTX.UseVisualStyleBackColor = true;
			// 
			// chkXLSX
			// 
			this.chkXLSX.AutoSize = true;
			this.chkXLSX.ForeColor = System.Drawing.Color.White;
			this.chkXLSX.Location = new System.Drawing.Point(12, 505);
			this.chkXLSX.Name = "chkXLSX";
			this.chkXLSX.Size = new System.Drawing.Size(52, 19);
			this.chkXLSX.TabIndex = 17;
			this.chkXLSX.Text = "XLSX";
			this.chkXLSX.UseVisualStyleBackColor = true;
			this.chkXLSX.Visible = false;
			// 
			// btnSave
			// 
			this.btnSave.BackColor = System.Drawing.Color.Black;
			this.btnSave.ForeColor = System.Drawing.Color.White;
			this.btnSave.Location = new System.Drawing.Point(310, 479);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(171, 45);
			this.btnSave.TabIndex = 18;
			this.btnSave.Text = "Save Case";
			this.btnSave.UseVisualStyleBackColor = false;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Black;
			this.button1.ForeColor = System.Drawing.Color.White;
			this.button1.Location = new System.Drawing.Point(72, 479);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(65, 43);
			this.button1.TabIndex = 19;
			this.button1.Text = "View";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtTags
			// 
			this.txtTags.BackColor = System.Drawing.Color.Black;
			this.txtTags.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtTags.ForeColor = System.Drawing.Color.White;
			this.txtTags.Location = new System.Drawing.Point(51, 247);
			this.txtTags.Name = "txtTags";
			this.txtTags.Size = new System.Drawing.Size(429, 25);
			this.txtTags.TabIndex = 20;
			// 
			// btnEditListSpeciality
			// 
			this.btnEditListSpeciality.BackColor = System.Drawing.Color.Black;
			this.btnEditListSpeciality.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEditListSpeciality.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.btnEditListSpeciality.ForeColor = System.Drawing.Color.White;
			this.btnEditListSpeciality.Image = ((System.Drawing.Image)(resources.GetObject("btnEditListSpeciality.Image")));
			this.btnEditListSpeciality.Location = new System.Drawing.Point(92, 13);
			this.btnEditListSpeciality.Name = "btnEditListSpeciality";
			this.btnEditListSpeciality.Size = new System.Drawing.Size(22, 22);
			this.btnEditListSpeciality.TabIndex = 21;
			this.btnEditListSpeciality.TabStop = false;
			this.btnEditListSpeciality.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.btnEditListSpeciality.UseVisualStyleBackColor = false;
			this.btnEditListSpeciality.Click += new System.EventHandler(this.btnEditListSpeciality_Click);
			// 
			// btnViewSpecialtyCases
			// 
			this.btnViewSpecialtyCases.BackColor = System.Drawing.Color.Black;
			this.btnViewSpecialtyCases.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnViewSpecialtyCases.Font = new System.Drawing.Font("Wingdings 3", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnViewSpecialtyCases.Location = new System.Drawing.Point(451, 12);
			this.btnViewSpecialtyCases.Name = "btnViewSpecialtyCases";
			this.btnViewSpecialtyCases.Size = new System.Drawing.Size(29, 24);
			this.btnViewSpecialtyCases.TabIndex = 22;
			this.btnViewSpecialtyCases.Text = "u";
			this.btnViewSpecialtyCases.UseVisualStyleBackColor = false;
			this.btnViewSpecialtyCases.Click += new System.EventHandler(this.btnViewSpecialtyCases_Click);
			// 
			// button3
			// 
			this.button3.BackColor = System.Drawing.Color.Black;
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
			this.button3.ForeColor = System.Drawing.Color.White;
			this.button3.Location = new System.Drawing.Point(38, 276);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(85, 25);
			this.button3.TabIndex = 23;
			this.button3.Text = "#Learning";
			this.button3.UseVisualStyleBackColor = false;
			this.button3.Click += new System.EventHandler(this.AddPresetTag_Click);
			// 
			// button4
			// 
			this.button4.BackColor = System.Drawing.Color.Black;
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
			this.button4.ForeColor = System.Drawing.Color.White;
			this.button4.Location = new System.Drawing.Point(129, 276);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(50, 25);
			this.button4.TabIndex = 24;
			this.button4.Text = "#Rare";
			this.button4.UseVisualStyleBackColor = false;
			this.button4.Click += new System.EventHandler(this.AddPresetTag_Click);
			// 
			// button5
			// 
			this.button5.BackColor = System.Drawing.Color.Black;
			this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
			this.button5.ForeColor = System.Drawing.Color.White;
			this.button5.Location = new System.Drawing.Point(192, 276);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(84, 25);
			this.button5.TabIndex = 25;
			this.button5.Text = "#FollowUp";
			this.button5.UseVisualStyleBackColor = false;
			this.button5.Click += new System.EventHandler(this.AddPresetTag_Click);
			// 
			// CaseLogForm2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(494, 536);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.btnViewSpecialtyCases);
			this.Controls.Add(this.btnEditListSpeciality);
			this.Controls.Add(this.txtTags);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.chkXLSX);
			this.Controls.Add(this.chkPPTX);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.btnCaptureScreen);
			this.Controls.Add(this.btnAddScreenshot);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtNotes);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.chkKeepBodyPart);
			this.Controls.Add(this.txtPTID);
			this.Controls.Add(this.cboPIDType);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cboBodyPart);
			this.Controls.Add(this.cboSpecialty);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.ForeColor = System.Drawing.SystemColors.Control;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Name = "CaseLogForm2";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "MyCaseLog";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CaseLogForm2_FormClosing);
			this.Load += new System.EventHandler(this.CaseLogForm2_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboSpecialty;
		private System.Windows.Forms.ComboBox cboBodyPart;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cboPIDType;
		private System.Windows.Forms.TextBox txtPTID;
		private System.Windows.Forms.CheckBox chkKeepBodyPart;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtNotes;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnAddScreenshot;
		private System.Windows.Forms.Button btnCaptureScreen;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.CheckBox chkPPTX;
		private System.Windows.Forms.CheckBox chkXLSX;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtTags;
		private System.Windows.Forms.Button btnEditListSpeciality;
		private System.Windows.Forms.Button btnViewSpecialtyCases;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
	}
}