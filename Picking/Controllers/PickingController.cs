using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Picking.Dtos;
using dotnet_Warehouse_Management_System.Picking.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_Warehouse_Management_System.Picking.Controllers
{
    [ApiController]
    [Route("api/v1/picking")]
    public class PickingController : ControllerBase
    {
        private readonly PickingService _pickingService;
        public PickingController(PickingService pickingService)
        {
            _pickingService = pickingService;
        }

        [HttpPost("next-item")]
        public async Task<IActionResult> GetNextPicklistItem([FromBody] NextItemRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            PicklistItemDto nextItem = await _pickingService.GetNextPickListItem(request);
            if (nextItem == null)
            {
                return NotFound("No item founds");
            }
            return Ok(nextItem);
        }

        //[HttpPost("confirm")]
        //public async Task<IActionResult> ConfirmPicking([FromBody] ConfirmPickingRequest request)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);
        //    _pickingService.ConfirmPicking(request);
        //    return Ok("picking confirmed");
        //}



    }
}
