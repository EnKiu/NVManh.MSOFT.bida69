using MSOFT.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        public async virtual Task<int> DeleteEntityByID<T>(string entityId)
        {
            return await _baseRepository.Delete<T>(entityId);
        }

        public async virtual Task<IEnumerable<T>> GetData<T>(string commandText = null)
        {
            if (commandText == null)
                return await _baseRepository.GetAll<T>();
            else
                return await _baseRepository.Get<T>(commandText);
        }

        public async virtual Task<T> GetEntityByID<T>(object entityID)
        {
            return await _baseRepository.GetById<T>(entityID);
        }

        public async virtual Task<int> InsertEntity<T>(T entity)
        {
            return await _baseRepository.Insert<T>(entity);
        }

        public async virtual Task<int> UpdateEntity<T>(T entity)
        {
            return await _baseRepository.Update<T>(entity);
        }

        public async virtual Task<int> UpdateEntity<T>(object[] param)
        {
            return await _baseRepository.Update<T>(parameters: param);
        }
    }
}
