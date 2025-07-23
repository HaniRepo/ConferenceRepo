using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.DAL
{
    public class DAL_user
    {
        public string DAL_Login(string username, string password)
        {
            return BL.BL_User.BL_Login(username, password);
        }

    }
}
