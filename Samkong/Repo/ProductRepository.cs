using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Samkong.AppContext;
using Samkong.Model;
using Samkong.Model.DTO;
using Samkong.Repo.IRepo;
using System.Threading.Tasks;

namespace Samkong.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDTO1>> getProducts()
        {
          var result =   await _context.Products.Include(c=>c.customers).Include(e=>e.employees).Select(x=> new ProductDTO1
             {
                 Contact = x.Contact,
                 CreateDate = x.CreateDate,
                 CusId = x.CusId,
                 EmpId = x.EmpId,
                 Price = x.Price,
                 ProductName = x.ProductName,
                 Tel= x.Tel,

                 Status = x.Status,
                 UpdateDate = x.UpdateDate,
                 CusName=x.customers.Name,
                 EmpName=x.employees.Name,
                 ProductId=x.ProductId,
                 MonthId=x.months.MonthId 

             }).Where(x=>x.Status == 0).OrderByDescending(x=>x.CreateDate).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Month>> getMonths()
        {
            var result = await _context.Months.OrderBy(x => x.Sort).ToListAsync();
            return result;
        }

        public async Task<IQueryable<ProductDTO1>> getSearchV2()
        {
           return _context.Products.Select(x=>new ProductDTO1()
           {
               ProductId=x.ProductId,
               ProductName=x.ProductName,
               Contact=x.Contact,
               CreateDate=x.CreateDate,
               CusId=x.CusId,
               EmpId=x.EmpId,
               Price=x.Price,
               UpdateDate=x.UpdateDate,
                Status=x.Status,
                Tel = x.Tel,
                CusName=x.customers.Name,
                EmpName=x.employees.Name
           }).AsQueryable();

        }
        public async Task<Product> getById(string id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task CreateAsync(ProductDto model)
        {
            var models = _mapper.Map<Product>(model);
              models.CreateDate = DateTime.Now;
              models.MonthId = Convert.ToDateTime(models.CreateDate).Month;
            await _context.Products.AddAsync(models);
        }
        public void UpdateAsync(Product model)
        {
            _context.Products.Update(model);
        }
        public void Deleted(Product model)
        {
            _context.Products.Remove(model);
           
        }
    }
}
