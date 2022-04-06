
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cboSpecialty = new System.Windows.Forms.ComboBox();
			this.cboBodyPart = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cboPIDType = new System.Windows.Forms.ComboBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.chkKeepSpecialty = new System.Windows.Forms.CheckBox();
			this.chkKeepBodyPart = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtNotes = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.cboTags = new System.Windows.Forms.ComboBox();
			this.btnAddScreenshot = new System.Windows.Forms.Button();
			this.btnCaptureScreen = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.chkPPTX = new System.Windows.Forms.CheckBox();
			this.chkXLSX = new System.Windows.Forms.CheckBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Sub-Specialty:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Body Part:";
			// 
			// cboSpecialty
			// 
			this.cboSpecialty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSpecialty.FormattingEnabled = true;
			this.cboSpecialty.Items.AddRange(new object[] {
            "Head",
            "Neck",
            "Chest",
            "Abdomen"});
			this.cboSpecialty.Location = new System.Drawing.Point(99, 12);
			this.cboSpecialty.Name = "cboSpecialty";
			this.cboSpecialty.Size = new System.Drawing.Size(299, 23);
			this.cboSpecialty.TabIndex = 2;
			// 
			// cboBodyPart
			// 
			this.cboBodyPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboBodyPart.FormattingEnabled = true;
			this.cboBodyPart.Items.AddRange(new object[] {
            "Sinus",
            "Abdoment/Pelvis"});
			this.cboBodyPart.Location = new System.Drawing.Point(99, 53);
			this.cboBodyPart.Name = "cboBodyPart";
			this.cboBodyPart.Size = new System.Drawing.Size(299, 23);
			this.cboBodyPart.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 15);
			this.label3.TabIndex = 4;
			this.label3.Text = "Patient ID:";
			// 
			// cboPIDType
			// 
			this.cboPIDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPIDType.FormattingEnabled = true;
			this.cboPIDType.Items.AddRange(new object[] {
            "MRN",
            "Accession"});
			this.cboPIDType.Location = new System.Drawing.Point(99, 101);
			this.cboPIDType.Name = "cboPIDType";
			this.cboPIDType.Size = new System.Drawing.Size(129, 23);
			this.cboPIDType.TabIndex = 5;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(243, 101);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(155, 23);
			this.textBox1.TabIndex = 6;
			// 
			// chkKeepSpecialty
			// 
			this.chkKeepSpecialty.AutoSize = true;
			this.chkKeepSpecialty.Location = new System.Drawing.Point(416, 15);
			this.chkKeepSpecialty.Name = "chkKeepSpecialty";
			this.chkKeepSpecialty.Size = new System.Drawing.Size(64, 19);
			this.chkKeepSpecialty.TabIndex = 7;
			this.chkKeepSpecialty.Text = "Default";
			this.chkKeepSpecialty.UseVisualStyleBackColor = true;
			// 
			// chkKeepBodyPart
			// 
			this.chkKeepBodyPart.AutoSize = true;
			this.chkKeepBodyPart.Location = new System.Drawing.Point(416, 57);
			this.chkKeepBodyPart.Name = "chkKeepBodyPart";
			this.chkKeepBodyPart.Size = new System.Drawing.Size(64, 19);
			this.chkKeepBodyPart.TabIndex = 8;
			this.chkKeepBodyPart.Text = "Default";
			this.chkKeepBodyPart.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 146);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 15);
			this.label4.TabIndex = 9;
			this.label4.Text = "Notes:";
			// 
			// txtNotes
			// 
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
			this.label5.Location = new System.Drawing.Point(11, 255);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(34, 15);
			this.label5.TabIndex = 11;
			this.label5.Text = "Tags:";
			// 
			// cboTags
			// 
			this.cboTags.AutoCompleteCustomSource.AddRange(new string[] {
            "Teaching",
            "Rare",
            "followup",
            "spleen"});
			this.cboTags.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cboTags.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			this.cboTags.FormattingEnabled = true;
			this.cboTags.Location = new System.Drawing.Point(51, 252);
			this.cboTags.Name = "cboTags";
			this.cboTags.Size = new System.Drawing.Size(429, 23);
			this.cboTags.TabIndex = 12;
			// 
			// btnAddScreenshot
			// 
			this.btnAddScreenshot.Location = new System.Drawing.Point(51, 281);
			this.btnAddScreenshot.Name = "btnAddScreenshot";
			this.btnAddScreenshot.Size = new System.Drawing.Size(105, 38);
			this.btnAddScreenshot.TabIndex = 13;
			this.btnAddScreenshot.Text = "Add Screenshot";
			this.btnAddScreenshot.UseVisualStyleBackColor = true;
			this.btnAddScreenshot.Click += new System.EventHandler(this.btnAddScreenshot_Click);
			// 
			// btnCaptureScreen
			// 
			this.btnCaptureScreen.Location = new System.Drawing.Point(375, 281);
			this.btnCaptureScreen.Name = "btnCaptureScreen";
			this.btnCaptureScreen.Size = new System.Drawing.Size(105, 38);
			this.btnCaptureScreen.TabIndex = 14;
			this.btnCaptureScreen.Text = "Capture Screen";
			this.btnCaptureScreen.UseVisualStyleBackColor = true;
			// 
			// listView1
			// 
			this.listView1.Activation = System.Windows.Forms.ItemActivation.TwoClick;
			this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.Left;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listView1.HideSelection = false;
			this.listView1.LargeImageList = this.imageList1;
			this.listView1.Location = new System.Drawing.Point(12, 325);
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
			this.chkPPTX.Location = new System.Drawing.Point(11, 440);
			this.chkPPTX.Name = "chkPPTX";
			this.chkPPTX.Size = new System.Drawing.Size(54, 19);
			this.chkPPTX.TabIndex = 16;
			this.chkPPTX.Text = "PPTX";
			this.chkPPTX.UseVisualStyleBackColor = true;
			// 
			// chkXLSX
			// 
			this.chkXLSX.AutoSize = true;
			this.chkXLSX.Location = new System.Drawing.Point(11, 466);
			this.chkXLSX.Name = "chkXLSX";
			this.chkXLSX.Size = new System.Drawing.Size(52, 19);
			this.chkXLSX.TabIndex = 17;
			this.chkXLSX.Text = "XLSX";
			this.chkXLSX.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(319, 440);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(171, 45);
			this.btnSave.TabIndex = 18;
			this.btnSave.Text = "Save Case";
			this.btnSave.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(71, 440);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 19;
			this.button1.Text = "Browse...";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// CaseLogForm2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(499, 495);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.chkXLSX);
			this.Controls.Add(this.chkPPTX);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.btnCaptureScreen);
			this.Controls.Add(this.btnAddScreenshot);
			this.Controls.Add(this.cboTags);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtNotes);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.chkKeepBodyPart);
			this.Controls.Add(this.chkKeepSpecialty);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.cboPIDType);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cboBodyPart);
			this.Controls.Add(this.cboSpecialty);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "CaseLogForm2";
			this.Text = "CaseLogForm2";
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
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.CheckBox chkKeepSpecialty;
		private System.Windows.Forms.CheckBox chkKeepBodyPart;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtNotes;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cboTags;
		private System.Windows.Forms.Button btnAddScreenshot;
		private System.Windows.Forms.Button btnCaptureScreen;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.CheckBox chkPPTX;
		private System.Windows.Forms.CheckBox chkXLSX;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button button1;
	}
}