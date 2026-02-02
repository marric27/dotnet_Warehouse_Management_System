using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Customers.Entities.Dtos;
using dotnet_Warehouse_Management_System.Customers.Entities.Services;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_Warehouse_Management_System.Customers.Controllers
{
    [ApiController]
    [Route("api/v1/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaginated([FromQuery] QueryObject query)
        {


            var customers = await _customerService.GetAllPaginatedAsync(query);
            return Ok(customers);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {


            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {

            var cust = await _customerService.GetByCodeAsync(code);
            if (cust == null)
            {
                return NotFound();
            }
            return Ok(cust);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerRequestDto customer)
        {

            var cust = await _customerService.CreateAsync(customer);
            return CreatedAtAction(nameof(GetByCode), new { code = cust.Code }, cust);
        }
    }
}
