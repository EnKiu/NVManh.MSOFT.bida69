using MSFOT.Infrastructor.DatabaseContext;
using MSFOT.Infrastructor.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MSFOT.Infrastructor.Repository
{
    public class ADORepository : QueryUtinity, IBaseRepository
    {
        public Task<int> Delete<T>(object entityID)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete<T>(string storeName, object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Get<T>(object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Get<T>(string procedureName, object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll<T>()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById<T>(object entityID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert<T>(T entity)
        {
            var propertyContainer = ParseProperties(entity);
            var sql = string.Format("INSERT INTO {0} ({1}) VALUES(@{2})",
                typeof(T).Name,
                string.Join(", ", propertyContainer.ValueNames),
                string.Join(", @", propertyContainer.ValueNames));
            //var paramValue = param.SelectMany(x => x.Value);
            using var sqlConnection = new MySqlConnector();
            var paramDic = propertyContainer.ValuePairs;
            return await sqlConnection.Insert<T>(sql, paramDic);
        }


        /// <summary>
        /// Select dữ liệu theo các tiêu chí
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="criteria">Các tiêu chí</param>
        /// <returns></returns>
        /// CreatedBy: NVMANH (17/09/2020)
        public async Task<IEnumerable<T>> Select<T>(object criteria = null)
        {
            var properties = ParseProperties(criteria);
            var tableName = typeof(T).Name;
            var sqlPairs = GetSqlPairs(properties.AllNames, " AND ");
            var sql = string.Format("SELECT * FROM {0} WHERE {1}", tableName, sqlPairs);
            if (criteria == null)
                sql = $"SELECT * FROM {tableName}";
            using var dbConnection = new MySqlConnector();
            return await dbConnection.GetData<T>(sql, commandType: CommandType.Text);
        }
    }

    public Task<int> Update<T>(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> Update<T>(string procedureName, object[] parameters)
    {
        throw new NotImplementedException();
    }
}
