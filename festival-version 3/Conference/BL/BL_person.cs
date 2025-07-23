using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Conference.BL
{
  public static  class BL_person
    {
      public static DataTable BL_search_barcode(string barcode)
      {
          SqlDataAdapter sd = new SqlDataAdapter("p_Search_Code '"+barcode+"'", BL_title.sc);
          DataTable dt = new DataTable();
          sd.Fill(dt);
          return dt;

      }

      public static DataTable BL_search_name(string name)
      {
          SqlDataAdapter sd = new SqlDataAdapter("select idp,title,name from t_person where name like N'" + name + "%'  ", BL_title.sc);
          DataTable dt = new DataTable();
          sd.Fill(dt);
          return dt;

      }

     

      public static bool BL_Insert(DAL.Person obj)
      {
          
          int idp = 1;
          SqlDataAdapter sd = new SqlDataAdapter("select MAX(idp) from t_person ",BL_title.sc);
          DataTable dt=new DataTable();
          sd.Fill(dt);
          if (dt.Rows.Count > 0)
              idp = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0])+1;

          SqlCommand scom = new SqlCommand("exec p_person_insert "+idp+",'"+obj.code+"',"+obj.vip+",N'"+obj.name+"','"+obj.ename+"',"+"N'"+obj.grade+"',N'"+obj.major+"','"+obj.telephone+"','"+obj.mobile+"','"+obj.email+"',N'"+obj.position+"',N'"+obj.title+"','"+obj.pdate+"',N'"+obj.ptime+"'", BL_title.sc);
          bool flag=true;
          try
          {
              BL_title.sc.Open();
              scom.ExecuteNonQuery();


          }
          catch (Exception ex)
          {
              string message = ex.Message;
              flag = false;

          }
          finally
          {
              if (BL_title.sc.State == ConnectionState.Open)
                  BL_title.sc.Close();
          }
          return flag;

      }

      public static DataTable BL_selectONE(int idp)
      {
          SqlDataAdapter sd = new SqlDataAdapter("select idp,title,name from t_person where idp="+idp+"", BL_title.sc);
          DataTable dt = new DataTable();
          sd.Fill(dt);
          return dt;
      }

      //public static bool BL_Update(DAL.Person obj)
      //{
      //    SqlCommand scom = new SqlCommand("UPDATE t_person SET title=N'" + title + "',name=N'" + name + "' WHERE idp=" + idp + ";", BL_title.sc);
      //    bool flag = true;
      //    try
      //    {
      //        BL_title.sc.Open();
      //        scom.ExecuteNonQuery();
             
      //    }
      //    catch
      //    {
      //        flag = false;
      //    }
      //    finally
      //    {
      //        if (BL_title.sc.State == ConnectionState.Open)
      //            BL_title.sc.Close();
      //    }
      //    return flag;
      //}

      public static DataTable BL_SelectALL()
      {
          SqlDataAdapter sd = new SqlDataAdapter("EXEC p_person_selectALL;", BL_title.sc);
          DataTable dt = new DataTable();
          sd.Fill(dt);
          return dt;
      }

     

    }
}
