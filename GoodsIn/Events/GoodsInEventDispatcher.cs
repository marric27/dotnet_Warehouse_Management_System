namespace dotnet_Warehouse_Management_System.GoodsIn.Events
{
    public class GoodsInEventDispatcher(IServiceProvider serviceProvider) : IEventPublisher
    {
        public async Task PublishAsync<TEvent>(TEvent evt, CancellationToken ct = default)
            where TEvent : IGoodsInDomainEvent
        {
            var handlers = serviceProvider.GetServices<IEventHandler<TEvent>>();

            foreach (var handler in handlers)
            {
                await handler.HandleAsync(evt, ct);
            }
        }
    }
}
