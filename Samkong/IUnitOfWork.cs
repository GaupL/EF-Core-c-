
using Samkong.Repo.IRepo;
using Samkong.Repo.RepoService;

namespace Samkong
{
    public interface IUnitOfWork:IDisposable
    {
        ICustomerRepository Customer { get; }
        IEmployeeRepository Employee { get; }
        IProductRepository Product { get; }
        Task<int> SaveChangesAsync();
    }
}