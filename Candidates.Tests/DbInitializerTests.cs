using Candidates.DataAccess;
using Candidates.Tests.Mocks;
using System.Configuration;
using System.Data.SqlClient;
using Xunit;

namespace Candidates.Tests
{
    public class DbInitializerTests
    {
        [Fact]
        public void Should_Create_Database()
        {
            var dbCommand = new DbCommandMock();
            var dbConnection = new DbConnectionMock(dbCommand);
            dbCommand.DataReader = new DataReaderMockEmpty();

            var cs = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
            var csBuilder = new SqlConnectionStringBuilder(cs);
            var databaseName = csBuilder.InitialCatalog;

            csBuilder.InitialCatalog = "master";
            var dbInitializer = new DbInitializer();
            var created = dbInitializer.EnsureDatabaseCreated(dbConnection, databaseName);

            Assert.Equal(false, dbCommand.ScalarExecuted);
            Assert.Equal(true, dbCommand.NonQueryExecuted);
            Assert.Equal(true, dbCommand.ReaderExecuted);
        }

        [Fact]
        public void Should_Not_Create_If_Database_Exist()
        {
            var dbCommand = new DbCommandMock();
            var dbConnection = new DbConnectionMock(dbCommand);
            dbCommand.DataReader = new DataReaderMockNotEmpty();

            var cs = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
            var csBuilder = new SqlConnectionStringBuilder(cs);
            var databaseName = csBuilder.InitialCatalog;

            csBuilder.InitialCatalog = "master";
            var dbInitializer = new DbInitializer();
            dbInitializer.EnsureDatabaseCreated(dbConnection, databaseName);

            Assert.Equal(false, dbCommand.ScalarExecuted);
            Assert.Equal(false, dbCommand.NonQueryExecuted);
            Assert.Equal(true, dbCommand.ReaderExecuted);
        }
        [Fact]
        public void Should_Create_Tables()
        {
            var dbCommand = new DbCommandMock();
            var dbConnection = new DbConnectionMock(dbCommand);

            var cs = ConfigurationManager.ConnectionStrings["local"].ConnectionString;

            var dbInitializer = new DbInitializer();
            dbInitializer.CreateTables(dbConnection);

            Assert.Equal(false, dbCommand.ScalarExecuted);
            Assert.Equal(true, dbCommand.NonQueryExecuted);
            Assert.Equal(false, dbCommand.ReaderExecuted);
        }
    }
}
