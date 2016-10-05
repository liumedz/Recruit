using Candidates.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidates.DataAccess.Repository.Abstractions
{
    public interface INoteRepository: IBaseRepository<Note>
    {
        IEnumerable<Note> GetByCandidateId(int id);
    }
}
