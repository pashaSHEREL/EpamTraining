using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public interface IMap<T, K> where T : class where K : class
    {
        K ConvertToObject(T entity);
        T ConvertToEntity(K obj);
        List<K> ConvertAllToObject(IQueryable<T> all);
    }
}