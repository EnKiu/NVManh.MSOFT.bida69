using MSOFT.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Core.Service
{
    public class EntityService : IEntityService
    {
        IBaseRepository _baseRepository;
        public EntityService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task<int> DeleteEntityByID<T>(string entityId)
        {
          return await  _baseRepository.Delete<T>(entityId);
        }

        public async Task<IEnumerable<T>> GetData<T>()
        {
            return await _baseRepository.GetAll<T>();
        }

        public async Task<T> GetEntityByID<T>(object entityID)
        {
            return await _baseRepository.GetById<T>(entityID);
        }

        public async Task<int> InsertEntity<T>(T entity)
        {
            return await _baseRepository.Insert<T>(entity);
        }

        public async Task<int> UpdateEntity<T>(T entity)
        {
            return await _baseRepository.Update<T>(entity);
        }

        public async Task<int> UpdateEntity<T>(object[] param)
        {
            return await _baseRepository.Update<T>(parameters:param);
        }
    }
}
