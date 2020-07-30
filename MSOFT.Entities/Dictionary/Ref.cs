using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Entities
{
    public class Ref:Entity
    {
        public Guid RefID { get; set; }
        public string RefNo { get; set; }
        public int RefType { get; set; }
        public DateTime RefDate { get; set; }
        
        public string JournalMemo { get; set; }
        public Guid? EmployeeID { get; set; }
        public Guid? CustomerID { get; set; }

        /// <summary>
        /// Tổng tiền thanh toán đối với hàng hóa
        /// </summary>
        public decimal TotalAmountInventory { get; set; }

        /// <summary>
        /// Tổng tiền thanh toán đối với dịch vụ
        /// </summary>
        public decimal TotalAmountService { get; set; }

        /// <summary>
        /// Tổng tiền thanh toán toàn bộ hóa đơn
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Tổng tiền thanh toán thực tế
        /// </summary>
        public decimal TotalAmountPay { get; set; }

        public IEnumerable<RefService> RefServices;
        public IEnumerable<RefDetail> RefDetails;
        public string ServiceName { get; set; }
        public int RefState { get; set; }
    }
}
