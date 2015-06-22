using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckPoint4;
using DAL.Models;

namespace DAL
{
    public class MapperForOrderItem : Map<OrderItem, ObjectOrderItem>
    {
        public override ObjectOrderItem ConvertToObject(OrderItem entity)
        {
            return new ObjectOrderItem()
            {
               OrderId = entity.order_id,
               ItemId = entity.item_id,
               Quantity = entity.item_quantity
            };
        }

        public override OrderItem ConvertToEntity(ObjectOrderItem obj)
        {
            return new OrderItem()
            {
                order_id = obj.OrderId,
                item_id = obj.ItemId,
                item_quantity = obj.Quantity
            };
        }
    }
}
