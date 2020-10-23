using MSOFT.Core.Enum;
using MSOFT.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MSOFT.Infrastructure
{
   
    public class QueryUtinity
    {
        protected class PropertyContainer
        {
            private readonly Dictionary<string, object> _ids;
            private readonly Dictionary<string, object> _values;

            #region Properties

            internal IEnumerable<string> IdNames
            {
                get { return _ids.Keys; }
            }

            internal IEnumerable<string> ValueNames
            {
                get { return _values.Keys; }
            }

            internal IEnumerable<string> AllNames
            {
                get { return _ids.Keys.Union(_values.Keys); }
            }

            internal IDictionary<string, object> IdPairs
            {
                get { return _ids; }
            }

            internal IDictionary<string, object> ValuePairs
            {
                get { return _values; }
            }

            internal IEnumerable<KeyValuePair<string, object>> AllPairs
            {
                get { return _ids.Concat(_values); }
            }

            #endregion

            #region Constructor

            internal PropertyContainer()
            {
                _ids = new Dictionary<string, object>();
                _values = new Dictionary<string, object>();
            }

            #endregion

            #region Methods

            internal void AddId(string name, object value)
            {
                _ids.Add(name, value);
            }

            internal void AddValue(string name, object value)
            {
                _values.Add(name, value);
            }

            #endregion
        }
        protected static PropertyContainer ParseProperties<T>(T obj, object propertyKeyValue = null)
        {
            var propertyContainer = new PropertyContainer();

            var typeName = typeof(T).Name;
            var validKeyNames = new[] {
                "Id",
                string.Format("{0}Id", typeName),
                string.Format("{0}_Id", typeName),
                string.Format("{0}ID", typeName)
            };

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                // Skip reference types (but still include string!)
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                    continue;

                // Skip methods without a public setter
                if (property.GetSetMethod() == null)
                    continue;

                // Skip methods specifically ignored
                if (property.IsDefined(typeof(PropertyIgnore), false))
                    continue;

                var name = property.Name;
                var value = typeof(T).GetProperty(property.Name).GetValue(obj, null);

                if (property.IsDefined(typeof(PropertyKey), false) || validKeyNames.Contains(name))
                {
                    if (propertyKeyValue != null)
                        value = propertyKeyValue;
                    propertyContainer.AddId(name, value);
                }
                else
                {
                    propertyContainer.AddValue(name, value);
                }
            }

            return propertyContainer;
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

        /// <summary>
        /// Create a commaseparated list of value pairs on 
        /// the form: "key1=@value1, key2=@value2, ..."
        /// </summary>
        protected static string GetSqlPairs
        (IEnumerable<string> keys, string separator = ", ")
        {
            var pairs = keys.Select(key => string.Format("{0}=@{0}", key)).ToList();
            return string.Join(separator, pairs);
        }

        protected static object castValue(object value, string sourceType, Type targetType)
        {
            //* This implementation requires further extension to convert types as per requirement arose.
            switch (sourceType)
            {
                case "System.DateTime":
                    switch (targetType.Name)
                    {
                        case "TimeSpan":
                            var dtValue = (DateTime)value;
                            TimeSpan tsValue = dtValue.TimeOfDay;
                            value = tsValue;
                            break;
                    }
                    break;
            }

            return value;
        }

        public static string GetEntityName<T>()
        {
            var entity = Activator.CreateInstance<T>();
            return entity.GetType().Name;
        }

        /// <summary>
        /// Sinh tên store theo tên bảng và kiểu thao tác dữ liệu của store
        /// </summary>
        /// <typeparam name="T">Entity truyền vào</typeparam>
        /// <param name="procdureTypeName">Kiểu thực thi của store (Thêm, sửa, xóa)</param>
        /// <returns></returns>
        /// CreatedBy: NVMANH (14/04/2020)
        public static string GeneateStoreName<T>(ProcdureTypeName procdureTypeName)
        {
            string storeName = string.Empty;
            var tableName = (Activator.CreateInstance<T>()).GetType().Name;
            switch (procdureTypeName)
            {
                case ProcdureTypeName.Get:
                    storeName = $"Proc_Get{tableName}s";
                    break;
                case ProcdureTypeName.GetById:
                    storeName = $"Proc_Get{tableName}ByID";
                    break;
                case ProcdureTypeName.Insert:
                    storeName = $"Proc_Insert{tableName}";
                    break;
                case ProcdureTypeName.Update:
                    storeName = $"Proc_Update{tableName}";
                    break;
                case ProcdureTypeName.Delete:
                    storeName = $"Proc_Delete{tableName}";
                    break;
                case ProcdureTypeName.GetPaging:
                    storeName = $"Proc_Get{tableName}sPaging";
                    break;
                default:
                    break;
            }
            return storeName;
        }

        /// <summary>
        /// Hàm thực hiện việc convert kiểu dữ liệu từ MySqlDbType sang kiểu dữ liệu tương ứng trên c#
        /// </summary>
        /// <param name="value">giá trị</param>
        /// <param name="sqlDbType">Kiểu dữ liệu trên MariaDb (MySqlDbType)</param>
        /// <returns></returns>
        /// CreatedBy: NVMANH (15/04/2020)
        public static object ConvertType(string value, MySqlDbType sqlDbType)
        {
            object result;
            Type type = GetClrType(sqlDbType);

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            try
            {
                var converter = TypeDescriptor.GetConverter(type);
                result = converter.ConvertFromString(value);

            }
            catch (Exception exception)
            {
                // Log this exception if required.
                throw new InvalidCastException(string.Format("Unable to cast the {0} to type {1}", value, "newType", exception));
            }
            return result;
        }

        /// <summary>
        /// Láy về kiểu dữ liệu tương ứng trên c#
        /// </summary>
        /// <param name="sqlType">MySqlDbType</param>
        /// <returns></returns>
        /// CreatedBy: NVMANH (15/04/2020)
        private static Type GetClrType(MySqlDbType sqlType)
        {
            switch (sqlType)
            {
                case MySqlDbType.UInt16:
                case MySqlDbType.UInt24:
                case MySqlDbType.UInt32:
                case MySqlDbType.UInt64:
                    return typeof(long?);

                case MySqlDbType.Binary:
                //case MySqlDbType.Image:
                case MySqlDbType.Timestamp:
                case MySqlDbType.VarBinary:
                    return typeof(byte[]);

                case MySqlDbType.Bit:
                    return typeof(bool?);

                case MySqlDbType.VarChar:
                case MySqlDbType.LongText:
                case MySqlDbType.String:
                case MySqlDbType.Text:
                    return typeof(string);

                case MySqlDbType.DateTime:
                case MySqlDbType.Date:
                case MySqlDbType.Time:
                    return typeof(DateTime?);

                case MySqlDbType.Decimal:
                    return typeof(decimal?);

                case MySqlDbType.Double:
                    return typeof(Double?);

                case MySqlDbType.Float:
                    return typeof(double?);

                case MySqlDbType.Int32:
                case MySqlDbType.Int64:
                    return typeof(int?);

                case MySqlDbType.Int16:
                    return typeof(short?);
                case MySqlDbType.Guid:
                    return typeof(Guid);
                default:
                    throw new ArgumentOutOfRangeException("sqlType");
            }
        }
    }
}
