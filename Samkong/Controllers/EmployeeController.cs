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
        //[HttpGet("{id}",Name ="GetByIdEmp")]
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(string id)
        {
            var result = await _service.getById(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
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
          var User =   await _service.Update(id, model);
            if (!User)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(string id)
        {
          var deleted = await  _service.Deleted(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
