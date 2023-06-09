﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Core.Interfaces
{
    public interface IRefServiceRepository : IBaseRepository
    {
        Task<IEnumerable<MSOFT.Entities.RefService>> GetServices();
        Task<int> UpdateTimeStartForRefService(Guid refServiceID, DateTime timeStart);
        Task<int> ChangeServiceForRefService(object[] param);
    }
}
