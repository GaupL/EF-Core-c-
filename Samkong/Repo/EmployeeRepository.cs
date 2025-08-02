using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Samkong.AppContext;
using Samkong.Model;
using Samkong.Model.DTO;
using Samkong.Repo.RepoService;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Samkong.Repo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public EmployeeRepository(ApplicationDbContext context,IMapper mapper, IWebHostEnvironment Env)
        {
            _context = context;
            _mapper = mapper;
            _env = Env;
        }
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.OrderByDescending(x=>x.Createdate).ToListAsync();
        }
        public async Task<IQueryable<Employee>> GetEmployees2(EmployeeDTOSearch model)
        {
            return _context.Employees.AsQueryable();
        }
        public async Task<Employee> getById(string id)
        {
            var result = await _context.Employees.FindAsync(id);
            return result!;
        }
        public async Task AddAsync(EmployeeDTO model)
        {
            var Emp = _mapper.Map<Employee>(model);
            if (model.Picture != null)
            {
                var fileName = model.Picture.FileName;
                if (!string.IsNullOrEmpty(fileName))
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                    var subFolder = "People";
                    string storeFileDirectory = Path.Combine(_env.WebRootPath, subFolder);
                    if (!Directory.Exists(storeFileDirectory))
                    {
                        Directory.CreateDirectory(storeFileDirectory);
                    }
                    string route = Path.Combine(storeFileDirectory, fileName);
                    using (var ms = new MemoryStream())
                    {
                        await model.Picture.CopyToAsync(ms);
                        var content = ms.ToArray();
                        await System.IO.File.WriteAllBytesAsync(route, content);
                    }
                    var fileLocation = Path.Combine(subFolder, fileName).Replace("\\", "/");
                    Emp.picture = fileLocation;
                }
                else
                {
                    Emp.picture = null;
                }
            }
            else
            {
                Emp.picture = null;
            }


                Emp.Createdate = DateTime.Now;
            await _context.Employees.AddAsync(Emp);
        }
        public void Update(Employee model)
        {
            _context.Employees.Update(model);
        }
        public void Deleted(Employee model)
        {
            _context.Employees.Remove(model);
        }
    }
}
