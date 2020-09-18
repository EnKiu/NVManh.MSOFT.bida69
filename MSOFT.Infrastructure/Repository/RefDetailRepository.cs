using MSOFT.Core.Interfaces;
using MSOFT.Entities;
using MSOFT.Infrastructure.Interfaces;
using MSOFT.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Infrastructure.Repository
{
    public class RefDetailRepository:ADORepository, IRefDetailRepository
    {
        public RefDetailRepository(IDataContext dataContext) : base(dataContext) { 
        }
        /// <summary>
        /// Cập nhật lại số lượng cho RefDetail
        /// </summary>
        /// <param name="refDetailID">Mã chi tiết hóa đơn</param>
        /// <param name="quantity">Số lượng mới cập nhật</param>
        /// <returns></returns>
        /// Author: NVMANH (04/08/2019)
        public async Task<int> UpdateQuantityForRefDetail(Guid refDetailID, int quantity)
        {
            return await _dataContext.ExecuteNonQueryAsync("[dbo].[Proc_UpdateQuantityForRefDetail]", new object[] { refDetailID, quantity});
        }
    }
}
