using Candidates.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidates.DataAccess.Repository.Abstractions
{
    public interface INoteRepository
    {
        void Delete(int id);
        void Save(Note entity);
        IEnumerable<Note> Get();
        Note Get(int id);
        IEnumerable<Note> GetByCandidateId(int id);
    }
}
