using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers;
using dotnet_Warehouse_Management_System.GoodsIn.Entities.Repositories;
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
        private readonly IGrnRepository _grnRepository;
        private readonly IGrnItemRepository _grnItemRepository;
        public ReceivingController(ApplicationDBContext context, IGrnRepository grnRepository, IGrnItemRepository grnItemRepository)
        {
            _context = context;
            _grnRepository = grnRepository;
            _grnItemRepository = grnItemRepository;
        }

        [HttpGet]
        [Route("grns")]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var grns = await _grnRepository.GetAllAsync(query);
            var dtos = grns.Select(g => g.ToResponseDto()).ToList();

            var pageResult = new Page<GrnResponseDto>
            {
                Content = dtos,
                TotalElements = dtos.Count,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };

            return Ok(pageResult);
        }

        [HttpGet("grns/code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var grn = await _grnRepository.GetByCodeAsync(code);
            if (grn == null)
            {
                return NotFound();
            }
            return Ok(grn);
        }
        [HttpPost]
        [Route("grns")]
        public async Task<IActionResult> Create([FromBody] GrnRequestDto grnDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var grn = grnDto.ToGrn();
            await _grnRepository.CreateAsync(grn);
            return CreatedAtAction(nameof(GetByCode), new { code = grn.Code }, grn.ToResponseDto());
        }



    }
}
