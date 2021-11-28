using Microsoft.EntityFrameworkCore;
using ShopBridge.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly ShopBridgeDbContext shopBridgeDbContext;

        public UserRepository(ShopBridgeDbContext shopBridgeDbContext)
        {
            this.shopBridgeDbContext = shopBridgeDbContext;
        }

        public async Task<bool> CheckIsProductAdmin(int userId)
        {
            var id = await shopBridgeDbContext.Users.Include(u => u.UserType)
                               .Where(x => x.Id == userId && x.UserType.Type.Equals("ProductAdmin"))
                               .Select(y => y.Id)
                               .FirstOrDefaultAsync();

            //var userId1 = await shopBridgeDbContext.Users.Include(z => z.UserType)
            //                        .Where(x => x.Id == userId && x.UserType.Type == "ProductAdmin")
            //                        .Select(x => x.Id).FirstOrDefaultAsync();
            return id > 0;
        }

        public async Task<int> GetUserId(string emailId, string password)
        {
            return await shopBridgeDbContext.Users.Where(x => x.Email.ToLower() == emailId.ToLower()
                                                              && x.Password.Equals(password))
                                                  .Select(x => x.Id)
                                                  .FirstOrDefaultAsync();
        }
    }
}
