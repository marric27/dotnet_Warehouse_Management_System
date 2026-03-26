namespace dotnet_Warehouse_Management_System.GoodsIn.Events
{
    public sealed record CheckingInfoCreatedEvent(
        long GrnItemId,
        string GrnItemCode,
        long CheckingInfoId) : IGoodsInDomainEvent;
}
