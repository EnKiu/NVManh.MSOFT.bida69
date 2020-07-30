using MSOFT.DL;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.BL
{
    public class UserBL:EntityBL<User>
    {
        public object Register(User user)
        {
            return InsertEntity(user);
        }
        public User GetUserAuthenticate(string userName, string password)
        {
            var userDL = new UserDL();
            return userDL.GetUserAuthenticate(userName, password);
        }
    }
}
