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
            return Ok(await _service.getById(id));
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
            await _service.CreatePro(model);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(string id)
        {
            await _service.Delete(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> update(ProductDto model,string id)
        {
            await _service.UpdatePro(model,id);
            return Ok();
        }
    }
}
