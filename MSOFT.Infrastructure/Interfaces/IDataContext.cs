using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Infrastructure.Interfaces
{
    public interface IDataContext
    {
        Task<List<T>> GetData<T>(string commandText, object[] parameters = null, CommandType commandType = CommandType.StoredProcedure);
        Task<int> Insert<T>(string commandText, IDictionary<string, object> parammeters, CommandType commandType = CommandType.Text);
        Task<int> Insert<T>(string procedureName, object[] parameters = null);
        Task<int> Update<T>(string commandText, IDictionary<string, object> parammeters = null, CommandType commandType = CommandType.Text);
        Task<int> Delete<T>(string commandText, IDictionary<string, object> parammeters = null, CommandType commandType = CommandType.Text);
        Task<int> ExecuteNonQueryAsync(string commandText = null, object[] parameters = null, CommandType commandType = CommandType.StoredProcedure);
        Task<object> ExecuteScalarAsync(string commandText = null, object[] parameters = null, CommandType commandType = CommandType.StoredProcedure);
    }
}
