using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Samkong.AppContext;
using Samkong.Model.DTO;
using Samkong.Service.IService;

namespace Samkong.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;

        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> get()
        {
            return Ok(await _service.GetProducts());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(string id)
        {
            var User = await _service.GetById(id);
            if(User == null)
            {
                return NotFound();
            }
            return Ok(User);
        }
        [HttpGet("V4")]
        public async Task<ActionResult<IEnumerable<ProductDTO1>>> getV4([FromQuery] ProductDTOSearch model)
        {
            return Ok(await _service.getSearchV2(model));
        }
        [HttpGet("Chart")]
        public async Task<IActionResult> getAllChart([FromQuery] SamaryCus model)
        {
            return Ok(await _service.GetSaleChart(model));
        }
        [HttpPost]
        public async Task<IActionResult> post([FromBody] ProductDto model)
        {
           var User =  await _service.CreatePro(model);
            return CreatedAtAction(nameof(getById),new {id = User.ProductId},User);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(string id)
        {
          var deleted =  await _service.Delete(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> update([FromBody]ProductDto model,string id)
        {
          var User =  await _service.UpdatePro(model,id);
            if(!User)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
