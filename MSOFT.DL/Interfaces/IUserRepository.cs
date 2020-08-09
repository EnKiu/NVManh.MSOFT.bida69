using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.DL.Interfaces
{
    public interface IUserRepository:IBaseRepository
    {
        User GetUserAuthenticate(string userName, string password);
    }
}
