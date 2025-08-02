using Samkong.AppContext;
using Samkong.Repo.IRepo;
using Samkong.Repo.RepoService;

namespace Samkong
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICustomerRepository Customer { get; }
        public IEmployeeRepository Employee { get; }
        public IProductRepository Product { get;}
        public UnitOfWork(ApplicationDbContext context, ICustomerRepository customer, IEmployeeRepository employee, IProductRepository product)
        {
            _context = context;
            Customer = customer;
            Employee = employee;
            Product = product;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
