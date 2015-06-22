using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using CheckPoint4;

namespace DAL
{
    public class RepositoryForCustomer : BaseRepository<Customer, ObjectCustomer, OrdersEntities>
    {
        public RepositoryForCustomer()
        {
            _map=new MapperForCustomer();
            {
               
            }
        }

        public override void Update(ObjectCustomer obj1, ObjectCustomer obj2)
        {
            Customer entity = _map.ConvertToEntity(obj1);
            Func<Customer, bool> g = new Func<Customer, bool>(x => x.Equals(entity));
            var l = _context.CreateObjectSet<Customer>().FirstOrDefault(g);

            if (l != null)
            {
                l.first_name = obj2.FirstName;
                l.last_name = obj2.LastName;
                l.address = obj2.Address;
                l.phone_number = obj2.PhoneNumber;
                l.email = obj2.Email;
            }


        }
    }
}
