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
    [Route("ic")]
    public class InventoryCategoryController : EntityController<InventoryCategory>
    {
        public InventoryCategoryController(IInventoryCategoryService inventoryCaregoryService, IDistributedCache distributedCache) : base(inventoryCaregoryService, distributedCache)
        {
        }
    }
}
