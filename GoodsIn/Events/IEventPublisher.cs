namespace dotnet_Warehouse_Management_System.GoodsIn.Events
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent evt, CancellationToken ct = default)
            where TEvent : IGoodsInDomainEvent;
    }
}
