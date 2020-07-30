﻿using Microsoft.AspNetCore.Mvc;
using MSOFT.bida69.core.Properties;
using MSOFT.BL;
using MSOFT.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSOFT.bida69.com.Controllers
{
    [Route("ref")]
    public class RefController : EntityController<Ref>
    {
        [HttpGet]
        [Route("refdetail/{id}")]
        public async Task<AjaxResult> GetRefDetail(string id)
        {
            try
            {
                var refBL = new RefBL();
                ajaxResult.Data = refBL.GetRefDetail(Guid.Parse(id));
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
        [Route("RefAndService")]
        //[AllowAnonymous]
        public async Task<AjaxResult> UpdateRefAndServiceWhenPayOrder([FromBody]JObject data)
        {
            try
            {
                var refId = data["refId"] != null ? Guid.Parse(data["refId"].ToString()) : Guid.Empty;
                var totalAmount = (decimal)data["totalAmount"];
                //var timeEnd = ((DateTime)data["timeEnd"]).ToLocalTime();
                //var timeEnd2 = ((DateTime)data["timeEnd"]);
                var timeEnd = DateTime.ParseExact(data["timeEnd"].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture); ;
                var refBL = new RefBL();
                ajaxResult.Data = refBL.UpdateRefAndServiceWhenPayOrder(refId, totalAmount, timeEnd);
                //ajaxResult.Data = new DateTime[] { timeEnd, timeEnd2 , timeEnd3 };
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
        [Route("RefDetailAndRefService/{refId}")]
        //[AllowAnonymous]
        public async Task<AjaxResult> DeleteRefDetailRefServiceAndUpdateServiceByRefID(Guid refId)
        {
            try
            {
                var refBL = new RefBL();
                ajaxResult.Data = refBL.DeleteRefDetailRefServiceAndUpdateServiceByRefID(refId);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Message = Resources.ExceptionErroMsg;
            }

            return await Task.FromResult(ajaxResult);
        }


        /// <summary>
        /// Thực hiện thống kê dữ liệu hóa đơn theo khoảng thời gian
        /// </summary>
        /// <param name="fromDate">Thời gian bắt đầu thống kê</param>
        /// <param name="toDate">Thời gian kế thúc thống kê</param>
        /// <returns>Danh sách thống kê chi tiết các hóa đơn</returns>
        /// CreatedBy: NVMANH (03/08/2019)
        [HttpGet]
        [Route("RefDataStatistic/{fromDate}/{toDate}")]
        //[AllowAnonymous]
        public async Task<AjaxResult> GetRefDataStatistic(DateTime fromDate, DateTime toDate)
        {
            try
            {
                toDate = toDate.AddDays(1);
                var refBL = new RefBL();
                ajaxResult.Data = refBL.GetRefDataStatistic(fromDate, toDate);
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
