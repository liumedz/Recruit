using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Candidates.DataAccess.Repository.Abstractions;
using Candidates.DataAccess.Repository;
using Candidates.DataAccess.Entities;

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
            _repository = new CandidateRepository();
        }
        // GET: api/Candidate
        public IEnumerable<Candidate> Get()
        {
            return _repository.GetCandidates();
        }
        
        // POST: api/Candidate
        public int Post([FromBody]Candidate candidate)
        {
            _repository.SaveCandidate(candidate);
            return candidate.Id;
        }

        // DELETE: api/Candidate/5
        public IHttpActionResult Delete(int id)
        {
            _repository.DeleteCandidate(id);
            return Ok();
        }
    }
}
