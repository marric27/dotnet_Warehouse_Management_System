using dotnet_Warehouse_Management_System.GoodsIn.Putaway.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_Warehouse_Management_System.GoodsIn.Putaway.Controller
{
    [ApiController]
    [Route("api/v1/putaway")]
    public class PutawayController(PutawayService putawayService) : ControllerBase
    {


        [HttpPost("{stockUnitCode}/assignToSlot/{slotCode}")]
        public async Task<IActionResult> AssignStockUnitToSlot([FromRoute] string stockUnitCode, [FromRoute] string slotCode)
        {


            var Slotdto = await putawayService.AssignStockUnitToSlotAsync(stockUnitCode, slotCode);

            return Ok(Slotdto);
        }


    }
}
