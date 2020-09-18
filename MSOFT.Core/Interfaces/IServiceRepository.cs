using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Core.Interfaces
{
    public interface IServiceRepository : IBaseRepository
    {
        Task<object> UpdateInUserForService(object[] parameters);

        /// <summary>
        /// Lấy danh sách các dịch vụ đang không sử dụng
        /// </summary>
        /// <returns></returns>
        /// Created By : NVMANH
        Task<object> GetServiceNotInUse();
    }
}
