using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conference
{
    public partial class SelectedEdit : Form
    {
        int idp;
        public SelectedEdit(int _idp)
        {
            idp = _idp;
            InitializeComponent();
        }
        public bool par
        {
            get { return flag; }
        }
        bool flag = false;
        private void btnInsert_Click(object sender, EventArgs e)
        {
            string title1 = Title.Text;
            string name=txtname.Text;
            string family=txtfamily.Text;
            DAL.Person obj = new DAL.Person();
            //flag = obj.DAL_Update(idp, name, title1);
            if(!flag)
                MessageBox.Show(".اصلاح انجام نگرفت. لطفاً دوباره سعی کنید","!توجّه");
            else
            {
                this.Close();
            }

        }

        private void SelectedEdit_Load(object sender, EventArgs e)
        {
            DAL.DAL_title obj = new DAL.DAL_title();
            string[] titles = obj.DAL_fill_title();
            for (int i = 0; i < titles.Length; i++)
                Title.Items.Add(titles[i]);
            DAL.Person obj2 = new DAL.Person();
            DataTable dt = obj2.DAL_selectONE(idp);
            Title.SelectedText = dt.Rows[0][1].ToString();
            txtname.Text = dt.Rows[0][2].ToString();
            txtfamily.Text = dt.Rows[0][3].ToString();

        }
    }
}
