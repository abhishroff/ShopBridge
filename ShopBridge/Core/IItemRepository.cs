using ShopBridge.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopBridge.Core
{
    public interface IItemRepository
    {
        Task<InventoryItem> GetItem(int id);

        Task<List<InventoryItem>> GetItems();
        void AddItem(InventoryItem inventoryItem);
    }
}