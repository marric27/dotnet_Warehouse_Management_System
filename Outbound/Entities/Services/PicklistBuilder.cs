using dotnet_Warehouse_Management_System.Outbound.Dtos;

namespace dotnet_Warehouse_Management_System.Outbound.Entities.Services
{
    public class PicklistBuilder
    {
        private readonly PicklistDto _picklist = new()
        {
            Code = $"PL-{Guid.NewGuid():N}".Substring(0, 12).ToUpper(),
            pickListItemList = []
        };

        public PicklistBuilder ForCustomer(string customerCode)
        {
            _picklist.CustomerCode = customerCode;
            return this;
        }

        public PicklistBuilder WithReleaseNumber(string releaseNumber)
        {
            _picklist.ReleaseNumber = releaseNumber;
            return this;
        }

        public PicklistBuilder AddItem(PicklistItemDto item)
        {
            _picklist.pickListItemList.Add(item);
            return this;
        }

        public PicklistDto Build() => _picklist;
    }
}
