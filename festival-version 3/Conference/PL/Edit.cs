using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conference.PL
{
    public partial class Edit : Form
    {
        public Edit()
        {
            InitializeComponent();
        }

        
        DAL.Person obj = new DAL.Person();

        private void btnsearch_barcode_Click(object sender, EventArgs e)
        {
            
            DataTable dt = obj.DAL_serach_barcode(txtBarcode.Text);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "عنوان";
                dataGridView1.Columns[2].HeaderText = "نام";
                dataGridView1.Columns[3].HeaderText = "نام خانوادگی";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                    row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
                dataGridView1.Rows[0].Selected = false;
            }
            else
            {
                MessageBox.Show(".اطلاعات وارد شده توسط شما در پایگاه داده موجود نیست","توجّه");
            }


        }

        private void btnSearch_name_Click(object sender, EventArgs e)
        {
            DataTable dt = obj.DAL_serach_name(txtName.Text);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "عنوان";
                dataGridView1.Columns[2].HeaderText = "نام";
                dataGridView1.Columns[3].HeaderText = "نام خانوادگی";
                foreach(DataGridViewRow row in dataGridView1.Rows)
                    row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
                dataGridView1.Rows[0].Selected = false;
                
          
                
            }
            else
            {
                MessageBox.Show(".اطلاعات وارد شده توسط شما در پایگاه داده موجود نیست", "توجّه");
            }

        }

        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                MessageBox.Show(".هیچ موردی انتخاب نشده است", "!توجّه");
            else
            {
                int idp = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                SelectedEdit frm = new SelectedEdit(idp);
                frm.ShowDialog();
                if (frm.par)
                {
                    DAL.Person obj = new DAL.Person();
                    DataTable dt = obj.DAL_selectONE(idp);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnsearch_barcode_Click(this,new EventArgs());
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnSearch_name_Click(this,new EventArgs());
        }

        
    }
}
