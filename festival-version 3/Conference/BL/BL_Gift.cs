using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace Conference.BL
{
    public static class BL_Gift
    {
        public static string BL_Gift_insert(string code, string gdate, string gtime)
        {
            SqlDataAdapter sd = new SqlDataAdapter("exec p_gift_check '" + code + "'", BL_title.sc);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString() + " -- " + dt.Rows[0][1].ToString();
            else
            {
                string flag="0";
                SqlCommand scom = new SqlCommand("exec p_gift '" + code + "','" + gdate + "',N'" + gtime + "'", BL_title.sc);
                try
                {
                    BL_title.sc.Open();
                    scom.ExecuteNonQuery();
                    flag = "1";

                }
                catch {
                    
                }

                finally
                {
                    if (BL_title.sc.State == System.Data.ConnectionState.Open)
                        BL_title.sc.Close();
                }
                return flag;
            }
        }
    }
}
