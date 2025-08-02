using Samkong.Model;
using Samkong.Model.DTO;

namespace Samkong.Repo.IRepo
{
    public interface IProductRepository
    {
        Task CreateAsync(ProductDto model);
        void Deleted(Product model);
        Task<Product> getById(string id);
        Task<IEnumerable<ProductDTO1>> getProducts();
        void UpdateAsync(Product model);
        Task<IQueryable<ProductDTO1>> getSearchV2();
        Task<IEnumerable<Month>> getMonths();
    }
}