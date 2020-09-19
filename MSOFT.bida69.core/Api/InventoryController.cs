using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MSOFT.BL;
using MSOFT.BL.Interfaces;
using MSOFT.Core.Interfaces;
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
        public InventoryController(IInventoryService inventorySercvice, IDistributedCache distributedCache) : base(inventorySercvice, distributedCache)
        {
        }
    }
}
