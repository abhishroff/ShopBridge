using Microsoft.EntityFrameworkCore;
using ShopBridge.Core;
using ShopBridge.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Persistence
{
    public class ItemRepository : IItemRepository
    {
        private readonly ShopBridgeDbContext shopBridgeDbContext;

        public ItemRepository(ShopBridgeDbContext shopBridgeDbContext)
        {
            this.shopBridgeDbContext = shopBridgeDbContext;
        }

        public void AddItem(InventoryItem inventoryItem)
        {
            shopBridgeDbContext.InventoryItems.Add(inventoryItem);
        }

        public async Task<InventoryItem> GetItem(int id)
        {
            return await shopBridgeDbContext.InventoryItems.FindAsync(id);
        }

        public async Task<List<InventoryItem>> GetItems()
        {
            return await shopBridgeDbContext.InventoryItems.Where(x => x.IsActive).ToListAsync();
        }
    }
}
