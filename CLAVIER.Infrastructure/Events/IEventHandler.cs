using System.Threading.Tasks;

namespace CLAVIER.Infrastructure.Events
{
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        Task SubmitAsync(TEvent @event);
    }
}
