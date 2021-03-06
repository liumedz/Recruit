﻿using System.Collections.Generic;
using System.Web.Http;
using Candidates.DataAccess.Repository.Abstractions;
using Candidates.DataAccess.Entities;
using System;

namespace Candidates.Api.Controllers
{
    public class CandidateController : ApiController
    {
        private IBaseRepository<Candidate> _repository;

        public CandidateController(IBaseRepository<Candidate> repository)
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
            if (candidate.Id != 0)
            {
                var c = _repository.Get(candidate.Id);
                if (c == null)
                    return NotFound();
            }
            candidate.Created = DateTime.Now;
            _repository.Save(candidate);
            return Ok(candidate.Id);
        }

        // DELETE: api/Candidate/5
        public IHttpActionResult Delete(int id)
        {
            var c = _repository.Get(id);
            if (c == null)
                return NotFound();

            _repository.Delete(id);
            return Ok();
        }
    }
}
