using Samkong.Model;
using Samkong.Model.DTO;

namespace Samkong.Repo.RepoService
{
    public interface ICustomerRepository
    {
        Task CreateAsync(CustomerDTO model);
        Task<IQueryable<Customer>> GetCustomersSearch();
        void Deleted(Customer model);
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> getById(string id);
        void Update(Customer model);
    }
}