namespace dotnet_Warehouse_Management_System.GoodsIn.Events
{
    public sealed record GrnItemCreatedEvent(
        long GrnItemId,
        string GrnItemCode,
        long GrnId) : IGoodsInDomainEvent;
}
