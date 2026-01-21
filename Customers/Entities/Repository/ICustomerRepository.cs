using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Customers.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Customers.Entities.Repository
{
    public interface ICustomerRepository
    {
        Task<Page<Customer>> GetAllPaginatedAsync(QueryObject query);
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByCodeAsync(string code);
        Task<Customer> CreateAsync(Customer Customer);
        Task<Customer> UpdateAsync(string code, CustomerRequestDto CustomerDto);
        Task<Customer?> DeleteAsync(string code);
    }
}
