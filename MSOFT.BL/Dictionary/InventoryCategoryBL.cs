using MSOFT.DL;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.BL
{
    public class InventoryCategoryBL:EntityBL<InventoryCategory>
    {
        public override IEnumerable<InventoryCategory> GetData()
        {
            InventoryCategoryDL icDL = new InventoryCategoryDL();
            return icDL.GetEntities<InventoryCategory>();
        }
    }
}
