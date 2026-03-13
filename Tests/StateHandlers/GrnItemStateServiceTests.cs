using dotnet_Warehouse_Management_System.Common;
using dotnet_Warehouse_Management_System.Common.Helpers;
using dotnet_Warehouse_Management_System.GoodsIn;
using dotnet_Warehouse_Management_System.GoodsIn.Dtos;
using dotnet_Warehouse_Management_System.GoodsIn.Services;
using dotnet_Warehouse_Management_System.GoodsIn.States;

namespace dotnet_Warehouse_Management_System.Tests.StateHandlers
{
    public class GrnItemStateServiceTests
    {
        [Fact]
        public async Task EvaluateAndProgressGrnItemStateAsync_ShouldCloseGrn_WhenItemAlreadyPutawayAndAllGrnItemsArePutaway()
        {
            var grn = new GrnResponseDto
            {
                Id = 1,
                Code = "GRN-01",
                Supplier = "SUP-01",
                State = State.OPEN,
                ReceivingDate = DateTime.UtcNow,
                Items =
                [
                    new GrnItemResponseDto { Id = 11, Code = "IT-1", State = State.PUTAWAY, GrnId = 1 },
                    new GrnItemResponseDto { Id = 12, Code = "IT-2", State = State.PUTAWAY, GrnId = 1 }
                ]
            };

            var grnService = new FakeGrnService(grn);
            var grnItemService = new FakeGrnItemService();
            var resolver = new GrnItemStateHandlerResolver(
            [
                new OpenGrnItemState(),
                new CheckedGrnItemState(),
                new PutawayGrnItemState()
            ]);

            var sut = new GrnItemStateService(grnService, grnItemService, resolver);
            var item = new GrnItemResponseDto { Id = 11, Code = "IT-1", State = State.PUTAWAY, GrnId = 1 };

            await sut.EvaluateAndProgressGrnItemStateAsync(item);

            Assert.Equal(State.CLOSED, grn.State);
            Assert.Equal(1, grnService.UpdateCalls);
        }

        private sealed class FakeGrnService(GrnResponseDto grn) : IGrnService
        {
            public int UpdateCalls { get; private set; }

            public Task<GrnResponseDto> CreateAsync(GrnRequestDto grnRequestDto) => throw new NotImplementedException();
            public Task<bool> DeleteAsync(string code) => throw new NotImplementedException();
            public Task<Page<GrnResponseDto>> GetAllAsync(QueryObject query) => throw new NotImplementedException();
            public Task<GrnResponseDto?> GetByCodeAsync(string code) => throw new NotImplementedException();
            public Task<GrnResponseDto?> GetByIdAsync(long id) => Task.FromResult<GrnResponseDto?>(grn);

            public Task<GrnResponseDto> UpdateAsync(GrnResponseDto grnDto)
            {
                UpdateCalls++;
                grn.State = grnDto.State;
                return Task.FromResult(grn);
            }
        }

        private sealed class FakeGrnItemService : IGrnItemService
        {
            public Task AddCheckingInfo(string grnItemCode, string checkingInfoCode) => throw new NotImplementedException();
            public Task<GrnItemResponseDto> CreateAsync(GrnItemRequestDto grnItemRequestDto) => throw new NotImplementedException();
            public Task<bool> DeleteAsync(string code) => throw new NotImplementedException();
            public Task<List<GrnItemResponseDto>> GetAllAsync() => throw new NotImplementedException();
            public Task<GrnItemResponseDto?> GetByCodeAsync(string code) => throw new NotImplementedException();
            public Task<GrnItemResponseDto?> GetByIdAsync(long Id) => throw new NotImplementedException();
            public Task<GrnItemResponseDto> UpdateAsync(GrnItemResponseDto grnItemResponseDto) => Task.FromResult(grnItemResponseDto);
        }
    }
}
