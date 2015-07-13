using System.Collections.Generic;
using System.Linq;
using CheckPoint4;

namespace DAL
{
    public class CustomerRepository : Repository<Customer, Models.Customer, OrdersEntities>
    {
        public CustomerRepository()
        {
            _map = new CustomerMapper();
        }

        public override IEnumerable<Models.Customer> GetAll()
        {
            var customers = _context.Customers.ToList();

            return customers.Select(item => this.GetRecord(item.customer_id)).ToList();
        }

        public override void Update(Models.Customer obj1, Models.Customer obj2)
        {
            var l = _context.Customers.FirstOrDefault(x => x.customer_id == obj1.CustomerId);

            if (l != null)
            {
                l.first_name = obj2.FirstName;
                l.last_name = obj2.LastName;
                l.address = obj2.Address;
                l.phone_number = obj2.PhoneNumber;
                l.email = obj2.Email;
            }
        }

        public override void Delete(Models.Customer obj)
        {
            var l = _context.Customers.FirstOrDefault(x => x.customer_id == obj.CustomerId);
            _context.DeleteObject(l);
        }

        public override Models.Customer GetRecord(int id)
        {
            OrderMapper orderMapper = new OrderMapper();
            var record = _context.Customers.Include("Orders").FirstOrDefault(x => x.customer_id == id);
            Models.Customer customer = _map.ConvertToObject(record);

            List<Models.Order> orders = record.Orders.Select(item => orderMapper.ConvertToObject(item)).ToList();
            customer.Orders = orders;

            return customer;
        }
    }
}