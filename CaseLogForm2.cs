
using MyCaseLog.Controllers;
using MyCaseLog.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyCaseLog
{
	public partial class CaseLogForm2 : Form
	{
		private FrmCaseList _frmCaseList;

		SelectArea fSA;
		CaseLogEntry logEntry;

		List<Image> snaps;

		int bmpSelectedIDX = -1;
		bool deleteInProgress = false;


		public CaseLogForm2(FrmCaseList caseList)
		{
			InitializeComponent();
			_frmCaseList = caseList;

			snaps = new List<Image>();

			fSA = new SelectArea();
			fSA.frm = this;
			fSA.StartPosition = FormStartPosition.Manual;
			fSA.Location = this.Location;
		}

		private void CaseLogForm2_Load(object sender, EventArgs e)
		{
			if (_frmCaseList.ListSpecialty.Length > 0)
				cboSpecialty.Items.Clear();

			cboSpecialty.Items.Add("");
			cboSpecialty.Items.AddRange(_frmCaseList.ListSpecialty);

			if (_frmCaseList.ListBodypart.Length > 0)
				cboBodyPart.Items.Clear();

			cboBodyPart.Items.Add("");
			cboBodyPart.Items.AddRange(_frmCaseList.ListBodypart);

			if (Settings.Default.UsrBodyPart != "")
			{
				cboBodyPart.SelectedItem = Settings.Default.UsrBodyPart;
				chkKeepBodyPart.Checked = true;
			}

			cboPIDType.SelectedIndex = 0;
		}

		public void ListRefreshed(string listType, string[] lst)
		{
			if (listType == "BodyPartList")
			{
				cboBodyPart.Items.Clear();
				cboBodyPart.Items.Add("");
				cboBodyPart.Items.AddRange(_frmCaseList.ListBodypart);
			}
			else
			{
				cboSpecialty.Items.Clear();
				cboSpecialty.Items.Add("");
				cboSpecialty.Items.AddRange(_frmCaseList.ListSpecialty);
			}
		}

		public void StartNewCase()
		{
			logEntry = new CaseLogEntry();
			logEntry.IDX = 0;
			logEntry.LogTSID = DateTime.Now.ToString("yyyyMMdd_HHmmss");
			logEntry.LogStudyPath = Path.Combine(Settings.Default.LogDir, logEntry.LogTSID);
			logEntry.SnapPaths = new List<string>();

			snaps = new List<Image>();

			txtNotes.Text = "";
			txtPTID.Text = "";
			txtTags.Text = "";

			snaps.Clear();

			imageList1.Images.Clear();
			listView1.Items.Clear();
		}

		internal void ShowCase(CaseLogEntry logEntry)
		{
			snaps.Clear();

			imageList1.Images.Clear();
			listView1.Items.Clear();

			this.logEntry = logEntry;
			this.Show();
			
			txtTags.Text = logEntry.Tags;
			txtNotes.Text = logEntry.Notes;
			cboPIDType.Text = logEntry.PTIdType;
			txtPTID.Text = logEntry.PTMRN;

			cboSpecialty.SelectedIndex = cboSpecialty.FindStringExact(logEntry.Specialty);
			cboSpecialty.Refresh();
			cboBodyPart.SelectedIndex = cboBodyPart.FindStringExact(logEntry.BodyPart);
			cboBodyPart.Refresh();
			//cboSpecialty.Text = logEntry.Specialty;
			//cboBodyPart.Text = logEntry.BodyPart;
			logEntry.LogStudyPath = Path.Combine(Settings.Default.LogDir, logEntry.LogTSID);
			logEntry.SnapPaths = new List<string>();

			if (Directory.Exists(logEntry.LogStudyPath))
			{
				logEntry.SnapPaths = Directory.GetFiles(logEntry.LogStudyPath, $"{logEntry.LogTSID}_*.jpeg").ToList();

				foreach (var jpgPath in logEntry.SnapPaths)
				{
					Image img = AppController.FromFile(jpgPath);
					snaps.Add(img);
					imageList1.Images.Add(img);
					listView1.Items.Add("", imageList1.Images.Count - 1);
				}

			}
			

		}

		internal void SnapshotDiscarded()
		{
			this.Show();
		}

		private void CaseLogForm2_FormClosing(object sender, FormClosingEventArgs e)
		{
			//Settings.Default.UsrSpecialty = cboSpecialty.Text;
			//Settings.Default.UsrBodyPart = (chkKeepBodyPart.Checked) ? cboBodyPart.Text : "";
			//Settings.Default.Save();
			snaps.Clear();

			imageList1.Images.Clear();
			listView1.Items.Clear();
			fSA.Dispose();
		}

		#region screenshot functions
		private void btnAddScreenshot_Click(object sender, EventArgs e)
		{
			fSA.Show();
			this.Hide();
		}

		internal void AddScreenshotCapturedBMP(Bitmap bmp)
		{
			
			if (!Directory.Exists(logEntry.LogStudyPath))
				Directory.CreateDirectory(logEntry.LogStudyPath);

			listView1.SelectedItems.Clear();
			snaps.Add(bmp);
			string bmpPath = $"{logEntry.LogStudyPath}\\{logEntry.LogTSID}_{(snaps.Count)}.jpeg";
			bmp.Save(bmpPath, System.Drawing.Imaging.ImageFormat.Jpeg);
			logEntry.SnapPaths.Add(bmpPath);
			//Image img = (Image)bmp;
			imageList1.Images.Add(bmp);

			ListViewItem newImgItem = new ListViewItem();
			newImgItem.ImageIndex = imageList1.Images.Count - 1;
			listView1.Items.Add(newImgItem);

			this.Show();

		}
		internal void DeleteScreenshot(int indx)
		{
			deleteInProgress = true;

			bool isLast = (indx == snaps.Count - 1);

			listView1.SelectedItems.Clear();
			snaps.RemoveAt(indx);
			imageList1.Images.RemoveAt(indx);
			listView1.Items.RemoveAt(indx);

			string jpgPath = logEntry.SnapPaths[indx];
			logEntry.SnapPaths.RemoveAt(indx);

			if (File.Exists(jpgPath))
				File.Delete(jpgPath);

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
				bmpSelectedIDX = listView1.SelectedIndices[0];
				var bmp = (Bitmap)snaps[bmpSelectedIDX];
				using (var frmImg = new ScreenshotForm(bmp, bmpSelectedIDX))
				{
					frmImg.frm = this;
					frmImg.StartPosition = FormStartPosition.CenterParent;
					frmImg.ShowDialog();
				}
					
			}
		}
		#endregion

		private void btnSave_Click(object sender, EventArgs e)
		{
			GetFormEntryData();

			//save unsaved snaps
			if (snaps.Count > logEntry.SnapPaths.Count)
			{
				int unsavedIndex = (snaps.Count - logEntry.SnapPaths.Count) - 1;

				if (!Directory.Exists(logEntry.LogStudyPath))
					Directory.CreateDirectory(logEntry.LogStudyPath);

				for (int i = unsavedIndex; i < snaps.Count; i++)
				{
					var bmpSnap = snaps[i];
					bmpSnap.Save($"{logEntry.LogStudyPath}\\{logEntry.LogTSID}_{(i + 1)}.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);

				}
			}

			this._frmCaseList.SaveCaseLogEntryToLog(logEntry);

			txtNotes.Text = "";
			txtPTID.Text = "";
			txtTags.Text = "";

			imageList1.Images.Clear();
			snaps.Clear();

			this.Hide();
		}

		private void GetFormEntryData()
		{
			logEntry.Specialty = cboSpecialty.Text;
			logEntry.BodyPart = cboBodyPart.Text;
			logEntry.Tags = txtTags.Text.Trim();
			logEntry.Notes = txtNotes.Text.Trim();
			logEntry.PTIdType = cboPIDType.Text;
			logEntry.PTMRN = txtPTID.Text.Trim();

		}

		private void btnView_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("explorer.exe", Settings.Default.LogDir);
		}

		private void btnCaptureScreen_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Coming Soon...", "No Video Yet", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void btnEditListSpeciality_Click(object sender, EventArgs e)
		{
			//FrmListEditor frmEditr = new FrmListEditor(_frmCaseList.ListSpecialty, "Specialty");
			//if (frmEditr.ShowDialog() == DialogResult.OK)
			//{
			//	_frmCaseList.ListSpecialty = frmEditr.ItemList.ToArray();
			//	cboSpecialty.Items.Clear();
			//	cboSpecialty.Items.AddRange(_frmCaseList.ListSpecialty);

			//	Settings.Default.SpecialityList = string.Join('|', _frmCaseList.ListSpecialty);
			//	Settings.Default.Save();
			//}
			//frmEditr.Close();
			//frmEditr.Dispose();
			MessageBox.Show("Log List Window -> Settings -> Speciality List");
		}

		private void btnEditListBodyPart_Click(object sender, EventArgs e)
		{
			//FrmListEditor frmEditr = new FrmListEditor(_frmCaseList.ListBodypart, "Body Part");
			//if (frmEditr.ShowDialog() == DialogResult.OK)
			//{
			//	_frmCaseList.ListBodypart = frmEditr.ItemList.ToArray();
			//	cboBodyPart.Items.Clear();
			//	cboBodyPart.Items.AddRange(_frmCaseList.ListBodypart);

			//	Settings.Default.BodyPartList = string.Join('|', _frmCaseList.ListBodypart);
			//	Settings.Default.Save();
			//}
			//frmEditr.Close();
			//frmEditr.Dispose();
			MessageBox.Show("Log List Window -> Settings -> Body Part List");
		}

		private void AddPresetTag_Click(object sender, EventArgs e)
		{
			txtTags.Text = txtTags.Text + " " + ((Button)sender).Text;
		}


		private async void btnOpenAccessionInEI_Click(object sender, EventArgs e)
		{
			string acc = txtPTID.Text.Trim();
			try
			{
				pbOpeningEI.Visible = true;

				var IsSuccess = await AgfaController.OpenInAgfaEI(acc);

				pbOpeningEI.Visible = false;
				if (!IsSuccess)
					MessageBox.Show("Couldn't contact AGFA EI to open ACCESSION", "Failed EI Call");

			}
			catch (Exception ex)
			{
				var x = ex.Message;
			}
			finally
			{
				pbOpeningEI.Visible = false;
			}
		}

		private void cboPIDType_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnOpenAccessionInEI.Visible = (cboPIDType.SelectedItem.ToString() == "Accession");
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Hide();

			_frmCaseList.Show();
		}

		private void btnDeleteCase_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Delete Case (all data,images and folder) ?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				snaps.Clear();
			
				imageList1.Images.Clear();
				imageList1.Dispose();
				listView1.Items.Clear();
				
				this.Hide();
				
				_frmCaseList.DeleteEntry(this.logEntry);
				
			}
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
