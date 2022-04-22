using MyCaseLog.Controllers;
using MyCaseLog.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
                Directory.CreateDirectory(Settings.Default.LogDir);

            cboPIDType.SelectedIndex = 0;

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


		private void button1_Click(object sender, EventArgs e)
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
            int newWidth = this.Width == 1000 ? 515 : 1000;
            string btnOpenerIcon = newWidth == 1000 ? "t" : "u";
            btnViewSpecialtyCases.Text = btnOpenerIcon;

            this.Width = newWidth;
            DataTable dtList = new DataTable();
            dtList.Columns.Add("#", typeof(string));
            dtList.Columns.Add("BodyPart", typeof(string));
            dtList.Columns.Add("MRN", typeof(string));
            dtList.Columns.Add("Date", typeof(string));
            dtList.Columns.Add("Tags", typeof(string));
            dtList.Columns.Add("Notes", typeof(string));

            if (this.Width == 1000)
            {
                PowerPointController p = new PowerPointController(Settings.Default.LogDir, pptxTemplatePath);
                List<string> pptTitles= p.GetAllCaseTitles(cboSpecialty.Text);
                foreach (string titleCSV in pptTitles)
                {
                    string[] t = titleCSV.Split("|");
                    t[2] = t[2].Replace("MRN:", "");
                    t[4] = t[4].Replace("[TAGS]:", "");

                    dtList.Rows.Add(t);
                }
                gvCaseList.DataSource = dtList;
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
