using System.Collections.Generic;
using System.Web.Http;
using Candidates.DataAccess.Repository.Abstractions;
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
        // GET: api/Candidate
        public IEnumerable<Candidate> Get()
        {
            return _repository.Get();
        }

        // POST: api/Candidate
        public IHttpActionResult Post([FromBody]Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _repository.Save(candidate);
            return Ok(candidate.Id);
        }

        // DELETE: api/Candidate/5
        public IHttpActionResult Delete(int id)
        {
            _repository.Delete(id);
            return Ok();
        }
    }
}
