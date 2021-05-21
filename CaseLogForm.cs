using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MyCaseLog
{
	public partial class CaseLogForm : Form
	{
		public CaseLogForm()
		{
			InitializeComponent();
		}

		CaseLogEntryForm lgFrm;
		DXViewerForm fakeViewer;
		private void btnSaveToLog_Click(object sender, EventArgs e)
		{
			lgFrm.HOSP = cboHosp.SelectedItem.ToString();
			lgFrm.ViewerWindowTitle = txtViewerTitle.Text;
			lgFrm.ResetInputFields();
			lgFrm.Show();
			
		}

		private void CaseLogForm_Load(object sender, EventArgs e)
		{

		}

		private void CaseLogForm_Shown(object sender, EventArgs e)
		{
			lgFrm = new CaseLogEntryForm();
			lgFrm.Owner = this;
			lgFrm.Visible = false;

			fakeViewer = new DXViewerForm();
			fakeViewer.Owner = this;
			fakeViewer.Show();

			cboHosp.SelectedIndex = 0;
		}

		private void btnOpenLogDir_Click(object sender, EventArgs e)
		{ 
			Process.Start("explorer.exe", Properties.Settings.Default.LogDir); //+ Properties.Settings.Default.LogDir); 
		}

		private void label2_MouseClick(object sender, MouseEventArgs e)
		{
			
		}
	}
}
