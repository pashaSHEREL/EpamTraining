using System.Collections.Generic;

namespace DAL
{
    public interface IRepository<K> where K : class
    {
        void Delete(K obj);
        void Add(K obj);
        void Update(K obj1, K obj2);
        K GetRecord(int id);
        void Save();
        IEnumerable<K> GetAll();
    }
}