using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using MSOFT.bida69.com.Controllers;
using MSOFT.bida69.core.Properties;
using MSOFT.Common;
using MSOFT.Core.Interfaces;
using MSOFT.Entities;
using MSOFT.Entities.Models;
using MSOFT.Infrastructure.DatabaseContext;
using Newtonsoft.Json.Linq;

namespace MSOFT.bida69.core.Api
{
    [Route("refs")]
    public class RefsController : EntityController<MSOFT.Entities.Ref>
    {
        IConfiguration _configuration;
        IRefRepository _refRepository;
        public RefsController(IRefRepository refRepository, IConfiguration configuration, IDistributedCache distributedCache) : base(distributedCache)
        {
            _refRepository = refRepository;
            _configuration = configuration;
        }
        #region ADO.NET
        [HttpGet]
        [Route("refdetail/{id}")]
        public async Task<AjaxResult> GetRefDetail(string id)
        {
            try
            {
                ajaxResult.Data = _refRepository.GetRefDetail(Guid.Parse(id));
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
        [Route("RefAndService")]
        //[AllowAnonymous]
        public async Task<AjaxResult> UpdateRefAndServiceWhenPayOrder([FromBody] JObject data)
        {
            try
            {
                var refId = data["refId"] != null ? Guid.Parse(data["refId"].ToString()) : Guid.Empty;
                var totalAmount = (decimal)data["totalAmount"];
                //var timeEnd = ((DateTime)data["timeEnd"]).ToLocalTime();
                //var timeEnd2 = ((DateTime)data["timeEnd"]);
                var timeEnd = DateTime.ParseExact(data["timeEnd"].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture); ;
                ajaxResult.Data = _refRepository.UpdateRefAndServiceWhenPayOrder(refId, totalAmount, timeEnd);
                //ajaxResult.Data = new DateTime[] { timeEnd, timeEnd2 , timeEnd3 };
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
        [Route("RefDetailAndRefService/{refId}")]
        //[AllowAnonymous]
        public async Task<AjaxResult> DeleteRefDetailRefServiceAndUpdateServiceByRefID(Guid refId)
        {
            try
            {
                ajaxResult.Data = _refRepository.DeleteRefDetailRefServiceAndUpdateServiceByRefID(refId);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Messenge = Resources.ExceptionErroMsg;
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
                ajaxResult.Data = await _refRepository.GetRefDataStatistic(fromDate, toDate);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Data = ex;
                ajaxResult.Messenge = Resources.ExceptionErroMsg;
            }

            return ajaxResult;
        }
        #endregion

        #region EF

        [HttpGet("NewRefCode")]
        public async Task<Entities.AjaxResult> GetNewRef()
        {
            // Lấy phiếu gần nhất:
            ajaxResult.Data = await _refRepository.GetNewRefCode();
            return ajaxResult;
        }

        /// <summary>
        /// Thêm mới hóa đơn khi thực hiện thanh toán bán lẻ
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// CreatedBy: NVMANH (09/08/2020)
        [HttpPost("RefSale")]
        public async Task<AjaxResult> PostRef(Entities.Ref order)
        {
            // Điều chỉnh thời gian đúng múi giờ được thiết lập (tránh tính toán sai sót thời gian)
            //string windowsZoneId = TimeZoneConvert.RailsToWindows(Common.Common.TimeZoneId);
            //TimeZoneInfo timeInfo = TimeZoneInfo.FindSystemTimeZoneById(windowsZoneId);
            //order.RefDate = TimeZoneInfo.ConvertTimeFromUtc((DateTime)order.RefDate, timeInfo);
            order.RefState = (int)RefState.Payed;
            order.RefType = (int)RefType.Sale;
            order.CreatedDate = DateTime.Now.AddHours(7);
            order.JournalMemo = "Thanh toán bán lẻ";

            ajaxResult.Data = await _refRepository.AddNewRefForSale(order);
            //_context.Ref.Add(order);
            //await _context.SaveChangesAsync();

            return ajaxResult;
        }
        #endregion
    }
}
