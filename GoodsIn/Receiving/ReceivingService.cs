using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Services;
using dotnet_Warehouse_Management_System.Products.Entities.Services;


namespace dotnet_Warehouse_Management_System.GoodsIn.Receiving
{
    public class ReceivingService
    {
        private readonly IGrnService _grnService;
        private readonly IGrnItemService _grnItemService;
        private readonly IProductService _productService;
        private readonly IGrnItemStateService _grnItemStateService;

        public ReceivingService(IGrnService grnService, IGrnItemService grnItemService, IProductService productService, IGrnItemStateService grnItemStateService)
        {
            _grnService = grnService;
            _grnItemService = grnItemService;
            _productService = productService;
            _grnItemStateService = grnItemStateService;
        }

        public Task<GrnResponseDto> CreateGrn(GrnRequestDto grnRequestDto)
        {
            return _grnService.CreateAsync(grnRequestDto);
        }

        public async Task<GrnItemResponseDto> CreateGrnItemForExistingGrnByCodeAsync(string grncode, GrnItemRequestDto grnItemRequestDto)
        {
            var grn = await _grnService.GetByCodeAsync(grncode);
            if (grn == null)
            {
                throw new KeyNotFoundException($"Grn {grncode} non existing");
            }
            else if (grn.State == State.CLOSED)
            {
                throw new Exception($"Grn {grncode} in closed state");
            }
            var prodToAdd = await _productService.GetByCodeAsync(grnItemRequestDto.ProductCode) ?? throw new Exception($"Grn {grnItemRequestDto.ProductCode} non existing");

            //_grnItemStateService.ValidateItemQuantities(grnItemRequestDto);
            // progressione stati



            return await _grnItemService.CreateGrnItemForExistingGrnByCodeAsync(grncode, grnItemRequestDto);
        }

        public Task<Page<GrnResponseDto>> GetAllGrnsAsync(QueryObject query)
        {
            return _grnService.GetAllAsync(query);
        }

        public Task<List<GrnItemResponseDto>> GetAllGrnItemsAsync(QueryObject query)
        {
            return _grnItemService.GetAllAsync(query);
        }

        public Task<GrnResponseDto> GetGrnByCodeAsync(string code)
        {
            return _grnService.GetByCodeAsync(code);
        }

        public Task<GrnItemResponseDto> GetGrnItemByCodeAsync(string code)
        {
            return _grnItemService.GetByCodeAsync(code);
        }
    }
}
