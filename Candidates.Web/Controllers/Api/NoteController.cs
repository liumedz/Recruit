using Candidates.DataAccess.Entities;
using Candidates.DataAccess.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Candidates.Web.Controllers.Api
{
    public class NoteController : ApiController
    {
        private INoteRepository _repository;

        public NoteController(INoteRepository repository)
        {
            _repository = repository;
        }
        // GET: api/Candidate
        public IEnumerable<Note> Get()
        {
            return _repository.Get();
        }
        [HttpGet]
        public Note GetNote(int id)
        {
            return _repository.Get(id);
        }
        public IEnumerable<Note> Get(int candidateId)
        {
            return _repository.GetByCandidateId(candidateId);
        }

        // POST: api/Candidate
        public IHttpActionResult Post([FromBody]Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (note.Id != 0)
            {
                var c = _repository.Get(note.Id);
                if (c == null)
                    return NotFound();
            }
            note.Created = DateTime.Now;
            _repository.Save(note);
            return Ok(note.Id);
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
