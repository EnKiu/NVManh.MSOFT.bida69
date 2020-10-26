using MSOFT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Entities
{
    public class Ref : Entity
    {
        public Guid RefID { get; set; }
        public string RefNo { get; set; }
        public int RefType { get; set; }
        public DateTime RefDate { get; set; }

        public string JournalMemo { get; set; }
        public Guid? EmployeeID { get; set; }
        public Guid? CustomerID { get; set; }

        public string RefTypeName
        {
            get
            {
                switch ((RefType)RefType)
                {
                    case Common.RefType.InStock:
                        return "Nhập kho";
                    case Common.RefType.Service:
                        return "Dịch vụ";
                    case Common.RefType.Sale:
                        return "Bán lẻ";
                    default:
                        return "Khác";
                }
            }
        }

        [PropertyIgnore]
        /// <summary>
        /// Tổng tiền thanh toán đối với hàng hóa
        /// </summary>
        public double TotalAmountInventory { get; set; }

        [PropertyIgnore]
        /// <summary>
        /// Tổng tiền thanh toán đối với dịch vụ
        /// </summary>
        public double TotalAmountService { get; set; }

        /// <summary>
        /// Tổng tiền thanh toán toàn bộ hóa đơn
        /// </summary>
        public double TotalAmount { get; set; }

        [PropertyIgnore]
        /// <summary>
        /// Tổng tiền thanh toán thực tế
        /// </summary>
        public double TotalAmountPay { get; set; }

        public IEnumerable<RefService> RefServices;
        public IEnumerable<RefDetail> RefDetails;

        [PropertyIgnore]
        public string ServiceName { get; set; }
        public int RefState { get; set; }
    }
}
