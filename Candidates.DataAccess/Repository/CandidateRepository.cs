using Candidates.DataAccess.Entities;
using Candidates.DataAccess.Repository.Abstractions;
using System.Data.SqlClient;

namespace Candidates.DataAccess.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private string connectionString { get; set; }

        public CandidateRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void DeleteCandidate(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = "DELETE FROM Candidates WHERE id = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void SaveCandidate(Candidate candidate)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                if (candidate.Id == 0)
                {
                    cmd.CommandText = "INSERT INTO Candidates (firstName, lastName, email, comment) VALUES(@firstName, @lastName, @email, @comment)";
                }
                else
                {
                    cmd.CommandText = "UPDATE Candidates SET  firstName = @firstName, lastName = @lastName, email = @email, comment = @comment WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", candidate.Id);
                }

                cmd.Parameters.AddWithValue("firstName", candidate.FirstName);
                cmd.Parameters.AddWithValue("lastName", candidate.LastName);
                cmd.Parameters.AddWithValue("email", candidate.Email);
                cmd.Parameters.AddWithValue("comment", candidate.Comment);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
