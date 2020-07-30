using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSOFT.Entities;
using MSOFT.Common;
using MSOFT.DL.Properties;

namespace MSOFT.DL
{
    public class DataAccess : IDisposable
    {
        /// <summary>
        /// Chuỗi kết nối
        /// </summary>
        public static string ConnectionString { get; set; }
        SqlConnection _sqlConnection;
        SqlCommand _sqlCommand;
        SqlTransaction _sqlTransaction;

        public DataAccess()
        {
            if (_sqlConnection == null)
            {
                _sqlConnection = new SqlConnection(ConnectionString);
            }
            _sqlCommand = _sqlConnection.CreateCommand();
            _sqlCommand.CommandType = CommandType.StoredProcedure;
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection.Open();
            }
        }
        public void SetCommandText(string commandText)
        {
            _sqlCommand.CommandText = commandText;
        }

        public void MapValueToStorePram(string commandText, object[] parameters)
        {
            var paramsFromStore = GetParamFromStore(commandText);
            if (paramsFromStore.Count > 1 && parameters != null)
            {
                for (int i = 1; i < paramsFromStore.Count; i++)
                {
                    if (i <= parameters.Length)
                    {
                        var value = parameters[i - 1].ToString();
                        var result = Utinity.ConvertType(value, paramsFromStore[i].SqlDbType);
                        paramsFromStore[i].Value = result;
                    }
                }
            }
        }

        public SqlParameterCollection GetParamFromStore()
        {
            SqlCommandBuilder.DeriveParameters(_sqlCommand);
            return _sqlCommand.Parameters;
        }

        public SqlParameterCollection GetParamFromStore(string commandText)
        {
            _sqlCommand.CommandText = commandText;
            SqlCommandBuilder.DeriveParameters(_sqlCommand);
            return _sqlCommand.Parameters;
        }
        public SqlDataReader ExecuteReader()
        {
            return _sqlCommand.ExecuteReader();
        }
        public SqlDataReader ExecuteReader(string commandText)
        {
            _sqlCommand.CommandText = commandText;
            return _sqlCommand.ExecuteReader();
        }
        public int ExecuteNonQuery()
        {
            var result = 0;
            try
            {
                _sqlTransaction = _sqlConnection.BeginTransaction();
                _sqlCommand.Transaction = _sqlTransaction;
                result = _sqlCommand.ExecuteNonQuery();
                _sqlTransaction.Commit();
            }
            catch (Exception)
            {
                _sqlTransaction.Rollback();
            }
            return result;
        }
        public int ExecuteNonQuery(string commandText)
        {
            var result = 0;
            try
            {
                _sqlTransaction = _sqlConnection.BeginTransaction();
                _sqlCommand.Transaction = _sqlTransaction;
                _sqlCommand.CommandText = commandText;
                result = _sqlCommand.ExecuteNonQuery();
                _sqlTransaction.Commit();
            }
            catch (Exception)
            {
                _sqlTransaction.Rollback();
            }
            return result;
        }
        public int ExecuteNonQuery(string commandText, object[] parameters)
        {
            var result = 0;
            try
            {
                _sqlCommand.CommandText = commandText;
                MapValueToStorePram(commandText, parameters);
                _sqlTransaction = _sqlConnection.BeginTransaction();
                _sqlCommand.Transaction = _sqlTransaction;
                result = _sqlCommand.ExecuteNonQuery();
                _sqlTransaction.Commit();
            }
            catch (Exception)
            {
                _sqlTransaction.Rollback();
            }
            return result;
        }
        public object ExecuteScalar()
        {
            object result = null;
            try
            {
                _sqlTransaction = _sqlConnection.BeginTransaction();
                _sqlCommand.Transaction = _sqlTransaction;
                result = _sqlCommand.ExecuteScalar();
                _sqlTransaction.Commit();
            }
            catch (Exception)
            {
                _sqlTransaction.Rollback();
            }
            return result;
        }

        public object ExecuteScalar(string commandText)
        {
            object result = null;
            try
            {
                _sqlCommand.CommandText = commandText;
                _sqlTransaction = _sqlConnection.BeginTransaction();
                _sqlCommand.Transaction = _sqlTransaction;
                result = _sqlCommand.ExecuteScalar();
                _sqlTransaction.Commit();
            }
            catch (Exception)
            {
                _sqlTransaction.Rollback();
            }
            return result;
        }
        public void Dispose()
        {
            _sqlConnection.Close();
        }

    }
}
