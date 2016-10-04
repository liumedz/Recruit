using Candidates.DataAccess.Entities;
using Candidates.DataAccess.Entities.Abstractions;
using Candidates.DataAccess.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Candidates.DataAccess.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : IEntity
    {
        protected IDbConnection _connection;
        protected string _tableName;
        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
            _tableName = $"{typeof(T).Name}s";
        }

        public void Delete(int id)
        {
            _connection.Open();
            var cmd = _connection.CreateCommand();
            cmd.CommandText = $"DELETE FROM {_tableName} WHERE id = @id";
            AddParameter("id", id, cmd);
            cmd.ExecuteNonQuery();
            _connection.Close();
        }

        public void Save(T entity)
        {
            var cmd = _connection.CreateCommand();
            try
            {
                _connection.Open();
                var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.Name != "Id");
                foreach (var prop in properties)
                {
                    AddParameter(prop.Name.ToLower(), prop.GetValue(entity), cmd);
                }

                if (entity.Id == 0)
                {
                    var columnNames = string.Join(",", properties.Select(x => x.Name.ToLower()));
                    var valuesNames = string.Join(",", properties.Select(x => "@" + x.Name.ToLower()));
                    cmd.CommandText = $"INSERT INTO {_tableName} ({columnNames}) VALUES({valuesNames}) SELECT SCOPE_IDENTITY()";
                    var id = cmd.ExecuteScalar();
                    entity.Id = Convert.ToInt32(id);
                }
                else
                {
                    var updateValues = string.Join(",", properties.Select(x => x.Name.ToLower() + "=@" + x.Name.ToLower()));
                    cmd.CommandText = $"UPDATE {_tableName} SET {updateValues} WHERE id = @id";
                    AddParameter("id", entity.Id, cmd);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                cmd.Dispose();
                _connection.Close();
            }
        }

        public IEnumerable<T> Get()
        {
            var cmd = _connection.CreateCommand();
            var list = new List<T>();
            try
            {
                _connection.Open();
                cmd.CommandText = $"SELECT * FROM {_tableName}";

                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
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

        public T Get(int id)
        {
            var cmd = _connection.CreateCommand();
            try
            {
                _connection.Open();
                cmd.CommandText = $"SELECT * FROM {_tableName} WHERE id = @id";
                AddParameter("id", id, cmd);
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return default(T);
                    }
                    return FillEntity(properties, reader);
                }
            }
            finally
            {
                cmd.Dispose();
                _connection.Close();
            }
        }
        protected void AddParameter(string name, object value, IDbCommand command)
        {
            var p = command.CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            command.Parameters.Add(p);
        }
        protected T FillEntity(PropertyInfo[] properties, IDataReader reader)
        {
            var values = new object[properties.Count()];
            reader.GetValues(values);
            var instance = (T)Activator.CreateInstance(typeof(T));
            for (var i = 0; i < values.Length; i++)
            {
                if (values[i] != DBNull.Value)
                    properties[i].SetValue(instance, values[i]);
            }
            return instance;
        }
    }
}
