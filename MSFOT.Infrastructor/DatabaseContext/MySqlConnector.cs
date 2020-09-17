using MSOFT.Common;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSFOT.Infrastructor.DatabaseContext
{
    public class MySqlConnector : IDisposable
    {
        #region Declare
        private readonly string connectionString = string.Empty;
        MySqlConnection sqlConnection;
        MySqlCommand sqlCommand;
        #endregion
        public MySqlConnector()
        {
            sqlConnection = new MySqlConnection(connectionString);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
        }

        public void Dispose()
        {
            if (sqlConnection.State == ConnectionState.Open)
                sqlConnection.Close();
        }
        #region METHOD
        #region GET
        /// <summary>
        /// Lấy dữ liệu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText">Sql Query hoặc tên thủ tục (procedure)</param>
        /// <param name="parameters">Đối số truyền vào</param>
        /// <param name="commandType">Thực thi chuỗi truy vấn hoặc qua thủ tục</param>
        /// <returns>List dữ liệu</returns>
        /// CreatedBy: NVMANH (17/09/2020)
        public async Task<List<T>> GetData<T>(string commandText, object[] parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            var entities = new List<T>();
            sqlCommand.CommandType = commandType;
            sqlCommand.CommandText = commandText;
            MySqlDataReader mySqlDataReader = sqlCommand.ExecuteReader();
            Func<MySqlDataReader, T> readRow = this.GetReader<T>(mySqlDataReader);
            while (await mySqlDataReader.ReadAsync())
                entities.Add(readRow(mySqlDataReader));
            return entities;
        }
        #endregion
        #region INSERT
        /// <summary>
        /// Thực hiện thêm mới đối tượng từ query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText">Câu lệnh truy vấn</param>
        /// <param name="parammeters">Dic các tham số map tương ứng với các property</param>
        /// <param name="commandType">Loại command</param>
        /// <returns></returns>
        /// CreatedBy: NVMANH (17/09/2020)
        public async Task<int> Insert<T>(string commandText, IDictionary<string, object> parammeters, CommandType commandType = CommandType.Text)
        {
            sqlCommand.CommandType = commandType;
            sqlCommand.CommandText = commandText;
            foreach (var item in parammeters)
            {
                sqlCommand.Parameters.AddWithValue($"@{item.Key}", item.Value);
            }
            var res = await sqlCommand.ExecuteNonQueryAsync();
            return res;
        }

        /// <summary>
        /// Thêm mới dữ liệu bằng cách sử dụng các Procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName">Tên Procedure đã viết trước</param>
        /// <param name="parameters">Mảng các đối số truyền vào theo thứ tự Param trong procedure</param>
        /// <returns>(int) - số bản ghi thêm mới được</returns>
        /// CreatedBy: NVMANH (07/09/2020)
        public async Task<int> Insert<T>(string procedureName, object[] parameters = null)
        {
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = procedureName;
            MySqlCommandBuilder.DeriveParameters(sqlCommand);
            var storeParameters = sqlCommand.Parameters;
            if (storeParameters.Count > 1 && parameters != null)
            {
                for (int i = 1; i < storeParameters.Count; i++)
                {
                    if (i <= parameters.Length)
                    {
                        var value = parameters[i - 1].ToString();
                        var result = Utinity.ConvertType(value, storeParameters[i].MySqlDbType);
                        storeParameters[i].Value = result;
                    }
                }
            }
            var res = await sqlCommand.ExecuteNonQueryAsync();
            return res;
        }
        #endregion

        #region UPDATE

        #endregion
        #region DELETE

        #endregion
        #endregion
        #region Utility
        /// <summary>
        /// GetReader and Dynamic Lambda Expressions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected Func<MySqlDataReader, T> GetReader<T>(MySqlDataReader reader)
        {
            Delegate resDelegate;
            // Step 1 - Get Column List
            List<string> readerColumns = new List<string>();
            for (int index = 0; index < reader.FieldCount; index++)
                readerColumns.Add(reader.GetName(index));

            // Step 2 - Setup Input ParameterExpression
            var readerParam = Expression.Parameter(typeof(MySqlDataReader), "reader");
            var readerGetValue = typeof(MySqlDataReader).GetMethod("GetValue");

            // Step 3 - Setup the DBNull Check
            var dbNullValue = typeof(System.DBNull).GetField("Value");
            //var dbNullExp = Expression.Field(Expression.Parameter(typeof(System.DBNull), "System.DBNull"), dbNullValue);
            var dbNullExp = Expression.Field(expression: null, type: typeof(DBNull), fieldName: "Value");

            // Step 4 - Create a List of MemberBinding Expressions for each Property
            List<MemberBinding> memberBindings = new List<MemberBinding>();
            foreach (var prop in typeof(T).GetProperties())
            {
                if (readerColumns.Contains(prop.Name))
                {
                    // determine the default value of the property
                    object defaultValue = null;
                    if (prop.PropertyType.IsValueType)
                        defaultValue = Activator.CreateInstance(prop.PropertyType);
                    else if (prop.PropertyType.Name.ToLower().Equals("string"))
                        defaultValue = string.Empty;

                    // build the Call expression to retrieve the data value from the reader
                    var indexExpression = Expression.Constant(reader.GetOrdinal(prop.Name));
                    var getValueExp = Expression.Call(readerParam, readerGetValue, new Expression[] { indexExpression });

                    // create the conditional expression to make sure the reader value != DBNull.Value
                    var testExp = Expression.NotEqual(dbNullExp, getValueExp);
                    var ifTrue = Expression.Convert(getValueExp, prop.PropertyType);
                    var ifFalse = Expression.Convert(Expression.Constant(defaultValue), prop.PropertyType);

                    // create the actual Bind expression to bind the value from the reader to the property value
                    MemberInfo mi = typeof(T).GetMember(prop.Name)[0];
                    MemberBinding mb = Expression.Bind(mi, Expression.Condition(testExp, ifTrue, ifFalse));
                    memberBindings.Add(mb);
                }
            }

            // Step 5 - Create and Compile the Lambda Function
            var newItem = Expression.New(typeof(T));
            var memberInit = Expression.MemberInit(newItem, memberBindings);
            var lambda = Expression.Lambda<Func<MySqlDataReader, T>>(memberInit, new ParameterExpression[] { readerParam });
            resDelegate = lambda.Compile();
            return (Func<MySqlDataReader, T>)resDelegate;
        }

        #endregion
    }
}
