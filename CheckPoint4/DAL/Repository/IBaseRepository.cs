using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public interface IBaseRepository<K> where K: class
    {
        void Delete(K obj);
        void Add(K obj);
        void Update(K obj1, K obj2);
        void Save();
        List<K> GetAll();
    }
}
