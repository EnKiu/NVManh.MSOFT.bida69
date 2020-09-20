using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Core.Interfaces
{
    public interface IEntityService
    {
        Task<IEnumerable<T>> GetData<T>(string commandText = null);
        Task<T> GetEntityByID<T>(object entityID);
        Task<int> InsertEntity<T>(T entity);
        Task<int> UpdateEntity<T>(T entity);
        Task<int> UpdateEntity<T>(object[] param);
        Task<int> DeleteEntityByID<T>(string entityId);
    }
}
