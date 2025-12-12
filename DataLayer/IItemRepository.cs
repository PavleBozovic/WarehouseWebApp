using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IItemRepository
    {
        List<Item> GetAllItems();
        Item GetItemById(int itemId);
        int InsertItem(Item item);
        int UpdateItem(Item item);
        int DeleteItem(Item item);
    }
}
