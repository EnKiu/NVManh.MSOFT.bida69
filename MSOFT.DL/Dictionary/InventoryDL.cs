using MSOFT.DL.Interfaces;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.DL
{
    public class InventoryDL:BaseDL, IInventoryRepository
    {
        public InventoryDL()
        {
            //_getDataStoreName = "[dbo].[Proc_GetInventories]";
        }
       
    }
}
