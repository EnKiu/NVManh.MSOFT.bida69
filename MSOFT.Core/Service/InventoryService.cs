using MSOFT.Core.Interfaces;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Core.Service
{
    public class InventoryService : EntityService, IInventoryService
    {
        IInventoryRepository _iInventoryRepository;
        public InventoryService(IInventoryRepository iInventoryRepository) : base(iInventoryRepository)
        {
            _iInventoryRepository = iInventoryRepository;
        }
        //public override IEnumerable<Inventory> GetData()
        //{
        //    return _iInventoryRepository.GetEntities<Inventory>();
        //}
    }
}
