using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.DL.Interfaces
{
    public interface IRefServiceRepository : IBaseRepository
    {
        int UpdateTimeStartForRefService(Guid refServiceID, DateTime timeStart);
        int ChangeServiceForRefService(object[] param);
    }
}
