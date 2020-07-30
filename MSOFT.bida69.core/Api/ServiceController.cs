using Microsoft.AspNetCore.Mvc;
using MSOFT.bida69.core.Properties;
using MSOFT.BL;
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
        [HttpPatch]
        [Route("edit/inuser")]
        public async Task<AjaxResult> UpdateInUserForService([FromBody]object[] param)
        {
            try
            {
                var serviceBL = new ServiceBL();
                ajaxResult.Data = serviceBL.UpdateInUserForService(param);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Message = Resources.ExceptionErroMsg;
            }

            return await Task.FromResult(ajaxResult);
        }

        [HttpGet]
        [Route("ServiceNotInUse")]
        public async Task<AjaxResult> GetServiceNotInUse()
        {
            try
            {
                var serviceBL = new ServiceBL();
                ajaxResult.Data = serviceBL.GetServiceNotInUse();
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Message = Resources.ExceptionErroMsg;
            }

            return await Task.FromResult(ajaxResult);
        }
    }
}
