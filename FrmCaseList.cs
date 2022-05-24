using MyCaseLog.Properties;
using System;
using System.Data;
using System.Windows.Forms;

namespace MyCaseLog
{
	public partial class FrmCaseList : Form
	{
		private DataTable _dtList;
		private DataView dv;
		private string pathToPPT = "";
		public FrmCaseList(string[] specialtyList, DataTable dt, string pathToPPTexe ="")
		{
			InitializeComponent();

			pathToPPT = pathToPPTexe;
			_dtList = dt;
			dv = new DataView(_dtList);
			gvList.DataSource = dv;

			cboSpecialty.Items.Add("");
			cboSpecialty.Items.AddRange(specialtyList);
			txtSearch.Focus();
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			string specialityFilter = "";
			if (cboSpecialty.SelectedItem != null)
				specialityFilter = $"Speciality='{cboSpecialty.SelectedItem.ToString()}' ";

			string term = txtSearch.Text.Trim();
			string searchTermFilter = term==""?"": $"(BodyPart LIKE '%{term}%' OR Tags LIKE '%{term}%' OR Notes LIKE '%{term}%' OR LogID LIKE '%{term}%' OR Accession LIKE '%{term}%' OR MRN LIKE '%{term}%')";

			if (specialityFilter != "" && searchTermFilter != "")
				specialityFilter += " AND";

			dv.RowFilter = $"{specialityFilter} {searchTermFilter}";

		}

		private void cboSpecialty_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtSearch_TextChanged(null, null);
		}

		private void gvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (pathToPPT != "")
			{
				var c = gvList.SelectedCells[0];
				string pptFileName = gvList.Rows[c.RowIndex].Cells[7].FormattedValue.ToString();
				string fullPathToPPTFile = Settings.Default.LogDir + pptFileName;
				string cmdLine = "\"" + pathToPPT + "\" /O \"" + fullPathToPPTFile + "\"";

				System.Diagnostics.Process.Start(cmdLine);
			}
					
			
		}
	}
}
