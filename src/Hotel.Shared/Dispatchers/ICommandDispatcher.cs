using Hotel.Shared.Handlers;

namespace Hotel.Shared.Dispatchers;

public interface ICommandDispatcher
{
    Task DispatchAsync<TCommand>(TCommand command) where TCommand : ICommand;
}
