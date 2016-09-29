using System.Data;

namespace Candidates.DataAccess
{
    public interface IDbInitializer
    {
        bool EnsureDatabaseCreated(IDbConnection connection, string databaseName);
        void CreateTables(IDbConnection connection);
    }
}