﻿using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.Core.Interfaces
{
    public interface IUserService
    {
        object Register(User user);
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        object LogOut();
    }
}
