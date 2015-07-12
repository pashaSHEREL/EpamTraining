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
                return new Models.Order()
                {
                    CustomerId = entity.customer_id,
                    Date = entity.date,
                    Time = entity.time,
                    OrderId = entity.order_id,
                    ManagerId = entity.manager_id
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
                return new Order()
                {
                    order_id = obj.OrderId,
                    date = obj.Date,
                    time = obj.Time,
                    customer_id = obj.CustomerId,
                    manager_id = obj.ManagerId
                };
            }
            else
            {
                return null;
            }
        }
    }
}