using MSFOT.Infrastructor.DatabaseContext;
using MSFOT.Infrastructor.Interfaces;
using System;
using System.Collections.Generic;
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

        public Task<int> Insert<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> Select<T>(object criteria = null)
        {
            IEnumerable<T> entities = new List<T>();
            var properties = ParseProperties(criteria);
            var tableName = typeof(T).Name;
            var sqlPairs = GetSqlPairs(properties.AllNames, " AND ");
            var sql = string.Format("SELECT * FROM {0} WHERE {1}", tableName, sqlPairs);
            if (criteria == null)
                sql = $"SELECT * FROM {tableName}";
            using var adoConnection = new ADOConnection();
            return await adoConnection.GetData<T>(sql, commandType: System.Data.CommandType.Text);
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
}
