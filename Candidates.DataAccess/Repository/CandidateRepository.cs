using Candidates.DataAccess.Entities;
using Candidates.DataAccess.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;

namespace Candidates.DataAccess.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private IDbConnection _connection;

        public CandidateRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Delete(int id)
        {

            _connection.Open();
            var cmd = _connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Candidates WHERE id = @id";
            var p1 = cmd.CreateParameter();
            p1.ParameterName = "id";
            p1.Value = id;
            cmd.Parameters.Add(p1);
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public void Save(Candidate candidate)
        {
            _connection.Open();

            var cmd = _connection.CreateCommand();
            var p1 = cmd.CreateParameter();
            p1.ParameterName = "firstName";
            p1.Value = (object)candidate.FirstName ?? DBNull.Value;
            cmd.Parameters.Add(p1);

            var p2 = cmd.CreateParameter();
            p2.ParameterName = "lastName";
            p2.Value = (object)candidate.LastName ?? DBNull.Value;
            cmd.Parameters.Add(p2);

            var p3 = cmd.CreateParameter();
            p3.ParameterName = "email";
            p3.Value = (object)candidate.Email ?? DBNull.Value;

            cmd.Parameters.Add(p3);

            var p4 = cmd.CreateParameter();
            p4.ParameterName = "comment";
            p4.Value = (object)candidate.Comment ?? DBNull.Value;
            cmd.Parameters.Add(p4);

            if (candidate.Id == 0)
            {
                candidate.Created = DateTime.Now;

                var p5 = cmd.CreateParameter();
                p5.ParameterName = "created";
                p5.Value = (object)candidate.Created ?? DBNull.Value;
                cmd.Parameters.Add(p5);

                cmd.CommandText = "INSERT INTO Candidates (firstName, lastName, email, comment, created) VALUES(@firstName, @lastName, @email, @comment, @created) SELECT SCOPE_IDENTITY()";
                var id = cmd.ExecuteScalar();
                candidate.Id = Convert.ToInt32(id);
            }
            else
            {
                cmd.CommandText = "UPDATE Candidates SET  firstName = @firstName, lastName = @lastName, email = @email, comment = @comment WHERE id = @id";
                var p6 = cmd.CreateParameter();
                p6.ParameterName = "id";
                p6.Value = (object)candidate.Id ?? DBNull.Value;
                cmd.Parameters.Add(p6);
                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public IEnumerable<Candidate> Get()
        {
            _connection.Open();
            var cmd = _connection.CreateCommand();
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
                    yield return candidate;
                }
            }
            _connection.Close();
        }

        public Candidate Get(int id)
        {
            _connection.Open();
            var cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Candidates WHERE id = @id";
            var p = cmd.CreateParameter();
            p.ParameterName = "id";
            p.Value = id;
            cmd.Parameters.Add(p);

            using (var reader = cmd.ExecuteReader())
            {
                if (!reader.Read())
                {
                    _connection.Close();
                    return null;
                }
                var values = new object[6];
                reader.GetValues(values);
                var candidate = new Candidate
                {
                    Id = Convert.ToInt32(values[0]),
                    FirstName = values[1].ToString(),
                    LastName = values[2].ToString(),
                    Email = values[3].ToString(),
                    Comment = values[4].ToString(),
                    Created = DateTime.Parse(values[5].ToString())
                };
                _connection.Close();
                return candidate;
            }
        }
    }
}
