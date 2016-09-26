using Candidates.DataAccess.Entities;
using System.Collections.Generic;

namespace Candidates.DataAccess.Repository.Abstractions
{
    public interface ICandidateRepository
    {
        void DeleteCandidate(int id);
        void SaveCandidate(Candidate candidate);
        IEnumerable<Candidate> GetCandidates();
    }
}
