using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.DL.Interfaces
{
    public interface IServiceRepository : IBaseRepository
    {
        object UpdateInUserForService(object[] parameters);

        /// <summary>
        /// Lấy danh sách các dịch vụ đang không sử dụng
        /// </summary>
        /// <returns></returns>
        /// Created By : NVMANH
        object GetServiceNotInUse();
    }
}
