using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Core.Interfaces
{
    public interface IBaseRepository
    {
        /// <summary>
        /// Select dữ liệu theo các tiêu chí
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="criteria">Các tiêu chí (không truyền vào nghĩa là lấy toàn bộ)</param>
        /// <returns></returns>
        /// CreatedBy: NVMANH (17/09/2020)
        Task<IEnumerable<T>> Select<T>(object criteria = null);

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// CreatedBy: NVMANH (17/09/2020)
        Task<IEnumerable<T>> GetAll<T>();

        /// <summary>
        /// Lấy bản ghi theo khóa chính
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityID">Giá trị của khóa chính</param>
        /// <returns></returns>
        /// CreatedBy: NVMANH (17/09/2020)
        Task<T> GetById<T>(object entityID);

        /// <summary>
        /// Lấy dữ liệu theo nhiều tiêu chí
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">Mảng chứa các tiêu chí cần lấy dữ liệu (không truyền vào nghĩa là sẽ lấy toàn bộ)</param>
        /// <returns></returns>
        /// CreatedBy: NVMANH (17/09/2020)
        Task<IEnumerable<T>> Get<T>(object[] parameters = null);

        /// <summary>
        /// Lấy dữ liệu theo các procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName">Tên của Procedure</param>
        /// <param name="parameters">Các tiêu chí sẽ lấy dữ liệu (null nghĩa là ko lấy theo tiêu chí nào)</param>
        /// <returns></returns>
        /// Createdby: NVMANH (17/09/2020)
        Task<IEnumerable<T>> Get<T>(string procedureName, object[] parameters = null);

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Createdby: NVMANH (17/09/2020)
        Task<int> Insert<T>(T entity);
        Task<int> Update<T>(T entity);
        Task<int> Update<T>(string procedureName, object[] parameters);
        Task<int> Delete<T>(object entityID);
        Task<int> Delete<T>(object[] parameters);
        Task<int> Delete<T>(string storeName, object[] parameters);
    }
}
