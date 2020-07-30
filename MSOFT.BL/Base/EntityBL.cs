using MSOFT.DL;
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
        public virtual IEnumerable<T> GetData()
        {
            BaseDL baseDL = new BaseDL();
            return baseDL.GetEntities<T>();
        }
        public virtual T GetEntityByID(object entityID)
        {
            BaseDL baseDL = new BaseDL();
            return baseDL.GetEntityByID<T>(entityID);
        }

        public virtual int InsertEntity(T entity)
        {
            BaseDL baseDL = new BaseDL();
            return baseDL.InsertEntity(entity);
        }

        public virtual int UpdateEntity(T entity)
        {
            BaseDL baseDL = new BaseDL();
            return baseDL.UpdateEntity(entity);
        }

        public virtual int UpdateEntity(object[] param)
        {
            BaseDL baseDL = new BaseDL();
            return baseDL.UpdateEntity(param);
        }

        public virtual int DeleteEntityByID(string entityId)
        {
            BaseDL baseDL = new BaseDL();
            return baseDL.DeleteEntityByID<T>(entityId);
        }
    }
}
