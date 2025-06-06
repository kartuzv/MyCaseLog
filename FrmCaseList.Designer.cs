
namespace MyCaseLog
{
	partial class FrmCaseList
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			gvList = new System.Windows.Forms.DataGridView();
			txtSearch = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			cboSpecialty = new System.Windows.Forms.ComboBox();
			label2 = new System.Windows.Forms.Label();
			btnAddNewCase = new System.Windows.Forms.Button();
			menuStrip1 = new System.Windows.Forms.MenuStrip();
			viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			viewSAVEDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			viewExcelLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			bodyPartListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			specialityListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)gvList).BeginInit();
			menuStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// gvList
			// 
			gvList.AllowUserToAddRows = false;
			gvList.AllowUserToDeleteRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			gvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			gvList.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			gvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			gvList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
			gvList.BackgroundColor = System.Drawing.Color.Gray;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			gvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			gvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			gvList.DefaultCellStyle = dataGridViewCellStyle3;
			gvList.GridColor = System.Drawing.Color.White;
			gvList.Location = new System.Drawing.Point(0, 67);
			gvList.MultiSelect = false;
			gvList.Name = "gvList";
			gvList.ReadOnly = true;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			gvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			gvList.RowTemplate.Height = 25;
			gvList.RowTemplate.ReadOnly = true;
			gvList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			gvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			gvList.ShowEditingIcon = false;
			gvList.Size = new System.Drawing.Size(1345, 463);
			gvList.TabIndex = 0;
			gvList.CellClick += gvList_CellClick;
			gvList.CellDoubleClick += gvList_CellDoubleClick;
			// 
			// txtSearch
			// 
			txtSearch.BackColor = System.Drawing.Color.Black;
			txtSearch.ForeColor = System.Drawing.Color.White;
			txtSearch.Location = new System.Drawing.Point(443, 38);
			txtSearch.Name = "txtSearch";
			txtSearch.Size = new System.Drawing.Size(358, 23);
			txtSearch.TabIndex = 1;
			txtSearch.TextChanged += txtSearch_TextChanged;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.ForeColor = System.Drawing.Color.White;
			label1.Location = new System.Drawing.Point(392, 38);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(45, 15);
			label1.TabIndex = 2;
			label1.Text = "Search:";
			// 
			// cboSpecialty
			// 
			cboSpecialty.BackColor = System.Drawing.Color.Black;
			cboSpecialty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cboSpecialty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			cboSpecialty.ForeColor = System.Drawing.Color.White;
			cboSpecialty.FormattingEnabled = true;
			cboSpecialty.Location = new System.Drawing.Point(62, 35);
			cboSpecialty.Name = "cboSpecialty";
			cboSpecialty.Size = new System.Drawing.Size(299, 23);
			cboSpecialty.TabIndex = 3;
			cboSpecialty.SelectedIndexChanged += cboSpecialty_SelectedIndexChanged;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(0, 41);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(60, 15);
			label2.TabIndex = 4;
			label2.Text = "Speciality:";
			// 
			// btnAddNewCase
			// 
			btnAddNewCase.Location = new System.Drawing.Point(1251, 37);
			btnAddNewCase.Name = "btnAddNewCase";
			btnAddNewCase.Size = new System.Drawing.Size(84, 23);
			btnAddNewCase.TabIndex = 5;
			btnAddNewCase.Text = "Add New Case";
			btnAddNewCase.UseVisualStyleBackColor = true;
			btnAddNewCase.Click += btnAddNewCase_Click;
			// 
			// menuStrip1
			// 
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { viewToolStripMenuItem, settingsToolStripMenuItem });
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new System.Drawing.Size(1347, 24);
			menuStrip1.TabIndex = 6;
			menuStrip1.Text = "menuStrip1";
			// 
			// viewToolStripMenuItem
			// 
			viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { viewSAVEDToolStripMenuItem, viewExcelLogToolStripMenuItem });
			viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			viewToolStripMenuItem.Text = "View";
			// 
			// viewSAVEDToolStripMenuItem
			// 
			viewSAVEDToolStripMenuItem.Name = "viewSAVEDToolStripMenuItem";
			viewSAVEDToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			viewSAVEDToolStripMenuItem.Text = "View SAVED Folder";
			viewSAVEDToolStripMenuItem.Click += viewSAVEDToolStripMenuItem_Click;
			// 
			// viewExcelLogToolStripMenuItem
			// 
			viewExcelLogToolStripMenuItem.Name = "viewExcelLogToolStripMenuItem";
			viewExcelLogToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			viewExcelLogToolStripMenuItem.Text = "View Excel Log";
			viewExcelLogToolStripMenuItem.Click += viewExcelLogToolStripMenuItem_Click;
			// 
			// settingsToolStripMenuItem
			// 
			settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { bodyPartListToolStripMenuItem, specialityListToolStripMenuItem });
			settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			settingsToolStripMenuItem.Text = "Settings";
			// 
			// bodyPartListToolStripMenuItem
			// 
			bodyPartListToolStripMenuItem.Name = "bodyPartListToolStripMenuItem";
			bodyPartListToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			bodyPartListToolStripMenuItem.Text = "Body Part List";
			bodyPartListToolStripMenuItem.Click += bodyPartListToolStripMenuItem_Click;
			// 
			// specialityListToolStripMenuItem
			// 
			specialityListToolStripMenuItem.Name = "specialityListToolStripMenuItem";
			specialityListToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			specialityListToolStripMenuItem.Text = "Speciality List";
			specialityListToolStripMenuItem.Click += specialityListToolStripMenuItem_Click;
			// 
			// FrmCaseList
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Black;
			ClientSize = new System.Drawing.Size(1347, 542);
			Controls.Add(btnAddNewCase);
			Controls.Add(label2);
			Controls.Add(cboSpecialty);
			Controls.Add(label1);
			Controls.Add(txtSearch);
			Controls.Add(gvList);
			Controls.Add(menuStrip1);
			MainMenuStrip = menuStrip1;
			Name = "FrmCaseList";
			StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "MyCaseLog";
			FormClosing += FrmCaseList_FormClosing;
			Load += FrmCaseList_Load;
			((System.ComponentModel.ISupportInitialize)gvList).EndInit();
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.DataGridView gvList;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboSpecialty;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnAddNewCase;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bodyPartListToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem specialityListToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewSAVEDToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewExcelLogToolStripMenuItem;
	}
}