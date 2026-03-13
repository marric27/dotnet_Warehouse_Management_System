using dotnet_Warehouse_Management_System.Common;

namespace dotnet_Warehouse_Management_System.Outbound.States
{
    public interface IPicklistItemStateHandlerResolver
    {
        IPicklistItemStateHandler Resolve(PicklistItemState state);
    }

    public class PicklistItemStateHandlerResolver(IEnumerable<IPicklistItemStateHandler> handlers) : IPicklistItemStateHandlerResolver
    {
        private readonly Dictionary<PicklistItemState, IPicklistItemStateHandler> _handlersByState = handlers.ToDictionary(h => h.State);

        public IPicklistItemStateHandler Resolve(PicklistItemState state)
        {
            if (_handlersByState.TryGetValue(state, out var handler))
            {
                return handler;
            }

            throw new KeyNotFoundException($"No picklist item state handler registered for state {state}");
        }
    }
}
