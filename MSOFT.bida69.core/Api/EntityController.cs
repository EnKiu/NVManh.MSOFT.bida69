using MSOFT.BL;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using MSOFT.bida69.core.Properties;
using Microsoft.AspNetCore.Mvc;
using MSOFT.bida69.Services;
using Microsoft.AspNetCore.Authorization;
using MSOFT.BL.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using MSOFT.Core.Interfaces;

namespace MSOFT.bida69.com.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public abstract class EntityController<T> : ControllerBase
    {
        protected readonly IDistributedCache _distributedCache;
        protected AjaxResult ajaxResult;
        //protected EntityBL<T> entityBL;
        private IUserBL _userService;
        IEntityService _entityService;

        public EntityController(IDistributedCache distributedCache)
        {
            ajaxResult = new AjaxResult();
        }
        public EntityController(IEntityService entityService, IDistributedCache distributedCache)
        {
            _entityService = entityService;
            ajaxResult = new AjaxResult();
            //entityBL = new EntityBL<T>();
        }
        public EntityController(IUserBL userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Lấy toàn bộ dữ liệu theo của đối tượng
        /// </summary>
        /// <returns></returns>
        /// CreateBy: NVMANH (07/07/2019)
        [Route("")]
        public async virtual Task<AjaxResult> Get()
        {
            try
            {
                ajaxResult.Data = await _entityService.GetData<T>();
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
        [Route("{id}")]
        public async virtual Task<AjaxResult> Get(string id)
        {
            try
            {
                ajaxResult.Data = await _entityService.GetEntityByID<T>(id);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Messenge = Resources.ExceptionErroMsg;
            }

            return await Task.FromResult(ajaxResult);
        }

        [HttpPost]
        [Route("")]
        public async virtual Task<AjaxResult> Post([FromBody]T entity)
        {
            try
            {
                ajaxResult.Data = await _entityService.InsertEntity(entity);
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
        [Route("")]
        public async virtual Task<AjaxResult> Put([FromBody]T entity)
        {
            try
            {
                ajaxResult.Data = await _entityService.UpdateEntity(entity);
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
        [Route("p")]
        public async virtual Task<AjaxResult> PutMultiParam([FromBody]object[] param)
        {
            try
            {
                ajaxResult.Data = await _entityService.UpdateEntity(param);//entityBL.UpdateEntity(entity);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Messenge = Resources.ExceptionErroMsg;
            }

            return await Task.FromResult(ajaxResult);
        }

        [HttpDelete]
        [Route("{id}")]
        public async virtual Task<AjaxResult> Delete(string id)
        {
            try
            {
                ajaxResult.Data = await _entityService.DeleteEntityByID<T>(id);
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
