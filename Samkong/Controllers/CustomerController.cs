using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Samkong.Model;
using Samkong.Model.DTO;
using Samkong.Service.IService;

namespace Samkong.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> get()
        {
          return Ok(await _service.GetAll());
        }
        [HttpGet("ddl")]
        public async Task<IActionResult> getddl()
        {
            return Ok(await _service.getAllDdl());
        }
        [HttpGet("v2")]
        public async Task<IActionResult> getSearch([FromQuery]CustomerDTOSearch model)
        {
            return Ok(await _service.GetAllSearch(model));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(string id)
        {
            var User = await _service.getById(id);
            if (User == null)
            {
                return NotFound();
            }
            return Ok(User);
        }

        [HttpPost]
        public async Task<IActionResult> post([FromBody] CustomerDTO model)
        {
            await _service.CreateAsync1(model);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> update([FromBody] CustomerDTO model, string id)
        {
           var User = await _service.Update(id, model);
            if (!User)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(string id)
        {
           var deleted =  await _service.Deleted(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
