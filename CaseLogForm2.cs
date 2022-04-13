using MyCaseLog.Controllers;
using MyCaseLog.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyCaseLog
{
	public partial class CaseLogForm2 : Form
	{
        List<Bitmap> snaps;
        bool deleteInProgress = false;
        //bool _gotSnapshot = false;

        public CaseLogForm2()
		{
			InitializeComponent();
            snaps = new List<Bitmap>();
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.WindowState = FormWindowState.Minimized;

        }

        #region screenshot functions
        private void btnAddScreenshot_Click(object sender, EventArgs e)
		{
			SelectArea fSA = new SelectArea();
			fSA.frm = this;
            fSA.StartPosition = FormStartPosition.Manual;
            fSA.Location = this.Location;
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
            PowerPointController pptxCtrl = new PowerPointController();
            pptxCtrl.AddMyCaseToCollection(logEntry);
            MessageBox.Show("Saved");
        }

        private CaseLogEntry GetFormEntryData()
        {
            CaseLogEntry e = new CaseLogEntry();
            e.LogTSID = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            e.LogStudyPath = Path.Combine(Settings.Default.LogDir, e.LogTSID);
           
            e.Specialty = cboSpecialty.Text;
            e.BodyPart = cboBodyPart.Text;
            
            e.Tags = cboTags.Text.Trim();
            e.Notes = txtNotes.Text.Trim();
            e.PTIdType = cboPIDType.Text;
            e.PTMRN = txtPTID.Text.Trim();
            
            e.snaps.AddRange(snaps);
            return e;
        }

		private void CaseLogForm2_FormClosing(object sender, FormClosingEventArgs e)
		{
            Settings.Default.UsrSpecialty = (chkKeepSpecialty.Checked) ? cboSpecialty.Text : "";
            Settings.Default.UsrBodyPart = (chkKeepBodyPart.Checked) ? cboBodyPart.Text : "";
            Settings.Default.Save();

        }

		private void CaseLogForm2_Load(object sender, EventArgs e)
		{
            if (Settings.Default.UsrSpecialty != "")
            {
                cboSpecialty.SelectedItem = Settings.Default.UsrSpecialty;
                chkKeepSpecialty.Checked = true;
            }

            if (Settings.Default.UsrBodyPart != "")
            {
                cboBodyPart.SelectedItem = Settings.Default.UsrBodyPart;
                chkKeepBodyPart.Checked = true;
            }

            //ensure ppt template is ready
            //next to exe.
            string applicationDirectory = Application.ExecutablePath;
            string xlsTemplate = applicationDirectory + "\\MyCaseLog.xlsx";

            if (!File.Exists(xlsTemplate))
                File.WriteAllBytes(xlsTemplate, Resources.MyCaseLogTemplate);

            string pptxTemplate = applicationDirectory + "\\PPTemplate2Pic.pptx";
            if (!File.Exists(pptxTemplate))
                File.WriteAllBytes(xlsTemplate, Resources.PPTemplate2Pic);

            if (Settings.Default.LogDir == "")
            {
                Settings.Default.LogDir = Application.ExecutablePath + "\\Saved\\";
                Settings.Default.Save();
            }

            if (!Directory.Exists(Settings.Default.LogDir))
                Directory.CreateDirectory(Settings.Default.LogDir);
        }

		private void button1_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("explorer.exe", Settings.Default.LogDir);
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
