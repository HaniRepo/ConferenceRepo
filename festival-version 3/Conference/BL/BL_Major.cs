using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Conference.BL
{
    public static class BL_Major
    {
        public static string[] BL_Major_SelectAll()
        {
            SqlDataAdapter sd = new SqlDataAdapter("exec p_major_selectall", BL_title.sc);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            string[] arr = new string[dt.Rows.Count];
            int i;
            for (i = 0; i < dt.Rows.Count; i++)
                arr[i] = dt.Rows[i][0].ToString();
            return arr;
        }

        public static bool BL_IsDr(string major)
        {
            SqlDataAdapter sd = new SqlDataAdapter("exec p_IsDr N'"+major+"'",BL_title.sc);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            if (dt.Rows.Count > 0)
                return (bool)(dt.Rows[0][0]);
            else
                return false;
        }
    }
}
