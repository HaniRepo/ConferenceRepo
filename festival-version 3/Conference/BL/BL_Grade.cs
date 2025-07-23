using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Conference.BL
{
    public static class BL_Grade
    {
        public static string[] BL_Grade_SelectAll()
        {
            SqlDataAdapter sd = new SqlDataAdapter("exec p_grade", BL_title.sc);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            string[] arr = new string[dt.Rows.Count];
            int i;
            for (i = 0; i < dt.Rows.Count; i++)
                arr[i] = dt.Rows[i][0].ToString();
            return arr;
        }
    }
}
