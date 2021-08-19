using MyCaseLog.Properties;
using System;
using System.Diagnostics;
using System.IO;
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
		//DXViewerForm fakeViewer;
		private void btnSaveToLog_Click(object sender, EventArgs e)
		{
			lgFrm.HOSP = cboHosp.SelectedItem.ToString();
			lgFrm.ViewerWindowTitle = txtViewerTitle.Text;
			lgFrm.ResetInputFields();
			lgFrm.Show();
			
		}

		private void CaseLogForm_Load(object sender, EventArgs e)
		{
			cboHosp.Items.Clear();
			string[] userSites = Settings.Default.Locations.Split(',', StringSplitOptions.RemoveEmptyEntries);
			if (userSites.Length > 0)
				cboHosp.Items.AddRange(userSites);
			else
				cboHosp.Items.Add("MyHospital");
		}

		private void CaseLogForm_Shown(object sender, EventArgs e)
		{
			lgFrm = new CaseLogEntryForm();
			lgFrm.Owner = this;
			lgFrm.Visible = false;

			
			if (!Directory.Exists(Settings.Default.LogDir))
				Directory.CreateDirectory(Settings.Default.LogDir);

			string testXlsFile = Settings.Default.LogDir + "\\MyCaseLog.xlsx";
			if (!File.Exists(testXlsFile))
			{
				File.WriteAllBytes(testXlsFile, Resources.MyCaseLogTemplate);
				
			}

			cboHosp.SelectedIndex = 0;

			//fakeViewer = new DXViewerForm();
			//fakeViewer.Owner = this;
			//fakeViewer.Show();
		}

		private void btnOpenLogDir_Click(object sender, EventArgs e)
		{ 
			Process.Start("explorer.exe", Properties.Settings.Default.LogDir); 
		}

		private void label2_MouseClick(object sender, MouseEventArgs e)
		{
			
		}
	}
}
