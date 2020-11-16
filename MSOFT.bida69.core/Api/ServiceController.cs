using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MSOFT.bida69.core.Properties;
using MSOFT.Core.Interfaces;
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
        IServiceRepository _serviceRepository;
        public ServiceController(IServiceService serviceService, IDistributedCache distributedCache, IServiceRepository serviceRepository) : base(serviceService, distributedCache)
        {
            _serviceRepository = serviceRepository;
        }

        [HttpPatch]
        [Route("edit/inuser")]
        public async Task<AjaxResult> UpdateInUserForService([FromBody]object[] param)
        {
            try
            {
                ajaxResult.Data = await _serviceRepository.UpdateInUserForService(param);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Messenge = Resources.ExceptionErroMsg;
            }

            return ajaxResult;
        }

        [HttpGet]
        [Route("ServiceNotInUse")]
        public async Task<AjaxResult> GetServiceNotInUse()
        {
            try
            {
                ajaxResult.Data = _serviceRepository.GetServiceNotInUse();
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
        [Route("ServiceTest")]
        public async Task<AjaxResult> GetServiceNotInUse_Test()
        {
            try
            {
                ajaxResult.Data = await _serviceRepository.GetServiceNotInUse();
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Messenge = Resources.ExceptionErroMsg;
            }

            return ajaxResult;
        }

    }
}
