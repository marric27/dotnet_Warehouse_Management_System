using dotnet_Warehouse_Management_System.GoodsIn.CheckGoodsIn.Services;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_Warehouse_Management_System.GoodsIn.CheckGoodsIn.Controllers
{
    [ApiController]
    [Route("api/v1/check-goods-in")]
    public class CheckGoodsInController(CheckGoodsInService checkGoodsInService) : ControllerBase
    {
        [HttpPost("{grnItemCode}/checking-info")]
        public async Task<ActionResult<GrnItemResponseDto>> CreateCheckingInfo([FromRoute] string grnItemCode, [FromBody] StockUnitRequestDto request)
        {
            GrnItemResponseDto result = await checkGoodsInService.CreateCheckingInfoAndStockUnit(grnItemCode, request);

            return Ok(result);
        }

        [HttpGet("stock-units")]
        public async Task<IActionResult> ListStockUnits()
        {
            var su = await checkGoodsInService.ListStockUnit();
            return Ok(su);
        }

    }
}
