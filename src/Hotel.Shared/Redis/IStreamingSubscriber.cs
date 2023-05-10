
using Hotel.Shared.Exceptions;
using Hotel.Shared.Handlers;
using System.Linq.Expressions;

namespace Hotel.Shared.Redis;

public interface IStreamingSubscriber
{
    IStreamingSubscriber SubscribeAsync<TCommand>(string topic, Func<TCommand, DomainException, TCommand>? onError) where TCommand : ICommand;
}
