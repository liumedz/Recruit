using Candidates.DataAccess.Entities;
using Candidates.DataAccess.Repository;
using Candidates.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Candidates.Tests
{
    public class NoteRepositoryTests
    {
        [Fact]
        public void Should_Create_Note()
        {
            var dbCommand = new DbCommandMock();
            var dbConnection = new DbConnectionMock(dbCommand);

            var repository = new NoteRepository(dbConnection);

            var note = new Note
            {
                Notes = "note",
                CandidateId = 1,
                Created = DateTime.Now
            };
            repository.Save(note);
            Assert.NotEqual(0, note.Id);
            Assert.Equal(true, dbCommand.ScalarExecuted);
            Assert.Equal(false, dbCommand.NonQueryExecuted);
            Assert.Equal(false, dbCommand.ReaderExecuted);
        }

        [Fact]
        public void Should_Update_Candidate()
        {
            var dbCommand = new DbCommandMock();
            var dbConnection = new DbConnectionMock(dbCommand);

            var repository = new NoteRepository(dbConnection);

            var note = new Note
            {
                Id = 1,
                Notes = "note",
                CandidateId = 1,
                Created = DateTime.Now
            };
            repository.Save(note);
            Assert.NotEqual(0, note.Id);
            Assert.Equal(false, dbCommand.ScalarExecuted);
            Assert.Equal(true, dbCommand.NonQueryExecuted);
            Assert.Equal(false, dbCommand.ReaderExecuted);
        }
    }
}
