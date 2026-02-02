using dotnet_Warehouse_Management_System.BaseRepository;
using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Outbound.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Repositories
{
    public interface IPicklistItemRepository : IBaseRepository<PicklistItem>
    {
        Task<List<PicklistItem>> GetAllAsync();
        Task<PicklistItem?> GetByCodeAsync(string code, bool track);
        Task<PicklistItem?> FindItemsByStateOrdered(List<long> plIds, PicklistItemState state);
    }
}
