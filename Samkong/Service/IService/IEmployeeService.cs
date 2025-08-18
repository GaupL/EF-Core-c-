using Samkong.Model;
using Samkong.Model.DTO;

namespace Samkong.Service.IService
{
    public interface IEmployeeService
    {
        Task CreateAsync1(EmployeeDTO Dto);
        Task<IEnumerable<Employee>> getAllDdl();
        Task<bool> Update(string id, EmployeeDTO model);
        Task<bool> Deleted(string id);
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> getById(string id);
        Task<IEnumerable<Employee>> GetAllV2(EmployeeDTOSearch model);
    }
}