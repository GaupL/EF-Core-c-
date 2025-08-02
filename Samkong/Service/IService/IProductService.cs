using Samkong.Model;
using Samkong.Model.DTO;

namespace Samkong.Service.IService
{
    public interface IProductService
    {
        Task CreatePro(ProductDto model);
        Task<bool> Delete(string id);
        Task<IEnumerable<ProductDTO1>> GetProducts();
        Task<bool> UpdatePro(ProductDto model, string id);
        Task<Product> getById(string id);
        Task<IEnumerable<ProductDTO1>> getSearchV2(ProductDTOSearch model);
        Task<IEnumerable<ChartSaleDTO>> GetSaleChart(SamaryCus model);
       // Task<IEnumerable<ChartSaleDTO>> GetSaleChartV1();
    }
}