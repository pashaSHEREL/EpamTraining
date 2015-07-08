using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public interface IMap<T, K> where T : class where K : class
    {
        K ConvertToObject(T entity);
        T ConvertToEntity(K obj);
        IEnumerable<K> ConvertAllToObject(IQueryable<T> all);
        IQueryable<T> ConvertAlltoEntity(List<K> all);
    }
}