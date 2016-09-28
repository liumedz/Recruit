using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidates.Tests.Mocks
{
    class DbConnectionMock : IDbConnection
    {
        private IDbCommand _command;

        public string ConnectionString
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int ConnectionTimeout
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Database
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ConnectionState State
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DbConnectionMock(IDbCommand command)
        {
            _command = command;
        }
        public IDbTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new NotImplementedException();
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            
        }

        public IDbCommand CreateCommand()
        {
            return _command;
        }

        public void Dispose()
        {
            
        }

        public void Open()
        {
            
        }
    }
}
