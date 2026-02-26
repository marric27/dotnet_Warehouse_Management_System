using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Picking.Dtos;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Services
{
    public interface IPicklistService
    {
        Task<Page<PicklistDto>> GetAllPaginatedAsync(QueryObject query);
        Task<List<PicklistDto>> GetAllAsync();
        Task<PicklistDto?> GetByCodeAsync(string code);
        Task<PicklistDto> CreateAsync(PicklistDto picklistDto);
        Task<PicklistItemDto?> GetNextPickListItemAsync(NextItemRequest request);
        Task<PicklistDto?> UpdateAsync(PicklistDto picklistDto);

        Task<List<PicklistDto>> CreateBulkAsync(List<PicklistDto> picklists);
    }
}
