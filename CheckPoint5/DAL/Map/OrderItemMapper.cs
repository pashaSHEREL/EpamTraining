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
                    Quantity = entity.item_quantity,
                    TotalCost = entity.total_cost,
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
                    item_quantity = obj.Quantity,
                    total_cost = obj.TotalCost,
                };
            }
            else
            {
                return null;
            }
        }
    }
}