using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.BL.Interfaces
{
    public interface IRefservice : IBaseBL<Ref>
    {
        Ref GetRefDetail(Guid id);
        int UpdateRefAndServiceWhenPayOrder(Guid refID, decimal totalAmount, DateTime timeEnd);
        int DeleteRefDetailRefServiceAndUpdateServiceByRefID(Guid refID);
        IEnumerable<Ref> GetRefDataStatistic(DateTime fromDate, DateTime toDate);
    }
}
