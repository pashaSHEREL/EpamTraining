using System.Linq;
using CheckPoint4;

namespace DAL
{
    public class ItemRepository : Repository<Item, Models.Item, OrdersEntities>
    {
        public ItemRepository()
        {
            _map = new ItemMapper();
        }

        public override Models.Item GetRecord(int id)
        {
            var record = _context.Items.FirstOrDefault(x => x.item_id == id);
            return _map.ConvertToObject(record);
        }

        public override void Update(Models.Item obj1, Models.Item obj2)
        {
            var l = _context.Items.FirstOrDefault(x => x.item_id == obj1.ItemId);

            if (l != null)
            {
                l.name = obj2.Name;
                l.description = obj2.Description;
                l.cost = obj2.Cost;
            }
        }

        public override void Delete(Models.Item obj)
        {
            var l = _context.Items.FirstOrDefault(x => x.item_id == obj.ItemId);
            _context.DeleteObject(l);
        }
    }
}