using MSOFT.DL.Interfaces;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.DL
{
    public class ServiceDL : BaseDL, IServiceRepository
    {
        public object UpdateInUserForService(object[] parameters)
        {
            return base.ExcuteScalar(parameters, "Proc_UpdateInUserInfoForService");
        }

        /// <summary>
        /// Lấy danh sách các dịch vụ đang không sử dụng
        /// </summary>
        /// <returns></returns>
        /// Created By : NVMANH
        public object GetServiceNotInUse()
        {
            return GetEntities<Service>("Proc_GetServiceNotInUse");
        }
    }
}
