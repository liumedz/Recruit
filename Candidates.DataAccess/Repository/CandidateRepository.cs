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
            AddParameter("id", id, cmd);
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public void Save(Candidate candidate)
        {
            _connection.Open();

            var cmd = _connection.CreateCommand();
            AddParameter("firstName", candidate.FirstName, cmd);
            AddParameter("lastName", candidate.LastName, cmd);
            AddParameter("email", candidate.Email, cmd);
            AddParameter("comment", candidate.Comment, cmd);

            if (candidate.Id == 0)
            {
                candidate.Created = DateTime.Now;
                AddParameter("created", candidate.Created, cmd);
                cmd.CommandText = "INSERT INTO Candidates (firstName, lastName, email, comment, created) VALUES(@firstName, @lastName, @email, @comment, @created) SELECT SCOPE_IDENTITY()";
                var id = cmd.ExecuteScalar();
                candidate.Id = Convert.ToInt32(id);
            }
            else
            {
                cmd.CommandText = "UPDATE Candidates SET  firstName = @firstName, lastName = @lastName, email = @email, comment = @comment WHERE id = @id";
                AddParameter("id", candidate.Id, cmd);
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
            AddParameter("id", id, cmd);

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
        private void AddParameter(string name, object value, IDbCommand command)
        {
            var p = command.CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            command.Parameters.Add(p);
        }
    }
}
