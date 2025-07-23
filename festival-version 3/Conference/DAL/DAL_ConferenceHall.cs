using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.DAL
{
    public class DAL_ConferenceHall
    {
        public bool DAL_ConferenceHall_Enter(string code, string hdate, string time_enter)
        {
            return BL.BL_ConferenceHall.BL_ConferenceHall_Enter(code, hdate, time_enter);
        }
        public bool DAL_ConferenceHall_Exit(string code, string hdate, string time_exit)
        {
            return BL.BL_ConferenceHall.BL_ConferenceHall_exit(code, hdate, time_exit);
        }
        public int DAL_ConferenceHall_Refresh(string hdate)
        {
            return BL.BL_ConferenceHall.BL_ConferenceHall_Refresh(hdate);
        }

    }
}
