using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class RefDetail
    {
        public Guid RefDetailId { get; set; }
        public Guid RefId { get; set; }
        public Guid InventoryId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public Guid? StockId { get; set; }
        public decimal? CostPrice { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual Inventory Inventory { get; set; }
        public virtual Ref Ref { get; set; }
    }
}
