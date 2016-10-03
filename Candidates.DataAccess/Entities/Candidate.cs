using Candidates.DataAccess.Entities.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Candidates.DataAccess.Entities
{
    public class Candidate : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Comment { get; set; }
        public DateTime? Created { get; set; }
    }
}