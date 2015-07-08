using CheckPoint4;

namespace DAL
{
    internal class OrderItemMapper : Map<OrderItem, Models.OrderItem>
    {
        public override Models.OrderItem ConvertToObject(OrderItem entity)
        {
            ItemMapper itemMapper = new ItemMapper();
            OrderMapper orderMapper=new OrderMapper();

            if (entity != null)
            {
                return new Models.OrderItem()
                {
                    Order = orderMapper.ConvertToObject( entity.Order),
                    Quantity = entity.item_quantity,
                    TotalCost = entity.total_cost,
                    Item = itemMapper.ConvertToObject(entity.Item),
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
            OrderMapper orderMapper = new OrderMapper();

            if (obj != null)
            {
                return new OrderItem()
                {
                    Order =orderMapper.ConvertToEntity(obj.Order),
                    item_quantity = obj.Quantity,
                    total_cost = obj.TotalCost,
                    item_id = itemMapper.ConvertToEntity(obj.Item).item_id
                };
            }
            else
            {
                return null;
            }
        }
    }
}