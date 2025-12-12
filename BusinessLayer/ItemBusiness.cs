using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class ItemBusiness
    {
        private readonly ItemRepository itemRepository;

        public ItemBusiness()
        {
            this.itemRepository = new ItemRepository();
        }

        public List<Item> GetAllItems()
        {
            return this.itemRepository.GetAllItems();
        }
        public Item GetItemById(int itemId)
        {
            return itemRepository.GetItemById(itemId);
        }

        public bool InsertItem(Item item)
        {
            if (this.itemRepository.InsertItem(item) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateItem(Item item)
        {
            if (this.itemRepository.UpdateItem(item) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteItem(Item item)
        {
            if (this.itemRepository.DeleteItem(item) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
