using System;
using System.Data;

namespace Candidates.DataAccess
{
    public interface ISqlDbTypeMapper
    {
        SqlDbType GetSqlDbType(Type type);
    }
}