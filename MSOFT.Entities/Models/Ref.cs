using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class Ref:Entity
    {
        public Ref()
        {
            RefDetail = new HashSet<RefDetail>();
            RefService = new HashSet<RefService>();
        }

        public Guid RefId { get; set; }
        public string RefNo { get; set; }
        public int? RefType { get; set; }
        public DateTime? RefDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string JournalMemo { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? CustomerId { get; set; }
        public int? RefState { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<RefDetail> RefDetail { get; set; }
        public virtual ICollection<RefService> RefService { get; set; }
    }
}
