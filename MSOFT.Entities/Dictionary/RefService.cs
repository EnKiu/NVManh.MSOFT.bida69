using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Entities
{
    public class RefService:Entity
    {
        public Guid RefServiceID { get; set; }
        public Guid ServiceID { get; set; }
        public Guid RefID { get; set; }
        public string ServiceName { get; set; }
        public int ServiceType { get; set; }
        public string UnitName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public bool InUse { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? EndTimeToPay { get; set; }
        public decimal? TotalTime { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountInventory { get; set; }
    }
}
