using Microsoft.Extensions.Caching.Distributed;
using MSOFT.BL;
using MSOFT.BL.Interfaces;
using MSOFT.Entities;
using MSOFT.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace MSOFT.bida69.com.Controllers
{
    public class UnitController : EntityController<Unit>
    {
        public UnitController(IBaseBL<Unit> baseBL, IDistributedCache distributedCache) : base(baseBL, distributedCache)
        {
        }
    }
}
