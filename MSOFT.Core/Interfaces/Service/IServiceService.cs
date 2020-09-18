using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.Core.Interfaces
{
    public interface IServiceService : IEntityService
    {
        object UpdateInUserForService(object[] parameters);
        object GetServiceNotInUse();
    }
}
