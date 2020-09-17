using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Entities
{
    public class Service:Entity
    {
        public Guid ServiceID { get; set; }
        public string ServiceName { get; set; }
        public int ServiceType { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool InUse { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double TotalAmountInventory { get; set; }
    }
}
