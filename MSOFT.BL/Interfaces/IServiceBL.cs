﻿using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.BL.Interfaces
{
    public interface IServiceServic:IBaseBL<Service>
    {
        object UpdateInUserForService(object[] parameters);
        object GetServiceNotInUse();
    }
}
