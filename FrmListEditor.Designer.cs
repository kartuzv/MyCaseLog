
namespace MyCaseLog
{
	partial class FrmListEditor
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
			this.gvList = new System.Windows.Forms.DataGridView();
			this.Listed = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DeleteCol = new System.Windows.Forms.DataGridViewButtonColumn();
			this.btnSaveList = new System.Windows.Forms.Button();
			this.txtNewItem = new System.Windows.Forms.TextBox();
			this.btnAddItemToList = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
			this.SuspendLayout();
			// 
			// gvList
			// 
			this.gvList.AllowUserToAddRows = false;
			this.gvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.gvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Listed,
            this.DeleteCol});
			this.gvList.Location = new System.Drawing.Point(12, 12);
			this.gvList.MultiSelect = false;
			this.gvList.Name = "gvList";
			this.gvList.RowTemplate.Height = 25;
			this.gvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.gvList.Size = new System.Drawing.Size(496, 265);
			this.gvList.TabIndex = 22;
			this.gvList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvList_CellMouseClick);
			// 
			// Listed
			// 
			this.Listed.HeaderText = "ListedItem";
			this.Listed.Name = "Listed";
			// 
			// DeleteCol
			// 
			this.DeleteCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Red;
			this.DeleteCol.DefaultCellStyle = dataGridViewCellStyle1;
			this.DeleteCol.FillWeight = 40F;
			this.DeleteCol.HeaderText = "X";
			this.DeleteCol.Name = "DeleteCol";
			this.DeleteCol.ReadOnly = true;
			this.DeleteCol.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.DeleteCol.Text = "X";
			this.DeleteCol.UseColumnTextForButtonValue = true;
			this.DeleteCol.Width = 30;
			// 
			// btnSaveList
			// 
			this.btnSaveList.Location = new System.Drawing.Point(412, 311);
			this.btnSaveList.Name = "btnSaveList";
			this.btnSaveList.Size = new System.Drawing.Size(96, 33);
			this.btnSaveList.TabIndex = 23;
			this.btnSaveList.Text = "Save Changes";
			this.btnSaveList.UseVisualStyleBackColor = true;
			this.btnSaveList.Click += new System.EventHandler(this.btnSaveList_Click);
			// 
			// txtNewItem
			// 
			this.txtNewItem.Location = new System.Drawing.Point(12, 283);
			this.txtNewItem.Name = "txtNewItem";
			this.txtNewItem.Size = new System.Drawing.Size(365, 23);
			this.txtNewItem.TabIndex = 24;
			// 
			// btnAddItemToList
			// 
			this.btnAddItemToList.Location = new System.Drawing.Point(383, 282);
			this.btnAddItemToList.Name = "btnAddItemToList";
			this.btnAddItemToList.Size = new System.Drawing.Size(85, 23);
			this.btnAddItemToList.TabIndex = 25;
			this.btnAddItemToList.Text = "+ Add New";
			this.btnAddItemToList.UseVisualStyleBackColor = true;
			this.btnAddItemToList.Click += new System.EventHandler(this.btnAddItemToList_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(12, 312);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 33);
			this.btnCancel.TabIndex = 26;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// FrmListEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(520, 343);
			this.ControlBox = false;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnAddItemToList);
			this.Controls.Add(this.txtNewItem);
			this.Controls.Add(this.btnSaveList);
			this.Controls.Add(this.gvList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FrmListEditor";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "List Editor";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView gvList;
		private System.Windows.Forms.Button btnSaveList;
		private System.Windows.Forms.TextBox txtNewItem;
		private System.Windows.Forms.Button btnAddItemToList;
		private System.Windows.Forms.DataGridViewTextBoxColumn Listed;
		private System.Windows.Forms.DataGridViewButtonColumn DeleteCol;
		private System.Windows.Forms.Button btnCancel;
	}
}