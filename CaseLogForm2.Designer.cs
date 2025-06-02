
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaseLogForm2));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			cboSpecialty = new System.Windows.Forms.ComboBox();
			cboBodyPart = new System.Windows.Forms.ComboBox();
			label3 = new System.Windows.Forms.Label();
			cboPIDType = new System.Windows.Forms.ComboBox();
			txtPTID = new System.Windows.Forms.TextBox();
			chkKeepBodyPart = new System.Windows.Forms.CheckBox();
			label4 = new System.Windows.Forms.Label();
			txtNotes = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			btnAddScreenshot = new System.Windows.Forms.Button();
			btnCaptureScreen = new System.Windows.Forms.Button();
			listView1 = new System.Windows.Forms.ListView();
			imageList1 = new System.Windows.Forms.ImageList(components);
			chkPPTX = new System.Windows.Forms.CheckBox();
			chkXLSX = new System.Windows.Forms.CheckBox();
			btnSave = new System.Windows.Forms.Button();
			btnView = new System.Windows.Forms.Button();
			txtTags = new System.Windows.Forms.TextBox();
			btnEditListSpeciality = new System.Windows.Forms.Button();
			btnViewSpecialtyCases = new System.Windows.Forms.Button();
			button3 = new System.Windows.Forms.Button();
			button4 = new System.Windows.Forms.Button();
			button5 = new System.Windows.Forms.Button();
			btnEditListBodyPart = new System.Windows.Forms.Button();
			gvCaseList = new System.Windows.Forms.DataGridView();
			btnOpenAccessionInEI = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)gvCaseList).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(11, 15);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 15);
			label1.TabIndex = 0;
			label1.Text = "Speciality:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(11, 56);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(61, 15);
			label2.TabIndex = 1;
			label2.Text = "Body Part:";
			// 
			// cboSpecialty
			// 
			cboSpecialty.BackColor = System.Drawing.Color.Black;
			cboSpecialty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cboSpecialty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cboSpecialty.ForeColor = System.Drawing.Color.White;
			cboSpecialty.FormattingEnabled = true;
			cboSpecialty.Items.AddRange(new object[] { "Abdominal", "Chest", "Body", "Nuclear", "Mammo", "Neuro", "Pediatrics", "MSK" });
			cboSpecialty.Location = new System.Drawing.Point(118, 13);
			cboSpecialty.Name = "cboSpecialty";
			cboSpecialty.Size = new System.Drawing.Size(299, 23);
			cboSpecialty.TabIndex = 2;
			// 
			// cboBodyPart
			// 
			cboBodyPart.BackColor = System.Drawing.Color.Black;
			cboBodyPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cboBodyPart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cboBodyPart.ForeColor = System.Drawing.Color.White;
			cboBodyPart.FormattingEnabled = true;
			cboBodyPart.Items.AddRange(new object[] { "Liver", "GB/Biliary", "Pancreas", "Kidneys/Ureters/Bladder", "Adrenals", "Lymph nodes", "Bowel", "Vasculature", "Female Gyn", "Male GU", "Genitourinary", "Bones", "Other" });
			cboBodyPart.Location = new System.Drawing.Point(118, 53);
			cboBodyPart.Name = "cboBodyPart";
			cboBodyPart.Size = new System.Drawing.Size(299, 23);
			cboBodyPart.TabIndex = 3;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.ForeColor = System.Drawing.Color.White;
			label3.Location = new System.Drawing.Point(11, 104);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(61, 15);
			label3.TabIndex = 4;
			label3.Text = "Patient ID:";
			// 
			// cboPIDType
			// 
			cboPIDType.BackColor = System.Drawing.Color.Black;
			cboPIDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cboPIDType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cboPIDType.ForeColor = System.Drawing.Color.White;
			cboPIDType.FormattingEnabled = true;
			cboPIDType.Items.AddRange(new object[] { "Accession", "MRN" });
			cboPIDType.Location = new System.Drawing.Point(99, 101);
			cboPIDType.Name = "cboPIDType";
			cboPIDType.Size = new System.Drawing.Size(129, 23);
			cboPIDType.TabIndex = 5;
			// 
			// txtPTID
			// 
			txtPTID.BackColor = System.Drawing.Color.Black;
			txtPTID.ForeColor = System.Drawing.Color.White;
			txtPTID.Location = new System.Drawing.Point(243, 101);
			txtPTID.Name = "txtPTID";
			txtPTID.Size = new System.Drawing.Size(174, 23);
			txtPTID.TabIndex = 6;
			// 
			// chkKeepBodyPart
			// 
			chkKeepBodyPart.AutoSize = true;
			chkKeepBodyPart.ForeColor = System.Drawing.Color.White;
			chkKeepBodyPart.Location = new System.Drawing.Point(428, 57);
			chkKeepBodyPart.Name = "chkKeepBodyPart";
			chkKeepBodyPart.Size = new System.Drawing.Size(52, 19);
			chkKeepBodyPart.TabIndex = 8;
			chkKeepBodyPart.Text = "Keep";
			chkKeepBodyPart.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.ForeColor = System.Drawing.Color.White;
			label4.Location = new System.Drawing.Point(11, 146);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(41, 15);
			label4.TabIndex = 9;
			label4.Text = "Notes:";
			// 
			// txtNotes
			// 
			txtNotes.BackColor = System.Drawing.SystemColors.ControlText;
			txtNotes.ForeColor = System.Drawing.Color.White;
			txtNotes.Location = new System.Drawing.Point(11, 164);
			txtNotes.Multiline = true;
			txtNotes.Name = "txtNotes";
			txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			txtNotes.Size = new System.Drawing.Size(469, 70);
			txtNotes.TabIndex = 10;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.ForeColor = System.Drawing.Color.White;
			label5.Location = new System.Drawing.Point(12, 250);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(33, 15);
			label5.TabIndex = 11;
			label5.Text = "Tags:";
			// 
			// btnAddScreenshot
			// 
			btnAddScreenshot.BackColor = System.Drawing.Color.Black;
			btnAddScreenshot.ForeColor = System.Drawing.Color.White;
			btnAddScreenshot.Location = new System.Drawing.Point(157, 320);
			btnAddScreenshot.Name = "btnAddScreenshot";
			btnAddScreenshot.Size = new System.Drawing.Size(175, 38);
			btnAddScreenshot.TabIndex = 13;
			btnAddScreenshot.TabStop = false;
			btnAddScreenshot.Text = "Add Screenshot";
			btnAddScreenshot.UseVisualStyleBackColor = false;
			btnAddScreenshot.Click += btnAddScreenshot_Click;
			// 
			// btnCaptureScreen
			// 
			btnCaptureScreen.BackColor = System.Drawing.Color.Black;
			btnCaptureScreen.ForeColor = System.Drawing.Color.White;
			btnCaptureScreen.Location = new System.Drawing.Point(417, 333);
			btnCaptureScreen.Name = "btnCaptureScreen";
			btnCaptureScreen.Size = new System.Drawing.Size(64, 25);
			btnCaptureScreen.TabIndex = 14;
			btnCaptureScreen.Text = "+Video";
			btnCaptureScreen.UseVisualStyleBackColor = false;
			btnCaptureScreen.Visible = false;
			btnCaptureScreen.Click += btnCaptureScreen_Click;
			// 
			// listView1
			// 
			listView1.Activation = System.Windows.Forms.ItemActivation.TwoClick;
			listView1.Alignment = System.Windows.Forms.ListViewAlignment.Left;
			listView1.BackColor = System.Drawing.SystemColors.InfoText;
			listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			listView1.LargeImageList = imageList1;
			listView1.Location = new System.Drawing.Point(13, 364);
			listView1.MultiSelect = false;
			listView1.Name = "listView1";
			listView1.Size = new System.Drawing.Size(468, 109);
			listView1.TabIndex = 15;
			listView1.UseCompatibleStateImageBehavior = false;
			listView1.MouseDoubleClick += listView1_MouseDoubleClick;
			// 
			// imageList1
			// 
			imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			imageList1.ImageSize = new System.Drawing.Size(100, 100);
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// chkPPTX
			// 
			chkPPTX.AutoSize = true;
			chkPPTX.Checked = true;
			chkPPTX.CheckState = System.Windows.Forms.CheckState.Checked;
			chkPPTX.ForeColor = System.Drawing.Color.White;
			chkPPTX.Location = new System.Drawing.Point(82, 339);
			chkPPTX.Name = "chkPPTX";
			chkPPTX.Size = new System.Drawing.Size(53, 19);
			chkPPTX.TabIndex = 16;
			chkPPTX.Text = "PPTX";
			chkPPTX.UseVisualStyleBackColor = true;
			chkPPTX.Visible = false;
			// 
			// chkXLSX
			// 
			chkXLSX.AutoSize = true;
			chkXLSX.Checked = true;
			chkXLSX.CheckState = System.Windows.Forms.CheckState.Checked;
			chkXLSX.ForeColor = System.Drawing.Color.White;
			chkXLSX.Location = new System.Drawing.Point(19, 339);
			chkXLSX.Name = "chkXLSX";
			chkXLSX.Size = new System.Drawing.Size(52, 19);
			chkXLSX.TabIndex = 17;
			chkXLSX.Text = "XLSX";
			chkXLSX.UseVisualStyleBackColor = true;
			chkXLSX.Visible = false;
			// 
			// btnSave
			// 
			btnSave.BackColor = System.Drawing.Color.Black;
			btnSave.ForeColor = System.Drawing.Color.White;
			btnSave.Location = new System.Drawing.Point(327, 480);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(154, 45);
			btnSave.TabIndex = 18;
			btnSave.Text = "Save Case";
			btnSave.UseVisualStyleBackColor = false;
			btnSave.Click += btnSave_Click;
			// 
			// btnView
			// 
			btnView.BackColor = System.Drawing.Color.Black;
			btnView.ForeColor = System.Drawing.Color.White;
			btnView.Location = new System.Drawing.Point(13, 479);
			btnView.Name = "btnView";
			btnView.Size = new System.Drawing.Size(133, 45);
			btnView.TabIndex = 19;
			btnView.Text = "View Log";
			btnView.UseVisualStyleBackColor = false;
			btnView.Click += btnView_Click;
			// 
			// txtTags
			// 
			txtTags.BackColor = System.Drawing.Color.Black;
			txtTags.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			txtTags.ForeColor = System.Drawing.Color.White;
			txtTags.Location = new System.Drawing.Point(51, 247);
			txtTags.Name = "txtTags";
			txtTags.Size = new System.Drawing.Size(429, 25);
			txtTags.TabIndex = 20;
			// 
			// btnEditListSpeciality
			// 
			btnEditListSpeciality.BackColor = System.Drawing.Color.Black;
			btnEditListSpeciality.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnEditListSpeciality.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			btnEditListSpeciality.ForeColor = System.Drawing.Color.White;
			btnEditListSpeciality.Image = (System.Drawing.Image)resources.GetObject("btnEditListSpeciality.Image");
			btnEditListSpeciality.Location = new System.Drawing.Point(90, 13);
			btnEditListSpeciality.Name = "btnEditListSpeciality";
			btnEditListSpeciality.Size = new System.Drawing.Size(23, 22);
			btnEditListSpeciality.TabIndex = 21;
			btnEditListSpeciality.TabStop = false;
			btnEditListSpeciality.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			btnEditListSpeciality.UseVisualStyleBackColor = false;
			btnEditListSpeciality.Click += btnEditListSpeciality_Click;
			// 
			// btnViewSpecialtyCases
			// 
			btnViewSpecialtyCases.BackColor = System.Drawing.Color.Black;
			btnViewSpecialtyCases.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnViewSpecialtyCases.Font = new System.Drawing.Font("Webdings", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			btnViewSpecialtyCases.Location = new System.Drawing.Point(441, 4);
			btnViewSpecialtyCases.Name = "btnViewSpecialtyCases";
			btnViewSpecialtyCases.Size = new System.Drawing.Size(51, 38);
			btnViewSpecialtyCases.TabIndex = 22;
			btnViewSpecialtyCases.Text = "L";
			btnViewSpecialtyCases.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			btnViewSpecialtyCases.UseVisualStyleBackColor = false;
			btnViewSpecialtyCases.Click += btnViewSpecialtyCases_Click;
			// 
			// button3
			// 
			button3.BackColor = System.Drawing.Color.Black;
			button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			button3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
			button3.ForeColor = System.Drawing.Color.White;
			button3.Location = new System.Drawing.Point(51, 276);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(85, 25);
			button3.TabIndex = 23;
			button3.Text = "#Learning";
			button3.UseVisualStyleBackColor = false;
			button3.Click += AddPresetTag_Click;
			// 
			// button4
			// 
			button4.BackColor = System.Drawing.Color.Black;
			button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			button4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
			button4.ForeColor = System.Drawing.Color.White;
			button4.Location = new System.Drawing.Point(136, 276);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(50, 25);
			button4.TabIndex = 24;
			button4.Text = "#Rare";
			button4.UseVisualStyleBackColor = false;
			button4.Click += AddPresetTag_Click;
			// 
			// button5
			// 
			button5.BackColor = System.Drawing.Color.Black;
			button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			button5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
			button5.ForeColor = System.Drawing.Color.White;
			button5.Location = new System.Drawing.Point(192, 276);
			button5.Name = "button5";
			button5.Size = new System.Drawing.Size(84, 25);
			button5.TabIndex = 25;
			button5.Text = "#FollowUp";
			button5.UseVisualStyleBackColor = false;
			button5.Click += AddPresetTag_Click;
			// 
			// btnEditListBodyPart
			// 
			btnEditListBodyPart.BackColor = System.Drawing.Color.Black;
			btnEditListBodyPart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnEditListBodyPart.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			btnEditListBodyPart.ForeColor = System.Drawing.Color.White;
			btnEditListBodyPart.Image = (System.Drawing.Image)resources.GetObject("btnEditListBodyPart.Image");
			btnEditListBodyPart.Location = new System.Drawing.Point(90, 54);
			btnEditListBodyPart.Name = "btnEditListBodyPart";
			btnEditListBodyPart.Size = new System.Drawing.Size(22, 22);
			btnEditListBodyPart.TabIndex = 26;
			btnEditListBodyPart.TabStop = false;
			btnEditListBodyPart.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			btnEditListBodyPart.UseVisualStyleBackColor = false;
			btnEditListBodyPart.Click += btnEditListBodyPart_Click;
			// 
			// gvCaseList
			// 
			gvCaseList.AllowUserToAddRows = false;
			gvCaseList.AllowUserToDeleteRows = false;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			gvCaseList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
			gvCaseList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			gvCaseList.BackgroundColor = System.Drawing.Color.Gray;
			gvCaseList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			gvCaseList.GridColor = System.Drawing.Color.White;
			gvCaseList.Location = new System.Drawing.Point(341, 278);
			gvCaseList.MultiSelect = false;
			gvCaseList.Name = "gvCaseList";
			gvCaseList.ReadOnly = true;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			gvCaseList.RowsDefaultCellStyle = dataGridViewCellStyle4;
			gvCaseList.RowTemplate.Height = 25;
			gvCaseList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			gvCaseList.Size = new System.Drawing.Size(76, 54);
			gvCaseList.TabIndex = 27;
			gvCaseList.Visible = false;
			// 
			// btnOpenAccessionInEI
			// 
			btnOpenAccessionInEI.BackgroundImage = Properties.Resources.agfa_24;
			btnOpenAccessionInEI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			btnOpenAccessionInEI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			btnOpenAccessionInEI.ForeColor = System.Drawing.Color.Black;
			btnOpenAccessionInEI.Location = new System.Drawing.Point(428, 96);
			btnOpenAccessionInEI.Name = "btnOpenAccessionInEI";
			btnOpenAccessionInEI.Size = new System.Drawing.Size(30, 30);
			btnOpenAccessionInEI.TabIndex = 28;
			btnOpenAccessionInEI.Text = "->";
			btnOpenAccessionInEI.UseVisualStyleBackColor = false;
			btnOpenAccessionInEI.Click += btnOpenAccessionInEI_Click;
			// 
			// CaseLogForm2
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Black;
			ClientSize = new System.Drawing.Size(494, 537);
			Controls.Add(btnOpenAccessionInEI);
			Controls.Add(gvCaseList);
			Controls.Add(btnEditListBodyPart);
			Controls.Add(button5);
			Controls.Add(button4);
			Controls.Add(button3);
			Controls.Add(btnViewSpecialtyCases);
			Controls.Add(btnEditListSpeciality);
			Controls.Add(txtTags);
			Controls.Add(btnView);
			Controls.Add(btnSave);
			Controls.Add(chkXLSX);
			Controls.Add(chkPPTX);
			Controls.Add(listView1);
			Controls.Add(btnCaptureScreen);
			Controls.Add(btnAddScreenshot);
			Controls.Add(label5);
			Controls.Add(txtNotes);
			Controls.Add(label4);
			Controls.Add(chkKeepBodyPart);
			Controls.Add(txtPTID);
			Controls.Add(cboPIDType);
			Controls.Add(label3);
			Controls.Add(cboBodyPart);
			Controls.Add(cboSpecialty);
			Controls.Add(label2);
			Controls.Add(label1);
			ForeColor = System.Drawing.SystemColors.Control;
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			MaximizeBox = false;
			Name = "CaseLogForm2";
			StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "MyCaseLog";
			FormClosing += CaseLogForm2_FormClosing;
			Load += CaseLogForm2_Load;
			((System.ComponentModel.ISupportInitialize)gvCaseList).EndInit();
			ResumeLayout(false);
			PerformLayout();
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
		private System.Windows.Forms.Button btnView;
		private System.Windows.Forms.TextBox txtTags;
		private System.Windows.Forms.Button btnEditListSpeciality;
		private System.Windows.Forms.Button btnViewSpecialtyCases;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button btnEditListBodyPart;
		private System.Windows.Forms.DataGridView gvCaseList;
		private System.Windows.Forms.Button btnOpenAccessionInEI;
	}
}