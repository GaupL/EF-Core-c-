using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Samkong.Model;
using Samkong.Model.DTO;
using Samkong.Service.IService;

namespace Samkong.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public CustomerService(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _unit.Customer.GetCustomers();
        }
        public async Task<IEnumerable<Customer>> GetAllSearch(CustomerDTOSearch model)
        {
            var query = await _unit.Customer.GetCustomersSearch();
            if (!string.IsNullOrEmpty(model.Name))
            {
                query = query.Where(x=>x.Name.Contains(model.Name));
            }
            if (!string.IsNullOrEmpty(model.Lastname))
            {
                query = query.Where(x => x.Lastname.Contains(model.Lastname));
            }
            if (!string.IsNullOrEmpty(model.Address))
            {
                query = query.Where(x => x.Address.Contains(model.Address));
            }
            var result =  await query.OrderBy(x=>x.Createdate).ToListAsync();
            return result;
        }
        public async Task<Customer> getById(string id)
        {
            return await _unit.Customer.getById(id);
        }
        public async Task CreateAsync1(CustomerDTO Dto)
        {
            await _unit.Customer.CreateAsync(Dto);
            await _unit.SaveChangesAsync();
        }

        public async Task<bool> Update(string id,CustomerDTO model)
        {
          var user =  await _unit.Customer.getById(id);
            if (user == null) return false;
            user.Updatedate= DateTime.Now;
            _mapper.Map(model,user);
            _unit.Customer.Update(user);
            await _unit.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Deleted(string id)
        {
            var result = await _unit.Customer.getById(id);
            if (result == null) return false;
            _unit.Customer.Deleted(result);
            await _unit.SaveChangesAsync();
            return true;
        }
    }
}
