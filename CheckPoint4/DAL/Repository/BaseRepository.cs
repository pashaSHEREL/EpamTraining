using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using CheckPoint4;

namespace DAL
{
    public abstract class BaseRepository<T, K, C> : IBaseRepository<K> where T : class
        where K : class
        where C : ObjectContext, new()
    {
        protected C _context = new C();
        protected IMap<T, K> _map;

        public List<K> GetAll()
        {
            var all = _context.CreateObjectSet<T>();
            return _map.ConvertAllToObject(all);
        }

        public void Delete(K obj)
        {
            Func<T, bool> g = new Func<T, bool>(x => x.Equals(_map.ConvertToEntity(obj)));
            var l = _context.CreateObjectSet<T>().FirstOrDefault(g);
            _context.DeleteObject(l);
        }

        public void Add(K obj)
        {
            _context.CreateObjectSet<T>().AddObject(_map.ConvertToEntity(obj));
        }

        public abstract void Update(K obj1, K obj2);
        
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}