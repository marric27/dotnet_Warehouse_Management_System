using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Customers.Entities.Dtos;
using dotnet_Warehouse_Management_System.Customers.Entities.Mappers;
using dotnet_Warehouse_Management_System.Customers.Entities.Repository;

namespace dotnet_Warehouse_Management_System.Customers.Entities.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CustomerResponseDto> CreateAsync(CustomerRequestDto CustomerDto)
        {
            var customer = CustomerDto.ToCustomer();
            customer.GenerateCode();

            var created = await _customerRepository.CreateAsync(customer);
            return created.ToResponseDto();
        }

        public async Task<CustomerResponseDto?> DeleteAsync(string code)
        {
            var deleted = await _customerRepository.DeleteAsync(code);
            return deleted?.ToResponseDto();
        }

        public async Task<Page<CustomerResponseDto>> GetAllAsync(QueryObject query)
        {
            var customers = await _customerRepository.GetAllAsync(query);

            var pagedDto = new Page<CustomerResponseDto>
            {
                PageNumber = customers.PageNumber,
                PageSize = customers.PageSize,
                TotalElements = customers.TotalElements,
                Content = customers.Content.Select(p => p.ToResponseDto()).ToList()
            };
            return pagedDto;
        }

        public async Task<CustomerResponseDto?> GetByCodeAsync(string code)
        {
            var cust = await _customerRepository.GetByCodeAsync(code);
            return cust?.ToResponseDto();
        }

        public async Task<CustomerResponseDto?> UpdateAsync(string code, CustomerRequestDto CustomerDto)
        {
            var updated = await _customerRepository.UpdateAsync(code, CustomerDto);
            return updated?.ToResponseDto();
        }
    }
}
