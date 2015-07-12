using System;
using System.Collections.Generic;
using System.Linq;
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
                l.manager_id = obj2.ManagerId;
            }
        }

        public override void Delete(Models.Order obj)
        {
            var l = _context.Orders.FirstOrDefault(x => x.order_id == obj.OrderId);
            _context.DeleteObject(l);
        }

        public IEnumerable<Models.Order> GetAll()
        {
            return _context.Orders.ToList().Select(item => this.GetRecord(item.order_id)).ToList();
        }

        public override void Add(Models.Order obj)
        {
            OrderItemMapper orderItemMapper = new OrderItemMapper();
            TrackableCollection<OrderItem> orderItems = new TrackableCollection<OrderItem>();
            Order order = new Order();

            order = _map.ConvertToEntity(obj);

            foreach (var item in obj.OrderItems)
            {
                orderItems.Add(orderItemMapper.ConvertToEntity(item));
            }

            order.OrderItems = orderItems;
            _context.Orders.AddObject(order);
        }

        public override Models.Order GetRecord(int id)
        {
            CustomerMapper customerMapper = new CustomerMapper();
            OrderItemMapper orderItemMapper = new OrderItemMapper();
            ManagerMapper managerMapper = new ManagerMapper();

            var record =
                _context.Orders.Include("OrderItems")
                    .Include("Customer")
                    .Include("Manager")
                    .FirstOrDefault(x => x.order_id == id);
            Models.Order order = _map.ConvertToObject(record);
            var t = _context.Orders.Where(x => x.order_id == id).Select(x => x.Customer.Orders).FirstOrDefault();
            order.Customer = customerMapper.ConvertToObject(record.Customer);
            order.Manager = managerMapper.ConvertToObject(record.Manager);

            List<Models.OrderItem> listOrderItems =
                record.OrderItems.Select(item => orderItemMapper.ConvertToObject(item)).ToList();
            order.OrderItems = listOrderItems;

            return order;
        }
    }
}