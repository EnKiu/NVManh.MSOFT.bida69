using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Core.Interfaces
{
    public interface IUserRepository:IBaseRepository
    {
        Task<User> GetUserAuthenticate(string userName, string password);
    }
}
