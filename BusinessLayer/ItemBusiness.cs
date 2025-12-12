using DataLayer;
using DataLayer.Models;
using System.Collections.Generic;
using System;

namespace BusinessLayer
{
    public class ItemBusiness
    {
        private readonly IItemRepository _itemRepository;

        public ItemBusiness(IItemRepository itemRepository)
        {
            this._itemRepository = itemRepository;
        }

        public List<Item> GetAllItems()
        {
            return this._itemRepository.GetAllItems();
        }

        public Item GetItemById(int itemId)
        {
            return _itemRepository.GetItemById(itemId);
        }

        public bool InsertItem(Item item)
        {
            if (item.Quantity < 1)
            {
                return false;
            }
            if (this._itemRepository.InsertItem(item) > 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdateItem(Item item)
        {
            if (this._itemRepository.UpdateItem(item) > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteItem(Item item)
        {
            if (this._itemRepository.DeleteItem(item) > 0)
            {
                return true;
            }
            return false;
        }
    }
}