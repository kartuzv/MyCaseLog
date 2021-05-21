using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MyCaseLog
{
	public partial class ScreenshotForm : Form
	{
        public Bitmap bmp;
        public CaseLogEntryForm frm;
        int imgIndex = -1;
        public ScreenshotForm(Bitmap img, int index)
        {
            InitializeComponent();

            bmp = img;
            pbCapture.Image = bmp;
            imgIndex = index;
        }
        public ScreenshotForm(Int32 x, Int32 y, Int32 w, Int32 h, Size s)
        {
            InitializeComponent();
            //C#: how to take a screenshot of a portion of screen https://stackoverflow.com/a/3306633/5260872
          
            Rectangle rect = new Rectangle(x, y, w, h);
            bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, s, CopyPixelOperation.SourceCopy);
            //bmp.Save(@"D:\screen.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            pbCapture.Image = bmp;
            //this.ClientSize = new System.Drawing.Size(rect.Width, rect.Height);
            //this.pbCapture.Size = new System.Drawing.Size(429, 219);
            imgIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (frm != null && imgIndex<0)
            {
                frm.AddScreenshotCapturedBMP(bmp);
                this.Hide();
            }
            else
                this.Close();
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.CheckPathExists = true;
            //sfd.FileName = "Capture";
            //sfd.Filter = "PNG Image(*.png)|*.png|JPG Image(*.jpg)|*.jpg|BMP Image(*.bmp)|*.bmp";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    pbCapture.Image.Save(sfd.FileName);
            //}
        }

		private void btnDelete_Click(object sender, EventArgs e)
		{
            if (frm != null && imgIndex > -1)
            {
                frm.DeleteScreenshot(imgIndex);
                this.Close();
            }
            else
                this.Close();
        }
	}
}
