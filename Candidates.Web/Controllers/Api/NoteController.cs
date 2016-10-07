using Candidates.DataAccess.Entities;
using Candidates.DataAccess.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Candidates.DataAccess.Cache;

namespace Candidates.Web.Controllers.Api
{
    public class NoteController : ApiController
    {
        private INoteRepository _repository;
        private ICacheService _cacheService;

        public NoteController(INoteRepository repository, ICacheService cacheService)
        {
            _repository = repository;
            _cacheService = cacheService;
        }
        // GET: api/Note
        public IEnumerable<Note> Get()
        {
            return _cacheService.Get("notes", () => { return _repository.Get(); });
        }
        [HttpGet]
        public Note GetNote(int id)
        {
            return _cacheService.Get("note" + id, () => { return _repository.Get(id); });
        }
        public IEnumerable<Note> Get(int candidateId)
        {
            return _cacheService.Get("notec" + candidateId, () => { return _repository.GetByCandidateId(candidateId); });
        }

        // POST: api/Note
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
            _cacheService.Invalidate("notec" + note.CandidateId);
            _cacheService.Invalidate("note" + note.Id);
            _cacheService.Invalidate("notes");

            return Ok(note.Id);
        }

        // DELETE: api/Note/5
        public IHttpActionResult Delete(int id)
        {
            var note = _repository.Get(id);
            if (note == null)
                return NotFound();

            _repository.Delete(id);
            _cacheService.Invalidate("notec" + note.CandidateId);
            _cacheService.Invalidate("note" + note.Id);
            _cacheService.Invalidate("notes");
            return Ok();
        }
    }
}
