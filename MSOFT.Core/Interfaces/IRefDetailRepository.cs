using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Core.Interfaces
{
    public interface IRefDetailRepository : IBaseRepository
    {
        Task<int> UpdateQuantityForRefDetail(Guid refDetailID, int quantity);
        Task<int> InsertInventoryForRefDetail(RefDetail refDetailID);
    }
}
