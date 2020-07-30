using MSOFT.Common;
using MSOFT.DL.Properties;
using MSOFT.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.DL
{
    public class BaseDL
    {
        protected string _getDataStoreName;
        private string _tableName;
        public BaseDL()
        {
            //_tableName = Utinity.GetEntityName<T>();
        }
        public virtual IEnumerable<T> GetEntities<T>()
        {
            _tableName = Utinity.GetEntityName<T>();
            string storeName = String.Format(Resources.ProcTemplate_GetData, _tableName);
            return GetEntities<T>(storeName);
        }

        public virtual T GetEntityByID<T>(object entityID)
        {
            _tableName = Utinity.GetEntityName<T>();
            string storeName = String.Format(Resources.ProcTemplate_GetDataByID, _tableName);
            return GetEntities<T>(storeName, new object[] { entityID }).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetEntities<T>(string storeName)
        {
            using (DataAccess dataAccess = new DataAccess())
            {
                SqlDataReader sqlDataReader = dataAccess.ExecuteReader(storeName);
                while (sqlDataReader.Read())
                {
                    var entity = Activator.CreateInstance<T>();
                    for (int i = 0; i < sqlDataReader.FieldCount; i++)
                    {
                        var fieldName = sqlDataReader.GetName(i);
                        var fieldValue = sqlDataReader.GetValue(i);
                        var property = entity.GetType().GetProperty(fieldName);
                        if (fieldValue != System.DBNull.Value && property != null)
                        {
                            property.SetValue(entity, fieldValue);
                        }
                    }
                    yield return entity;
                }
            }
        }

        public virtual IEnumerable<T> GetEntities<T>(string storeName, object[] parameters)
        {
            using (DataAccess dataAccess = new DataAccess())
            {
                dataAccess.MapValueToStorePram(storeName, parameters);
                SqlDataReader sqlDataReader = dataAccess.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    var entity = Activator.CreateInstance<T>();
                    for (int i = 0; i < sqlDataReader.FieldCount; i++)
                    {
                        var fieldName = sqlDataReader.GetName(i);
                        var fieldValue = sqlDataReader.GetValue(i);
                        var property = entity.GetType().GetProperty(fieldName);
                        if (fieldValue != System.DBNull.Value && property != null)
                        {
                            property.SetValue(entity, fieldValue);
                        }
                    }
                    yield return entity;
                }
            }
        }

        public virtual int InsertEntity<T>(T entity)
        {
            _tableName = Utinity.GetEntityName<T>();
            string storeName = String.Format(Resources.ProcTemplate_InsertEntity, _tableName);
            return SubmitEntity(entity, storeName);
        }

        public virtual int UpdateEntity<T>(T entity)
        {
            _tableName = Utinity.GetEntityName<T>();
            string storeName = String.Format(Resources.ProcTemplate_UpdateEntity, _tableName);
            return SubmitEntity(entity, storeName);
        }

        /// <summary>
        /// Cập nhật đối tượng theo nhiều tham số truyền vào
        /// </summary>
        /// <param name="parameters">mảng các tham số truyền vào</param>
        /// <returns>Số lượng bản ghi cập nhật thành công</returns>
        /// CreatedBy: NVManh (07/07/2019)
        public virtual int UpdateEntity<T>(object[] parameters)
        {
            _tableName = Utinity.GetEntityName<T>();
            string storeName = String.Format(Resources.ProcTemplate_UpdateEntity, _tableName);
            return ExecuteNonQuery(parameters, storeName);
        }


        /// <summary>
        /// Cập nhật đối tượng theo nhiều tham số truyền vào
        /// </summary>
        /// <param name="parameters">mảng các tham số truyền vào</param>
        /// <returns>Số lượng bản ghi cập nhật thành công</returns>
        /// CreatedBy: NVManh (07/07/2019)
        public virtual int UpdateEntity<T>(string storeName, object[] parameters)
        {
            return ExecuteNonQuery(parameters, storeName);
        }

        /// <summary>
        /// xóa đối tượng theo khóa chính
        /// </summary>
        /// <param name="entityID">khóa chính bản ghi</param>
        /// <returns>Số lượng bản ghi xóa thành công</returns>
        /// CreatedBy: NVManh (07/07/2019)
        public virtual int DeleteEntityByID<T>(string entityID)
        {
            _tableName = Utinity.GetEntityName<T>();
            string storeName = String.Format(Resources.ProcTemplate_DeleteEntityByID, _tableName);
            return DeleteEntity<T>(storeName, new object[] { entityID });
        }

        public virtual int DeleteEntity<T>(object[] parameters)
        {
            _tableName = Utinity.GetEntityName<T>();
            string storeName = String.Format(Resources.ProcTemplate_DeleteEntityByMultiParm, _tableName);
            return DeleteEntity<T>(storeName, parameters);
        }

        public virtual int DeleteEntity<T>(string storeName, object[] parameters)
        {
            using (DataAccess data = new DataAccess())
            {
                return data.ExecuteNonQuery(storeName, parameters);
            }
        }

        public virtual int SubmitEntity<T>(T entity, string storeName)
        {
            using (DataAccess dataAccess = new DataAccess())
            {
                var parameters = dataAccess.GetParamFromStore(storeName);
                foreach (SqlParameter parameter in parameters)
                {
                    var paramName = parameter.ToString().Replace("@", string.Empty);
                    var property = entity.GetType().GetProperty(paramName);
                    if (property != null)
                    {
                        var paramValue = property.GetValue(entity);
                        parameter.Value = paramValue != null ? paramValue : DBNull.Value;
                    }
                    else
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
                return dataAccess.ExecuteNonQuery();
            }
        }

        public virtual int ExecuteNonQuery(object[] parameters, string storeName)
        {
            using (DataAccess dataAccess = new DataAccess())
            {
                dataAccess.MapValueToStorePram(storeName, parameters);
                return dataAccess.ExecuteNonQuery();
            }
        }

        public virtual object ExcuteScalar(object[] parameters, string storeName)
        {
            using (DataAccess dataAccess = new DataAccess())
            {
                dataAccess.MapValueToStorePram(storeName, parameters);
                return dataAccess.ExecuteScalar();
            }
        }
    }
}
