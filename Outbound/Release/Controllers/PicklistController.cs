using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities.Services;
using dotnet_Warehouse_Management_System.Outbound.Release.Services;
using dotnet_Warehouse_Management_System.Outbound.SalesOrders.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_Warehouse_Management_System.Outbound.Release.Controllers
{
    [ApiController]
    [Route("api/v1/picklists")]
    public class PicklistController : ControllerBase
    {
        private readonly PicklistGenService _picklistGenService;
        private readonly IPicklistService _picklistService;
        public PicklistController(PicklistGenService picklistGenService, IPicklistService picklistService)
        {
            _picklistGenService = picklistGenService;
            _picklistService = picklistService;
        }

        [HttpPost]
        [Route("release")]
        public async Task<IActionResult> GeneratePicklist([FromBody] List<long> ids)
        {

            var picklists = await _picklistGenService.GeneratePicklists(ids);
            return Created("", picklists);
        }

        [HttpGet]
        [Route("code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {

            var picklist = await _picklistService.GetByCodeAsync(code);
            if(picklist == null) return NotFound();
            return Ok(picklist);
        }

        [HttpGet("release-paged")]
        public async Task<IActionResult> GetAllPaginated([FromQuery] QueryObject query)
        {

            var pl = await _picklistService.GetAllPaginatedAsync(query);
            return Ok(pl);
        }

        [HttpGet("release")]
        public async Task<IActionResult> GetAll()
        {

            var pl = await _picklistService.GetAllAsync();
            return Ok(pl);
        }
    }
}
