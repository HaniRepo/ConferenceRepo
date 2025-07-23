using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.DAL
{
   public class DAL_title
    {
       private int _idt;
       public int idt
       {
           get { return _idt; }
           set { _idt = value; }
       }


       private string _title;
       public string title
       {
           get { return _title; }
           set { _title = value; }
       }


       public string[] DAL_fill_title()
       {
           return BL.BL_title.BL_Fill_title();       
       }
       public string DAL_etitle(string title)
       {
           return BL.BL_title.BL_etitle(title);
       }
       
    }
}
