﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MSOFT.bida69.core.Properties;
using MSOFT.Core.Interfaces;
using MSOFT.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSOFT.bida69.com.Controllers
{
    [Route("rs")]
    public class RefServiceController : EntityController<RefService>
    {
        IRefServiceService _refServiceService;
        public RefServiceController(IRefServiceService refService, IDistributedCache distributedCache) : base(refService, distributedCache)
        {
            _refServiceService = refService;
        }

        public async override Task<AjaxResult> Get()
        {
            ajaxResult.Data = await _refServiceService.GetServices();
            return ajaxResult;
        }

        [HttpPut]
        [Route("timeStart")]
        public async virtual Task<AjaxResult> Put([FromBody]JObject data)
        {
            try
            {
                var refDetailID = Guid.Parse(data["RefServiceID"].ToString());
                DateTime timeStart = (DateTime)data["TimeStart"];
                ajaxResult.Data = _refServiceService.UpdateTimeStartForRefService(refDetailID, timeStart);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Messenge = Resources.ExceptionErroMsg;
            }

            return await Task.FromResult(ajaxResult);
        }

        [HttpPut]
        [Route("service/{refServiceID}/{serviceID}")]
        public async virtual Task<AjaxResult> ChangeService(Guid refServiceID, Guid serviceID)
        {
            try
            {
                ajaxResult.Data = _refServiceService.ChangeServiceForRefService(new object[] { refServiceID, serviceID });
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Messenge = Resources.ExceptionErroMsg;
            }

            return await Task.FromResult(ajaxResult);
        }
    }
}
