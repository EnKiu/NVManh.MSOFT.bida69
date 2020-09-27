using MSOFT.Core.Interfaces;
using MSOFT.Infrastructure.DatabaseContext;
using MSOFT.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Infrastructure.Repository
{
    public class ADORepository : QueryUtinity, IBaseRepository
    {
        protected readonly IDataContext _dataContext;
        public ADORepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #region GET
        public async Task<IEnumerable<T>> Select<T>(object criteria = null)
        {
            var properties = ParseProperties(criteria);
            var tableName = typeof(T).Name;
            var sqlPairs = GetSqlPairs(properties.AllNames, " AND ");
            var sql = string.Format("SELECT * FROM {0} WHERE {1}", tableName, sqlPairs);
            if (criteria == null)
                sql = $"SELECT * FROM {tableName}";
            //using var dbConnection = new MySqlConnector();
            return await _dataContext.GetData<T>(sql, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            return await Select<T>();
        }

        public Task<IEnumerable<T>> Get<T>(object[] parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> Get<T>(string procedureName, object[] parameters = null)
        {
            return await _dataContext.GetData<T>(procedureName, parameters);
        }

        public async Task<T> GetById<T>(object entityID)
        {
            var entity = Activator.CreateInstance<T>();
            var propertyContainer = ParseProperties(entity, entityID);
            var tableName = typeof(T).Name;
            var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
            var sql = string.Format("SELECT * FROM {0} WHERE {1}", tableName, sqlIdPairs);
            return (await _dataContext.GetById<T>(sql, propertyContainer.IdPairs, commandType: CommandType.Text));
        }
        #endregion

        #region INSERT
        public async Task<int> Insert<T>(T entity)
        {
            var propertyContainer = ParseProperties(entity);
            var sql = string.Format("INSERT INTO {0} ({1}) VALUES(@{2})",
                typeof(T).Name,
                string.Join(", ", propertyContainer.ValueNames),
                string.Join(", @", propertyContainer.ValueNames));
            //var paramValue = param.SelectMany(x => x.Value);
            //using var sqlConnection = new MySqlConnector();
            var paramDic = propertyContainer.ValuePairs;
            return await _dataContext.Insert<T>(sql, paramDic);
        }

        #endregion

        #region UPDATE
        public async Task<int> Update<T>(T entity)
        {
            var propertyContainer = ParseProperties(entity);
            var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
            var sqlValuePairs = GetSqlPairs(propertyContainer.ValueNames);
            var sql = string.Format("UPDATE {0} SET {1} WHERE {2}",
                typeof(T).Name,
                sqlValuePairs,
                sqlIdPairs);
            //using var sqlConnection = new MySqlConnector();
            return await _dataContext.Update<T>(sql, propertyContainer.AllPairs);
        }

        public Task<int> Update<T>(string procedureName, object[] parameters)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region DELETE
        public async Task<int> Delete<T>(object entityID)
        {
            var obj = Activator.CreateInstance<T>();
            var propertyContainer = ParseProperties(obj);
            var sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
            var sql = string.Format("DELETE FROM [{0}] WHERE {1}", 
                typeof(T).Name, sqlIdPairs);
            //using var sqlConnection = new MySqlConnector();
            return await _dataContext.ExecuteNonQueryAsync(sql);
        }

        public Task<int> Delete<T>(object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(string storeName, object[] parameters)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Common- method

        #endregion
    }
}
