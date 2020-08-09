using System;
using System.Collections.Generic;

namespace MSOFT.Entities.Models
{
    public partial class InventoryCategory
    {
        public InventoryCategory()
        {
            Inventory = new HashSet<Inventory>();
        }

        public int InventoryCategoryId { get; set; }
        public string InventoryCategoryName { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
