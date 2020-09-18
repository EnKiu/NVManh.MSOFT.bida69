using MSOFT.Core.Interfaces;
using MSOFT.Entities;
using MSOFT.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Infrastructure.Repository
{
    public class ServiceRepository:ADORepository, IServiceRepository
    {
        public ServiceRepository(IDataContext dataContext):base(dataContext)
        {
        }
        public async Task<object> UpdateInUserForService(object[] parameters)
        {
            return await _dataContext.ExecuteScalarAsync("Proc_UpdateInUserInfoForService", parameters);
        }

        /// <summary>
        /// Lấy danh sách các dịch vụ đang không sử dụng
        /// </summary>
        /// <returns></returns>
        /// Created By : NVMANH
        public async Task<object> GetServiceNotInUse()
        {
            return await Get<Service>("Proc_GetServiceNotInUse");
        }
    }
}
