using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_Warehouse_Management_System.GoodsIn.Receiving
{
    [ApiController]
    [Route("api/v1/receiving")]
    public class ReceivingController : ControllerBase
    {
        private readonly ReceivingService _receivingService;
        public ReceivingController(ReceivingService receivingService)
        {
            _receivingService = receivingService;
        }

        [HttpGet]
        [Route("grns")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {

            var page = await _receivingService.GetAllGrnsAsync(query);
            return Ok(page);
        }

        [HttpGet("grns/code/{code}")]
        public async Task<IActionResult> GetGrnByCode([FromRoute] string code)
        {

            var grn = await _receivingService.GetGrnByCodeAsync(code);
            if (grn == null)
            {
                return NotFound();
            }
            return Ok(grn);
        }

        [HttpPost]
        [Route("grns")]
        public async Task<IActionResult> CreateGrn([FromBody] GrnRequestDto grnRequestDto)
        {

            var grnDto = await _receivingService.CreateGrn(grnRequestDto);

            return CreatedAtAction(nameof(GetGrnByCode), new { code = grnDto.Code }, grnDto);
        }

        [HttpGet("items/code/{code}")]
        public async Task<IActionResult> GetGrnItemByCode([FromRoute] string code)
        {

            var item = await _receivingService.GetGrnItemByCodeAsync(code);
            if (item == null)
            {
                return NotFound($"GrnItem non trovato con codice {code}");
            }
            return Ok(item);
        }

        [HttpPost]
        [Route("grns/{grncode}/items")]
        public async Task<IActionResult> CreateGrnItem([FromRoute] string grncode, [FromBody] GrnItemRequestDto itemRequestDto)
        {

            var item = await _receivingService.CreateGrnItemForExistingGrnByCodeAsync(grncode, itemRequestDto);
            return CreatedAtAction(nameof(GetGrnItemByCode), new { code = item.Code }, item);
        }



    }
}
