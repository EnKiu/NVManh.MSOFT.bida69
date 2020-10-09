using MSOFT.Core.Interfaces;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Core.Service
{
    public class RefDetailService:EntityService,IRefDetailService
    {
        IRefDetailRepository _iRefDetailRepository;
        public RefDetailService(IRefDetailRepository iRefDetailRepository) : base(iRefDetailRepository)
        {
            _iRefDetailRepository = iRefDetailRepository;
        }

        public async Task<int> InsertInventoryForRefDetail(RefDetail refDetailID)
        {
            return await _iRefDetailRepository.InsertInventoryForRefDetail(refDetailID);
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
            return await _iRefDetailRepository.UpdateQuantityForRefDetail(refDetailID, quantity);
        }
    }
}
