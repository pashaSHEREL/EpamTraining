﻿using System.Collections;
using System.Collections.Generic;
using System.Data.Objects;

namespace DAL
{
    public abstract class Repository<T, K, C> : IRepository<K> where T : class
        where K : class
        where C : ObjectContext, new()
    {
        protected C _context = new C();
        protected IMap<T, K> _map;

        public void Add(K obj)
        {
            _context.CreateObjectSet<T>().AddObject(_map.ConvertToEntity(obj));
        }

        public IEnumerable<K> GetAll()
        {
            var l = _context.CreateObjectSet<T>();
            return _map.ConvertAllToObject(l);
        }

        public abstract void Delete(K obj);
        public abstract void Update(K obj1, K obj2);
        public abstract K GetRecord(int id);

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}