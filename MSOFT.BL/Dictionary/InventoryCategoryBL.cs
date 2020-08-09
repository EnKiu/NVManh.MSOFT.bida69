using MSOFT.BL.Interfaces;
using MSOFT.DL;
using MSOFT.DL.Interfaces;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.BL
{
    public class InventoryCategoryBL : EntityBL<InventoryCategory>, IInventoryCategoryBL
    {
        IInventoryCategoryRepository _inventoryCategoryRepository;
        public InventoryCategoryBL(IInventoryCategoryRepository inventoryCategoryRepository) : base(inventoryCategoryRepository)
        {
            _inventoryCategoryRepository = inventoryCategoryRepository;
        }
        public override IEnumerable<InventoryCategory> GetData()
        {
            return _inventoryCategoryRepository.GetEntities<InventoryCategory>();
        }
    }
}
