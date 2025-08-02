using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samkong.AppContext;
using Samkong.Model;
using Samkong.Model.DTO;
using Samkong.Service.IService;

namespace Samkong.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> get()
        {
            return Ok(await _service.GetAll());
        }
        [HttpGet("V2")]
        public async Task<IActionResult> getV2([FromQuery] EmployeeDTOSearch model)
        {
            return Ok(await _service.GetAllV2(model));
        }
        [HttpGet("{id}",Name ="GetByIdEmp")]
        public async Task<IActionResult> getById(string id)
        {
            return Ok(await _service.getById(id));
        }

        [HttpPost]
        public async Task<IActionResult> post([FromForm] EmployeeDTO model)
        {
            await _service.CreateAsync1(model);
            return Ok();
          //  return CreatedAtAction("GetByIdEmp",new {id = model.Name},model);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> update([FromForm] EmployeeDTO model, string id)
        {
            await _service.Update(id, model);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(string id)
        {
           await  _service.Deleted(id);
            return Ok();
        }
    }
}
