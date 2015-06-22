using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckPoint4;

namespace DAL.Repository
{
    class RepositoryForOrder : BaseRepository<Order, ObjectOrder, OrdersEntities>
    {
        public RepositoryForOrder()
        {
             _map=new MapperForOrder();
        }

        public override void Update(ObjectOrder obj1, ObjectOrder obj2)
        {
            throw new NotImplementedException();
        }
    }
}
