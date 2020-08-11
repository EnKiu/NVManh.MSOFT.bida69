﻿using Microsoft.AspNetCore.Mvc;
using MSOFT.bida69.core.Properties;
using MSOFT.BL;
using MSOFT.BL.Interfaces;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MSOFT.bida69.com.Controllers
{
    public class ServiceController : EntityController<Service>
    {
        IServiceBL _serviceBL;
        public ServiceController(IServiceBL serviceBL) : base(serviceBL)
        {
            _serviceBL = serviceBL;
        }

        [HttpPatch]
        [Route("edit/inuser")]
        public async Task<AjaxResult> UpdateInUserForService([FromBody]object[] param)
        {
            try
            {
                ajaxResult.Data = _serviceBL.UpdateInUserForService(param);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Messenge = Resources.ExceptionErroMsg;
            }

            return await Task.FromResult(ajaxResult);
        }

        [HttpGet]
        [Route("ServiceNotInUse")]
        public async Task<AjaxResult> GetServiceNotInUse()
        {
            try
            {
                ajaxResult.Data = _serviceBL.GetServiceNotInUse();
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
