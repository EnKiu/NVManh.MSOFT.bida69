using System;
using System.Collections.Generic;
using System.Text;

namespace MSOFT.BL.Interfaces
{
    public interface IBaseBL<T>
    {
        IEnumerable<T> GetData();
        T GetEntityByID(object entityID);
        int InsertEntity(T entity);
        int UpdateEntity(T entity);
        int UpdateEntity(object[] param);
        int DeleteEntityByID(string entityId);
    }
}
