
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.gvList = new System.Windows.Forms.DataGridView();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cboSpecialty = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			this.SuspendLayout();
			// 
			// gvList
			// 
			this.gvList.AllowUserToAddRows = false;
			this.gvList.AllowUserToDeleteRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.gvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.gvList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
			this.gvList.BackgroundColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.gvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gvList.DefaultCellStyle = dataGridViewCellStyle3;
			this.gvList.GridColor = System.Drawing.Color.White;
			this.gvList.Location = new System.Drawing.Point(0, 39);
			this.gvList.MultiSelect = false;
			this.gvList.Name = "gvList";
			this.gvList.ReadOnly = true;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.gvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.gvList.RowTemplate.Height = 25;
			this.gvList.RowTemplate.ReadOnly = true;
			this.gvList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.gvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.gvList.ShowEditingIcon = false;
			this.gvList.Size = new System.Drawing.Size(1002, 490);
			this.gvList.TabIndex = 0;
			this.gvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvList_CellDoubleClick);
			// 
			// bindingSource1
			// 
			this.bindingSource1.AllowNew = false;
			// 
			// txtSearch
			// 
			this.txtSearch.BackColor = System.Drawing.Color.Black;
			this.txtSearch.ForeColor = System.Drawing.Color.White;
			this.txtSearch.Location = new System.Drawing.Point(444, 7);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(358, 23);
			this.txtSearch.TabIndex = 1;
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(393, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "Search:";
			// 
			// cboSpecialty
			// 
			this.cboSpecialty.BackColor = System.Drawing.Color.Black;
			this.cboSpecialty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSpecialty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cboSpecialty.ForeColor = System.Drawing.Color.White;
			this.cboSpecialty.FormattingEnabled = true;
			this.cboSpecialty.Location = new System.Drawing.Point(63, 7);
			this.cboSpecialty.Name = "cboSpecialty";
			this.cboSpecialty.Size = new System.Drawing.Size(299, 23);
			this.cboSpecialty.TabIndex = 3;
			this.cboSpecialty.SelectedIndexChanged += new System.EventHandler(this.cboSpecialty_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(0, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 15);
			this.label2.TabIndex = 4;
			this.label2.Text = "Speciality:";
			// 
			// FrmCaseList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1004, 541);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cboSpecialty);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtSearch);
			this.Controls.Add(this.gvList);
			this.Name = "FrmCaseList";
			this.Text = "FrmCaseList";
			((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView gvList;
		private System.Windows.Forms.BindingSource bindingSource1;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboSpecialty;
		private System.Windows.Forms.Label label2;
	}
}