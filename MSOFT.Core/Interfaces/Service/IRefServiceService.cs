using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Core.Interfaces
{
    public interface IRefServiceService : IEntityService
    {
        Task<IEnumerable<MSOFT.Entities.Service>> GetServices();
        Task<int> UpdateTimeStartForRefService(Guid refServiceID, DateTime timeStart);
        Task<int> ChangeServiceForRefService(object[] param);
    }
}
