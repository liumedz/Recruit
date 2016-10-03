using Candidates.DataAccess.Entities;
using Candidates.DataAccess.Repository;
using System;
using System.Configuration;
using System.Data.SqlClient;
using Xunit;

namespace Candidates.Integration.Tests
{
    public class CandidateRepositoryTests
    {
        private BaseRepository<Candidate> _repository;

        public CandidateRepositoryTests()
        {
            var cs = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
            var connection = new SqlConnection(cs);
            _repository = new BaseRepository<Candidate>(new SqlConnection(cs));
        }
        [Fact]
        public void Should_Throw_Null_Reference_Exception_If_Candidate_Is_Null()
        {
            Assert.Throws(typeof(NullReferenceException), () => { _repository.Save(null); });
        }
        [Fact]
        public void Should_Set_Candidate_Id_After_Save()
        {
            var candidate = new Candidate
            {
                FirstName = "fname",
                LastName = "lname",
                Comment = "comment",
                Email = "email",
                Created = DateTime.Now
            };
            _repository.Save(candidate);
            Assert.NotEqual(0, candidate.Id);
        }
        [Fact]
        public void Should_Update_Existing_Candidate()
        {
            var candidate = new Candidate
            {
                FirstName = "fname",
                LastName = "lname",
                Comment = "comment",
                Email = "email",
                Created = DateTime.Now
            };
            _repository.Save(candidate);

            var candidateNew = new Candidate
            {
                FirstName = "fnameNew",
                LastName = "lnameNew",
                Comment = "commentNew",
                Email = "emailNew",
                Id = candidate.Id,
                Created = DateTime.Now
            };
            _repository.Save(candidateNew);
            var newCandidate = _repository.Get(candidate.Id);

            Assert.Equal(candidate.Id, newCandidate.Id);
            Assert.Equal("fnameNew", newCandidate.FirstName);
            Assert.Equal("lnameNew", newCandidate.LastName);
            Assert.Equal("commentNew", newCandidate.Comment);
            Assert.Equal("emailNew", newCandidate.Email);
        }

        [Fact]
        public void Should_Return_Null_If_Id_Not_Exist()
        {
            var candidate = _repository.Get(-1);
            Assert.Null(candidate);
        }
        [Fact]
        public void Should_Return_Inserted_Candidate()
        {
            var candidate = new Candidate
            {
                FirstName = "fname",
                LastName = "lname",
                Comment = "comment",
                Email = "email",
                Created = DateTime.Now
            };
            _repository.Save(candidate);
            Assert.NotEqual(0, candidate.Id);
            var id = candidate.Id;
            var candidate2 = _repository.Get(id);
            Assert.Equal(candidate2.Id, candidate.Id);
        }

        [Fact]
        public void Should_Delete_Candidate()
        {
            var candidate = new Candidate
            {
                FirstName = "fname",
                LastName = "lname",
                Comment = "comment",
                Email = "email",
                Created = DateTime.Now
            };
            _repository.Save(candidate);
            _repository.Delete(candidate.Id);
            candidate = _repository.Get(candidate.Id);
            Assert.Null(candidate);
        }
    }
}
