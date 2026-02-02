using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_Warehouse_Management_System.Customers.Entities
{
    public class Customer
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string ShippingAddress { get; set; }

        public string BillingAddress { get; set; }

        public string Email { get; set; }

        public string TaxCode { get; set; }

        public string Code { get; set; }

        public void GenerateCode()
        {
            Code = $"Cust-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }
    }
}