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
        Payed =1,
        /// <summary>
        /// Hóa đơn nợ
        /// </summary>
        DebitPay =2,
        /// <summary>
        /// Hóa đơn hủy
        /// </summary>
        CancelPay = 3
    }
}
