using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Products.Entities;
using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos
{
    public class SlotResponseDto
    {
        public long Id { get; set; }
        public required string Code { get; set; }
        public Category Category { get; set; }
        public int PickingSequence { get; set; }
        public int Capacity { get; set; }
        public Product? Product { get; set; }
        //public List<StockUnitDto> StockUnits { get; set; }
    }
}
