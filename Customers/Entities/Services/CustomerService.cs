using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Customers.Entities.Dtos;
using dotnet_Warehouse_Management_System.Customers.Entities.Mappers;
using dotnet_Warehouse_Management_System.Customers.Entities.Repository;

namespace dotnet_Warehouse_Management_System.Customers.Entities.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        public async Task<CustomerResponseDto> CreateAsync(CustomerRequestDto CustomerDto)
        {
            var customer = CustomerDto.ToCustomer();
            customer.GenerateCode();

            var created = await customerRepository.CreateAsync(customer);
            return created.ToResponseDto();
        }

        public async Task<CustomerResponseDto?> DeleteAsync(string code)
        {
            var deleted = await customerRepository.DeleteAsync(code);
            return deleted?.ToResponseDto();
        }

        public async Task<Page<CustomerResponseDto>> GetAllPaginatedAsync(QueryObject query)
        {
            var customers = await customerRepository.GetAllPaginatedAsync(query);

            var pagedDto = new Page<CustomerResponseDto>
            {
                PageNumber = customers.PageNumber,
                PageSize = customers.PageSize,
                TotalElements = customers.TotalElements,
                Content = customers.Content.Select(p => p.ToResponseDto()).ToList()
            };
            return pagedDto;
        }

        public async Task<List<CustomerResponseDto>> GetAllAsync()
        {
            var customers = await customerRepository.GetAllAsync();
            return customers.Select(c => c.ToResponseDto()).ToList();
        }

        public async Task<CustomerResponseDto?> GetByCodeAsync(string code)
        {
            var cust = await customerRepository.GetByCodeAsync(code) ?? throw new KeyNotFoundException("Customer not found");
            return cust?.ToResponseDto();
        }

        public async Task<CustomerResponseDto?> UpdateAsync(string code, CustomerRequestDto CustomerDto)
        {
            var updated = await customerRepository.UpdateAsync(code, CustomerDto);
            return updated?.ToResponseDto();
        }
    }
}
