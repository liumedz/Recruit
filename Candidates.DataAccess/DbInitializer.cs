using Candidates.DataAccess.Entities.Abstractions;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Candidates.DataAccess
{
    public class DbInitializer : IDbInitializer
    {
        public bool EnsureDatabaseCreated(IDbConnection connection, string databaseName)
        {
            connection.Open();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"SELECT * FROM sysdatabases WHERE name='{databaseName}'";
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return false;
                }

                cmd.CommandText = $"CREATE DATABASE {databaseName}";
                cmd.ExecuteNonQuery();
            }
            connection.Close();
            return true;
        }
        public void CreateTables(IDbConnection connection)
        {
            var EntityType = typeof(IEntity);
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && EntityType.IsAssignableFrom(x));
            var sb = new StringBuilder();
            foreach (var type in types)
            {
                var columns = GetColumns(type);
                var tableName = $"{type.Name}s";
                sb.Append($"CREATE TABLE {tableName}({columns})");
            }
            connection.Open();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = sb.ToString();
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }

        private string GetColumns(Type type)
        {
            var sb = new StringBuilder();
            var list = new List<string>();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                var name = prop.Name.ToLower();
                var sqlType = GetSqlType(prop.PropertyType);
                sb.Append($"{name} {sqlType}");
                if (prop.Name == "Id")
                {
                    sb.Append(" IDENTITY(1,1)");
                }
                else
                {
                    var nullable = IsNullable(prop);
                    if (!nullable)
                        sb.Append(" NOT");
                    sb.Append(" NULL");
                }
                list.Add(sb.ToString());
                sb.Clear();
            }
            return string.Join(",", list);
        }

        private bool IsNullable(PropertyInfo propertyInfo)
        {
            var propType = propertyInfo.PropertyType;
            var isNullableType = propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>);
            var requiredAttribute = propertyInfo.CustomAttributes.SingleOrDefault(x => x.AttributeType.Equals(typeof(RequiredAttribute)));
            if (requiredAttribute != null)
                return false;
            if (isNullableType)
                return true;
            return !propType.IsValueType;
        }

        private string GetSqlType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type);
            }
            if (type.Equals(typeof(DateTime)))
                return "datetime";
            if (type.Equals(typeof(int)))
                return "int";
            if (type.Equals(typeof(string)))
                return "text";
            else
                throw new ArgumentException("Unknown type");
        }
    }
}
