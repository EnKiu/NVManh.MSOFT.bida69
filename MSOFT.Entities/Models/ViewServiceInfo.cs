using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class ViewServiceInfo
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int? ServiceType { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public bool InUse { get; set; }
        public Guid? RefServiceId { get; set; }
        public Guid? RefId { get; set; }
        public decimal? UniPrice { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? TotalAmountInventory { get; set; }
    }
}
