using Candidates.DataAccess.Entities;
using Candidates.DataAccess.Repository.Abstractions;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Candidates.DataAccess.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private string _connectionString { get; set; }

        public CandidateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = "DELETE FROM Candidates WHERE id = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Save(Candidate candidate)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();

                cmd.Parameters.AddWithValue("firstName", (object) candidate.FirstName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("lastName", (object) candidate.LastName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("email", (object) candidate.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("comment", (object) candidate.Comment ?? DBNull.Value);

                if (candidate.Id == 0)
                {
                    candidate.Created = DateTime.Now;
                    cmd.Parameters.AddWithValue("created",(object) candidate.Created ?? DBNull.Value);
                    cmd.CommandText = "INSERT INTO Candidates (firstName, lastName, email, comment, created) VALUES(@firstName, @lastName, @email, @comment, @created) SELECT SCOPE_IDENTITY()";
                    var id = cmd.ExecuteScalar();
                    candidate.Id = Convert.ToInt32(id);
                }
                else
                {
                    cmd.CommandText = "UPDATE Candidates SET  firstName = @firstName, lastName = @lastName, email = @email, comment = @comment WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", candidate.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Candidate> Get()
        {
            var list = new List<Candidate>();
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = "SELECT * FROM Candidates";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var candidate = new Candidate();
                        var values = new object[6];
                        reader.GetValues(values);
                        candidate.Id = Convert.ToInt32(values[0]);
                        candidate.FirstName = values[1].ToString();
                        candidate.LastName = values[2].ToString();
                        candidate.Email = values[3].ToString();
                        candidate.Comment = values[4].ToString();
                        candidate.Created = DateTime.Parse(values[5].ToString());
                        list.Add(candidate);
                    }
                }
            }
            return list;
        }
    }
}
