using MSOFT.Core.Interfaces;
using MSOFT.Entities;
using MSOFT.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Infrastructure.Repository
{
    public class RefServiceRepository:ADORepository, IRefServiceRepository
    {
        public RefServiceRepository(IDataContext dataContext) : base(dataContext)
        {

        }
        /// <summary>
        /// Cập nhật thời gian bắt đầu sử dụng dịch vụ
        /// </summary>
        /// <param name="refServiceID">MÃ chi tiết dịch vụ</param>
        /// <param name="timeStart">Thời gian cập nhật mới</param>
        /// <returns>Số lượng dịch vụ cập nhật thành công</returns>
        /// CreatedBy: NVMANH (04/08/2019)
        public async Task<int> UpdateTimeStartForRefService(Guid refServiceID, DateTime timeStart)
        {
            return await _dataContext.ExecuteNonQueryAsync("Proc_UpdateTimeStartForRefService", new object[] { refServiceID, timeStart });
        }

        /// <summary>
        /// Chuyển hóa đơn tới bàn dịch vụ khác
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// CreatedBy: NVMANH (04/08/2019)
        public async Task<int> ChangeServiceForRefService(object[] param)
        {
            return await _dataContext.ExecuteNonQueryAsync("Proc_ChangeServiceForRefService", param);
        }
    }
}
