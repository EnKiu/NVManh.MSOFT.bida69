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
    public class InventoryBL : EntityBL<Inventory>, IInventoryBL
    {
        IInventoryRepository _iInventoryRepository;
        public InventoryBL(IInventoryRepository iInventoryRepository) : base(iInventoryRepository)
        {
            _iInventoryRepository = iInventoryRepository;
        }
        public override IEnumerable<Inventory> GetData()
        {
            return _iInventoryRepository.GetEntities<Inventory>();
        }
    }
}
