using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.DL
{
    public class RefDetailDL:BaseDL
    {
        /// <summary>
        /// Cập nhật lại số lượng cho RefDetail
        /// </summary>
        /// <param name="refDetailID">Mã chi tiết hóa đơn</param>
        /// <param name="quantity">Số lượng mới cập nhật</param>
        /// <returns></returns>
        /// Author: NVMANH (04/08/2019)
        public int UpdateQuantityForRefDetail(Guid refDetailID, int quantity)
        {
            return UpdateEntity<RefDetail>("[dbo].[Proc_UpdateQuantityForRefDetail]", new object[] { refDetailID, quantity});
        }
    }
}
