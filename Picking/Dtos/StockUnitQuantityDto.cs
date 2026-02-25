using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.Picking.Dtos
{
    public class StockUnitQuantityDto
    {
        [Required]
        [MinLength(3)]
        public string SuId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
