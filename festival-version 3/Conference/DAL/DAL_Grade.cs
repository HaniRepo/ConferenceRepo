using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.DAL
{
    public class DAL_Grade
    {
        public string[] DAL_Grade_SelectALL()
        {
            return BL.BL_Grade.BL_Grade_SelectAll();
        }
    }
}
