using MSOFT.Core.Interfaces;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Core.Service
{
    public class InventoryCategoryService : EntityService, IInventoryCategoryService
    {
        IInventoryCategoryRepository _inventoryCategoryRepository;
        public InventoryCategoryService(IInventoryCategoryRepository inventoryCategoryRepository) : base(inventoryCategoryRepository)
        {
            _inventoryCategoryRepository = inventoryCategoryRepository;
        }
        //public override IEnumerable<InventoryCategory> GetData()
        //{
        //    return _inventoryCategoryRepository.GetEntities<InventoryCategory>();
        //}
    }
}
