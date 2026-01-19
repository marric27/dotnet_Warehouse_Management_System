using dotnet_Warehouse_Management_System.Common;
using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos
{
    public class SlotRequestDto
    {

        [Required]
        public Category Category { get; set; }
        [Required]
        public int PickingSequence { get; set; }
        [Required]
        public int Capacity {  get; set; }
    }
}
