using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Conference.BL
{
    public class BL_User
    {
        public static string BL_Login(string username, string password)
        {
            SqlDataAdapter sd = new SqlDataAdapter("select code from t_user where username=N'" + username + "' AND password=N'" + password + "'", BL_title.sc);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            string code="0";
            if (dt.Rows.Count > 0)
                code = dt.Rows[0][0].ToString();
            return code;

        }
    }
}
