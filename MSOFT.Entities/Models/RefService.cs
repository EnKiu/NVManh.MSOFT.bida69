using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class RefService
    {
        public Guid RefServiceId { get; set; }
        public Guid RefId { get; set; }
        public Guid ServiceId { get; set; }
        public decimal? UniPrice { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual Ref Ref { get; set; }
        public virtual Service Service { get; set; }
    }
}
