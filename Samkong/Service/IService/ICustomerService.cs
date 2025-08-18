using Samkong.Model;
using Samkong.Model.DTO;

namespace Samkong.Service.IService
{
    public interface ICustomerService
    {
        Task CreateAsync1(CustomerDTO Dto);
        Task<IEnumerable<Customer>> GetAllSearch(CustomerDTOSearch model);
        Task<bool> Deleted(string id);
        Task<IEnumerable<Customer>> GetAll();
        Task<bool> Update(string id, CustomerDTO model);
        Task<Customer> getById(string id);
        Task<IEnumerable<Customer>> getAllDdl();
    }
}