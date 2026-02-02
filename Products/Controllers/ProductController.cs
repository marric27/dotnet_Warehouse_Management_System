using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;
using dotnet_Warehouse_Management_System.Products.Entities.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_Warehouse_Management_System.Products.Controller
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController(IProductService productService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllPaginated([FromQuery] QueryObject query)
        {

            var products = await productService.GetAllPaginatedAsync(query);
            return Ok(products);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {

            var products = await productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            var result = await productService.GetByCodeAsync(code);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequestDto product)
        {
            var prod = await productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetByCode), new { code = prod.Code }, prod);
        }

        [HttpPut("bycode/{code}")]
        public async Task<IActionResult> UpdateByCode([FromRoute] string code, [FromBody] ProductRequestDto dto)
        {
            var result = await productService.UpdateAsync(code, dto);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("bycode/{code}")]
        public async Task<IActionResult> DeleteByCode([FromRoute] string code)
        {
            var success = await productService.DeleteAsync(code);
            return success ? NoContent() : NotFound();
        }







    }
}
