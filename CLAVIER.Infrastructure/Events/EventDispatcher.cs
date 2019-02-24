using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CLAVIER.Infrastructure.Events
{
    internal class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task DispatchAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var handler = _serviceProvider.GetRequiredService<IEventHandler<TEvent>>();

            return handler.SubmitAsync(@event);
        }
    }
}
