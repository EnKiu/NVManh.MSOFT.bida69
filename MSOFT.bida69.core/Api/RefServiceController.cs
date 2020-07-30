using Microsoft.AspNetCore.Mvc;
using MSOFT.bida69.core.Properties;
using MSOFT.BL;
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
        [HttpPut]
        [Route("timeStart")]
        public async virtual Task<AjaxResult> Put([FromBody]JObject data)
        {
            try
            {
                var refDetailID = Guid.Parse(data["RefServiceID"].ToString());
                DateTime timeStart = (DateTime)data["TimeStart"];
                var refServiceBL = new RefServiceBL();
                ajaxResult.Data = refServiceBL.UpdateTimeStartForRefService(refDetailID, timeStart);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Message = Resources.ExceptionErroMsg;
            }

            return await Task.FromResult(ajaxResult);
        }

        [HttpPut]
        [Route("service/{refServiceID}/{serviceID}")]
        public async virtual Task<AjaxResult> ChangeService(Guid refServiceID, Guid serviceID)
        {
            try
            {
                var refServiceBL = new RefServiceBL();
                ajaxResult.Data = refServiceBL.ChangeServiceForRefService(new object[] { refServiceID, serviceID });
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
