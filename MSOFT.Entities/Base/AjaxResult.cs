﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Entities
{
    public class AjaxResult
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public AjaxResult()
        {
            Success = true;
        }
    }
}
