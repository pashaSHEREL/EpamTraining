using CheckPoint4;

namespace DAL
{
    internal class CustomerMapper : Map<Customer, Models.Customer>
    {
        public override Models.Customer ConvertToObject(Customer entity)
        {
            if (entity != null)
            {
                return new Models.Customer()
                {
                    CustomerId = entity.customer_id,
                    FirstName = entity.first_name,
                    LastName = entity.last_name,
                    Address = entity.address,
                    PhoneNumber = entity.phone_number,
                    Email = entity.email
                };
            }
            else
            {
                return null;
            }
        }

        public override Customer ConvertToEntity(Models.Customer obj)
        {
            if (obj != null)
            {
                return new Customer()
                {
                    customer_id = obj.CustomerId,
                    first_name = obj.FirstName,
                    last_name = obj.LastName,
                    address = obj.Address,
                    phone_number = obj.PhoneNumber,
                    email = obj.Email
                };
            }
            else
            {
                return null;
            }
        }
    }
}