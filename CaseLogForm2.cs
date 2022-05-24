using MyCaseLog.Controllers;
using MyCaseLog.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OfficeOpenXml;

namespace MyCaseLog
{
    public partial class CaseLogForm2 : Form
    {
        List<Bitmap> snaps;
        bool deleteInProgress = false;
        //bool _gotSnapshot = false;
        string xlsTemplatePath = "";
        string pptxTemplatePath = "";
        SelectArea fSA;
        string[] _specialtyList;
        string[] _bodypartList;

        private string _excelLogFileName = "MyCaseLog.xlsx";
        int lastLogRowIDX = -1;

        DataTable _dtCaseList = new DataTable();
        string _pathToPPTexe = "";
        public CaseLogForm2()
        {
            InitializeComponent();
            snaps = new List<Bitmap>();
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.WindowState = FormWindowState.Minimized;

            fSA = new SelectArea();
            fSA.frm = this;
            fSA.StartPosition = FormStartPosition.Manual;
            fSA.Location = this.Location;
        }

        private void CaseLogForm2_Load(object sender, EventArgs e)
        {
            //load user configurable specialty
            _specialtyList = Settings.Default.SpecialityList.Split('|', StringSplitOptions.RemoveEmptyEntries);

            if (_specialtyList.Length > 0)
            {
                Array.Sort(_specialtyList, StringComparer.InvariantCulture);

                cboSpecialty.Items.Clear();
                cboSpecialty.Items.AddRange(_specialtyList);

            }

            //load user configuarable bodypart
            _bodypartList = Settings.Default.BodyPartList.Split('|', StringSplitOptions.RemoveEmptyEntries);
            if (_bodypartList.Length > 0)
            {
                Array.Sort(_bodypartList, StringComparer.InvariantCulture);
                cboBodyPart.Items.Clear();
                cboBodyPart.Items.AddRange(_bodypartList);
            }


            if (Settings.Default.UsrSpecialty != "")
            {
                cboSpecialty.SelectedItem = Settings.Default.UsrSpecialty;
                //chkKeepSpecialty.Checked = true;
            }

            if (Settings.Default.UsrBodyPart != "")
            {
                cboBodyPart.SelectedItem = Settings.Default.UsrBodyPart;
                chkKeepBodyPart.Checked = true;
            }

            //ensure ppt template is ready
            //next to exe.
            string applicationDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            string templateDir = applicationDirectory + "\\Template";
            if (!Directory.Exists(templateDir))
                Directory.CreateDirectory(templateDir);

            xlsTemplatePath = templateDir + "\\MyCaseLog.xlsx";
            pptxTemplatePath = templateDir + "\\PPTemplate2Pic.pptx";

            if (!File.Exists(xlsTemplatePath))
                File.WriteAllBytes(xlsTemplatePath, Resources.MyCaseLogTemplate);

            if (!File.Exists(pptxTemplatePath))
                File.WriteAllBytes(pptxTemplatePath, Resources.PPTemplate2Pic);

            if (string.IsNullOrEmpty(Settings.Default.LogDir))
            {
                Settings.Default.LogDir = applicationDirectory + "\\Saved\\";
                Settings.Default.Save();
            }

            if (!Directory.Exists(Settings.Default.LogDir))
            {
                Directory.CreateDirectory(Settings.Default.LogDir);
                File.Copy(xlsTemplatePath, Settings.Default.LogDir + _excelLogFileName);
            }

            //load list of cases from excel log
            _dtCaseList = ExcelToDataTable(Settings.Default.LogDir + _excelLogFileName);

            cboPIDType.SelectedIndex = 0;

            //find powerpoint installed 
            //so we can open it later, 12,14,15,16 365 /root/office16
            for (int i = 16; i > 11; i--)
            {
                //"c:\Program Files\Microsoft Office\root\Office16\powerpnt.exe" /O Filename1.pptx,
                string testPathOffice64bit = $"C:\\Program Files\\Microsoft Office\\Office{i}\\powerpnt.exe";
               
                if (File.Exists(testPathOffice64bit))
                {
                    _pathToPPTexe = testPathOffice64bit;
                    break;
                }
                string testPathOffice32bit = $"C:\\Program Files (x86)\\Microsoft Office\\Office{i}\\powerpnt.exe";
                if (File.Exists(testPathOffice32bit))
                {
                    _pathToPPTexe = testPathOffice32bit;
                    break;
                }
            }
            //attemt to check office365 pth
            if (_pathToPPTexe == "" && File.Exists(@"c:\Program Files\Microsoft Office\root\Office16\powerpnt.exe"))
            {
                _pathToPPTexe = @"c:\Program Files\Microsoft Office\root\Office16\powerpnt.exe";
            }

        } 

		internal void SnapshotDiscarded()
		{
            this.Show();
        }

		private void CaseLogForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.UsrSpecialty = cboSpecialty.Text;
            Settings.Default.UsrBodyPart = (chkKeepBodyPart.Checked) ? cboBodyPart.Text : "";
            Settings.Default.Save();

        }

        #region screenshot functions
        private void btnAddScreenshot_Click(object sender, EventArgs e)
		{	
			fSA.Show();
            this.Hide();
		}

        internal void AddScreenshotCapturedBMP(Bitmap bmp)
        {
            //pictureBox1.Image = bmp;
            listView1.SelectedItems.Clear();
            snaps.Add(bmp);
            //Image img = (Image)bmp;
            imageList1.Images.Add(bmp);

            ListViewItem newImgItem = new ListViewItem();
            newImgItem.ImageIndex = imageList1.Images.Count - 1;
            listView1.Items.Add(newImgItem);

            fSA.Show();
            //newImgItem.Position = new Point(newImgItem.GetBounds(ItemBoundsPortion.Entire).Width * (listView1.Items.Count - 1), 0);

            //listView1.Items.Add("", imageList1.Images.Count - 1);

            //ListViewItem lvi = new ListViewItem(imageList1.Images.Count.ToString(), imageList1.Images.Count - 1);
            //listView1.Items.Add(lvi);
            //
            //_gotSnapshot = true;
            //chkSnap_CheckedChanged(null, null);
        }
        internal void DeleteScreenshot(int indx)
        {
            deleteInProgress = true;

            bool isLast = (indx == snaps.Count - 1);

            listView1.SelectedItems.Clear();
            snaps.RemoveAt(indx);
            imageList1.Images.RemoveAt(indx);
            listView1.Items.RemoveAt(indx);
			if (!isLast)
			{
				imageList1.Images.Clear();
				listView1.Items.Clear();
				foreach (Bitmap b in snaps)
				{
					imageList1.Images.Add(b);
					listView1.Items.Add("", imageList1.Images.Count - 1);
				}
			}

			listView1.Refresh();
            //chkSnap_CheckedChanged(null, null);
            deleteInProgress = false;
        }

		internal void ReDoScreenshot()
		{
            fSA.Show();
            this.Hide();
        }

		private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
            if (listView1.SelectedIndices.Count > 0 && !deleteInProgress)
            {
                int imgIndex = listView1.SelectedIndices[0];
                var bmp = snaps[imgIndex];
                var frmImg = new ScreenshotForm(bmp, imgIndex);
                frmImg.frm = this;
                frmImg.StartPosition = FormStartPosition.CenterParent;
                frmImg.ShowDialog();

            }
		}
		#endregion

		private void btnSave_Click(object sender, EventArgs e)
		{
            CaseLogEntry logEntry = GetFormEntryData();
            PowerPointController pptxCtrl = new PowerPointController(Settings.Default.LogDir, pptxTemplatePath);
            pptxCtrl.AddMyCaseToCollection(logEntry);

            SaveEntryToExcel(logEntry);

            //add to local list in memory
            DataRow r = _dtCaseList.NewRow();
            r["LogID"] = logEntry.LogTSID;
            r["Speciality"] = logEntry.Specialty;
            r["BodyPart"] = logEntry.BodyPart;
            r["Accession"] = logEntry.PTIdType == "MRN" ? "" : logEntry.PTMRN;
            r["MRN"] = logEntry.PTIdType == "MRN" ? logEntry.PTMRN : "";
            r["Notes"] = logEntry.Notes;
            r["Tags"] = logEntry.Tags;
            r["PowerPointFile"] = $"MCL_{logEntry.Specialty}.pptx";
            _dtCaseList.Rows.Add(r);

         MessageBox.Show("Saved");
            txtNotes.Text = "";
            txtPTID.Text = "";
            txtTags.Text = "";

            imageList1.Images.Clear();
            snaps.Clear();

            //this.WindowState = FormWindowState.Minimized;
        }

        private CaseLogEntry GetFormEntryData()
        {
            CaseLogEntry e = new CaseLogEntry();
            e.LogTSID = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            e.LogStudyPath = Path.Combine(Settings.Default.LogDir, e.LogTSID);
           
            e.Specialty = cboSpecialty.Text;
            e.BodyPart = cboBodyPart.Text;           
            e.Tags = txtTags.Text.Trim();
            e.Notes = txtNotes.Text.Trim();
            e.PTIdType = cboPIDType.Text;
            e.PTMRN = txtPTID.Text.Trim();
            
            e.snaps.AddRange(snaps);
            return e;
        }

        public void SaveEntryToExcel(CaseLogEntry e)
        {
            string pathToExcelLog = Settings.Default.LogDir + _excelLogFileName;

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage(new FileInfo(pathToExcelLog));
            //ExcelWorksheet wsDt;

            //const string invalidCharsRegex = @"[/\\*'?[\]:()]+";
            //const int maxLength = 31;//excel tabname char limit
            ExcelWorksheet ws = pck.Workbook.Worksheets[0];
            // find next
            int rowIDX = 2;
            int colIDX = 1;
            if (lastLogRowIDX < 0)
            {
                //Cells only contains references to cells with actual data
                //var cells = ws.Cells;
                //int cnt = cells.Count();

                //var rowIndicies = cells
                //    .Select(c => c.Start.Row)
                //    .Distinct()
                //    .ToList();

                ////Skip the header row which was added by LoadFromDataTable
                //for (var i = 1; i <= 10; i++)
                //    Console.WriteLine($"Row {i} is empty: {rowIndicies.Contains(i)}");

                //string colIDValue = "x";
                while (lastLogRowIDX < 0)
                {

                    if (ws.Cells[rowIDX, colIDX].Value == null)
                    {
                        //colIDValue = ws.Cells[rowIDX, colIDX].Value.ToString().Trim();
                        lastLogRowIDX = rowIDX;
                    }
                    rowIDX++;
                }

            }
            rowIDX = lastLogRowIDX;
            //Debug.WriteLine(rowIDX);

            ws.Cells[rowIDX, 1].Value = e.LogTSID;
            ws.Cells[rowIDX, 2].Value = e.Specialty;
            ws.Cells[rowIDX, 3].Value = e.BodyPart;

            ws.Cells[rowIDX, 4].Value = e.PTIdType=="MRN"? "": e.PTMRN;
            ws.Cells[rowIDX, 5].Value = e.PTIdType == "MRN" ? e.PTMRN : "";
           
            ws.Cells[rowIDX, 6].Value = e.Notes;
            ws.Cells[rowIDX, 7].Value = e.Tags;

            ws.Cells[rowIDX, 8].Formula = $"HYPERLINK(\"MCL_{e.Specialty}.pptx\", \"MCL_{e.Specialty}.pptx\")";
            

            pck.Save();
            pck.Dispose();
            lastLogRowIDX++;//skip searching next time

        }

        public static DataTable ExcelPackageToDataTable(ExcelPackage excelPackage, int sheetIndex=0)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            DataTable dt = new DataTable();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[sheetIndex];

            //check if the worksheet is completely empty
            if (worksheet.Dimension == null)
            {
                return dt;
            }

            //create a list to hold the column names
            List<string> columnNames = new List<string>();

            //needed to keep track of empty column headers
            int currentColumn = 1;

            //loop all columns in the sheet and add them to the datatable
            foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                string columnName = cell.Text.Trim();

                //check if the previous header was empty and add it if it was
                if (cell.Start.Column != currentColumn)
                {
                    columnNames.Add("Header_" + currentColumn);
                    dt.Columns.Add("Header_" + currentColumn);
                    currentColumn++;
                }

                //add the column name to the list to count the duplicates
                columnNames.Add(columnName);

                //count the duplicate column names and make them unique to avoid the exception
                //A column named 'Name' already belongs to this DataTable
                int occurrences = columnNames.Count(x => x.Equals(columnName));
                if (occurrences > 1)
                {
                    columnName = columnName + "_" + occurrences;
                }

                //add the column to the datatable
                dt.Columns.Add(columnName);

                currentColumn++;
            }

            //start adding the contents of the excel file to the datatable
            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
            {
                var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                DataRow newRow = dt.NewRow();

                //loop all cells in the row
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }

                dt.Rows.Add(newRow);
            }

            return dt;
        }

        public static DataTable ExcelToDataTable(string filePath, int sheetIndex = 0)
        {
            DataTable dt = new DataTable();
            FileInfo existingFile = new FileInfo(filePath);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage(existingFile))
            {
                var ws = pck.Workbook.Worksheets[0];
                foreach (var cell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {                   
                    dt.Columns.Add(cell.Text.Trim());
                }
                for (int i = 2; i <= ws.Dimension.End.Row; i++)
                {
                    var row = ws.Cells[i, 1, i, ws.Dimension.End.Column];
                    DataRow newRow = dt.NewRow();

                    //loop all cells in the row
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }

                    dt.Rows.Add(newRow);
                   
                }
            }
            return dt;
        }
           

        private void btnView_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("explorer.exe", Settings.Default.LogDir);
        }

		private void btnCaptureScreen_Click(object sender, EventArgs e)
		{
            MessageBox.Show("Coming Soon...","No Video Yet", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

		private void btnEditListSpeciality_Click(object sender, EventArgs e)
		{            
            FrmListEditor frmEditr = new FrmListEditor(_specialtyList, "Specialty");
            if (frmEditr.ShowDialog() == DialogResult.OK)
            {
                _specialtyList = frmEditr.ItemList.ToArray();
                cboSpecialty.Items.Clear();
                cboSpecialty.Items.AddRange(_specialtyList);

                Settings.Default.SpecialityList = string.Join('|', _specialtyList);
                Settings.Default.Save();
            }
            frmEditr.Close();
            frmEditr.Dispose();
        }

        private void btnEditListBodyPart_Click(object sender, EventArgs e)
        {
            FrmListEditor frmEditr = new FrmListEditor(_bodypartList, "Body Part");
            if (frmEditr.ShowDialog() == DialogResult.OK)
            {
                _bodypartList = frmEditr.ItemList.ToArray();
                cboBodyPart.Items.Clear();
                cboBodyPart.Items.AddRange(_bodypartList);

                Settings.Default.BodyPartList = string.Join('|', _bodypartList);
                Settings.Default.Save();
            }
            frmEditr.Close();
            frmEditr.Dispose();
        }

        private void AddPresetTag_Click(object sender, EventArgs e)
		{
            txtTags.Text = txtTags.Text + " " + ((Button)sender).Text;
		}

		private void btnViewSpecialtyCases_Click(object sender, EventArgs e)
		{
            FrmCaseList lstFrm = new FrmCaseList(_specialtyList, _dtCaseList,_pathToPPTexe);
            lstFrm.StartPosition = FormStartPosition.CenterParent;
            lstFrm.ShowDialog();
            //int newWidth = this.Width == 1000 ? 515 : 1000;
            //string btnOpenerIcon = newWidth == 1000 ? "t" : "u";
            //btnViewSpecialtyCases.Text = btnOpenerIcon;

            //this.Width = newWidth;
            //DataTable dtList = new DataTable();
            //dtList.Columns.Add("#", typeof(string));
            //dtList.Columns.Add("BodyPart", typeof(string));
            //dtList.Columns.Add("MRN", typeof(string));
            //dtList.Columns.Add("Date", typeof(string));
            //dtList.Columns.Add("Tags", typeof(string));
            //dtList.Columns.Add("Notes", typeof(string));

            //if (this.Width == 1000)
            //{
            //    PowerPointController p = new PowerPointController(Settings.Default.LogDir, pptxTemplatePath);
            //    List<string> pptTitles= p.GetAllCaseTitles(cboSpecialty.Text);
            //    foreach (string titleCSV in pptTitles)
            //    {
            //        string[] t = titleCSV.Split("|");
            //        t[2] = t[2].Replace("MRN:", "");
            //        t[4] = t[4].Replace("[TAGS]:", "");

            //        dtList.Rows.Add(t);
            //    }
            //    gvCaseList.DataSource = dtList;
            //}
		}

		

		/* https://code-examples.net/en/q/207b9
         // AutoCompleteStringCollection   
AutoCompleteStringCollection data = new AutoCompleteStringCollection();  
data.Add("Mahesh Chand");  
data.Add("Mac Jocky");  
data.Add("Millan Peter");  
comboBox1.AutoCompleteCustomSource = data; 
        * I went with the custom source because I want to assign a custom collection from the AutoCompleteStringCollection() class **
        AutoCompleteStringCollection predictive_Collection = new AutoCompleteStringCollection();
        ** Define your collection of predictive words and phrases **
        string[] predictive_Words = { "my", "name", "is", "Sheepings", "my name is Sheepings" };
        /** Define your array of words or suggested text(s) **
        predictive_Collection.AddRange(predictive_Words);
            /** Add your suggested text(s) of predicted words to the predictive collection, and then assign the source below. **
            comboBox1.AutoCompleteCustomSource = predictive_Collection;
         */
	}
}
