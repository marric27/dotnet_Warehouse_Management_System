using dotnet_Warehouse_Management_System.Common;

namespace dotnet_Warehouse_Management_System.GoodsIn.States
{
    public interface IGrnItemStateHandlerResolver
    {
        IGrnItemStateHandler Resolve(State state);
    }

    public class GrnItemStateHandlerResolver(IEnumerable<IGrnItemStateHandler> handlers) : IGrnItemStateHandlerResolver
    {
        private readonly Dictionary<State, IGrnItemStateHandler> _handlersByState = handlers.ToDictionary(h => h.State);

        public IGrnItemStateHandler Resolve(State state)
        {
            if (_handlersByState.TryGetValue(state, out var handler))
            {
                return handler;
            }

            throw new KeyNotFoundException($"No GRN item state handler registered for state {state}");
        }
    }
}
