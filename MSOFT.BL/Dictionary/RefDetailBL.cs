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
    public class RefDetailBL:EntityBL<RefDetail>,IRefDetailBL
    {
        IRefDetailRepository _iRefDetailRepository;
        public RefDetailBL(IRefDetailRepository iRefDetailRepository) : base(iRefDetailRepository)
        {
            _iRefDetailRepository = iRefDetailRepository;
        }
        /// <summary>
        /// Cập nhật lại số lượng cho RefDetail
        /// </summary>
        /// <param name="refDetailID">Mã chi tiết hóa đơn</param>
        /// <param name="quantity">Số lượng mới cập nhật</param>
        /// <returns></returns>
        /// Author: NVMANH (04/08/2019)
        public int UpdateQuantityForRefDetail(Guid refDetailID, int quantity)
        {
            return _iRefDetailRepository.UpdateQuantityForRefDetail(refDetailID, quantity);
        }
    }
}
