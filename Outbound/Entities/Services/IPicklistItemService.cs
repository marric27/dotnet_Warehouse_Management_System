using dotnet_Warehouse_Management_System.Outbound.Dtos;
using dotnet_Warehouse_Management_System.Outbound.Entities.Repositories;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Services
{
    public interface IPicklistItemService
    {
        Task<PicklistItemDto?> UpdateAsync(string code, PicklistItemDto dto);

    }
}
