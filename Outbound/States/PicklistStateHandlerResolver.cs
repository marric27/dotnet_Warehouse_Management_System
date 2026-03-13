using dotnet_Warehouse_Management_System.Outbound.Entities;

namespace dotnet_Warehouse_Management_System.Outbound.States
{
    public interface IPicklistStateHandlerResolver
    {
        IPicklistStateHandler Resolve(PicklistState state);
    }

    public class PicklistStateHandlerResolver(IEnumerable<IPicklistStateHandler> handlers) : IPicklistStateHandlerResolver
    {
        private readonly Dictionary<PicklistState, IPicklistStateHandler> _handlersByState = handlers.ToDictionary(h => h.State);

        public IPicklistStateHandler Resolve(PicklistState state)
        {
            if (_handlersByState.TryGetValue(state, out var handler))
            {
                return handler;
            }

            throw new KeyNotFoundException($"No picklist state handler registered for state {state}");
        }
    }
}
