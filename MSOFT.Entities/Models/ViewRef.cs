using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class ViewRef
    {
        public Guid RefId { get; set; }
        public string RefNo { get; set; }
        public int? RefType { get; set; }
        public DateTime? RefDate { get; set; }
        public string JournalMemo { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? CustomerId { get; set; }
        public decimal? TotalAmountInventory { get; set; }
        public decimal? TotalAmountService { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
