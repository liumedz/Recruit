using Candidates.DataAccess.Entities.Abstractions;
using System.Collections.Generic;

namespace Candidates.DataAccess.Repository.Abstractions
{
    public interface IBaseRepository<T>
        where T : IEntity
    {
        void Delete(int id);
        void Save(T entity);
        IEnumerable<T> Get();
        T Get(int id);
    }
}
