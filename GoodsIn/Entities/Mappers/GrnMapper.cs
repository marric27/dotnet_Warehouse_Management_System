using dotnet_Warehouse_Management_System.GoodsIn.Dtos;

namespace dotnet_Warehouse_Management_System.GoodsIn.Entities.Mappers
{
    public static class GrnMapper
    {
        public static GrnResponseDto ToResponseDto(this Grn grn)
        {
            return new GrnResponseDto
            {
                Id = grn.Id,
                Code = grn.Code,
                Supplier = grn.Supplier,
                State = grn.State,
                ReceivingDate = grn.ReceivingDate,
                Items = grn.Items.Select(i => i.ToResponseDto()).ToList(),
            };
        }

        public static Grn ToGrn(this GrnRequestDto dto)
        {
            return new Grn
            {
                Supplier = dto.Supplier,
                ReceivingDate = dto.ReceivingDate
            };
        }
    }
}
