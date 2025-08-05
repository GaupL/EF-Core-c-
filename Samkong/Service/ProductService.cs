using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Samkong.Model;
using Samkong.Model.DTO;
using Samkong.Service.IService;
using System.Globalization;

namespace Samkong.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork uow,IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDTO1>> GetProducts()
        {
            return await _uow.Product.getProducts();
        }
        public async Task<IEnumerable<ChartSaleDTO>> GetSaleChart(SamaryCus model)
        {
            var months =await _uow.Product.getMonths();
            var products=await _uow.Product.getProducts();
            if (!string.IsNullOrEmpty(model.CusId))
            {
                products = products.Where(p=>p.CusId == model.CusId);
            }
            if (!string.IsNullOrEmpty(model.Year))
            {
                products = products.Where(p=>p.CreateDate.Year.ToString() == model.Year);
            }
            var result = months.GroupJoin(products,
                m=>m.MonthId,
                p=>p.MonthId,
                (m,group)=> new ChartSaleDTO()
                {
                    MonthId=m.MonthId,
                    MonthName=m.MonthName,
                    Price = group.Sum(a=>a.Price),
                }).OrderBy(o=>o.MonthId).ToList();

            return result;
        }
        public async Task<IEnumerable<ProductDTO1>> getSearchV2(ProductDTOSearch model)
        {
            var query = await _uow.Product.getSearchV2();
            if (!string.IsNullOrEmpty(model.CusId))
            {
                query = query.Where(x => x.CusId == model.CusId);
            }
            if (!string.IsNullOrEmpty(model.EmpId))
            {
                query = query.Where(x => x.EmpId == model.EmpId);
            }
            if (!string.IsNullOrEmpty(model.ProductName))
            {
                query = query.Where(x => x.ProductName!.Contains(model.ProductName));
            }
            if (model.Price.HasValue && model.ToPrice.HasValue)
            {
                query = query.Where(x => x.Price >= model.Price && x.Price <= model.ToPrice);
            }
            if (!string.IsNullOrEmpty(model.Status.ToString()))
            {
                query = query.Where(x => x.Status == model.Status);
            }
            else
            {
                query = query.Where(x => x.Status == 0);
            }
            var result = await query.OrderByDescending(x=>x.CreateDate).ToListAsync();
            return result;
        }
        public async Task<Product>GetById(string id)
        {
            return await _uow.Product.GetById(id);
        }
        public async Task<Product> CreatePro(ProductDto model)
        {
           var result =  await _uow.Product.CreateAsync(model);
            result.CreateDate = DateTime.Now;
            result.MonthId = Convert.ToDateTime(result.CreateDate).Month;
            await _uow.SaveChangesAsync();
            return result;
        }
        public async Task<bool> UpdatePro(ProductDto model,string id)
        {
            var user = await _uow.Product.GetById(id);
            if (user == null) return false;
            user.UpdateDate= DateTime.Now;
            user.MonthId = user.MonthId;
            _mapper.Map(model,user);
            _uow.Product.UpdateAsync(user);
            await _uow.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(string id)
        {
            var User = await _uow.Product.GetById(id);
            if (User == null) return false;
            _uow.Product.Deleted(User);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
