using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckPoint4;

namespace DAL
{
    public class MapperForOrder:Map<Order, ObjectOrder>
    {
        public override ObjectOrder ConvertToObject(Order entity)
        {
            return new ObjectOrder()
            {
                CustomerId = entity.customer_id,
                Date = entity.date,
                Time = entity.time,
                OrderId = entity.order_id
            };
        }

        public override Order ConvertToEntity(ObjectOrder obj)
        {
            return new Order()
            {
                order_id = obj.OrderId,
                date = obj.Date,
                time = obj.Time,
                customer_id = obj.CustomerId
            };
        }
    }
}
