using dotnet_Warehouse_Management_System.Common;
using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.Picking.Dtos
{
    public class ConfirmPickingRequest
    {
        [Required]
        public string PickListCode { get; set; }
        [Required]
        public string PickListItemCode { get; set; }
        [Required]
        public List<StockUnitQuantityDto> stockUnitQuantities { get; set; }
        public ErrorReason? ErrorReason { get; set; }
        [Required]
        public string User { get; set; }
    }
}
