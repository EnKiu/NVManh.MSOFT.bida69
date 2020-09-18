using MSOFT.Core.Interfaces;
using MSOFT.Entities;
using MSOFT.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSOFT.Infrastructure.Repository
{
    public class UserRepository: ADORepository, IUserRepository
    {
        public UserRepository(IDataContext dataContext) : base(dataContext)
        {
        }
        public async Task<User> GetUserAuthenticate(string userName, string password)
        {
            var userData = (await Get<User>("Proc_GetUserAuthenticate", new object[] { userName, password })).FirstOrDefault();
            return userData;
        }
    }
}
