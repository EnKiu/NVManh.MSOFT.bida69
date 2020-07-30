using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.DL
{
    public class InventoryCategoryDL:BaseDL
    {
        public InventoryCategoryDL()
        {
            _getDataStoreName = "[dbo].[Proc_GetInventoryCategories]";
        }
    }
}
