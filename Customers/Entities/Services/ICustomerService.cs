using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Customers.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Customers.Entities.Services
{
    public interface ICustomerService
    {
        Task<Page<CustomerResponseDto>> GetAllPaginatedAsync(QueryObject query);
        Task<List<CustomerResponseDto>> GetAllAsync();
        Task<CustomerResponseDto?> GetByCodeAsync(string code);
        Task<CustomerResponseDto> CreateAsync(CustomerRequestDto CustomerDto);
        Task<CustomerResponseDto?> UpdateAsync(string code, CustomerRequestDto CustomerDto);
        Task<CustomerResponseDto?> DeleteAsync(string code);
    }
}
