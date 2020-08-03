using MSOFT.DL;
using MSOFT.DL.Interfaces;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.BL
{
    public class EntityBL<T>
    {
        IBaseDL _baseDL;
        public EntityBL(IBaseDL baseDL)
        {
            _baseDL = baseDL;
        }
        public virtual IEnumerable<T> GetData()
        {
            return _baseDL.GetEntities<T>();
        }
        public virtual T GetEntityByID(object entityID)
        {
            return _baseDL.GetEntityByID<T>(entityID);
        }

        public virtual int InsertEntity(T entity)
        {
            return _baseDL.InsertEntity(entity);
        }

        public virtual int UpdateEntity(T entity)
        {
            return _baseDL.UpdateEntity(entity);
        }

        public virtual int UpdateEntity(object[] param)
        {
            return _baseDL.UpdateEntity(param);
        }

        public virtual int DeleteEntityByID(string entityId)
        {
            return baseDL.DeleteEntityByID<T>(entityId);
        }
    }
}
