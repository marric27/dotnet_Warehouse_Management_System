namespace dotnet_Warehouse_Management_System.GoodsIn.Events
{
    public interface IEventHandler<TEvent> where TEvent : IGoodsInDomainEvent
    {
        Task HandleAsync(TEvent evt, CancellationToken ct = default);
    }
}
