using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.BL.Interfaces
{
    public interface IRefDetailBL : IBaseBL<RefDetail>
    {
        int UpdateQuantityForRefDetail(Guid refDetailID, int quantity);
    }
}
