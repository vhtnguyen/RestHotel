namespace Hotel.Shared.Handlers;

public interface ICommandHandler<TCommand>
     where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}
