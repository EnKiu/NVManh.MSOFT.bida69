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
    [Route("rd")]
    public class RefDetailController : EntityController<RefDetail>
    {
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
                var refDetalBL = new RefDetailBL();
                ajaxResult.Data = refDetalBL.UpdateQuantityForRefDetail(refDetailID, quantity);
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
