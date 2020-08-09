using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.DL.Interfaces
{
    public interface IRefRepository : IBaseRepository
    {
        Ref GetRefDetail(Guid id);
        int UpdateRefAndServiceWhenPayOrder(Guid refID, decimal totalAmount, DateTime timeEnd);
        int DeleteRefDetailRefServiceAndUpdateServiceByRefID(Guid refID);
        IEnumerable<Ref> GetRefDataStatistic(DateTime fromDate, DateTime toDate);
    }
}
