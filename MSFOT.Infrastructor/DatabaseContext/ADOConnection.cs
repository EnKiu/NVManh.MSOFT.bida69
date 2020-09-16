using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSFOT.Infrastructor.DatabaseContext
{
    public class ADOConnection : IDisposable
    {
        private readonly string connectionString = string.Empty;
        MySqlConnection sqlConnection;
        MySqlCommand sqlCommand;

        public ADOConnection()
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

        public IEnumerable<T> GetData<T>()
        {
            sqlCommand.CommandText = $"Proc_Get{typeof(T).Name}";
            MySqlDataReader mySqlDataReader = sqlCommand.ExecuteReader();
            Func<MySqlDataReader, T> readRow = this.GetReader<T>(mySqlDataReader);

            while (mySqlDataReader.Read())
                yield return readRow(mySqlDataReader);
        }


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
    }
}
