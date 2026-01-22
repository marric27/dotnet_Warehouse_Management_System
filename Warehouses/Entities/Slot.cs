using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Products.Entities;

namespace dotnet_Warehouse_Management_System.Warehouses.Entities
{
    public class Slot
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public int PickingSequence { get; set; }
        public int Capacity { get; set; }
        public Category Category { get; set; }
        public long? ProductId { get; set; }
        public Product? Product { get; set; }

        public void GenerateCode()
        {
            Code = $"SLOT-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }
    }
}
