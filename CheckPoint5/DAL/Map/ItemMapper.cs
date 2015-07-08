using CheckPoint4;

namespace DAL
{
    internal class ItemMapper : Map<Item, Models.Item>
    {
        public override Models.Item ConvertToObject(Item entity)
        {
            if (entity != null)
            {
                return new Models.Item()
                {
                    ItemId = entity.item_id,
                    Cost = entity.cost,
                    Name = entity.name,
                    Description = entity.description
                };
            }
            else
            {
                return null;
            }
        }

        public override Item ConvertToEntity(Models.Item obj)
        {
            if (obj != null)
            {
                return new Item()
                {
                    item_id = obj.ItemId,
                    cost = obj.Cost,
                    description = obj.Description,
                    name = obj.Name
                };
            }
            else
            {
                return null;
            }
        }
    }
}