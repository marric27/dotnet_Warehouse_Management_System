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
                code = entity.code,
                productCode = entity.ProductCode,
                State = entity.State,
                Quantity = entity.qty,
                PickedQty = entity.PickedQty,
                PickingSequence = entity.PickingSequence,
                ErrorReason = entity.ErrorReason,
                SlotCode = entity.SlotCode,
                salesOrderCode = entity.SalesOrderCode,
                salesOrderLineNumber = entity.SalesOrderLineNumber,
            };
        }

        public static PicklistItem ToEntity(this PicklistItemDto dto)
        {
            if (dto == null)
                return null;

            return new PicklistItem
            {
                code = dto.code,
                ProductCode = dto.productCode,
                State = dto.State,
                qty = dto.Quantity,
                PickedQty = dto.PickedQty,
                PickingSequence = dto.PickingSequence,
                ErrorReason = dto.ErrorReason,
                SlotCode = dto.SlotCode,
                SalesOrderCode = dto.salesOrderCode,
                SalesOrderLineNumber = dto.salesOrderLineNumber,
            };
        }
    }
}
