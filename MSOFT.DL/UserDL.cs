using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSOFT.DL
{
    public class UserDL: BaseDL
    {
        public User GetUserAuthenticate(string userName, string password)
        {
            var userData = GetEntities<User>("[dbo].[Proc_GetUserAuthenticate]", new object[] { userName, password }).FirstOrDefault();
            return userData;
        }
    }
}
