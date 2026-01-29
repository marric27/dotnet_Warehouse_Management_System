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
            var prod = await productService.GetByCodeAsync(code);
            if (prod == null)
            {
                return NotFound();
            }
            return Ok(prod);
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
            var prod = await productService.UpdateAsync(code, dto);

            if (prod == null)
            {
                return NotFound();
            }

            return Ok(prod);
        }

        [HttpDelete("bycode/{code}")]
        public async Task<IActionResult> DeleteByCode([FromRoute] string code)
        {
            var prod = await productService.DeleteAsync(code);

            if (prod == null)
            {
                return NotFound();
            }

            return NoContent();
        }







    }
}
