using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Products.Dtos;
using dotnet_Warehouse_Management_System.Products.Helpers;
using dotnet_Warehouse_Management_System.Products.Interfaces;
using dotnet_Warehouse_Management_System.Products.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_Warehouse_Management_System.Products.Controller
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly IProductRepository _productRepository;
        public ProductController(ApplicationDBContext context, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var products = await _productRepository.GetAllAsync(query);
            var dtos = products.Select(p => p.ToResponseDto()).ToList();
            var pageResult = new Page<ProductResponseDto>
            {
                Content = dtos,
                TotalElements = dtos.Count,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
            return Ok(pageResult);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var prod = await _context.Products.FindAsync(id);
            if (prod == null)
            {
                return NotFound();
            }
            return Ok(prod.ToResponseDto());
        }

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var prod = await _productRepository.GetByCodeAsync(code);
            if (prod == null)
            {
                return NotFound();
            }
            return Ok(prod);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequestDto product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var prod = product.ToProduct();
            await _productRepository.CreateAsync(prod);
            return CreatedAtAction(nameof(GetById), new { id = prod.Id }, prod.ToResponseDto());
        }

        [HttpPut("bycode/{code}")]
        public async Task<IActionResult> UpdateByCode([FromRoute] string code, [FromBody] ProductRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var prod = await _productRepository.UpdateAsync(code, dto);

            if (prod == null)
            {
                return NotFound();
            }

            return Ok(prod.ToResponseDto());
        }

        [HttpDelete("bycode/{code}")]
        public async Task<IActionResult> DeleteByCode([FromRoute] string code)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var prod = await _productRepository.DeleteAsync(code);

            if (prod == null)
            {
                return NotFound();
            }

            return NoContent();
        }







    }
}
