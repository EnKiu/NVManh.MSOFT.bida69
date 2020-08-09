using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.DL.Interfaces
{
    public interface IRefDetailRepository : IBaseRepository
    {
        int UpdateQuantityForRefDetail(Guid refDetailID, int quantity);
    }
}
