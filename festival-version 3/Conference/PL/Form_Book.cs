using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace Conference
{
    public partial class Form_Book : Form
    {
        string usercode;
        public Form_Book(string _usercode)
        {
            InitializeComponent();
            usercode = _usercode;
        }



        //----------------------
        public class Demo : IDisposable
        {
            private int m_currentPageIndex;
            private IList<Stream> m_streams;

            //private DataTable LoadSalesData()
            //{
            //    // Create a new DataSet and read sales data file 
            //    //    data.xml into the first DataTable.
            //    DataSet dataSet = new DataSet();
            //    dataSet.ReadXml(@"..\..\data.xml");
            //    return dataSet.Tables[0];
            //}
            // Routine to provide to the report renderer, in order to
            //    save an image for each page of the report.
            private Stream CreateStream(string name,
              string fileNameExtension, Encoding encoding,
              string mimeType, bool willSeek)
            {
                Stream stream = new MemoryStream();
                m_streams.Add(stream);
                return stream;
            }
            // Export the given report as an EMF (Enhanced Metafile) file.
            private void Export(LocalReport report)
            {
                string deviceInfo =
                  @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";
                Warning[] warnings;
                m_streams = new List<Stream>();
                try
                {
                    report.Render("Image", deviceInfo, CreateStream, out warnings);
                }
                catch (Exception ex)
                {
                    Exception inner = ex.InnerException;
                    while (!(inner ==null))
                    {
                        MessageBox.Show(inner.Message);
                        inner = inner.InnerException;
                    }
                }
                foreach (Stream stream in m_streams)
                    stream.Position = 0;
            }
            // Handler for PrintPageEvents
            private void PrintPage(object sender, PrintPageEventArgs ev)
            {
                Metafile pageImage = new
                   Metafile(m_streams[m_currentPageIndex]);

                // Adjust rectangular area with printer margins.
                Rectangle adjustedRect = new Rectangle(
                    ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                    ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                    ev.PageBounds.Width,
                    ev.PageBounds.Height);

                // Draw a white background for the report
                ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

                // Draw the report content
                ev.Graphics.DrawImage(pageImage, adjustedRect);

                // Prepare for the next page. Make sure we haven't hit the end.
                m_currentPageIndex++;
                ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
            }

            private void Print()
            {
                if (m_streams == null || m_streams.Count == 0)
                    throw new Exception("Error: no stream to print.");
                PrintDocument printDoc = new PrintDocument();
                if (!printDoc.PrinterSettings.IsValid)
                {
                    throw new Exception("Error: cannot find the default printer.");
                }
                else
                {
                    printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                    m_currentPageIndex = 0;
                    printDoc.Print();
                }
            }
            // Create a local report for Report.rdlc, load the data,
            //    export the report to an .emf file, and print it.
            public void Run()
            {
                LocalReport report = new LocalReport();
                report.ReportPath = @"..\..\Report\CardReport.rdlc";
                ReportParameter rpName = new ReportParameter("rpName", "سلام");
                ReportParameter rpTitle = new ReportParameter("rpTitle", "من");
                ReportParameter rpBarcode = new ReportParameter("rpBarcode", "*" + "4564545546" + "*");
                ReportParameter rpCode = new ReportParameter("rpCode", "45456456456");
                ReportParameter rpisVIP = new ReportParameter("rpisVIP", "وی آی پی");
                try
                {
                    report.SetParameters(rpName);
                    report.SetParameters(rpTitle);
                    report.SetParameters(rpBarcode);
                    report.SetParameters(rpCode);
                    report.SetParameters(rpisVIP);
                }
                catch (Exception ex)
                {
                    Exception inner = ex.InnerException;
                    while (!(inner == null))
                    {
                        MessageBox.Show(inner.Message);
                        inner = inner.InnerException;
                    }
                }

                
                report.Refresh();
                //report.DataSources.Add(
                //   new ReportDataSource("Sales", LoadSalesData()));
                Export(report);
                Print();
            }

            public void Dispose()
            {
                if (m_streams != null)
                {
                    foreach (Stream stream in m_streams)
                        stream.Close();
                    m_streams = null;
                }
            }

        }
        //----------------------
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form_Book_Load(object sender, EventArgs e)
        {
            txtcode.Focus();
            DAL.DAL_title obj_title = new DAL.DAL_title();
            string[] titles = obj_title.DAL_fill_title();
            Title.Items.Add("");
            Title.DataSource = titles;
            DAL.DAL_Major obj_major = new DAL.DAL_Major();
            string[] majors = obj_major.DAL_Major_SelectAll();
            combomajor.Items.Add("مدرک تحصيلی را انتخاب کنيد");
            for (int i = 0; i < majors.Length; i++)
                combomajor.Items.Add(majors[i]);
            combomajor.SelectedIndex = 0;
            DAL.DAL_Grade obj_grade = new DAL.DAL_Grade();
            string[] grades = obj_grade.DAL_Grade_SelectALL();
            combograde.Items.Add("رشته تحصيلی را انتخاب کنيد");
            for (int i = 0; i < grades.Length; i++)
                combograde.Items.Add(grades[i]);
            combograde.SelectedIndex = 0;
            

            
            


        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (txtname.Text == "" || combograde.Text == "" || combomajor.Text == "" || Title.Text == "" || combomajor.SelectedIndex == 0||combograde.SelectedIndex==0)
            {
                MessageBox.Show(".لطفاً اطلاعات را به صورت کامل وارد نماييد");
            }
            else if (txtcode.TextLength != 10)
            {
                MessageBox.Show(".کد ملی وارد شده بایستی ده رقمی باشد");
            }
            else if (txtmobile.TextLength != 11)
            {
                MessageBox.Show(".شماره موبایل وارد شده صحیح نمیباشد");
            }
            else
            {
                PersianCalendar pc = new PersianCalendar();
                DateTime miladi = DateTime.Now;
                string date = pc.GetYear(miladi) + "/" + pc.GetMonth(miladi) + "/" + pc.GetDayOfMonth(miladi);
                string time = System.DateTime.Now.ToLongTimeString();
                DAL.Person obj = new DAL.Person();
                obj.code = txtcode.Text;
                if (chkvip.Checked)
                    obj.vip = true;
                else
                    obj.vip = false;
                obj.name = txtname.Text;
                obj.ename = txtename.Text;
                obj.grade = combograde.Text;
                obj.major = combomajor.Text;
                obj.telephone = txttelephone.Text;
                obj.mobile = txtmobile.Text;
                obj.email = txtemail.Text;
                obj.position = txtposition.Text;
                obj.title = Title.Text;
                obj.pdate = date;
                obj.ptime = time;
                bool flag=false;
                if (isExist)
                { }
                else
                {
                    flag = obj.DAL_Insert(obj);
                }

                // تعيين دکتر بودن يا نبودن
                string name = txtname.Text; ;
                string ename = txtename.Text;
                string etitle = "";
                DAL.DAL_Major obj_isdr = new DAL.DAL_Major();
                if (obj_isdr.DAL_isdr(combomajor.Text) == true)
                {
                    name = " دکتر" + txtname.Text;
                    if (ename != "")
                    {
                        ename = "Dr. " + ename;
                        //تعيين عنوان انگليسی
                        DAL.DAL_title obj_etitle = new DAL.DAL_title();
                        string title = Title.Text;
                        etitle = obj_etitle.DAL_etitle(title);
                        //-------------------
                    }
                    else
                    {
                        ename = name;
                        etitle = Title.Text;
                    }
                }
                else
                {
                    if (ename != "")
                    {

                        //تعيين عنوان انگليسی
                        DAL.DAL_title obj_etitle = new DAL.DAL_title();
                        string title = Title.Text;
                        etitle = obj_etitle.DAL_etitle(title);
                        //-------------------


                    }
                    else
                    {
                        ename = name;
                        etitle = Title.Text;
                    }
                }

                    //---------------------------------



                    if (!flag)
                    {
                        MessageBox.Show(".اطلاعات وارد شده در سيستم ثبت نشد. لطفاً اطلاعات را مجدداً وارد نماييد");
                        Title.Focus();

                    }
                    else
                    {
                        DialogResult di = MessageBox.Show("آيا مايل به چاپ کارت شرکت کننده هستيد؟", "", MessageBoxButtons.YesNo);
                        if (di == DialogResult.Yes)
                        {
                            string VIP = "";
                            if (obj.vip)
                                VIP = "V.I.P";
                            else
                                VIP = "";



                            PL.CardReport frm = new PL.CardReport(name, obj.title, obj.code, VIP, ename, etitle);
                            frm.ShowDialog();
                        }

                        txtcode.Text = "";
                        txtname.Text = "";
                        txtename.Text = "";
                        combograde.Text = "";
                        combomajor.SelectedIndex = 0;
                        txttelephone.Text = "";
                        txtmobile.Text = "";
                        txtemail.Text = "";
                        txtposition.Text = "";
                        Title.Text = "";
                        chkvip.Checked = false;

                        txtcode.Focus();
                        lblMessage.Text = ".مشخصات با موفقيت در سيستم ثبت شد";
                        // ثبت انجام شده
                        // سایر عملیات

                        //--------------------------------

                        //using (Demo demo = new Demo())
                        //{
                        //    demo.Run();
                        //}

                        //--------------------------------


                    }
                

            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            PL.Edit frm = new PL.Edit();
            frm.ShowDialog();

        }

        private void Form_Book_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Title_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtname.Focus();
        }

        private void txtname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtcode.Focus();
        }

       

        private void rbexit_CheckedChanged(object sender, EventArgs e)
        {

            
            txtexit.Enabled = true;
            btnexit.Enabled = true;
            txtenter.Enabled = false;
            btnenter.Enabled = false;
        }

        private void rbenter_CheckedChanged(object sender, EventArgs e)
        {
            txtexit.Enabled = false;
            btnexit.Enabled = false;
            txtenter.Enabled = true;
            btnenter.Enabled = true;
        }

        private void btnenter_Click(object sender, EventArgs e)
        {
            if (txtenter.TextLength == 10)
                sabte_vorod_lunch();
            else
                MessageBox.Show("کد ملی بايستی ده رقمی باشد.");
        }

        private void sabte_vorod_lunch()
        {
            lblEnter.Text = "";
            lblExit.Text = "";
            string code = txtenter.Text;
            PersianCalendar pc = new PersianCalendar();
            DateTime miladi = DateTime.Now;
            string ldate = pc.GetYear(miladi) + "/" + pc.GetMonth(miladi) + "/" + pc.GetDayOfMonth(miladi);
            string time_enter = System.DateTime.Now.ToLongTimeString();
            DAL.DAL_lunch obj = new DAL.DAL_lunch();
            string check= obj.DAL_lunch_insert(code, ldate, time_enter);
            if (check=="1")
            {
                lblEnter.ForeColor = Color.Green;
                lblEnter.Text = "ورود بلامانع است.";
                txtenter.Text = "";
                txtenter.Focus();
            }
            else if (check == "2")
            {

                MessageBox.Show("خطا در پايگاه داده");
            }
            else
            {
                lblEnter.ForeColor = Color.Red;
                lblEnter.Text = "پذيرايي قبلا انجام شده";
                
                Console.Beep(3000, 500);
                Console.Beep(3000, 500);
                Console.Beep(3000, 2000);
                string message = " ورود شما در سالن غذاخوری در ساعت زير ثبت شده است " +"\r\n       "+ check;
                MessageBox.Show(message);
            }

        }

        private void sabte_khorooj_lunch()
        {
            txtenter.Text = "";
            lblEnter.Text = "";
            lblExit.Text = "";
            string code = txtexit.Text;
            PersianCalendar pc = new PersianCalendar();
            DateTime miladi = DateTime.Now;
            string ldate = pc.GetYear(miladi) + "/" + pc.GetMonth(miladi) + "/" + pc.GetDayOfMonth(miladi);
            string time_exit = System.DateTime.Now.ToLongTimeString();
            DAL.DAL_lunch obj = new DAL.DAL_lunch();
            bool flag = obj.DAL_lunch_exit(code, ldate, time_exit);
            if (flag)
            {
                lblExit.Text = "خروج با موفقیت ثبت شد";
                lblExit.ForeColor = Color.Green;
                txtexit.Text = "";
                txtexit.Focus();
            }
            else {
                lblExit.Text = "ورود این شرکت کننده به سالن ثبت نشده است";
                lblExit.ForeColor = Color.Red;
                txtexit.Text = "";
                txtexit.Focus();
            }

        
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }

        private void btnPrintCard_Click(object sender, EventArgs e)
        {
            PL.SelectPersonForm frm = new PL.SelectPersonForm();
            frm.ShowDialog();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void combomajor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void combograde_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtmobile_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtenter_TextChanged(object sender, EventArgs e)
        {
            
            if (txtenter.TextLength == 10)
                sabte_vorod_lunch();
           
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void txtexit_TextChanged(object sender, EventArgs e)
        {
            if (txtexit.TextLength == 10)
                sabte_khorooj_lunch();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            if (txtexit.TextLength == 10)
                sabte_khorooj_lunch();
            else
                MessageBox.Show("کد ملی باید ده رقمی باشد");
        }

        private void btnRefresh_Lunch_Click(object sender, EventArgs e)
        {
            DAL.DAL_lunch obj = new DAL.DAL_lunch();
            PersianCalendar pc = new PersianCalendar();
            DateTime miladi = DateTime.Now;
            string ldate = pc.GetYear(miladi) + "/" + pc.GetMonth(miladi) + "/" + pc.GetDayOfMonth(miladi);
            int count = obj.DAL_Lunch_Refresh(ldate);
            lblLunchPresent.Text = count.ToString();
        }

        private void rbConferenceHallEnter_CheckedChanged(object sender, EventArgs e)
        {
            txtConferenceEnter.Enabled = true;
            btnConferenceEnter.Enabled = true;
            txtConferenceExit.Enabled = false;
            btnConferenceExit.Enabled = false;
        }

        private void rbConferenceHallExit_CheckedChanged(object sender, EventArgs e)
        {
            txtConferenceEnter.Enabled = false;
            btnConferenceEnter.Enabled = false;
            txtConferenceExit.Enabled = true;
            btnConferenceExit.Enabled = true;
        }

        private void btnConferenceEnter_Click(object sender, EventArgs e)
        {
            if (txtConferenceEnter.TextLength == 10)
                Hall_Sabte_Vorood();
            else
            {
                MessageBox.Show("بارکد به درستي وارد نشده است");
            }
        }

        private void Hall_Sabte_Vorood()
        {
            lblConferenceExit.Text = "";
            lblConferenceEnter.Text = "";
            PersianCalendar pc = new PersianCalendar();
            DateTime miladi = DateTime.Now;
            string hdate = pc.GetYear(miladi) + "/" + pc.GetMonth(miladi) + "/" + pc.GetDayOfMonth(miladi);
            string time_enter = System.DateTime.Now.ToLongTimeString();
            string code=txtConferenceEnter.Text;
            DAL.DAL_ConferenceHall obj = new DAL.DAL_ConferenceHall();
            bool flag = obj.DAL_ConferenceHall_Enter(code, hdate, time_enter);
            if (flag)
            {
                lblConferenceEnter.Text = "ورود با موفقیت ثبت شد";
                lblConferenceEnter.ForeColor = Color.Green;
                txtConferenceEnter.Text = "";
                txtConferenceEnter.Focus();
            }
            else
            {
                MessageBox.Show("ثبت داده انجام نگرفت");
                txtConferenceEnter.Text = "";
                txtConferenceEnter.Focus();
            }

        }

        private void txtConferenceEnter_TextChanged(object sender, EventArgs e)
        {
            if (txtConferenceEnter.TextLength == 10)
                Hall_Sabte_Vorood();
        }

        private void btnConferenceExit_Click(object sender, EventArgs e)
        {
            if (txtConferenceExit.TextLength == 10)
                Hall_sabte_khorooj();
            else
                MessageBox.Show("بارکد به درستي وارد نشده است");
        }

        private void Hall_sabte_khorooj()
        {
            lblConferenceEnter.Text = "";
            lblConferenceExit.Text = "";
            PersianCalendar pc = new PersianCalendar();
            DateTime miladi = DateTime.Now;
            string hdate = pc.GetYear(miladi) + "/" + pc.GetMonth(miladi) + "/" + pc.GetDayOfMonth(miladi);
            string time_exit = System.DateTime.Now.ToLongTimeString();
            string code = txtConferenceExit.Text;
            DAL.DAL_ConferenceHall obj = new DAL.DAL_ConferenceHall();
            bool flag = obj.DAL_ConferenceHall_Exit(code, hdate, time_exit);
            if (flag)
            {
                lblConferenceExit.Text = "خروج با موفقیت ثبت شد";
                lblConferenceExit.ForeColor = Color.Green;
                txtConferenceExit.Text = "";
                txtConferenceExit.Focus();
            }
            else
            {
                lblConferenceExit.Text = "ورود شرکت کننده قبلا ثبت نشده است";
                lblConferenceExit.ForeColor = Color.Red;
                txtConferenceExit.Text = "";
                txtConferenceExit.Focus();
            }

        }

        private void txtConferenceExit_TextChanged(object sender, EventArgs e)
        {
            if (txtConferenceExit.TextLength == 10)
                Hall_sabte_khorooj();
        }

        private void btnConferenceRefresh_Click(object sender, EventArgs e)
        {
            DAL.DAL_ConferenceHall obj = new DAL.DAL_ConferenceHall();
            PersianCalendar pc = new PersianCalendar();
            DateTime miladi = DateTime.Now;
            string hdate = pc.GetYear(miladi) + "/" + pc.GetMonth(miladi) + "/" + pc.GetDayOfMonth(miladi);
            int count = obj.DAL_ConferenceHall_Refresh(hdate);
            lblConferenceRefresh.Text = count.ToString();

        }

        private void btnGift_Click(object sender, EventArgs e)
        {
            if (txtGift.TextLength == 10)
                sabte_gift();
            else
                MessageBox.Show("بارکد به درستی وارد نشده");
        }
        private void sabte_gift()
        {
            lblGift.Text="";
            string code=txtGift.Text;
            DAL.DAL_Gift obj_gift = new DAL.DAL_Gift();
            PersianCalendar pc = new PersianCalendar();
            DateTime miladi = DateTime.Now;
            string gdate = pc.GetYear(miladi) + "/" + pc.GetMonth(miladi) + "/" + pc.GetDayOfMonth(miladi);
            string gtime= System.DateTime.Now.ToLongTimeString();
            string flag = obj_gift.DAL_Gift_Insert(code, gdate, gtime);
            if (flag == "1")
            {
                lblGift.ForeColor = Color.Green;
                lblGift.Text = "تحويل هديه بلامانع است";
                txtGift.Text = "";
                txtGift.Focus();
            }
            else if (flag == "0")
            {
                MessageBox.Show("خطای پايگاه داده");
                txtGift.Text = "";
                txtGift.Focus();
            }
            else
            {
                lblGift.ForeColor = Color.Red;
                lblGift.Text = "هديه قبلا تحويل داده شده";

                Console.Beep(3000, 500);
                Console.Beep(3000, 500);
                Console.Beep(3000, 2000);
                string message = "شرکت کننده عزيز تحويل هديه به شما در زمان زير صورت گرفته است" + "\r\n               " + flag;
                MessageBox.Show(message);
                txtGift.Text = "";
            }

            

        }

        private void txtGift_TextChanged(object sender, EventArgs e)
        {
            if (txtGift.TextLength == 10)
                sabte_gift();
        }

        private void btnCertificate_Click(object sender, EventArgs e)
        {
            PL.CertificatePreview frm = new PL.CertificatePreview();
            frm.ShowDialog();
        }
        bool isExist = false;
        private void txtcode_Leave(object sender, EventArgs e)
        {
            string code = txtcode.Text;
            DAL.Person obj_form_code_search = new DAL.Person();
            DataTable dt = obj_form_code_search.DAL_serach_barcode(code);
            if(dt.Rows.Count>0)
            {
                isExist = true;
                txtname.Text = dt.Rows[0][0].ToString();
                txtename.Text = dt.Rows[0][1].ToString();
                Title.Text = dt.Rows[0][3].ToString();
                combograde.Text = dt.Rows[0][4].ToString();
                combomajor.Text = dt.Rows[0][5].ToString();
                txtmobile.Text = dt.Rows[0][6].ToString();
                txttelephone.Text = dt.Rows[0][7].ToString();
                txtemail.Text = dt.Rows[0][8].ToString();
                txtposition.Text = dt.Rows[0][9].ToString();
                chkvip.Checked = (bool)(dt.Rows[0][10]);
                
            }
        }

        

       

     
    }
}
