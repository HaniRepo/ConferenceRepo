using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;


namespace Conference.PL
{
    public partial class SelectPersonForm : Form
    {
        //      
        public SelectPersonForm()
        {
            InitializeComponent();
        }

        private void SelectPersonForm_Load(object sender, EventArgs e)
        {
            DAL.Person obj = new DAL.Person();
            dgvSelectPerson.DataSource = obj.DAL_SelectALL();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string name = dgvSelectPerson.SelectedRows[0].Cells[0].Value.ToString();
            string ename = dgvSelectPerson.SelectedRows[0].Cells[1].Value.ToString();
            string code = dgvSelectPerson.SelectedRows[0].Cells[2].Value.ToString();
            string title = dgvSelectPerson.SelectedRows[0].Cells[3].Value.ToString();
            bool isVip = (bool)(dgvSelectPerson.SelectedRows[0].Cells[10].Value);
            string VIP = "";
            if (isVip)
                VIP = "V.I.P";
            // تعيين دکتر بودن يا نبودن
            
            string major = dgvSelectPerson.SelectedRows[0].Cells[5].Value.ToString();
            string etitle = "";
            DAL.DAL_Major obj_isdr = new DAL.DAL_Major();
            if (obj_isdr.DAL_isdr(major))
            {
                name = " دکتر " + name;
                if (ename != "")
                {
                    ename = "Dr. " + ename;
                    //تعيين عنوان انگليسی
                    DAL.DAL_title obj_etitle = new DAL.DAL_title();

                    etitle = obj_etitle.DAL_etitle(title);
                    //-------------------
                }
                else
                {
                    ename = name;
                    etitle = title;
                }
            }
            else
            {
                if (ename != "")
                {

                    //تعيين عنوان انگليسی
                    DAL.DAL_title obj_etitle = new DAL.DAL_title();

                    etitle = obj_etitle.DAL_etitle(title);
                    //-------------------


                }
                else
                {
                    ename = name;
                    etitle = title;
                }
            }
            

            
            //---------------------------------

            CardReport frm = new CardReport(name, title, code, VIP,ename,etitle);
            frm.ShowDialog();

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.TextLength == 10)
            {
                DAL.Person obj_search = new DAL.Person();
                dgvSelectPerson.DataSource = obj_search.DAL_serach_barcode(txtSearch.Text);
            }
        }
    }
}
