using MSOFT.DL;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.BL
{
    public class InventoryBL:EntityBL<Inventory>
    {
        public InventoryBL():base()
        {

        }
        public override IEnumerable<Inventory> GetData()
        {
            InventoryDL inventoryDL = new InventoryDL();
            return inventoryDL.GetEntities<Inventory>();
        }
    }
}
