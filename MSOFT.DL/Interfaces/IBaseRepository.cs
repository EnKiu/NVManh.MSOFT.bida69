using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.DL.Interfaces
{
    public interface IBaseRepository
    {
        IEnumerable<T> GetEntities<T>();
        T GetEntityByID<T>(object entityID);
        IEnumerable<T> GetEntities<T>(string storeName);
        IEnumerable<T> GetEntities<T>(string storeName, object[] parameters);
        int InsertEntity<T>(T entity);
        int UpdateEntity<T>(T entity);
        int UpdateEntity<T>(object[] parameters);
        int UpdateEntity<T>(string storeName, object[] parameters);
        int DeleteEntityByID<T>(string entityID);
        int DeleteEntity<T>(object[] parameters);
        int DeleteEntity<T>(string storeName, object[] parameters);
    }
}
