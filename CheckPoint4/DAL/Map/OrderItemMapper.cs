using CheckPoint4;

namespace DAL
{
    internal class OrderItemMapper : Map<OrderItem, Models.OrderItem>
    {
        public override Models.OrderItem ConvertToObject(OrderItem entity)
        {
            ItemMapper itemMapper = new ItemMapper();

            if (entity != null)
            {
                return new Models.OrderItem()
                {
                    OrderId = entity.order_id,
                    ItemId = entity.item_id,
                    Quantity = entity.item_quantity,
                    TotalCost = entity.total_cost,
                    Item = itemMapper.ConvertToObject(entity.Item)
                };
            }
            else
            {
                return null;
            }
        }

        public override OrderItem ConvertToEntity(Models.OrderItem obj)
        {
            ItemMapper itemMapper = new ItemMapper();

            if (obj != null)
            {
                return new OrderItem()
                {
                    order_id = obj.OrderId,
                    item_id = obj.ItemId,
                    item_quantity = obj.Quantity,
                    total_cost = obj.TotalCost,
                    Item = itemMapper.ConvertToEntity(obj.Item)
                };
            }
            else
            {
                return null;
            }
        }
    }
}