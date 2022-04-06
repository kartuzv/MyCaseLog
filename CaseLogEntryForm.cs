using MyCaseLog.Properties;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCaseLog
{
	public partial class CaseLogEntryForm : Form
    {   //grab patient context fields
        //grab windows with imgs
        //save pt to excel
        //save img to folder structure
        private string _excelLogFileName = "MyCaseLog.xlsx";
        //static Bitmap _bitmap;
        //static Graphics _graphics;
        int lastLogRowIDX = -1;
        static List<WindowObjInfo> wino;// = new List<WindowObjInfo>();
        bool _gotSnapshot = false;
        public string HOSP = "";
        public string ViewerWindowTitle = "";
        List<Bitmap> snaps;

        bool deleteInProgress = false;

        public CaseLogEntryForm()
		{
			InitializeComponent();
            snaps = new List<Bitmap>();

        }
	
        public void ResetInputFields()
        {
            chkCT.Checked = false;
            chkXRay.Checked = false;
            chkMammo.Checked = false;
            chkNM.Checked = false;
            chkHead.Checked = false;
            chkNeck.Checked = false;
            chkSpine.Checked = false;
            chkBreast.Checked = false;
            chkChest.Checked = false;
            chkAbdomen.Checked = false;
            chkArm.Checked = false;
            chkLeg.Checked = false;
            

            chkMale.Checked = false;
            chkFemale.Checked = false;
            chkTrans.Checked = false;

            txtProtocol.Text = "";
            txtMRN.Text = "";
            txtACC.Text = "";
            txtBill.Text = "";

            rtxtDescr.Text = "";
            chkIsPub.Checked = false;
            chkIsConf.Checked = false;
            chkSocConf.Checked = false;
            pictureBox1.Image = null;

            //listView1.Items.Clear();
            imageList1.Images.Clear();
            ///listView1.SmallImageList = imageList1;
            snaps.Clear();
        }



        private void btnSaveToLog_Click(object sender, EventArgs ea)
        {

            CaseLogEntry e = GetFormEntryData();
            /*
            SaveEntryToExcel(e);
            //Debug.WriteLine(e.ToString());

            //if (_gotSnapshot && chkSnap.Checked)
            //{


           // string savingPath = e.LogStudyPath + "\\Screenshot_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpeg";
            //pictureBox1.Image.Save(savingPath, ImageFormat.Jpeg);
            //don't create hyperlink to folder unless have screenshot
            if (snaps.Count > 0)
            {
                if (!Directory.Exists(e.LogStudyPath))
                    Directory.CreateDirectory(e.LogStudyPath);

                int i = 1;
                foreach (Bitmap img in snaps)
                {
                    string savingPath = e.LogStudyPath + "\\Screenshot_" + DateTime.Now.ToString("yyyyMMddhhmmss") + $"_{i}.jpeg";
                    img.Save(savingPath, ImageFormat.Jpeg);
                    i++;
                }
            }
            */
           


            //Controllers.PowerPointController.AddMyCaseToCollection(e);

            //}
            this.Hide();
        }

        private CaseLogEntry GetFormEntryData()
        {
            CaseLogEntry e = new CaseLogEntry();
            e.LogTSID = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            e.LogStudyPath = Path.Combine(Settings.Default.LogDir, e.LogTSID);
            e.Hosp = HOSP;
            e.Modality = "|";
            e.Modality += chkCT.Checked ? "|" + chkCT.Text : "";

            e.Modality += chkXRay.Checked ? "|" + chkXRay.Text : "";
            e.Modality += chkMammo.Checked ? "|" + chkMammo.Text : "";
            e.Modality += chkMRI.Checked ? "|" + chkMRI.Text : "";
            e.Modality += chkNM.Checked ? "|" + chkNM.Text : "";
            e.Modality = e.Modality.Replace("||", "");

            e.BodyPart = "|";
            e.BodyPart += chkHead.Checked ? "|" + chkHead.Text : "";
            e.BodyPart += chkNeck.Checked ? "|" + chkNeck.Text : "";
            e.BodyPart += chkSpine.Checked ? "|" + chkSpine.Text : "";

            e.BodyPart += chkBreast.Checked ? "|" + chkBreast.Text : "";
            e.BodyPart += chkChest.Checked ? "|" + chkChest.Text : "";
            e.BodyPart += chkAbdomen.Checked ? "|" + chkAbdomen.Text : "";

            e.BodyPart += chkArm.Checked ? "|" + chkArm.Text : "";
            e.BodyPart += chkLeg.Checked ? "|" + chkLeg.Text : "";

            e.BodyPart = e.BodyPart.Replace("||", "");
            e.Protocol = txtProtocol.Text.Trim();
            e.Dx = txtDx.Text.Trim();

            if (!chkAnon.Checked)
            {
                e.PTSex = chkMale.Checked ? "M" : "F";
                e.PTSex = chkFemale.Checked ? "F" : "M";

                if (chkTrans.Checked)
                    e.PTSex = "T";


                e.PTAge = txtAGE.Text;
                e.PTMRN = txtMRN.Text;
            }
            else
            {
                e.PTSex = "";
                e.PTAge = "";
                e.PTMRN = "";
            }

            e.StudyAcc = txtACC.Text;
            e.StudyDesc = rtxtDescr.Text;
            e.BillAmount = txtBill.Text;

            e.IsPublished = chkIsPub.Checked;
            e.IsLocalConference = chkIsConf.Checked;
            e.IsSocietyConference = chkSocConf.Checked;

            e.snaps.AddRange(snaps);
            return e;
        }

        

        #region Excel export
        public void SaveEntryToExcel(CaseLogEntry e)
        {
            string pathToExcelLog = Settings.Default.LogDir + "\\" + _excelLogFileName;

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

                string colIDValue = "x";
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
            ws.Cells[rowIDX, 2].Value = e.Hosp;
            ws.Cells[rowIDX, 3].Value = e.Modality;
            ws.Cells[rowIDX, 4].Value = e.BodyPart;
            ws.Cells[rowIDX, 5].Value = e.Protocol;
            ws.Cells[rowIDX, 6].Value = e.PTSex;
            ws.Cells[rowIDX, 7].Value = e.PTAge;
            ws.Cells[rowIDX, 8].Value = e.StudyAcc;
            ws.Cells[rowIDX, 9].Value = e.PTMRN;
            ws.Cells[rowIDX, 10].Value = e.StudyDesc;
            ws.Cells[rowIDX, 11].Value = e.IsLocalConference ? "Yes" : "";
            ws.Cells[rowIDX, 12].Value = e.IsSocietyConference ? "Yes" : "";
            ws.Cells[rowIDX, 13].Value = e.IsPublished ? "Yes" : "";
            ws.Cells[rowIDX, 14].Value = e.BillAmount;

            //don't create hyperlink to folder unless have screenshot
            if (snaps.Count>0)
            {
                ws.Cells[rowIDX, 15].Formula = $"HYPERLINK(\"{e.LogStudyPath}\", \"{e.LogTSID}\")";
                //int i = 1;
                //foreach (Bitmap img in snaps)
                //{
                //    string savingPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Screenshot_" + DateTime.Now.ToString("yyyyMMddhhmmss") + $"_{i}.jpeg";
                //    img.Save(savingPath, ImageFormat.Jpeg);
                //    i++;
                //}
            }
           
            pck.Save();
            pck.Dispose();
            lastLogRowIDX++;//skip searching next time

        }
     

		#endregion

		private void CaseLogEntryForm_VisibleChanged(object sender, EventArgs e)
		{
            if (this.Visible)
            {
                Debug.WriteLine("visible");
                this.Width = 338;
                //PrepareSnapshot();

            }
            else
            {
                Debug.WriteLine("hidden");
                this.Width = 338;
            }
		}

		private void CaseLogEntryForm_FormClosing(object sender, FormClosingEventArgs e)
		{
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                if (this.Visible)
                    this.Hide();

            }
		}

        private void PrepareSnapshot()
        {
            _gotSnapshot = false;
               IntPtr hWdlViewer = SearchForWindow("WindowsForms", ViewerWindowTitle);
            //this.ckbClientWnd.Checked gives the option to capture only the client area saving some space
            if (hWdlViewer != IntPtr.Zero)
            {
                Bitmap bitmap = MakeSnapshot(hWdlViewer, true, Win32API.WindowShowStyle.Restore);
                if (bitmap != null)
                {
                    pictureBox1.Image = bitmap;
                    _gotSnapshot = true;
                    //string savingPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\Screenshot_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpeg";
                    //pictureBox1.Image.Save(savingPath, ImageFormat.Jpeg);
                    this.Width = 780;
                }
                else
                {
                    //lblHwnd.Text = "Hwnd:";
                    //btnSnapSot.Enabled = false;
                    pictureBox1.Image = null;
                    this.Width = 334;
                    Debug.WriteLine("nope");
                }
                //btnSaveImage.Enabled = pictureBox1.Image != null;
                Win32API.SetForegroundWindow(this.Handle);
            }
        }
        internal void AddScreenshotCapturedBMP(Bitmap bmp)
        {
            //pictureBox1.Image = bmp;
            listView1.SelectedItems.Clear();
            snaps.Add(bmp);
            //Image img = (Image)bmp;
            imageList1.Images.Add(bmp);
            listView1.Items.Add(imageList1.Images.Count.ToString(), imageList1.Images.Count - 1); 
            
            _gotSnapshot = true;
            chkSnap_CheckedChanged(null, null);
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
                    listView1.Items.Add(imageList1.Images.Count.ToString(), imageList1.Images.Count - 1);
                }
            }

            listView1.Refresh();
            chkSnap_CheckedChanged(null, null);
            deleteInProgress = false;
        }

        #region screenshot functions

        private static Bitmap MakeSnapshot(IntPtr AppWndHandle, bool IsClientWnd, Win32API.WindowShowStyle nCmdShow)
        {

            if (AppWndHandle == IntPtr.Zero || !Win32API.IsWindow(AppWndHandle) || !Win32API.IsWindowVisible(AppWndHandle))
                return null;
            if (Win32API.IsIconic(AppWndHandle))
                Win32API.ShowWindow(AppWndHandle, nCmdShow);//show it
            if (!Win32API.SetForegroundWindow(AppWndHandle))
                return null;//can't bring it to front
            System.Threading.Thread.Sleep(1000);//give it some time to redraw
            RECT appRect;
            bool res = IsClientWnd ? Win32API.GetClientRect(AppWndHandle, out appRect) : Win32API.GetWindowRect(AppWndHandle, out appRect);
            if (!res || appRect.Height == 0 || appRect.Width == 0)
            {
                return null;//some hidden window
            }
            if (IsClientWnd)
            {
                Point lt = new Point(appRect.Left, appRect.Top);
                Point rb = new Point(appRect.Right, appRect.Bottom);
                Win32API.ClientToScreen(AppWndHandle, ref lt);
                Win32API.ClientToScreen(AppWndHandle, ref rb);
                appRect.Left = lt.X;
                appRect.Top = lt.Y;
                appRect.Right = rb.X;
                appRect.Bottom = rb.Y;
            }
            //Intersect with the Desktop rectangle and get what's visible
            IntPtr DesktopHandle = Win32API.GetDesktopWindow();
            RECT desktopRect;
            Win32API.GetWindowRect(DesktopHandle, out desktopRect);
            RECT visibleRect;
            if (!Win32API.IntersectRect(out visibleRect, ref desktopRect, ref appRect))
            {
                visibleRect = appRect;
            }
            if (Win32API.IsRectEmpty(ref visibleRect))
                return null;

            int Width = visibleRect.Width;
            int Height = visibleRect.Height;
            IntPtr hdcTo = IntPtr.Zero;
            IntPtr hdcFrom = IntPtr.Zero;
            IntPtr hBitmap = IntPtr.Zero;
            try
            {
                Bitmap clsRet = null;

                // get device context of the window...
                hdcFrom = IsClientWnd ? Win32API.GetDC(AppWndHandle) : Win32API.GetWindowDC(AppWndHandle);

                // create dc that we can draw to...
                hdcTo = Win32API.CreateCompatibleDC(hdcFrom);
                hBitmap = Win32API.CreateCompatibleBitmap(hdcFrom, Width, Height);

                //  validate...
                if (hBitmap != IntPtr.Zero)
                {
                    // copy...
                    int x = appRect.Left < 0 ? -appRect.Left : 0;
                    int y = appRect.Top < 0 ? -appRect.Top : 0;
                    IntPtr hLocalBitmap = Win32API.SelectObject(hdcTo, hBitmap);
                    Win32API.BitBlt(hdcTo, 0, 0, Width, Height, hdcFrom, x, y, Win32API.SRCCOPY);
                    Win32API.SelectObject(hdcTo, hLocalBitmap);
                    //  create bitmap for window image...
                    clsRet = System.Drawing.Image.FromHbitmap(hBitmap);
                }
                //MessageBox.Show(string.Format("rect ={0} \n deskrect ={1} \n visiblerect = {2}",rct,drct,VisibleRCT));
                //  return...
                return clsRet;
            }
            finally
            {
                //  release ...
                if (hdcFrom != IntPtr.Zero)
                    Win32API.ReleaseDC(AppWndHandle, hdcFrom);
                if (hdcTo != IntPtr.Zero)
                    Win32API.DeleteDC(hdcTo);
                if (hBitmap != IntPtr.Zero)
                    Win32API.DeleteObject(hBitmap);
            }


        }

        #endregion

        public static IntPtr SearchForWindow(string wndclass, string title)
        {
            WindowObjInfo sd = new WindowObjInfo { Wndclass = wndclass, Title = title, hWnd = IntPtr.Zero };
            WinAPI.EnumWindows(new EnumWindowsProc(EnumProc), ref sd);
            return sd.hWnd;
        }

        public static bool EnumProc(IntPtr hWnd, ref WindowObjInfo data)
        {
            // Check classname and title

            StringBuilder sb = new StringBuilder(1024);
            WinAPI.GetWindowText(hWnd, sb, sb.Capacity);
            string wTitle = sb.ToString();
            //ui windows usually have titles shown in taskbar

            if (wTitle.StartsWith(data.Title) || (data.Title == "" && wTitle != ""))//found it?
            {
                sb = new StringBuilder(1024);
                WinAPI.GetClassName(hWnd, sb, sb.Capacity);
                string wClass = sb.ToString();

                if (data.Wndclass != "")//filter for class specified
                {
                    if (wClass.StartsWith(data.Wndclass)) //check filter if matches 
                    {
                        data.Wndclass = wClass;
                        //appSuffix = wClass.Substring(wClass.LastIndexOf(".0.") + 3);
                        data.hWnd = hWnd;
                        return false;    // Found the wnd, halt enumeration
                    }
                }
                else
                {
                    if (data.Title == "" && data.Wndclass == "")//all windows (collection mode)
                    {
                        if (wTitle != "") //adding to list only windows
                        {
                            if (UIApp.IsValidUIWnd(hWnd))
                            {
                                var w = new WindowObjInfo
                                {
                                    hWnd = hWnd,
                                    Title = wTitle,
                                    Wndclass = wClass
                                };
                                wino.Add(w);
                            }

                        }

                        return true; //keep enumerating...

                    }


                    //if (data.Title != "")
                    //{
                    //	data.Wndclass = wClass;
                    //	data.hWnd = hWnd;
                    //}

                    return false;    // Found the wnd, halt enumeration

                }


            }

            return true;//keep 'em coming...

        }

		private void chkSnap_CheckedChanged(object sender, EventArgs e)
		{
            if (chkSnap.Checked && snaps.Count>0)
            {
                this.Width = 545;
            }

            if (!chkSnap.Checked || snaps.Count == 0)
            {
                this.Width = 334;
            }
		}

		private void chkAnon_CheckedChanged(object sender, EventArgs e)
		{
            if (chkAnon.Checked)
            {
                txtAGE.Text = "";
                txtMRN.Text = "";
               
            }

            txtAGE.Enabled = chkAnon.Checked;
            txtMRN.Enabled = chkAnon.Checked;
        }

		private void btnAddScreenshot_Click(object sender, EventArgs e)
		{
            SelectArea fSA = new SelectArea( );
            //fSA.frm = this;
            fSA.Show();
        }

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{   
            if (listView1.SelectedIndices.Count > 0 && !deleteInProgress)
            {
                int imgIndex = listView1.SelectedIndices[0];
                var bmp = snaps[imgIndex];
                var frmImg = new ScreenshotForm(bmp, imgIndex);
                //frmImg.frm = this;
                frmImg.StartPosition = FormStartPosition.CenterScreen;
                frmImg.ShowDialog();

            }
            
        }
	}
}
