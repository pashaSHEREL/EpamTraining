using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CheckPoint4;

namespace DAL
{
    public class MapperForItem : Map<Item, ObjectItem>
    {
        public override ObjectItem ConvertToObject(Item entity)
        {
            return new ObjectItem()
            {
                ItemId = entity.item_id,
                Cost = entity.cost,
                Name = entity.name,
                Description = entity.description
            };
        }

        public override Item ConvertToEntity(ObjectItem obj)
        {
            return new Item()
            {
                item_id = obj.ItemId,
                cost = obj.Cost,
                description = obj.Description,
                name = obj.Name
            };
        }
    }
}