
namespace MyCaseLog
{
	partial class CaseLogEntryForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaseLogEntryForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkNM = new System.Windows.Forms.CheckBox();
			this.chkMammo = new System.Windows.Forms.CheckBox();
			this.chkMRI = new System.Windows.Forms.CheckBox();
			this.chkXRay = new System.Windows.Forms.CheckBox();
			this.chkCT = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtProtocol = new System.Windows.Forms.TextBox();
			this.chkSpine = new System.Windows.Forms.CheckBox();
			this.chkBreast = new System.Windows.Forms.CheckBox();
			this.chkNeck = new System.Windows.Forms.CheckBox();
			this.chkAbdomen = new System.Windows.Forms.CheckBox();
			this.chkChest = new System.Windows.Forms.CheckBox();
			this.chkLeg = new System.Windows.Forms.CheckBox();
			this.chkArm = new System.Windows.Forms.CheckBox();
			this.chkHead = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.chkAnon = new System.Windows.Forms.CheckBox();
			this.chkTrans = new System.Windows.Forms.CheckBox();
			this.txtMRN = new System.Windows.Forms.TextBox();
			this.txtACC = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtAGE = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chkFemale = new System.Windows.Forms.CheckBox();
			this.chkMale = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.rtxtDescr = new System.Windows.Forms.RichTextBox();
			this.chkIsConf = new System.Windows.Forms.CheckBox();
			this.chkIsPub = new System.Windows.Forms.CheckBox();
			this.btnSaveToLog = new System.Windows.Forms.Button();
			this.txtBill = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.chkSocConf = new System.Windows.Forms.CheckBox();
			this.chkSnap = new System.Windows.Forms.CheckBox();
			this.btnAddScreenshot = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.txtDx = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkNM);
			this.groupBox1.Controls.Add(this.chkMammo);
			this.groupBox1.Controls.Add(this.chkMRI);
			this.groupBox1.Controls.Add(this.chkXRay);
			this.groupBox1.Controls.Add(this.chkCT);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(295, 66);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Modality";
			// 
			// chkNM
			// 
			this.chkNM.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkNM.AutoSize = true;
			this.chkNM.Location = new System.Drawing.Point(248, 22);
			this.chkNM.Name = "chkNM";
			this.chkNM.Size = new System.Drawing.Size(37, 25);
			this.chkNM.TabIndex = 5;
			this.chkNM.Text = "NM";
			this.chkNM.UseVisualStyleBackColor = true;
			// 
			// chkMammo
			// 
			this.chkMammo.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkMammo.AutoSize = true;
			this.chkMammo.Location = new System.Drawing.Point(193, 22);
			this.chkMammo.Name = "chkMammo";
			this.chkMammo.Size = new System.Drawing.Size(36, 25);
			this.chkMammo.TabIndex = 4;
			this.chkMammo.Text = "MA";
			this.chkMammo.UseVisualStyleBackColor = true;
			// 
			// chkMRI
			// 
			this.chkMRI.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkMRI.AutoSize = true;
			this.chkMRI.Location = new System.Drawing.Point(129, 22);
			this.chkMRI.Name = "chkMRI";
			this.chkMRI.Size = new System.Drawing.Size(35, 25);
			this.chkMRI.TabIndex = 3;
			this.chkMRI.Text = "MR";
			this.chkMRI.UseVisualStyleBackColor = true;
			// 
			// chkXRay
			// 
			this.chkXRay.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkXRay.AutoSize = true;
			this.chkXRay.Location = new System.Drawing.Point(75, 22);
			this.chkXRay.Name = "chkXRay";
			this.chkXRay.Size = new System.Drawing.Size(31, 25);
			this.chkXRay.TabIndex = 2;
			this.chkXRay.Text = "XR";
			this.chkXRay.UseVisualStyleBackColor = true;
			// 
			// chkCT
			// 
			this.chkCT.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkCT.AutoSize = true;
			this.chkCT.Location = new System.Drawing.Point(25, 22);
			this.chkCT.Name = "chkCT";
			this.chkCT.Size = new System.Drawing.Size(32, 25);
			this.chkCT.TabIndex = 1;
			this.chkCT.Text = "CT";
			this.chkCT.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.txtDx);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.txtProtocol);
			this.groupBox2.Controls.Add(this.chkSpine);
			this.groupBox2.Controls.Add(this.chkBreast);
			this.groupBox2.Controls.Add(this.chkNeck);
			this.groupBox2.Controls.Add(this.chkAbdomen);
			this.groupBox2.Controls.Add(this.chkChest);
			this.groupBox2.Controls.Add(this.chkLeg);
			this.groupBox2.Controls.Add(this.chkArm);
			this.groupBox2.Controls.Add(this.chkHead);
			this.groupBox2.Location = new System.Drawing.Point(12, 84);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(295, 159);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Study";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(2, 93);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(55, 15);
			this.label5.TabIndex = 16;
			this.label5.Text = "Protocol:";
			// 
			// txtProtocol
			// 
			this.txtProtocol.Location = new System.Drawing.Point(59, 90);
			this.txtProtocol.Name = "txtProtocol";
			this.txtProtocol.Size = new System.Drawing.Size(230, 23);
			this.txtProtocol.TabIndex = 17;
			this.txtProtocol.Text = "CT high-resolution";
			// 
			// chkSpine
			// 
			this.chkSpine.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkSpine.Location = new System.Drawing.Point(153, 22);
			this.chkSpine.Name = "chkSpine";
			this.chkSpine.Size = new System.Drawing.Size(76, 25);
			this.chkSpine.TabIndex = 9;
			this.chkSpine.Text = "Spine";
			this.chkSpine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkSpine.UseVisualStyleBackColor = true;
			// 
			// chkBreast
			// 
			this.chkBreast.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkBreast.Location = new System.Drawing.Point(82, 53);
			this.chkBreast.Name = "chkBreast";
			this.chkBreast.Size = new System.Drawing.Size(52, 25);
			this.chkBreast.TabIndex = 8;
			this.chkBreast.Text = "Breast";
			this.chkBreast.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkBreast.UseVisualStyleBackColor = true;
			// 
			// chkNeck
			// 
			this.chkNeck.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkNeck.Location = new System.Drawing.Point(82, 22);
			this.chkNeck.Name = "chkNeck";
			this.chkNeck.Size = new System.Drawing.Size(52, 25);
			this.chkNeck.TabIndex = 7;
			this.chkNeck.Text = "Neck";
			this.chkNeck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkNeck.UseVisualStyleBackColor = true;
			// 
			// chkAbdomen
			// 
			this.chkAbdomen.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkAbdomen.Location = new System.Drawing.Point(153, 53);
			this.chkAbdomen.Name = "chkAbdomen";
			this.chkAbdomen.Size = new System.Drawing.Size(76, 25);
			this.chkAbdomen.TabIndex = 6;
			this.chkAbdomen.Text = "Abdomen";
			this.chkAbdomen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkAbdomen.UseVisualStyleBackColor = true;
			// 
			// chkChest
			// 
			this.chkChest.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkChest.Location = new System.Drawing.Point(13, 53);
			this.chkChest.Name = "chkChest";
			this.chkChest.Size = new System.Drawing.Size(45, 25);
			this.chkChest.TabIndex = 5;
			this.chkChest.Text = "Chest";
			this.chkChest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkChest.UseVisualStyleBackColor = true;
			// 
			// chkLeg
			// 
			this.chkLeg.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkLeg.Location = new System.Drawing.Point(240, 53);
			this.chkLeg.Name = "chkLeg";
			this.chkLeg.Size = new System.Drawing.Size(45, 25);
			this.chkLeg.TabIndex = 4;
			this.chkLeg.Text = "Leg";
			this.chkLeg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkLeg.UseVisualStyleBackColor = true;
			// 
			// chkArm
			// 
			this.chkArm.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkArm.Location = new System.Drawing.Point(240, 22);
			this.chkArm.Name = "chkArm";
			this.chkArm.Size = new System.Drawing.Size(45, 25);
			this.chkArm.TabIndex = 3;
			this.chkArm.Text = "Arm";
			this.chkArm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkArm.UseVisualStyleBackColor = true;
			// 
			// chkHead
			// 
			this.chkHead.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkHead.Location = new System.Drawing.Point(12, 22);
			this.chkHead.Name = "chkHead";
			this.chkHead.Size = new System.Drawing.Size(45, 25);
			this.chkHead.TabIndex = 2;
			this.chkHead.Text = "Head";
			this.chkHead.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkHead.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.chkAnon);
			this.groupBox3.Controls.Add(this.chkTrans);
			this.groupBox3.Controls.Add(this.txtMRN);
			this.groupBox3.Controls.Add(this.txtACC);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.txtAGE);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.chkFemale);
			this.groupBox3.Controls.Add(this.chkMale);
			this.groupBox3.Location = new System.Drawing.Point(12, 249);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(295, 128);
			this.groupBox3.TabIndex = 7;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Patient";
			// 
			// chkAnon
			// 
			this.chkAnon.AutoSize = true;
			this.chkAnon.Location = new System.Drawing.Point(200, 0);
			this.chkAnon.Name = "chkAnon";
			this.chkAnon.Size = new System.Drawing.Size(90, 19);
			this.chkAnon.TabIndex = 16;
			this.chkAnon.Text = "Anonimized";
			this.chkAnon.UseVisualStyleBackColor = true;
			this.chkAnon.CheckedChanged += new System.EventHandler(this.chkAnon_CheckedChanged);
			// 
			// chkTrans
			// 
			this.chkTrans.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkTrans.AutoSize = true;
			this.chkTrans.Location = new System.Drawing.Point(194, 22);
			this.chkTrans.Name = "chkTrans";
			this.chkTrans.Size = new System.Drawing.Size(82, 25);
			this.chkTrans.TabIndex = 12;
			this.chkTrans.Text = "Transgender";
			this.chkTrans.UseVisualStyleBackColor = true;
			// 
			// txtMRN
			// 
			this.txtMRN.Location = new System.Drawing.Point(168, 58);
			this.txtMRN.Name = "txtMRN";
			this.txtMRN.Size = new System.Drawing.Size(115, 23);
			this.txtMRN.TabIndex = 11;
			this.txtMRN.Text = "555777";
			// 
			// txtACC
			// 
			this.txtACC.Location = new System.Drawing.Point(168, 91);
			this.txtACC.Name = "txtACC";
			this.txtACC.Size = new System.Drawing.Size(116, 23);
			this.txtACC.TabIndex = 10;
			this.txtACC.Text = "123456789";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(128, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 15);
			this.label3.TabIndex = 9;
			this.label3.Text = "MRN";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(102, 94);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 15);
			this.label2.TabIndex = 8;
			this.label2.Text = "Accession";
			// 
			// txtAGE
			// 
			this.txtAGE.Location = new System.Drawing.Point(51, 58);
			this.txtAGE.Name = "txtAGE";
			this.txtAGE.Size = new System.Drawing.Size(40, 23);
			this.txtAGE.TabIndex = 7;
			this.txtAGE.Text = "55";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 15);
			this.label1.TabIndex = 6;
			this.label1.Text = "AGE:";
			// 
			// chkFemale
			// 
			this.chkFemale.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkFemale.AutoSize = true;
			this.chkFemale.Location = new System.Drawing.Point(110, 22);
			this.chkFemale.Name = "chkFemale";
			this.chkFemale.Size = new System.Drawing.Size(55, 25);
			this.chkFemale.TabIndex = 5;
			this.chkFemale.Text = "Female";
			this.chkFemale.UseVisualStyleBackColor = true;
			// 
			// chkMale
			// 
			this.chkMale.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkMale.AutoSize = true;
			this.chkMale.Location = new System.Drawing.Point(18, 22);
			this.chkMale.Name = "chkMale";
			this.chkMale.Size = new System.Drawing.Size(43, 25);
			this.chkMale.TabIndex = 2;
			this.chkMale.Text = "Male";
			this.chkMale.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.rtxtDescr);
			this.groupBox4.Location = new System.Drawing.Point(12, 392);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(294, 105);
			this.groupBox4.TabIndex = 8;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Points of Interest";
			// 
			// rtxtDescr
			// 
			this.rtxtDescr.Location = new System.Drawing.Point(6, 22);
			this.rtxtDescr.Name = "rtxtDescr";
			this.rtxtDescr.Size = new System.Drawing.Size(277, 77);
			this.rtxtDescr.TabIndex = 0;
			this.rtxtDescr.Text = "";
			// 
			// chkIsConf
			// 
			this.chkIsConf.AutoSize = true;
			this.chkIsConf.Checked = true;
			this.chkIsConf.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkIsConf.Location = new System.Drawing.Point(18, 503);
			this.chkIsConf.Name = "chkIsConf";
			this.chkIsConf.Size = new System.Drawing.Size(118, 19);
			this.chkIsConf.TabIndex = 9;
			this.chkIsConf.Text = "Local Conference";
			this.chkIsConf.UseVisualStyleBackColor = true;
			// 
			// chkIsPub
			// 
			this.chkIsPub.AutoSize = true;
			this.chkIsPub.Location = new System.Drawing.Point(17, 553);
			this.chkIsPub.Name = "chkIsPub";
			this.chkIsPub.Size = new System.Drawing.Size(141, 19);
			this.chkIsPub.TabIndex = 10;
			this.chkIsPub.Text = "Manuscript Published";
			this.chkIsPub.UseVisualStyleBackColor = true;
			// 
			// btnSaveToLog
			// 
			this.btnSaveToLog.BackColor = System.Drawing.Color.SeaGreen;
			this.btnSaveToLog.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.btnSaveToLog.Location = new System.Drawing.Point(67, 578);
			this.btnSaveToLog.Name = "btnSaveToLog";
			this.btnSaveToLog.Size = new System.Drawing.Size(174, 48);
			this.btnSaveToLog.TabIndex = 11;
			this.btnSaveToLog.Text = "SAVE";
			this.btnSaveToLog.UseVisualStyleBackColor = false;
			this.btnSaveToLog.Click += new System.EventHandler(this.btnSaveToLog_Click);
			// 
			// txtBill
			// 
			this.txtBill.Location = new System.Drawing.Point(231, 503);
			this.txtBill.Name = "txtBill";
			this.txtBill.Size = new System.Drawing.Size(75, 23);
			this.txtBill.TabIndex = 12;
			this.txtBill.Text = "999.99";
			this.txtBill.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label4.Location = new System.Drawing.Point(203, 505);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(22, 21);
			this.label4.TabIndex = 12;
			this.label4.Text = "$:";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Black;
			this.pictureBox1.Location = new System.Drawing.Point(439, 428);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(219, 189);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 13;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Visible = false;
			// 
			// chkSocConf
			// 
			this.chkSocConf.AutoSize = true;
			this.chkSocConf.Checked = true;
			this.chkSocConf.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSocConf.Location = new System.Drawing.Point(18, 528);
			this.chkSocConf.Name = "chkSocConf";
			this.chkSocConf.Size = new System.Drawing.Size(128, 19);
			this.chkSocConf.TabIndex = 14;
			this.chkSocConf.Text = "Society Conference";
			this.chkSocConf.UseVisualStyleBackColor = true;
			// 
			// chkSnap
			// 
			this.chkSnap.AutoSize = true;
			this.chkSnap.Checked = true;
			this.chkSnap.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSnap.Location = new System.Drawing.Point(-14, 607);
			this.chkSnap.Name = "chkSnap";
			this.chkSnap.Size = new System.Drawing.Size(75, 19);
			this.chkSnap.TabIndex = 15;
			this.chkSnap.Text = "Snapshot";
			this.chkSnap.UseVisualStyleBackColor = true;
			this.chkSnap.Visible = false;
			this.chkSnap.CheckedChanged += new System.EventHandler(this.chkSnap_CheckedChanged);
			// 
			// btnAddScreenshot
			// 
			this.btnAddScreenshot.BackColor = System.Drawing.SystemColors.MenuHighlight;
			this.btnAddScreenshot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddScreenshot.BackgroundImage")));
			this.btnAddScreenshot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnAddScreenshot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAddScreenshot.Location = new System.Drawing.Point(253, 532);
			this.btnAddScreenshot.Name = "btnAddScreenshot";
			this.btnAddScreenshot.Size = new System.Drawing.Size(53, 50);
			this.btnAddScreenshot.TabIndex = 16;
			this.btnAddScreenshot.UseVisualStyleBackColor = false;
			this.btnAddScreenshot.Click += new System.EventHandler(this.btnAddScreenshot_Click);
			// 
			// listView1
			// 
			this.listView1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(328, 12);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(191, 614);
			this.listView1.SmallImageList = this.imageList1;
			this.listView1.TabIndex = 17;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.List;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(220, 220);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// txtDx
			// 
			this.txtDx.Location = new System.Drawing.Point(59, 125);
			this.txtDx.Name = "txtDx";
			this.txtDx.Size = new System.Drawing.Size(230, 23);
			this.txtDx.TabIndex = 18;
			this.txtDx.Text = "dx-info-goes-here";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(29, 128);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(23, 15);
			this.label6.TabIndex = 19;
			this.label6.Text = "Dx:";
			// 
			// CaseLogEntryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ClientSize = new System.Drawing.Size(529, 629);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.btnAddScreenshot);
			this.Controls.Add(this.chkSnap);
			this.Controls.Add(this.chkSocConf);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtBill);
			this.Controls.Add(this.btnSaveToLog);
			this.Controls.Add(this.chkIsPub);
			this.Controls.Add(this.chkIsConf);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.DoubleBuffered = true;
			this.Name = "CaseLogEntryForm";
			this.Text = "MyCaseLog Entry";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CaseLogEntryForm_FormClosing);
			this.VisibleChanged += new System.EventHandler(this.CaseLogEntryForm_VisibleChanged);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkMammo;
		private System.Windows.Forms.CheckBox chkMRI;
		private System.Windows.Forms.CheckBox chkXRay;
		private System.Windows.Forms.CheckBox chkCT;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chkAbdomen;
		private System.Windows.Forms.CheckBox chkChest;
		private System.Windows.Forms.CheckBox chkLeg;
		private System.Windows.Forms.CheckBox chkArm;
		private System.Windows.Forms.CheckBox chkHead;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox txtMRN;
		private System.Windows.Forms.TextBox txtACC;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtAGE;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkFemale;
		private System.Windows.Forms.CheckBox chkMale;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.RichTextBox rtxtDescr;
		private System.Windows.Forms.CheckBox chkIsConf;
		private System.Windows.Forms.CheckBox chkIsPub;
		private System.Windows.Forms.Button btnSaveToLog;
		private System.Windows.Forms.TextBox txtBill;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.CheckBox chkNM;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtProtocol;
		private System.Windows.Forms.CheckBox chkSpine;
		private System.Windows.Forms.CheckBox chkBreast;
		private System.Windows.Forms.CheckBox chkNeck;
		private System.Windows.Forms.CheckBox chkSocConf;
		private System.Windows.Forms.CheckBox chkTrans;
		private System.Windows.Forms.CheckBox chkSnap;
		private System.Windows.Forms.CheckBox chkAnon;
		private System.Windows.Forms.Button btnAddScreenshot;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtDx;
	}
}