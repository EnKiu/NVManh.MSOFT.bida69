using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class Inventory
    {
        public Inventory()
        {
            RefDetail = new HashSet<RefDetail>();
        }

        public Guid InventoryId { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryName { get; set; }
        public int? InventoryCategoryId { get; set; }
        public int? InventoryType { get; set; }
        public Guid? UnitId { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public bool? Inactive { get; set; }
        public bool? IsDeleted { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual InventoryCategory InventoryCategory { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<RefDetail> RefDetail { get; set; }
    }
}
