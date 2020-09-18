using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Core.Interfaces
{
    public interface IRefService : IEntityService
    {
        Task<Ref> GetRefDetail(Guid id);
        Task<int> UpdateRefAndServiceWhenPayOrder(Guid refID, decimal totalAmount, DateTime timeEnd);
        Task<int> DeleteRefDetailRefServiceAndUpdateServiceByRefID(Guid refID);
        Task<IEnumerable<Ref>> GetRefDataStatistic(DateTime fromDate, DateTime toDate);
    }
}
