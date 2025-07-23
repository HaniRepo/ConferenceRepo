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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string User=txtUser.Text;
            string Pass = txtPass.Text;
            DAL.DAL_user obj = new DAL.DAL_user();
            string usercode = obj.DAL_Login(User, Pass);
            if (usercode=="0")
            {
                MessageBox.Show(".اطلاعات وارد شده معتبر نيست");
                txtUser.Text = txtPass.Text = "";
                txtUser.Focus(); 
            }
            else
            {
                Form_Book frm = new Form_Book(usercode);
                frm.Show();
                this.Hide();


            }

        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtPass.Focus();
           
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnLogin_Click(this, new EventArgs());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
    }
}
