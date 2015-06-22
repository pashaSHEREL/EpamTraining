using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;

namespace DAL
{
    public abstract class Map<T, K> : IMap<T, K>
        where T : class
        where K : class
    {
        public abstract K ConvertToObject(T entity);
        public abstract T ConvertToEntity(K obj);

        public List<K> ConvertAllToObject(IQueryable<T> all)
        {
            List<K> listObj = new List<K>();

            foreach (var item in all)
            {
                listObj.Add(this.ConvertToObject(item));
            }

            return listObj;
        }

        public IQueryable<T> ConvertAlltoEntity(List<K> all)
        {
            List<T> listEntity= all.Select(item => ConvertToEntity(item)).ToList();

            return listEntity.AsQueryable();
        }
    }
}