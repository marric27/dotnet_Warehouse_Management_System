using System.ComponentModel.DataAnnotations;

namespace dotnet_Warehouse_Management_System.Picking.Dtos
{
    public class NextItemRequest
    {
        [Required]
        [MinLength(1)]
        public List<long> PickListIds { get; set; } = [];
    }

}
