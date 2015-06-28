using System.Linq;
using CheckPoint4;

namespace DAL
{
    public class OrderItemRepository : Repository<OrderItem, Models.OrderItem, OrdersEntities>
    {
        public OrderItemRepository()
        {
            _map = new OrderItemMapper();
        }

        public override void Update(Models.OrderItem obj1, Models.OrderItem obj2)
        {
            var l =
                _context.OrderItems
                    .FirstOrDefault(x => x.order_id == obj1.OrderId && x.item_id == obj1.ItemId);

            if (l != null)
            {
                l.item_quantity = obj2.Quantity;
            }
        }

        public override void Delete(Models.OrderItem obj)
        {
            var l =
                _context.OrderItems
                    .FirstOrDefault(x => x.order_id == obj.OrderId && x.item_id == obj.ItemId);

            _context.DeleteObject(l);
        }

        public override Models.OrderItem GetRecord(int id)
        {
            var record = _context.OrderItems.FirstOrDefault(x => x.order_id == id);
            return _map.ConvertToObject(record);
        }
    }
}