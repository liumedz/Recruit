﻿using System.Data;

namespace Candidates.DataAccess
{
    public class DbInitializer
    {
        public bool EnsureCreated(IDbConnection connection, string databaseName)
        {
            connection.Open();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT * FROM sysdatabases WHERE name='{databaseName}'";
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return false;
                }

                cmd.CommandText = $"CREATE DATABASE {databaseName}";
                cmd.ExecuteNonQuery();
            }
            connection.Close();
            return true;
        }
        public void CreateTables(IDbConnection connection)
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
            connection.Close();
        }
    }
}
