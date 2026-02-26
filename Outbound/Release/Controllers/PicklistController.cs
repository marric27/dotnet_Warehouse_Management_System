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
    public class PicklistController(PicklistGenService picklistGenService, IPicklistService picklistService) : ControllerBase
    {
        [HttpPost]
        [Route("release")]
        public async Task<IActionResult> GeneratePicklist([FromBody] List<long> ids)
        {
            var picklists = await picklistGenService.GeneratePicklists(ids);
            return Created("", picklists);
        }

        [HttpPost("batch")]
        public async Task<IActionResult> CreateBulk([FromBody] List<PicklistDto> picklists)
        {
            var createdPicklists = await picklistService.CreateBulkAsync(picklists);
            return Created("", createdPicklists);
        }

        [HttpGet]
        [Route("code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            var picklist = await picklistService.GetByCodeAsync(code);
            return picklist != null ? Ok(picklist) : NotFound();
        }

        [HttpGet("release-paged")]
        public async Task<IActionResult> GetAllPaginated([FromQuery] QueryObject query)
        {
            var pl = await picklistService.GetAllPaginatedAsync(query);
            return Ok(pl);
        }

        [HttpGet("release")]
        public async Task<IActionResult> GetAll()
        {
            var pl = await picklistService.GetAllAsync();
            return Ok(pl);
        }
    }
}
