using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Conference.BL
{
    public static class BL_Lunch
    {
        public static string BL_Lunch_Insert(string code, string ldate, string enter_time)
        {
            SqlDataAdapter sd = new SqlDataAdapter("exec p_lunch_verification '" + code + "','" + ldate + "'", BL_title.sc);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            if (dt.Rows.Count > 0)
                return (dt.Rows[0][0]).ToString();
            else
            {
                SqlCommand scom = new SqlCommand("exec P_lunch_insert '" + code + "','" + ldate + "',N'" + enter_time + "'", BL_title.sc);
                try
                {
                    BL_title.sc.Open();
                    scom.ExecuteNonQuery();
                    
                    return "1";
                }
                catch
                {
                    return "2";
                }
                finally
                {
                    if (BL_title.sc.State == ConnectionState.Open)
                        BL_title.sc.Close();
                }


            }
        }

        public static bool BL_Lunch_Exit(string code, string ldate, string exit_time)
        {
            bool flag = false;
            SqlCommand scom = new SqlCommand("EXEC p_lunch_exit '" + code + "','" + ldate + "',N'" + exit_time + "'", BL_title.sc);
            try {
                BL_title.sc.Open();
                int numberofRecords= scom.ExecuteNonQuery();
                if(numberofRecords!=0)
                    flag = true;
                
            }

            catch
            {
                
            }
            finally
            {
                if (BL_title.sc.State == ConnectionState.Open)
                    BL_title.sc.Close();
            }
            return flag;
        }

        public static int BL_Lunch_Refresh(string ldate)
        {
            SqlDataAdapter sd = new SqlDataAdapter("EXEC p_lunch_refresh '"+ldate+"'", BL_title.sc);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
    }
}
