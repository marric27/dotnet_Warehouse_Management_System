using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Exceptions;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.Data;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Services;
using dotnet_Warehouse_Management_System.Products.Entities.Services;


namespace dotnet_Warehouse_Management_System.GoodsIn.Receiving
{
    public class ReceivingService(ApplicationDBContext context, IGrnService grnService, IGrnItemService grnItemService, IProductService productService, IGrnItemStateService grnItemStateService)
    {
        public Task<GrnResponseDto> CreateGrn(GrnRequestDto grnRequestDto)
        {
            return grnService.CreateAsync(grnRequestDto);
        }

        public async Task<GrnItemResponseDto> CreateGrnItemForExistingGrnByCodeAsync(string grncode, GrnItemRequestDto grnItemRequestDto)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var grn = await grnService.GetByCodeAsync(grncode);

                if (grn == null)
                    throw new KeyNotFoundException($"Grn {grncode} non existing");
                else if (grn.State == State.CLOSED)
                    throw new DomainConflictException($"Grn {grncode} in closed state");

                _ = await productService.GetByCodeAsync(grnItemRequestDto.ProductCode) ?? throw new KeyNotFoundException($"Product {grnItemRequestDto.ProductCode} non existing");

                grnItemStateService.ValidateItemQuantities(grnItemRequestDto);

                grnItemRequestDto.GrnId = grn.Id;
                var created = await grnItemService.CreateAsync(grnItemRequestDto);

                await grnItemStateService.EvaluateAndProgressGrnItemStateAsync(created);
                await transaction.CommitAsync();
                return created;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public Task<Page<GrnResponseDto>> GetAllGrnsAsync(QueryObject query)
        {
            return grnService.GetAllAsync(query);
        }

        public Task<List<GrnItemResponseDto>> GetAllGrnItemsAsync()
        {
            return grnItemService.GetAllAsync();
        }

        public Task<GrnResponseDto> GetGrnByCodeAsync(string code)
        {
            return grnService.GetByCodeAsync(code);
        }

        public Task<GrnItemResponseDto> GetGrnItemByCodeAsync(string code)
        {
            return grnItemService.GetByCodeAsync(code);
        }
    }
}
