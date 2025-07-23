using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Conference.BL
{
    public static class BL_ConferenceHall
    {
        public static bool BL_ConferenceHall_Enter(string code,string hdate,string time_enter)
        {
            bool flag=false;
            SqlCommand scom=new SqlCommand("EXEC p_hall_insert '"+code+"','"+hdate+"',N'"+time_enter+"'",BL_title.sc);
            try
            {
                BL_title.sc.Open();
                scom.ExecuteNonQuery();
                flag=true;
                
            }
            catch
            {}
            finally
            {
                if (BL_title.sc.State == ConnectionState.Open)
                    BL_title.sc.Close();
            }
            return flag;
        }

        public static bool BL_ConferenceHall_exit(string code, string hdate, string time_exit)
        {
            bool flag = false;
            SqlCommand scom = new SqlCommand("EXEC p_hall_exit '" + code + "','" + hdate + "',N'" + time_exit + "'", BL_title.sc);
            try
            {
                BL_title.sc.Open();
                int numofRecords= scom.ExecuteNonQuery();
                if(numofRecords==1) 
                    flag = true;
                
            }

            catch { }
            finally
            {
                if (BL_title.sc.State == ConnectionState.Open)
                    BL_title.sc.Close();
            }
            return flag;
        }

        public static int BL_ConferenceHall_Refresh(string hdate)
        {
            SqlDataAdapter sd = new SqlDataAdapter("EXEC p_hall_refresh '" + hdate + "'", BL_title.sc);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
    }
}
