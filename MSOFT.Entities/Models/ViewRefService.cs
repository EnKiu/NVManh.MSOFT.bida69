using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class ViewRefService
    {
        public Guid RefServiceId { get; set; }
        public Guid RefId { get; set; }
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string UnitName { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTime? StartTime { get; set; }
        public decimal? TotalTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? EndTimeToPay { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
