using dotnet_Warehouse_Management_System.Common;
using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.Picking.Dtos
{
    public class ConfirmPickingRequest
    {
        [Required]
        [MinLength(3)]
        public string PickListCode { get; set; }
        [Required]
        [MinLength(3)]
        public string PickListItemCode { get; set; }
        [Required]
        [MinLength(1)]
        public List<StockUnitQuantityDto> stockUnitQuantities { get; set; }
        public ErrorReason? ErrorReason { get; set; }
        [Required]
        [MinLength(3)]
        public string User { get; set; }
    }
}
