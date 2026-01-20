using dotnet_Warehouse_Management_System.Customers.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Customers.Entities.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerResponseDto ToResponseDto(this Customer customer)
        {
            return new CustomerResponseDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                ShippingAddress = customer.ShippingAddress,
                BillingAddress = customer.BillingAddress,
                Email = customer.Email,
                TaxCode = customer.TaxCode,
                Code = customer.Code,
            };
        }

        public static Customer ToCustomer(this CustomerRequestDto dto)
        {
            return new Customer
            {
                Name = dto.Name,
                Surname = dto.Surname,
                ShippingAddress = dto.ShippingAddress,
                BillingAddress = dto.BillingAddress,
                Email = dto.Email,
                TaxCode = dto.TaxCode
            };
        }
    }
}
