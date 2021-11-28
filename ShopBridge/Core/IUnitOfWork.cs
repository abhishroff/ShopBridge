using System.Threading.Tasks;

namespace ShopBridge.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}