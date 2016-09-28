using System.Data.SqlClient;

namespace Candidates.DataAccess
{
    public static class DbInitializer
    {
        public static void EnsureCreated(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = connectionStringBuilder.InitialCatalog;

            connectionStringBuilder.InitialCatalog = "master";

            using (var connection = new SqlConnection(connectionStringBuilder.ToString()))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT * FROM master.dbo.sysdatabases WHERE name='{0}'", databaseName);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) 
                            return;
                    }

                    cmd.CommandText = string.Format("CREATE DATABASE {0}", databaseName);
                    cmd.ExecuteNonQuery();
                }
            }
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                    CREATE TABLE Candidates 
                       (id int IDENTITY(1,1),  
                        firstName text NULL,  
                        lastName text NULL,  
                        email text NULL,
	                    comment text NULL,
	                    created datetime NULL)";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
