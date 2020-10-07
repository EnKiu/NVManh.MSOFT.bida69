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
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public Guid? StockID { get; set; }
        public decimal? CostPrice { get; set; }
        [PropertyIgnore]
        public decimal? TotalAmount { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
