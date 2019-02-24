using CLAVIER.Infrastructure.Commands;
using CLAVIER.Infrastructure.Events;
using CLAVIER.Infrastructure.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace CLAVIER.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection source)
        {
            source.BuildServiceProvider();

            source.TryAdd(GetDispatchers());

            return source;
        }

        private static IEnumerable<ServiceDescriptor> GetDispatchers()
        {
            yield return ServiceDescriptor.Transient<ICommandDispatcher, CommandDispatcher>();
            yield return ServiceDescriptor.Transient<IQueryDispatcher, QueryDispatcher>();
            yield return ServiceDescriptor.Transient<IEventDispatcher, EventDispatcher>();
        }
    }
}
