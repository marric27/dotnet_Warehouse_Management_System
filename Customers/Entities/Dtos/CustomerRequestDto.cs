using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.Customers.Entities.Dtos
{
    public class CustomerRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public string BillingAddress { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il codice fiscale deve essere esattamente di 16 caratteri.")]
        public string TaxCode { get; set; }
    }
}
