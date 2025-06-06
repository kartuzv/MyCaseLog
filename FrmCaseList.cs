using MyCaseLog.Controllers;
using MyCaseLog.Properties;
using OfficeOpenXml;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyCaseLog
{
	public partial class FrmCaseList : Form
	{
		public string ExcelLogFileName = "MyCaseLog.xlsx";
		public string ExcelLogFilePath = "";

		public string[] ListSpecialty;
		public string[] ListBodypart;
		public string PathToPPTexe = "";
		public string PathToPPTTemplate = "";

		DataTable _dtCaseList = new DataTable();
		DataView dv;
		//int lastLogRowIDX = -1;
		//int listEditRowIndex = -1;
		CaseLogForm2 frmNewCase;

		public FrmCaseList()
		{   //string[] specialtyList, DataTable dt, string pathToPPTexe = ""
			InitializeComponent();
			this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, Screen.PrimaryScreen.WorkingArea.Height - this.Height);

			LoadAppConfig();
		}

		private void FrmCaseList_Load(object sender, EventArgs e)
		{

			frmNewCase = new CaseLogForm2(this);

			if (Settings.Default.UsrSpecialty != "")
			{
				cboSpecialty.SelectedItem = Settings.Default.UsrSpecialty;
				//chkKeepSpecialty.Checked = true;
			}

			//load list of cases from excel log
			_dtCaseList = AppController.ExcelToDataTable(ExcelLogFilePath);

			//0row header, + x rows, next row 
			//lastLogRowIDX = _dtCaseList.Rows.Count + 1;

			dv = new DataView(_dtCaseList);
			gvList.DataSource = dv;

			DataGridViewButtonColumn gvbtnFolder = new DataGridViewButtonColumn();
			gvbtnFolder.Name = "Folder";
			gvbtnFolder.HeaderText = "";
			gvbtnFolder.Text = "Folder";
			gvbtnFolder.UseColumnTextForButtonValue = true;

			gvList.Columns.Insert(8, gvbtnFolder);

			DataGridViewButtonColumn gvbtnEdit = new DataGridViewButtonColumn();
			gvbtnEdit.Name = "Edit";
			gvbtnEdit.HeaderText = "";
			gvbtnEdit.Text = "Edit";
			gvbtnEdit.UseColumnTextForButtonValue = true;

			gvList.Columns.Insert(9, gvbtnEdit);

			DataGridViewButtonColumn gvbtnCasePPTX = new DataGridViewButtonColumn();
			gvbtnCasePPTX.Name = "PPT";
			gvbtnCasePPTX.HeaderText = "";
			gvbtnCasePPTX.Text = "PPT";
			gvbtnCasePPTX.UseColumnTextForButtonValue = true;

			gvList.Columns.Insert(10, gvbtnCasePPTX);

			cboSpecialty.Items.Add("");
			cboSpecialty.Items.AddRange(ListSpecialty);
			txtSearch.Focus();
		}


		public void LoadAppConfig()
		{
			//load user configurable specialty
			ListSpecialty = Settings.Default.SpecialityList.Split('|', StringSplitOptions.RemoveEmptyEntries);

			if (ListSpecialty.Length > 0)
				Array.Sort(ListSpecialty, StringComparer.InvariantCulture);

			//load user configuarable bodypart
			ListBodypart = Settings.Default.BodyPartList.Split('|', StringSplitOptions.RemoveEmptyEntries);
			Array.Sort(ListBodypart, StringComparer.InvariantCulture);


			//ensure ppt template is ready
			//next to exe.
			string applicationDirectory = Path.GetDirectoryName(Application.ExecutablePath);
			string templateDir = applicationDirectory + "\\Template";
			if (!Directory.Exists(templateDir))
				Directory.CreateDirectory(templateDir);

			string xlsTemplatePath = templateDir + "\\MyCaseLog.xlsx";
			PathToPPTTemplate = templateDir + "\\PPTemplate2Pic.pptx";

			if (!File.Exists(xlsTemplatePath))
				File.WriteAllBytes(xlsTemplatePath, Resources.MyCaseLogTemplate);

			if (!File.Exists(PathToPPTTemplate))
				File.WriteAllBytes(PathToPPTTemplate, Resources.PPTemplate2Pic);

			if (string.IsNullOrEmpty(Settings.Default.LogDir))
			{
				Settings.Default.LogDir = applicationDirectory + "\\Saved\\";
				Settings.Default.Save();
			}

			ExcelLogFilePath = Settings.Default.LogDir + ExcelLogFileName;

			if (!Directory.Exists(Settings.Default.LogDir))
			{
				Directory.CreateDirectory(Settings.Default.LogDir);
				File.Copy(xlsTemplatePath, ExcelLogFilePath);
			}


			//find powerpoint installed 
			//so we can open it later, 12,14,15,16 365 /root/office16
			for (int i = 16; i > 11; i--)
			{
				//"c:\Program Files\Microsoft Office\root\Office16\powerpnt.exe" /O Filename1.pptx,
				string testPathOffice64bit = $"C:\\Program Files\\Microsoft Office\\Office{i}\\powerpnt.exe";

				if (File.Exists(testPathOffice64bit))
				{
					PathToPPTexe = testPathOffice64bit;
					break;
				}
				string testPathOffice32bit = $"C:\\Program Files (x86)\\Microsoft Office\\Office{i}\\powerpnt.exe";
				if (File.Exists(testPathOffice32bit))
				{
					PathToPPTexe = testPathOffice32bit;
					break;
				}
			}
			//attemt to check office365 pth
			if (PathToPPTexe == "" && File.Exists(@"c:\Program Files\Microsoft Office\root\Office16\powerpnt.exe"))
			{
				PathToPPTexe = @"c:\Program Files\Microsoft Office\root\Office16\powerpnt.exe";
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			string specialityFilter = "";
			if (cboSpecialty.SelectedItem != null && cboSpecialty.SelectedItem.ToString() != "")
				specialityFilter = $"Speciality='{cboSpecialty.SelectedItem.ToString()}' ";

			string term = txtSearch.Text.Trim();
			string searchTermFilter = term == "" ? "" : $"(BodyPart LIKE '%{term}%' OR Tags LIKE '%{term}%' OR Notes LIKE '%{term}%' OR LogID LIKE '%{term}%' OR Accession LIKE '%{term}%' OR MRN LIKE '%{term}%')";

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
			//if (PathToPPTexe != "")
			//{
			//	var c = gvList.SelectedCells[0];
			//	string pptFileName = gvList.Rows[c.RowIndex].Cells[7].FormattedValue.ToString();
			//	string fullPathToPPTFile = Settings.Default.LogDir + pptFileName;
			//	string cmdLine = "\"" + PathToPPTexe + "\" /O \"" + fullPathToPPTFile + "\"";

			//	System.Diagnostics.Process.Start(cmdLine);
			//}


		}


		private void btnAddNewCase_Click(object sender, EventArgs e)
		{
			frmNewCase.StartNewCase();

			frmNewCase.Show(this);
			this.Hide();
		}

		internal void SaveCaseLogEntryToLog(CaseLogEntry logEntry)
		{
			SaveEntryToExcel(logEntry);
			DataRow r;
			//update local list in memory
			var dv = _dtCaseList.DefaultView;
			if (logEntry.IDX == 0)
			{
				r = _dtCaseList.NewRow();
			}
			else
			{
				dv.RowFilter = $"LogID='{logEntry.LogTSID}'";
				r = dv[0].Row;
			}

			//set values
			r["LogID"] = logEntry.LogTSID;
			r["Speciality"] = logEntry.Specialty;
			r["BodyPart"] = logEntry.BodyPart;
			r["Accession"] = logEntry.PTIdType == "MRN" ? "" : logEntry.PTMRN;
			r["MRN"] = logEntry.PTIdType == "MRN" ? logEntry.PTMRN : "";
			r["Notes"] = logEntry.Notes;
			r["Tags"] = logEntry.Tags;
			r["PowerPointFile"] = $"MCL_{logEntry.LogTSID}.pptx";

			if (logEntry.IDX == 0)
				_dtCaseList.Rows.Add(r);

			_dtCaseList.AcceptChanges();

			this.Show();

			//generate PPT
			PowerPointController pptxCtrl = new PowerPointController(logEntry.LogStudyPath, PathToPPTTemplate);
			pptxCtrl.GenerateCasePowerPoint(logEntry);

			//	PowerPointController pptxCtrl = new PowerPointController(Settings.Default.LogDir, PathToPPTTemplate);
			//	pptxCtrl.AddMyCaseToCollection(logEntry);
		}

		public void SaveEntryToExcel(CaseLogEntry e)
		{
			//string pathToExcelLog = Settings.Default.LogDir + _excelLogFileName;

			ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
			ExcelPackage pck = new ExcelPackage(new FileInfo(ExcelLogFilePath));

			//const string invalidCharsRegex = @"[/\\*'?[\]:()]+";
			//const int maxLength = 31;//excel tabname char limit
			ExcelWorksheet ws = pck.Workbook.Worksheets[0];
			// find next
			int rowIDX = 2;//where are we saving this row:default header=1,case start =2
			int colIDX = 1;


			if (e.IDX == 0)
			{
				int nextBlankRow = -1;
				//find next empty row starting from 2
				while (nextBlankRow < 0)
				{
					if (ws.Cells[rowIDX, colIDX].Value == null)
						nextBlankRow = rowIDX;

					rowIDX++;
				}
				rowIDX = nextBlankRow;
			}
			else
			{
				//existing
				int caseRowIDX = -1;
				int allRowsInFile = _dtCaseList.Rows.Count + 2;//(1-Based row list + 1-header row)
															   //find next empty row starting from 2
				while ((caseRowIDX < 0) && (rowIDX < allRowsInFile))
				{
					if (ws.Cells[rowIDX, colIDX].Value != null && ws.Cells[rowIDX, colIDX].Value.ToString() == e.LogTSID)
						caseRowIDX = rowIDX;

					rowIDX++;
				}
				rowIDX = caseRowIDX;
			}

			ws.Cells[rowIDX, 1].Value = e.LogTSID;
			ws.Cells[rowIDX, 2].Value = e.Specialty;
			ws.Cells[rowIDX, 3].Value = e.BodyPart;

			ws.Cells[rowIDX, 4].Value = e.PTIdType == "MRN" ? "" : e.PTMRN;
			ws.Cells[rowIDX, 5].Value = e.PTIdType == "MRN" ? e.PTMRN : "";

			ws.Cells[rowIDX, 6].Value = e.Notes;
			ws.Cells[rowIDX, 7].Value = e.Tags;

			ws.Cells[rowIDX, 8].Formula = $"HYPERLINK(\"{e.LogTSID}\\MCL_{e.LogTSID}.pptx\", \"{e.LogTSID}\\MCL_{e.LogTSID}.pptx\")";

			pck.Save();
			pck.Dispose();


		}
		private void DeleteEntryFromExcelLog(CaseLogEntry e)
		{

			ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
			ExcelPackage pck = new ExcelPackage(new FileInfo(ExcelLogFilePath));

			//const string invalidCharsRegex = @"[/\\*'?[\]:()]+";
			//const int maxLength = 31;//excel tabname char limit
			ExcelWorksheet ws = pck.Workbook.Worksheets[0];
			// find next
			int rowIDX = 2;//where are we saving this row:default header=1,case start =2
			int colIDX = 1;

			int caseRowIDX = -1;
			int allRowsInFile = _dtCaseList.Rows.Count + 3;//(1-Based row list + 1-header row )
														   //find next empty row starting from 2
			while ((caseRowIDX < 0) && (rowIDX < allRowsInFile))
			{
				if (ws.Cells[rowIDX, colIDX].Value != null && ws.Cells[rowIDX, colIDX].Value.ToString() == e.LogTSID)
					caseRowIDX = rowIDX;

				rowIDX++;
			}
			rowIDX = caseRowIDX;

			ws.DeleteRow(rowIDX);
			pck.Save();
			pck.Dispose();
		}
		private void FrmCaseList_FormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.Save();
		}

		private void viewSAVEDToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("explorer.exe", Settings.Default.LogDir);
		}

		private void bodyPartListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FrmListEditor frmEditr = new FrmListEditor(ListBodypart, "Body Part");
			if (frmEditr.ShowDialog() == DialogResult.OK)
			{
				ListBodypart = frmEditr.ItemList.ToArray();

				Settings.Default.BodyPartList = string.Join('|', ListBodypart);
				Settings.Default.Save();

				frmNewCase.ListRefreshed("BodyPartList", ListBodypart);
			}
			frmEditr.Close();
			frmEditr.Dispose();
		}
		private void specialityListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FrmListEditor frmEditr = new FrmListEditor(ListSpecialty, "Specialty");
			if (frmEditr.ShowDialog() == DialogResult.OK)
			{
				ListSpecialty = frmEditr.ItemList.ToArray();
				cboSpecialty.Items.Clear();
				cboSpecialty.Items.AddRange(ListSpecialty);

				Settings.Default.SpecialityList = string.Join('|', ListSpecialty);
				Settings.Default.Save();

				frmNewCase.ListRefreshed("SpecialtyPartList", ListSpecialty);
			}
			frmEditr.Close();
			frmEditr.Dispose();
		}

		private CaseLogEntry MakeLogEntryFromDataRow(int idx, DataRow r)
		{
			return new CaseLogEntry()
			{
				IDX = idx + 1,
				LogTSID = r["LogID"].ToString(),
				Specialty = r["Speciality"].ToString(),
				BodyPart = r["BodyPart"].ToString(),
				PTIdType = r["MRN"].ToString() == "" ? "Accession" : "MRN",
				PTMRN = (r["MRN"].ToString() == "" ? r["Accession"].ToString() : r["MRN"].ToString()),
				Notes = r["Notes"].ToString(),
				Tags = r["Tags"].ToString(),

			};

		}
		private void gvList_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			int rowIDx = e.RowIndex;
			if (rowIDx > -1)
			{
				string caseFolderName = gvList.Rows[rowIDx].Cells[0].Value.ToString();
				string caseFolderPath = Settings.Default.LogDir + caseFolderName;
				DataRowView drv = gvList.Rows[rowIDx].DataBoundItem as DataRowView;

				if (e.ColumnIndex == 8) //dataGridViewSoftware.Columns["select"].Index
				{
					if (Directory.Exists(caseFolderPath))
						System.Diagnostics.Process.Start("explorer.exe", caseFolderPath);
					else
						MessageBox.Show("This Case Doesn't Have Snapshots");
				}

				if (e.ColumnIndex == 9)
				{
					CaseLogEntry logEntry = MakeLogEntryFromDataRow(rowIDx, drv.Row);

					frmNewCase.ShowCase(logEntry);

					this.Hide();
				}

				if (e.ColumnIndex == 10)
				{
					string fullPathToPPTFile = caseFolderPath + $"\\MCL_{drv.Row["LogID"]}.pptx";
					string cmdLine = "\"" + PathToPPTexe + "\" /O \"" + fullPathToPPTFile + "\"";

					System.Diagnostics.Process.Start(cmdLine);
					this.WindowState = FormWindowState.Minimized;
				}
			}

		}

		internal void DeleteEntry(CaseLogEntry logEntry)
		{
			if (Directory.Exists(logEntry.LogStudyPath))
				Directory.Delete(logEntry.LogStudyPath, true);

			//find index in _list
			int dtRowID = -1;
			for (int i = 0; i < _dtCaseList.Rows.Count; i++)
			{
				if (_dtCaseList.Rows[i]["LogID"].ToString() == logEntry.LogTSID)
				{
					dtRowID = i;
					break;
				}
			}
			if (dtRowID > -1)
				_dtCaseList.Rows.RemoveAt(dtRowID);

			_dtCaseList.AcceptChanges();


			DeleteEntryFromExcelLog(logEntry);

			this.Show();
		}

		private void viewExcelLogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
			Process.Start(new ProcessStartInfo { FileName = ExcelLogFilePath, UseShellExecute = true });
			
		}


		//private void MakePowerPointForCase(CaseLogEntry logEntry)
		//{
		//	PowerPointController pptxCtrl = new PowerPointController(logEntry.LogStudyPath, PathToPPTTemplate);
		//	pptxCtrl.AddMyCaseToCollection(logEntry);
		//}
	}
}
