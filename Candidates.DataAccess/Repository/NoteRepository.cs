using Candidates.DataAccess.Repository.Abstractions;
using System.Collections.Generic;
using Candidates.DataAccess.Entities;
using System.Data;
using System.Reflection;

namespace Candidates.DataAccess.Repository
{
    public class NoteRepository : BaseRepository<Note>, INoteRepository
    {
        public NoteRepository(IDbConnection connection) :
            base(connection){}

        public IEnumerable<Note> GetByCandidateId(int id)
        {
            var cmd = _connection.CreateCommand();
            var list = new List<Note>();
            try
            {
                _connection.Open();
                cmd.CommandText = $"SELECT * FROM {_tableName} WHERE candidateid = {id}";

                var properties = typeof(Note).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(FillEntity(properties, reader));
                    }
                }
                return list;
            }
            finally
            {
                cmd.Dispose();
                _connection.Close();
            }
        }
    }
}
