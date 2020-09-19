using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.BL.Interfaces
{
    public interface IRefServiceService : IBaseBL<RefService>
    {
        int UpdateTimeStartForRefService(Guid refServiceID, DateTime timeStart);
        int ChangeServiceForRefService(object[] param);
    }
}
