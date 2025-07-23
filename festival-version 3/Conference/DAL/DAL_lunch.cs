using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.DAL
{
    public class DAL_lunch
    {
        public string  DAL_lunch_insert(string code, string ldate, string time_enter)
        {
            return BL.BL_Lunch.BL_Lunch_Insert(code, ldate, time_enter);
        }

        public bool DAL_lunch_exit(string code, string ldate, string time_exit)
        {
            return BL.BL_Lunch.BL_Lunch_Exit(code, ldate, time_exit);
        }

        public int DAL_Lunch_Refresh(string ldate)
        {
            return BL.BL_Lunch.BL_Lunch_Refresh(ldate);
        }

    }
}
