using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Products.Entities.Services;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Mappers;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_Warehouse_Management_System.Warehouses.Controllers
{
    [ApiController]
    [Route("api/v1/slots")]
    public class SlotController(ISlotService slotService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var slots = await slotService.GetAllAsync(query);
            return Ok(slots);
        }
        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var slot = await slotService.GetByCodeAsync(code);
            if (slot == null)
            {
                return NotFound();
            }
            return Ok(slot);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SlotRequestDto slotDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var slot = await slotService.CreateAsync(slotDto);
            return CreatedAtAction(nameof(GetByCode), new { code = slot.Code }, slot);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var slots = await slotService.GetAllAsync();
            return Ok(slots);
        }

    }
}
