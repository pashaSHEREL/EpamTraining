using System.Collections.Generic;
using System.Linq;
using CheckPoint4;

namespace DAL
{
    internal class OrderMapper : Map<Order, Models.Order>
    {
        public override Models.Order ConvertToObject(Order entity)
        {
            if (entity != null)
            {
                OrderItemMapper orderItemMapper = new OrderItemMapper();
                CustomerMapper mapCustomer = new CustomerMapper();
                List<Models.OrderItem> orderItemCol = new List<Models.OrderItem>();

                foreach (var item in entity.OrderItems)
                {
                    orderItemCol.Add(orderItemMapper.ConvertToObject(item));
                }

                return new Models.Order()
                {
                    CustomerId = entity.customer_id,
                    Date = entity.date,
                    Time = entity.time,
                    OrderId = entity.order_id,
                    Customer = mapCustomer.ConvertToObject(entity.Customer),
                    OrderItems = orderItemCol,
                };
            }
            else
            {
                return null;
            }
        }

        public override Order ConvertToEntity(Models.Order obj)
        {
            if (obj != null)
            {
                TrackableCollection<OrderItem> orderItemCol = new TrackableCollection<OrderItem>();
                OrderItemMapper orderItemMapper = new OrderItemMapper();
                CustomerMapper mapCustomer = new CustomerMapper();

                foreach (var item in obj.OrderItems)
                {
                    orderItemCol.Add(orderItemMapper.ConvertToEntity(item));
                }

                return new Order()
                {
                    order_id = obj.OrderId,
                    date = obj.Date,
                    time = obj.Time,
                    customer_id = obj.CustomerId,
                    OrderItems = orderItemCol,
                    Customer = mapCustomer.ConvertToEntity(obj.Customer),
                };
            }
            else
            {
                return null;
            }
        }
    }
}