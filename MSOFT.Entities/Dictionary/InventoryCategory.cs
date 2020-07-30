using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Entities
{
    public class InventoryCategory:Entity
    {
        public int InventoryCategoryID { get; set; }
        public string InventoryCategoryName { get; set; }
        public string Description { get; set; }

    }
}
