using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidates.Tests.Mocks
{
    class DbCommandMock : IDbCommand
    {
        private SqlCommand _sqlCommand;
        public IDataReader DataReader { get; set; }



        public bool NonQueryExecuted { get; set; }
        public bool ReaderExecuted { get; set; }
        public bool ScalarExecuted { get; set; }

        public string CommandText
        {
            get
            {
                return _sqlCommand.CommandText;
            }

            set
            {
                _sqlCommand.CommandText = value;
            }
        }

        public int CommandTimeout
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

        public CommandType CommandType
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

        public IDbConnection Connection
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

        public IDataParameterCollection Parameters
        {
            get { return _sqlCommand.Parameters; }
        }

        public IDbTransaction Transaction
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

        public UpdateRowSource UpdatedRowSource
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

        public DbCommandMock()
        {
            _sqlCommand = new SqlCommand();
        }
        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public IDbDataParameter CreateParameter()
        {
            return _sqlCommand.CreateParameter();
        }

        public void Dispose()
        {
            
        }

        public int ExecuteNonQuery()
        {
            NonQueryExecuted = true;
            return 1;
        }

        public IDataReader ExecuteReader()
        {
            ReaderExecuted = true;
            return DataReader;
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar()
        {
            ScalarExecuted = true;
            return 1;
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }
    }
}
