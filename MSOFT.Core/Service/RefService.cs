using MSOFT.Core.Interfaces;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Core.Service
{
    public class RefService : EntityService, IRefService
    {
        IRefRepository _iRefRepository;
        public RefService(IRefRepository iRefRepository) : base(iRefRepository)
        {
            _iRefRepository = iRefRepository;
        }
        public async Task<Ref> GetRefDetail(Guid id)
        {
            return await _iRefRepository.GetRefDetail(id);
        }
        /// <summary>
        /// Thực hiện cập nhật thông tin hóa đơn và các dữ liệu trong  hóa đơn liên quan khi thực hiện thanh toàn và In Hóa đơn.
        /// <param name="refID">ID hóa đơn</param>
        /// <param name="totalAmount">Tổng tiền</param>
        /// <param name="timeEnd">Thời gian kết thúc</param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// Author: NVMANH (31/07/2019)
        public async Task<int> UpdateRefAndServiceWhenPayOrder(Guid refID, decimal totalAmount, DateTime timeEnd)
        {
            return await _iRefRepository.UpdateRefAndServiceWhenPayOrder(refID, totalAmount, timeEnd);
        }

        /// <summary>
        /// Thực hiện xóa hóa đơn và cập nhật trạng thái bàn sang chưa sử dụng
        /// </summary>
        /// <param name="refID">Mã hóa đơn</param>
        /// <returns></returns>
        /// Author: NVMANH (31/07/2019) 
        public async Task<int> DeleteRefDetailRefServiceAndUpdateServiceByRefID(Guid refID)
        {
            return await _iRefRepository.DeleteRefDetailRefServiceAndUpdateServiceByRefID(refID);
        }

        /// <summary>
        /// Thống kê hóa đơn theo thời gian
        /// </summary>
        /// <param name="fromDate">thời gian bắt đầu</param>
        /// <param name="toDate">Thời gian kết thúc</param>
        /// <returns>Dữ liệu thống kê chi tiết các hóa đơn trong khoảng thời gian cần thống kê</returns>
        /// CreatedBy : NVMANH (02/08/2019)
        public async Task<IEnumerable<Ref>> GetRefDataStatistic(DateTime fromDate, DateTime toDate)
        {
            return await _iRefRepository.GetRefDataStatistic(fromDate, toDate);
        }
    }
}
