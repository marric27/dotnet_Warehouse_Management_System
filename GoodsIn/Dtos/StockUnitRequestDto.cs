using dotnet_Warehouse_Management_System.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.GoodsIn.Dtos
{
    public class StockUnitRequestDto
    {
        [Required]
        [MinLength(3)]
        public string BatchNumber { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [MinLength(3)]
        public string ProductCode { get; set; }
        public Category Category { get; set; }
    }
}