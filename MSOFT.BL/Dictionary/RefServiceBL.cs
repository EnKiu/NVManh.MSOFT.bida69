using MSOFT.BL.Interfaces;
using MSOFT.DL;
using MSOFT.DL.Interfaces;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.BL
{
    public class RefServiceBL: EntityBL<RefService>,IRefServiceBL
    {
        IRefServiceRepository _iRefServiceRepository;
        public RefServiceBL(IRefServiceRepository iRefServiceRepository) : base(iRefServiceRepository)
        {
            _iRefServiceRepository = iRefServiceRepository;
        }

        /// <summary>
        /// Cập nhật thời gian bắt đầu sử dụng dịch vụ
        /// </summary>
        /// <param name="refServiceID">MÃ chi tiết dịch vụ</param>
        /// <param name="timeStart">Thời gian cập nhật mới</param>
        /// <returns>Số lượng dịch vụ cập nhật thành công</returns>
        /// CreatedBy: NVMANH (04/08/2019)
        public int UpdateTimeStartForRefService(Guid refServiceID, DateTime timeStart)
        {
            return _iRefServiceRepository.UpdateTimeStartForRefService( refServiceID, timeStart);
        }

        /// <summary>
        /// Chuyển hóa đơn tới bàn dịch vụ khác
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// CreatedBy: NVMANH (04/08/2019)
        public int ChangeServiceForRefService(object[] param)
        {
            return _iRefServiceRepository.ChangeServiceForRefService(param);
        }
    }
}
