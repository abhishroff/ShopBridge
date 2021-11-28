using Microsoft.EntityFrameworkCore;
using ShopBridge.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Persistence
{
    public class ShopBridgeDbContext : DbContext
    {
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<UserTypes> UserTypes { get; set; }
        public DbSet<Users> Users { get; set; }
        public ShopBridgeDbContext(DbContextOptions<ShopBridgeDbContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

    }
}
