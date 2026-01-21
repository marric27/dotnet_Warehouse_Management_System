using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.SalesOrders.Services;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_Warehouse_Management_System.Outbound.SalesOrders.Controllers
{
    [ApiController]
    [Route("api/v1/sales-order")]
    public class SalesOrderController : ControllerBase
    {
        private readonly SalesOrderService _salesOrderService;
        public SalesOrderController(SalesOrderService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }

        [HttpPost]
        [Route("create-order")]
        public async Task<IActionResult> Create([FromBody] OrderRequestDto orderDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var order = await _salesOrderService.CreateOrderAndAssign(orderDto.CustomerCode, orderDto);
            return CreatedAtAction(nameof(GetByCode), new { code = order.Code }, order);
        }

        [HttpGet("orders/code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ord = await _salesOrderService.GetByCodeAsync(code);
            if (ord == null)
            {
                return NotFound();
            }
            return Ok(ord);
        }

        [HttpGet("orders-paged")]
        public async Task<IActionResult> GetAllPaginated([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ord = await _salesOrderService.GetAllPaginated(query);
            return Ok(ord);
        }
    }
}
