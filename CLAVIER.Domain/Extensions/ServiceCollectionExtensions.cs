using CLAVIER.Domain.Commands.CodeEditor;
using CLAVIER.Infrastructure.Commands;
using CLAVIER.Infrastructure.Commands.CodeEditor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using CLAVIER.Domain.Queries.CodeEditor;
using CLAVIER.Infrastructure.Queries;

namespace CLAVIER.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection source)
        {
            source.BuildServiceProvider();

            source.TryAdd(GetCommandHandlers());
            source.TryAdd(GetQueryHandlers());

            return source;
        }

        private static IEnumerable<ServiceDescriptor> GetCommandHandlers()
        {
            yield return ServiceDescriptor.Transient<ICommandHandler<UpdateCodeCommand>, CodeCommandHandler>();
        }

        private static IEnumerable<ServiceDescriptor> GetEventHandlers()
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<ServiceDescriptor> GetQueryHandlers()
        {
            yield return ServiceDescriptor.Transient<IQueryHandler<ReadCodeQuery, ReadCodeResult>, CodeQueryHandler>();;
        }

        private static IEnumerable<ServiceDescriptor> GetValidationHandlers()
        {
            throw new NotImplementedException();
        }
    }
}
