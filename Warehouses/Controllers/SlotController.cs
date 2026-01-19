using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.Warehouses.Entities;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_Warehouse_Management_System.Warehouses.Controllers
{
    [ApiController]
    [Route("api/v1/slots")]
    public class SlotController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ISlotRepository _slotRepository;

        public SlotController(ApplicationDBContext context, ISlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var slots = await _slotRepository.GetAllAsync(query);
            var dtos = slots.Select(s => s.ToResponseDto()).ToList();
            var pageResult = new Page<SlotResponseDto>
            {
                Content = dtos,
                TotalElements = dtos.Count,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
            return Ok(pageResult);
        }
        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var slot = await _slotRepository.GetByCodeAsync(code);
            if (slot == null)
            {
                return NotFound();
            }
            return Ok(slot);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SlotRequestDto slot)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var _slot = slot.ToSLot();
            await _slotRepository.CreateAsync(_slot);
            return CreatedAtAction(nameof(GetByCode), new { code = _slot.Code }, _slot.ToResponseDto());
        }

    }
}
