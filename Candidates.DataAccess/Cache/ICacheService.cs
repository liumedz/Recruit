using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidates.DataAccess.Cache
{
    public interface ICacheService
    {
        T Get<T>(string key, Func<T> getItemCallback) where T : class;
        void Invalidate(string key);
    }
}
