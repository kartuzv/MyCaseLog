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
	public partial class CaseLogForm2 : Form
	{
        List<Bitmap> snaps;
        bool deleteInProgress = false;
        //bool _gotSnapshot = false;

        public CaseLogForm2()
		{
			InitializeComponent();
            snaps = new List<Bitmap>();
        }

        #region screenshot functions
        private void btnAddScreenshot_Click(object sender, EventArgs e)
		{
			SelectArea fSA = new SelectArea();
			fSA.frm = this;
			fSA.Show();
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
    }
}
