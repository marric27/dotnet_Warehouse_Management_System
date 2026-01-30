using dotnet_Warehouse_Management_System.Outbound.Entities;
using dotnet_Warehouse_Management_System.Outbound.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.Mappers
{
    public static class PicklistItemMapper
    {
        public static PicklistItemDto ToResponseDto(this PicklistItem entity)
        {
            if (entity == null)
                return null;

            return new PicklistItemDto
            {
                Id = entity.Id,
                Code = entity.code,
                ProductCode = entity.ProductCode,
                State = entity.State,
                Qty = entity.qty,
                PickedQty = entity.PickedQty,
                PickingSequence = entity.PickingSequence,
                ErrorReason = entity.ErrorReason,
                SlotCode = entity.SlotCode,
                SalesOrderCode = entity.SalesOrderCode,
                SalesOrderLineNumber = entity.SalesOrderLineNumber,
            };
        }

        public static PicklistItem ToEntity(this PicklistItemDto dto)
        {
            if (dto == null)
                return null;

            return new PicklistItem
            {
                code = dto.Code,
                ProductCode = dto.ProductCode,
                State = dto.State,
                qty = dto.Qty,
                PickedQty = dto.PickedQty,
                PickingSequence = dto.PickingSequence,
                ErrorReason = dto.ErrorReason,
                SlotCode = dto.SlotCode,
                SalesOrderCode = dto.SalesOrderCode,
                SalesOrderLineNumber = dto.SalesOrderLineNumber,
            };
        }
    }
}
