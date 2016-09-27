using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Candidates.DataAccess.Repository.Abstractions;
using Candidates.DataAccess.Repository;
using Candidates.DataAccess.Entities;
using System.Configuration;

namespace Candidates.Api.Controllers
{
    public class CandidateController : ApiController
    {
        private ICandidateRepository _repository;

        public CandidateController(ICandidateRepository repository)
        {
            _repository = repository;
        }
        public CandidateController()
        {
            var cs = ConfigurationManager.ConnectionStrings["local"].ConnectionString;
            _repository = new CandidateRepository(cs);
        }
        // GET: api/Candidate
        public IEnumerable<Candidate> Get()
        {
            return _repository.Get();
        }
        
        // POST: api/Candidate
        public int Post([FromBody]Candidate candidate)
        {
            _repository.Save(candidate);
            return candidate.Id;
        }

        // DELETE: api/Candidate/5
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok();
        }
    }
}
