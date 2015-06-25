using CheckPoint4;

namespace DAL
{
    internal class OrderItemMapper : Map<OrderItem, Models.OrderItem>
    {
        public override Models.OrderItem ConvertToObject(OrderItem entity)
        {
            if (entity != null)
            {
                return new Models.OrderItem()
                {
                    OrderId = entity.order_id,
                    ItemId = entity.item_id,
                    Quantity = entity.item_quantity
                };
            }
            else
            {
                return null;
            }
        }

        public override OrderItem ConvertToEntity(Models.OrderItem obj)
        {
            if (obj != null)
            {
                return new OrderItem()
                {
                    order_id = obj.OrderId,
                    item_id = obj.ItemId,
                    item_quantity = obj.Quantity
                };
            }
            else
            {
                return null;
            }
        }
    }
}