using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSOFT.Common
{
    public class Utinity
    {
        public static string GetEntityName<T>()
        {
            var entity = Activator.CreateInstance<T>();
            return entity.GetType().Name;
        }

        public static object ConvertType(string value, SqlDbType sqlDbType)
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

        public static Type GetClrType(SqlDbType sqlType)
        {
            switch (sqlType)
            {
                case SqlDbType.BigInt:
                    return typeof(long?);

                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                    return typeof(byte[]);

                case SqlDbType.Bit:
                    return typeof(bool?);

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                    return typeof(string);

                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.Time:
                case SqlDbType.DateTime2:
                    return typeof(DateTime?);

                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return typeof(decimal?);

                case SqlDbType.Float:
                    return typeof(double?);

                case SqlDbType.Int:
                    return typeof(int?);

                case SqlDbType.Real:
                    return typeof(float?);

                case SqlDbType.UniqueIdentifier:
                    return typeof(Guid?);

                case SqlDbType.SmallInt:
                    return typeof(short?);

                case SqlDbType.TinyInt:
                    return typeof(byte?);

                case SqlDbType.Variant:
                case SqlDbType.Udt:
                    return typeof(object);

                case SqlDbType.Structured:
                    return typeof(DataTable);

                case SqlDbType.DateTimeOffset:
                    return typeof(DateTimeOffset?);

                default:
                    throw new ArgumentOutOfRangeException("sqlType");
            }
        }
    }
}
