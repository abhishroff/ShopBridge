using ShopBridge.Core;
using System.Threading.Tasks;

namespace ShopBridge.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopBridgeDbContext context;

        public UnitOfWork(ShopBridgeDbContext context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }

}
