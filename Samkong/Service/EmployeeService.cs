using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Samkong.Model;
using Samkong.Model.DTO;
using Samkong.Service.IService;

namespace Samkong.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public EmployeeService(IUnitOfWork unit,IMapper mapper, IWebHostEnvironment env)
        {
            _unit = unit;
            _mapper = mapper;
            _env = env;
        }
        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _unit.Employee.GetEmployees();
        }
        public async Task<IEnumerable<Employee>> getAllDdl()
        {
            var query = await _unit.Employee.GetEmployeesSearch();
            return await query.OrderBy(x => x.Name).ToListAsync();
        }
        
        public async Task<Employee> getById(string id)
        {
            return await _unit.Employee.getById(id);
        }

        public async Task<IEnumerable<Employee>> GetAllV2(EmployeeDTOSearch model)
        {
            int NewAge = Convert.ToInt16(model.Age);
            int NewToAge = Convert.ToInt16(model.ToAge);
            var Query = await _unit.Employee.GetEmployeesSearch();

            if (!string.IsNullOrEmpty(model.Name))
            {
                Query = Query.Where(x => x.Name.Contains(model.Name));
            }
            if (!string.IsNullOrEmpty(model.Lastname))
            {
                Query = Query.Where(x => x.Lastname.Contains(model.Lastname));

            }
            if (!string.IsNullOrEmpty(model.Age))
            {
                Query = Query.Where(x => Convert.ToInt16(x.Age) >= NewAge && Convert.ToInt16(x.Age) <= NewToAge);
            }
            var result = await Query.OrderByDescending(x => x.Createdate).ToListAsync();
            return result;
        }
        public async Task CreateAsync1(EmployeeDTO Dto)
        {
            await _unit.Employee.AddAsync(Dto);
            await _unit.SaveChangesAsync();
        }

        public async Task<bool> Update(string id, EmployeeDTO model)
        {
            var user = await _unit.Employee.getById(id);
            if (user == null) return false;
            _mapper.Map(model,user);
            if (!string.IsNullOrEmpty(user.picture))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, user.picture.Replace("/", Path.DirectorySeparatorChar.ToString()));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
            if(model.Picture != null)
            {
                var FileName = model.Picture!.FileName;
                if (!string.IsNullOrEmpty(FileName))
                {
                    FileName = Guid.NewGuid().ToString() + Path.GetExtension(FileName);
                    var subFolder = "People";
                    string storeFileDirectory = Path.Combine(_env.WebRootPath, subFolder);
                    if (!Directory.Exists(storeFileDirectory))
                    {
                        Directory.CreateDirectory(storeFileDirectory);
                    }
                    string filePath = Path.Combine(storeFileDirectory, FileName);
                    using (var ms = new MemoryStream())
                    {
                        await model.Picture.CopyToAsync(ms);
                        var content = ms.ToArray();
                        await System.IO.File.WriteAllBytesAsync(filePath, content);
                    }
                    user.picture = Path.Combine(subFolder, FileName).Replace("\\", "/");
                }
            }
            else
            {
                user.picture = null;
            }
            user.Updatedate = DateTime.Now;

            _unit.Employee.Update(user);
            await _unit.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Deleted(string id)
        {
            var result = await _unit.Employee.getById(id);
            if (result == null) return false;
            _unit.Employee.Deleted(result);
            await _unit.SaveChangesAsync();
            return true;
        }
    }
}
