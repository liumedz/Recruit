using Candidates.DataAccess.Entities;

namespace Candidates.DataAccess.Repository.Abstractions
{
    public interface ICandidateRepository
    {
        void DeleteCandidate(int id);
        void SaveCandidate(Candidate candidate);
    }
}
