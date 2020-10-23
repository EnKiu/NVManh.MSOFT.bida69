using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Entities
{
    public class RefDetail:Entity
    {
        public Guid RefDetailID { get; set; }
        public Guid RefID { get; set; }
        public Guid InventoryID { get; set; }
        [PropertyIgnore]
        public string InventoryName { get; set; }
        [PropertyIgnore]
        public string UnitName { get; set; }
        public Double? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public Guid? StockID { get; set; }
        public Double? CostPrice { get; set; }
        [PropertyIgnore]
        public Double? TotalAmount { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
