using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MSFOT.Infrastructor.Interfaces
{
    public interface IBaseRepository
    {
        Task<IEnumerable<T>> Select<T>(object criteria = null);
        Task<IEnumerable<T>> GetAll<T>();
        Task<T> GetById<T>(object entityID);
        Task<IEnumerable<T>> Get<T>(object[] parameters);
        Task<IEnumerable<T>> Get<T>(string procedureName, object[] parameters);
        Task<int> Insert<T>(T entity);
        Task<int> Update<T>(T entity);
        Task<int> Update<T>(string procedureName, object[] parameters);
        Task<int> Delete<T>(object entityID);
        Task<int> Delete<T>(object[] parameters);
        Task<int> Delete<T>(string storeName, object[] parameters);
    }
}
