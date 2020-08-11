using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using MSOFT.Common.Properties;
using MSOFT.Common;
using MSOFT.Entities;

namespace MSOFT.bida69.core.Middleware
{
    /// <summary>
    /// Xử lý khi có exception xảy ra
    /// </summary>
    /// CreatedBy: NVMANH (05/2020)
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var msg = Resources.ErrorException; // 500 if unexpected
            var mCode = MNVCode.Exception;

            //else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (ex is MyException) code = HttpStatusCode.BadRequest;
            var result = JsonConvert.SerializeObject(
                new AjaxResult
                {
                    Success = false,
                    Messenge = msg,
                    Code = mCode,
                    Data = ex.Message
                });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
