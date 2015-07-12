using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DAL;
using Models;

namespace Bll
{
    public class ItemBll
    {
        private ItemRepository _itemRepository = new ItemRepository();

        public IEnumerable<Item> GetAll()
        {
            return _itemRepository.GetAll().AsQueryable();
        }

        public void Add(Item item)
        {
            _itemRepository.Add(item);
        }

        public void Delete(Item item)
        {
            _itemRepository.Delete(item);
        }

        public void Update(Item item1, Item item2)
        {
            _itemRepository.Update(item1, item2);
        }

        public Item GetRecord(int id)
        {
            return _itemRepository.GetRecord(id);
        }

        public void Save()
        {
            _itemRepository.Save();
        }

        public IEnumerable<Item> Get(int numRec, int numSkip)
        {
            return _itemRepository.Get(numRec, numSkip);
        }
    }
}