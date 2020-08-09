using MSOFT.BL.Interfaces;
using MSOFT.DL;
using MSOFT.DL.Interfaces;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.BL
{
    public class ServiceBL:EntityBL<Service>,IDisposable,IServiceBL
    {
        IServiceRepository _iServiceRepository;
        public ServiceBL(IServiceRepository iServiceRepository) : base(iServiceRepository)
        {
            _iServiceRepository = iServiceRepository;
        }
        public object UpdateInUserForService(object[] parameters)
        {
            return _iServiceRepository.UpdateInUserForService(parameters);
        }

        /// <summary>
        /// Lấy danh sách các dịch vụ đang không sử dụng
        /// </summary>
        /// <returns></returns>
        /// Created By : NVMANH
        public object GetServiceNotInUse()
        {
            return _iServiceRepository.GetServiceNotInUse();
        }
        public void Dispose()
        {
        }
    }
}
