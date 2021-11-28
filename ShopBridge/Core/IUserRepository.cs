using System.Threading.Tasks;

namespace ShopBridge.Core
{
    public interface IUserRepository
    {
        Task<int> GetUserId(string emailId, string password);

        Task<bool> CheckIsProductAdmin(int userId);
    }
}