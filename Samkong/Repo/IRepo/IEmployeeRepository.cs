using Samkong.Model;
using Samkong.Model.DTO;

namespace Samkong.Repo.RepoService
{
    public interface IEmployeeRepository
    {
        Task AddAsync(EmployeeDTO model);
        void Deleted(Employee model);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> getById(string id);
        void Update(Employee model);
        Task<IQueryable<Employee>> GetEmployees2(EmployeeDTOSearch model);
    }
}