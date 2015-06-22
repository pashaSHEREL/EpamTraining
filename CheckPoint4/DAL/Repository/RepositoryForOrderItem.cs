using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckPoint4;
using DAL.Models;

namespace DAL.Repository
{
    class RepositoryForOrderItem : BaseRepository<OrderItem, ObjectOrderItem, OrdersEntities>
    {
        public RepositoryForOrderItem()
        {
            _map=new MapperForOrderItem();
        }

        public override void Update(ObjectOrderItem obj1, ObjectOrderItem obj2)
        {
            throw new NotImplementedException();
        }
    }
}
