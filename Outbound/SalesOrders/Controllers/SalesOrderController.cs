using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.SalesOrders.Services;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_Warehouse_Management_System.Outbound.SalesOrders.Controllers
{
    [ApiController]
    [Route("api/v1/sales-order")]
    public class SalesOrderController(SalesOrderService salesOrderService) : ControllerBase
    {
        [HttpPost]
        [Route("create-order")]
        public async Task<IActionResult> Create([FromBody] OrderRequestDto orderDto)
        {
            var order = await salesOrderService.CreateOrderAndAssign(orderDto.CustomerCode, orderDto);
            return CreatedAtAction(nameof(GetByCode), new { code = order.code }, order);
        }

        [HttpGet("orders/code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            var ord = await salesOrderService.GetByCodeAsync(code);
            return ord == null ? NotFound() : Ok(ord);
        }

        [HttpGet("orders-paged")]
        public async Task<IActionResult> GetAllPaginated([FromQuery] QueryObject query)
        {
            var ord = await salesOrderService.GetAllPaginated(query);
            return Ok(ord);
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAll()
        {
            var ord = await salesOrderService.GetAll();
            return Ok(ord);
        }
    }
}
