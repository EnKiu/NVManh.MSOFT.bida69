using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.Common
{
    public enum RefState
    {
        /// <summary>
        /// Chưa thanh toán
        /// </summary>
        NotYetPay = 0,
        /// <summary>
        /// Đã thanh toán
        /// </summary>
        Payed = 1,
        /// <summary>
        /// Hóa đơn nợ
        /// </summary>
        DebitPay = 2,
        /// <summary>
        /// Hóa đơn hủy
        /// </summary>
        CancelPay = 3
    }

    /// <summary>
    /// Loại phiếu
    /// </summary>
    public enum RefType
    {
        /// <summary>
        /// Hóa đơn sử dụng dịch vụ (bao gồm bán kèm đồ ăn/ uống)
        /// </summary>
        Service = 1,

        /// <summary>
        /// Hóa đơn bán lẻ
        /// </summary>
        Sale = 2,

        /// <summary>
        /// Phiếu nhập hàng
        /// </summary>
        InStock = 3,

        /// <summary>
        /// Phiếu xuất hàng
        /// </summary>
        Other = 4
    }
}
