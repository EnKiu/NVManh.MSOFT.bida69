using Microsoft.AspNetCore.Mvc;
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
    [Route("rd")]
    public class RefDetailController : EntityController<RefDetail>
    {
        IRefDetailService _refDetailService;
        public RefDetailController(IRefDetailService refDetailService, IDistributedCache distributedCache) : base(refDetailService, distributedCache)
        {
            _refDetailService = refDetailService;
        }
        /// <summary>
        /// Cập nhật lại số lượng cho RefDetail
        /// </summary>
        /// <param name="refDetailID">Mã chi tiết hóa đơn</param>
        /// <param name="quantity">Số lượng mới cập nhật</param>
        /// <returns></returns>
        /// Author: NVMANH (04/08/2019)
        [HttpPut]
        [Route("quantity")]
        public async Task<AjaxResult> Put(JObject data)
        {
            try
            {
                var refDetailID = Guid.Parse(data["RefDetailID"].ToString());
                var quantity = (int)data["Quantity"];
                ajaxResult.Data = _refDetailService.UpdateQuantityForRefDetail(refDetailID, quantity);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Messenge = Resources.ExceptionErroMsg;
            }

            return await Task.FromResult(ajaxResult);
        }
        public async override Task<AjaxResult> Post([FromBody] RefDetail entity)
        {
            ajaxResult.Data = await _refDetailService.InsertInventoryForRefDetail(entity);
            return ajaxResult;
        }
    }
}
