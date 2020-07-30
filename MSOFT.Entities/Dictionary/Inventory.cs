using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Entities
{
    public class Inventory:Entity
    {
        public Guid InventoryID { get; set; }
        public string InventoryCode { get; set; }
        public string InventoryName { get; set; }
        public int InventoryType { get; set; }
        public int InventoryCategoryID { get; set; }
        public Guid? UnitID { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool Inactive { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        #region "Thông tin bổ sung"
        public string UnitName { get; set; }
        #endregion

    }
}
