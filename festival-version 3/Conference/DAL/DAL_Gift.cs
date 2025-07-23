using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.DAL
{
    public class DAL_Gift
    {
        public string DAL_Gift_Insert(string code, string gdate, string gtime)
        {
            return BL.BL_Gift.BL_Gift_insert(code, gdate, gtime);
        }
    }
}
