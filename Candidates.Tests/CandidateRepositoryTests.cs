﻿using Candidates.DataAccess.Entities;
using Candidates.DataAccess.Repository;
using Candidates.Tests.Mocks;
using Xunit;

namespace Candidates.Tests
{
    public class CandidateRepositoryTests
    {
        [Fact]
        public void Should_Create_Candidate()
        {
            var dbCommand = new DbCommandMock();
            var dbConnection = new DbConnectionMock(dbCommand);

            var repository = new CandidateRepository(dbConnection);

            var candidate = new Candidate
            {
                FirstName = "fname",
                LastName = "lname",
                Comment = "comment",
                Email = "email"
            };
            repository.Save(candidate);
            Assert.NotEqual(0, candidate.Id);
            Assert.Equal(true, dbCommand.ScalarExecuted);
            Assert.Equal(false, dbCommand.NonQueryExecuted);
            Assert.Equal(false, dbCommand.ReaderExecuted);
        }

        [Fact]
        public void Should_Update_Candidate()
        {
            var dbCommand = new DbCommandMock();
            var dbConnection = new DbConnectionMock(dbCommand);

            var repository = new CandidateRepository(dbConnection);

            var candidate = new Candidate
            {
                Id = 10,
                FirstName = "fname",
                LastName = "lname",
                Comment = "comment",
                Email = "email"
            };
            repository.Save(candidate);
            Assert.NotEqual(0, candidate.Id);
            Assert.Equal(false, dbCommand.ScalarExecuted);
            Assert.Equal(true, dbCommand.NonQueryExecuted);
            Assert.Equal(false, dbCommand.ReaderExecuted);
        }

    }
}
