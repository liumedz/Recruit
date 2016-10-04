using System;
using System.Collections.Generic;
using System.Data;

namespace Candidates.DataAccess
{
    public class SqlDbTypeMapper: ISqlDbTypeMapper
    {
        private static Dictionary<Type, SqlDbType> _types = new Dictionary<Type, SqlDbType>()
        {
            {typeof(Int64) , SqlDbType.BigInt},
            {typeof(Byte), SqlDbType.Binary},
            {typeof(Boolean), SqlDbType.Bit},
            {typeof(DateTime), SqlDbType.DateTime},
            {typeof(DateTimeOffset), SqlDbType.DateTimeOffset},
            {typeof(Decimal), SqlDbType.Decimal},
            {typeof(Double), SqlDbType.Float},
            {typeof(Int32), SqlDbType.Int},
            {typeof(Single), SqlDbType.Real},
            {typeof(Int16), SqlDbType.SmallInt},
            {typeof(String), SqlDbType.Text},
            {typeof(TimeSpan), SqlDbType.Time},
            {typeof(Guid), SqlDbType.UniqueIdentifier}
        };
        public SqlDbType GetSqlDbType(Type type)
        {
            SqlDbType dbType;
            if(!_types.ContainsKey(type))
                throw new ArgumentException("Unknown type");
            _types.TryGetValue(type, out dbType);
            return dbType;
        }
    }
}
