using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckPoint4;

namespace DAL
{
    public class MapperForCustomer : Map<Customer, ObjectCustomer>
    {
        public override ObjectCustomer ConvertToObject(Customer entity)
        {
            return new ObjectCustomer()
            {
                CustomerId = entity.customer_id,
                FirstName = entity.first_name,
                LastName = entity.last_name,
                Address = entity.address,
                PhoneNumber = entity.phone_number,
                Email = entity.email
            };
        }

        public override Customer ConvertToEntity(ObjectCustomer obj)
        {
            return new Customer()
            {
                customer_id = obj.CustomerId,
                first_name = obj.FirstName,
                last_name = obj.FirstName,
                address = obj.Address,
                phone_number = obj.PhoneNumber,
                email = obj.Email
            };
        }
    }
}