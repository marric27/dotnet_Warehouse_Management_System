namespace dotnet_Warehouse_Management_System.Customers.Entities.Dtos
{
    public class CustomerResponseDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string ShippingAddress { get; set; }

        public string BillingAddress { get; set; }

        public string Email { get; set; }

        public string TaxCode { get; set; }

        public string Code { get; set; }
    }
}
