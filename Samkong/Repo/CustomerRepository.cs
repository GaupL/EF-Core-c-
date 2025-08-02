using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Samkong.AppContext;
using Samkong.Model;
using Samkong.Model.DTO;
using Samkong.Repo.RepoService;
using System.Threading.Tasks;

namespace Samkong.Repo
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customers.OrderByDescending(x=>x.Createdate).ToListAsync();
        }
        public async Task<IQueryable<Customer>> GetCustomersSearch()
        {
            return _context.Customers.AsQueryable();
        }
        public async Task<Customer> getById(string id)
        {
           var result = await _context.Customers.FindAsync(id);
            return result!;
        }
        public async Task CreateAsync(CustomerDTO model)
        {
            var Cus = _mapper.Map<Customer>(model);
            Cus.Createdate = DateTime.Now;
            await _context.Customers.AddAsync(Cus);
        }
        public void Update(Customer model)
        {
            _context.Customers.Update(model);
        }
        public void Deleted(Customer model)
        {
            _context.Customers.Remove(model);
        }

    }
}
