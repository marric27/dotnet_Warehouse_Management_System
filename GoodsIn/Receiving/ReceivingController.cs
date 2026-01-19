using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;
using dotnet_Warehouse_Management_System.GoodsIn.Services;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;
using dotnet_Warehouse_Management_System.Products.Entities.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_Warehouse_Management_System.GoodsIn.Receiving
{
    [ApiController]
    [Route("api/v1/receiving")]
    public class ReceivingController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ReceivingService _receivingService;
        public ReceivingController(ApplicationDBContext context, ReceivingService receivingService)
        {
            _context = context;
            _receivingService = receivingService;
        }

        [HttpGet]
        [Route("grns")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var grns = await _receivingService.GetAllGrnsAsync(query);

            var pageResult = new Page<GrnResponseDto>
            {
                Content = grns,
                TotalElements = grns.Count,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
            return Ok(pageResult);
        }

        [HttpGet("grns/code/{code}")]
        public async Task<IActionResult> GetGrnByCode([FromRoute] string code)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
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
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var grnDto = await _receivingService.CreateGrn(grnRequestDto);

            return CreatedAtAction(nameof(GetGrnByCode), new { code = grnDto.Code }, grnDto);
        }

        [HttpGet("items/code/{code}")]
        public async Task<IActionResult> GetGrnItemByCode([FromRoute] string code)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var item = await _receivingService.GetGrnItemByCodeAsync(code);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        [Route("grns/{grncode}/items")]
        public async Task<IActionResult> CreateGrnItem([FromRoute] string grncode, [FromBody] GrnItemRequestDto itemRequestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var item = await _receivingService.CreateGrnItemForExistingGrnByCodeAsync(grncode, itemRequestDto);
            return CreatedAtAction(nameof(GetGrnItemByCode), new { code = item.Code }, item);
        }



    }
}
