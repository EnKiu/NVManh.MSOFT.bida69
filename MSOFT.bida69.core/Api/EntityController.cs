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

namespace MSOFT.bida69.com.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public abstract class EntityController<T> : ControllerBase
    {
        protected AjaxResult ajaxResult;
        protected EntityBL<T> entityBL;
        private IUserService _userService;

        public EntityController()
        {
            
            ajaxResult = new AjaxResult();
            entityBL = new EntityBL<T>();
        }
        public EntityController(IUserService userService)
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
                ajaxResult.Data = entityBL.GetData();
                //await Task.Delay(3000);
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
        [Route("{id}")]
        public async virtual Task<AjaxResult> Get(string id)
        {
            try
            {
                ajaxResult.Data = entityBL.GetEntityByID(id);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Message = Resources.ExceptionErroMsg;
            }

            return await Task.FromResult(ajaxResult);
        }

        [HttpPost]
        [Route("")]
        public async virtual Task<AjaxResult> Post([FromBody]T entity)
        {
            try
            {
                ajaxResult.Data = entityBL.InsertEntity(entity);
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
        [Route("")]
        public async virtual Task<AjaxResult> Put([FromBody]T entity)
        {
            try
            {
                ajaxResult.Data = entityBL.UpdateEntity(entity);
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
        [Route("p")]
        public async virtual Task<AjaxResult> PutMultiParam([FromBody]object[] param)
        {
            try
            {
                ajaxResult.Data = entityBL.UpdateEntity(param);//entityBL.UpdateEntity(entity);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Message = Resources.ExceptionErroMsg;
            }

            return await Task.FromResult(ajaxResult);
        }

        [HttpDelete]
        [Route("{id}")]
        public async virtual Task<AjaxResult> Delete(string id)
        {
            try
            {
                ajaxResult.Data = entityBL.DeleteEntityByID(id);
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
