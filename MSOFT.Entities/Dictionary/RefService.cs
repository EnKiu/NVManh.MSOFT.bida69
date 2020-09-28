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
        public double UnitPrice { get; set; }
        public string Description { get; set; }
        public bool InUse { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? EndTimeToPay { get; set; }
        public double? TotalTime { get; set; }
        public double TotalAmount { get; set; }
        public double TotalAmountInventory { get; set; }
    }
}
