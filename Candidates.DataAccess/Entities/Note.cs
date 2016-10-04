using Candidates.DataAccess.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidates.DataAccess.Entities
{
    public class Note : IEntity
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string Notes { get; set; }
        public DateTime? Created { get; set; }
    }
}
