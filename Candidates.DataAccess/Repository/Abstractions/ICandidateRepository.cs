using Candidates.DataAccess.Entities;
using System.Collections.Generic;

namespace Candidates.DataAccess.Repository.Abstractions
{
    public interface ICandidateRepository
    {
        void Delete(int id);
        void Save(Candidate candidate);
        IEnumerable<Candidate> Get();
    }
}
