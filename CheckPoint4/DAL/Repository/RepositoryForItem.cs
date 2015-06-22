using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckPoint4;

namespace DAL
{
    class RepositoryForItem:BaseRepository<Item, ObjectItem, OrdersEntities>
    {
        public RepositoryForItem()
        {
            _map=new MapperForItem();
        }

        public override void Update(ObjectItem obj1, ObjectItem obj2)
        {
            throw new NotImplementedException();
        }
    }
}
