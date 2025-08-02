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
        [HttpGet("v2")]
        public async Task<IActionResult> getSearch([FromQuery]CustomerDTOSearch model)
        {
            return Ok(await _service.GetAllSearch(model));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(string id)
        {
            return Ok(await _service.getById(id));
        }

        [HttpPost]
        public async Task<IActionResult> post([FromBody] CustomerDTO model)
        {
            await _service.CreateAsync1(model);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> update(CustomerDTO model, string id)
        {
            await _service.Update(id, model);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(string id)
        {
            await _service.Deleted(id);
            return Ok();
        }
    }
}
