using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CheckPoint4;

namespace DAL
{
    public class OrderRepository : Repository<Order, Models.Order, OrdersEntities>
    {
        public OrderRepository()
        {
            _map = new OrderMapper();
        }

        public override void Update(Models.Order obj1, Models.Order obj2)
        {
            var l = _context.Orders.FirstOrDefault(x => x.order_id == obj1.OrderId);

            if (l != null)
            {
                l.date = obj2.Date;
                l.time = obj2.Time;
                l.customer_id = obj2.CustomerId;
            }
        }

        public override void Delete(Models.Order obj)
        {
            var l = _context.Orders.FirstOrDefault(x => x.order_id == obj.OrderId);
            _context.DeleteObject(l);
        }

        public override Models.Order GetRecord(int id)
        {
            var record = _context.Orders.Include("Customer").Include("OrderItems").FirstOrDefault(x => x.order_id == id);
            return _map.ConvertToObject(record);
        }
    }
}