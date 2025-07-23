using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.DAL
{
    public class DAL_Major
    {
        public string[] DAL_Major_SelectAll()
        {
            return BL.BL_Major.BL_Major_SelectAll();
        }


        public bool DAL_isdr(string major)
        {
            return BL.BL_Major.BL_IsDr(major);
        }

        

    }

}
