namespace dotnet_Warehouse_Management_System.GoodsIn.Events
{
    public sealed record StockUnitPutawayAssignedEvent(
        long GrnItemId,
        long StockUnitId,
        long SlotId) : IGoodsInDomainEvent;
}
