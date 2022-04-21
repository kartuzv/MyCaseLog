using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCaseLog
{
	public partial class FrmListEditor : Form
	{

		public List<string> ItemList;
		public FrmListEditor(string[] lst, string listName)
		{
			
			InitializeComponent();

			ItemList = lst.ToList();
			gvList.Columns[0].HeaderText = listName;
			foreach (string s in ItemList)
			{
				gvList.Rows.Add(s);

			}
				
		}


		private void AddItemToList()
		{
			gvList.Rows.Add();
			int RowIndex = gvList.RowCount - 1;
			DataGridViewRow R = gvList.Rows[RowIndex];
			//R.Cells["YourName"].Value = tbName.Text;
			//R.Cells["Age"].Value = cbAges.Text;
		}

		private void gvList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.ColumnIndex == 1)
			{
				string itemText = gvList.Rows[e.RowIndex].Cells[0].Value.ToString(); 
				DialogResult result = MessageBox.Show("\r\n " + itemText + "\r\n ","DELETE From List?",MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (result == DialogResult.Yes)
				{
					gvList.Rows.RemoveAt(e.RowIndex);
				}
			}
		}

		private void btnSaveList_Click(object sender, EventArgs e)
		{
			ItemList.Clear();
			foreach (DataGridViewRow r in gvList.Rows)
			{
				ItemList.Add(r.Cells[0].Value.ToString().Trim());
			}

			this.DialogResult = DialogResult.OK;
			this.Hide();

		}

		private void btnAddItemToList_Click(object sender, EventArgs e)
		{
			if (txtNewItem.Text.Length > 2)
			{
				gvList.Rows.Add(txtNewItem.Text.Trim());
				txtNewItem.Text = "";
			}
				
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
	}
}
