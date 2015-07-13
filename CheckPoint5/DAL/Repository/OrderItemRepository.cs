using System.Collections.Generic;
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
                    .FirstOrDefault(x => x.order_id == obj1.Order.OrderId && x.item_id == obj1.Item.ItemId);

            if (l != null)
            {
                l.item_quantity = obj2.Quantity;
            }
        }

        public override void Delete(Models.OrderItem obj)
        {
            var l =
                _context.OrderItems
                    .FirstOrDefault(x => x.order_id == obj.Order.OrderId && x.item_id == obj.Item.ItemId);

            _context.DeleteObject(l);
        }

        public override IEnumerable<Models.OrderItem> GetAll()
        {
            return _context.OrderItems.Select(item => this.GetRecord(item.order_id)).ToList();
        }

        public override Models.OrderItem GetRecord(int id)
        {
            OrderMapper orderMapper = new OrderMapper();
            ItemMapper itemMapper = new ItemMapper();
            var record = _context.OrderItems.FirstOrDefault(x => x.order_id == id);
            Models.OrderItem orderItem = _map.ConvertToObject(record);

            orderItem.Order = orderMapper.ConvertToObject(record.Order);
            orderItem.Item = itemMapper.ConvertToObject(record.Item);

            return orderItem;
        }

        public override void Add(Models.OrderItem obj)
        {
            OrderItem orderItem = new OrderItem();

            orderItem = _map.ConvertToEntity(obj);
            orderItem.item_id = obj.Order.OrderId;
            _context.OrderItems.AddObject(orderItem);
        }
    }
}