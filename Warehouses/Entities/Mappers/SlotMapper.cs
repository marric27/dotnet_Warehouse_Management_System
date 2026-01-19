using dotnet_Warehouse_Management_System.Products.Entities;
using dotnet_Warehouse_Management_System.Products.Entities.Dtos;
using dotnet_Warehouse_Management_System.Warehouses.Entities.Dtos;

namespace dotnet_Warehouse_Management_System.Warehouses.Entities.Mappers
{
    public static class SlotMapper
    {
        public static SlotResponseDto ToResponseDto(this Slot slot)
        {
            return new SlotResponseDto
            {
                Code = slot.Code,
                Category = slot.Category,
                PickingSequence = slot.PickingSequence,
                Capacity = slot.Capacity
                //Product = slot.Product
            };
        }

        public static Slot ToSLot(this SlotRequestDto dto)
        {
            return new Slot
            {
                Capacity = dto.Capacity,
                Category = dto.Category,
                PickingSequence = dto.PickingSequence
            };
        }
    }
}
