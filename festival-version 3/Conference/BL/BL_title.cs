using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Conference.BL
{
    public static class BL_title
    {
       public static SqlConnection sc = new SqlConnection("Data Source=.;Initial Catalog=Conference;Integrated Security=True");
        public static string[] BL_Fill_title()
        {
            SqlDataAdapter sd = new SqlDataAdapter("exec p_title",sc);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            string[] arr =new string[dt.Rows.Count];;
            if (dt.Rows.Count > 0)
            {
                int i;
                for (i = 0; i < arr.Count(); i++)
                    arr[i] = (dt.Rows[i][0]).ToString();

            }

            return arr;
        }

        public static string BL_etitle(string title)
        {
            SqlDataAdapter sd = new SqlDataAdapter("exec p_etitle N'"+title+"'",sc);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }

    }
}
