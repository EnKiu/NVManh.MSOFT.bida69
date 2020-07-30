using MSOFT.DL;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.BL
{
    public class ServiceBL:EntityBL<Service>,IDisposable
    {
        public object UpdateInUserForService(object[] parameters)
        {
            var serviceDL = new ServiceDL();
            return serviceDL.UpdateInUserForService(parameters);
        }

        /// <summary>
        /// Lấy danh sách các dịch vụ đang không sử dụng
        /// </summary>
        /// <returns></returns>
        /// Created By : NVMANH
        public object GetServiceNotInUse()
        {
            var serviceDL = new ServiceDL();
            return serviceDL.GetServiceNotInUse();
        }
        public void Dispose()
        {
        }
    }
}
