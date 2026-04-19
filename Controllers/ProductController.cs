using dotnet_example_clean_arch_with_entity_framework.DOTs;
using dotnet_example_clean_arch_with_entity_framework.Models;
using dotnet_example_clean_arch_with_entity_framework.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_example_clean_arch_with_entity_framework.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _iProductService;
        public ProductController(IProductService iProductService)
        {
            _iProductService = iProductService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _iProductService.GetAll());


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) =>
            Ok(await _iProductService.GetById(id));

        [HttpPost]
        public async Task<IActionResult> Post(ProductDto dto)
        {
            var productModel = new Products
            {
                Name = dto.Name,
                Price = dto.Price
            };
            var id = await _iProductService.Add(productModel);
            return CreatedAtAction(nameof(Get), new { id }, productModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductDto dto)
        {
            var productModelUpdate = new Products
            {
                Name = dto.Name,
                Price = dto.Price
            };
            await _iProductService.Update(id, productModelUpdate);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, UpdateProductDto dto)
        {
            await _iProductService.Patch(id, dto);
            return Ok("Partially Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _iProductService.Detele(id);
            return Ok("Record is deleted");
        }

        [HttpHead("{id}")]
        public async Task<IActionResult> Head(int id)
        {
            var exists = await _iProductService.IsExists(id);
            if (!exists)
            {
                return NotFound();
            }

            Response.Headers.Add("X-Resource-Exists", "true");
            return Ok();
        }

        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, PATCH, HEAD, OPTIONS");
            return Ok();
        }


    }
}
