using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MSOFT.BL;
using MSOFT.BL.Interfaces;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace MSOFT.bida69.com.Controllers
{
    [Route("inventories")]
    public class InventoryController : EntityController<Inventory>
    {
        IInventoryBL _inventoryBL;
        public InventoryController(IInventoryBL inventoryBL, IDistributedCache distributedCache) : base(inventoryBL, distributedCache)
        {
            _inventoryBL = inventoryBL;
        }
    }
}
