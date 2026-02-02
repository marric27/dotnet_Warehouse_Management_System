using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Picking.Dtos;
using dotnet_Warehouse_Management_System.Picking.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_Warehouse_Management_System.Picking.Controllers
{
    [ApiController]
    [Route("api/v1/picking")]
    public class PickingController(PickingService pickingService) : ControllerBase
    {
        [HttpPost("next-item")]
        public async Task<IActionResult> GetNextPicklistItem([FromBody] NextItemRequest request)
        {
            PicklistItemDto nextItem = await pickingService.GetNextPickListItem(request);
            return nextItem == null ? NotFound() : Ok(nextItem);
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmPicking([FromBody] ConfirmPickingRequest request)
        {
            await pickingService.ConfirmPickingAsync(request);
            return Ok(new { message = "picking confirmed" });
        }

    }
}
