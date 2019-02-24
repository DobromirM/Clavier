using System.Threading.Tasks;

namespace CLAVIER.Infrastructure.Events
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<TEvent>(TEvent @event)
            where TEvent : IEvent;
    }
}
