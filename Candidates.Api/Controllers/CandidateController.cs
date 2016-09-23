using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Candidates.DataAccess.Repository.Abstractions;
namespace Candidates.Api.Controllers
{
    public class CandidateController : ApiController
    {
        private ICandidateRepository repository;

        public CandidateController(ICandidateRepository repository)
        {
            this.repository = repository;
        }
        public CandidateController()
        {

        }
        // GET: api/Candidate
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Candidate/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Candidate
        public void Post([FromBody]string value)
        {
        }

        // DELETE: api/Candidate/5
        public void Delete(int id)
        {

        }
    }
}
