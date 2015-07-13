using System.Collections.Generic;
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

        public override IEnumerable<Models.Item> GetAll()
        {
            return _map.ConvertAllToObject(_context.Items);
        }

        public override Models.Item GetRecord(int id)
        {
            return _map.ConvertToObject(_context.Items.FirstOrDefault(x => x.item_id == id));
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
            _context.DeleteObject(_context.Items.FirstOrDefault(x => x.item_id == obj.ItemId));
        }

        public IEnumerable<Models.Item> Get(int numberOfRecords, int numSkip)
        {
            var list =
                _context.Items.OrderBy(x => x.item_id).Skip(numSkip).Take(numberOfRecords).Select(x => x).ToList();
            return list.Select(x => _map.ConvertToObject(x));
        }
    }
}