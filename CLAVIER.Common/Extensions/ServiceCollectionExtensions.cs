using CLAVIER.Common.File;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace CLAVIER.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection source)
        {
            source.BuildServiceProvider();

            source.TryAdd(GetFileServices());

            return source;
        }

        private static IEnumerable<ServiceDescriptor> GetFileServices()
        {
            yield return ServiceDescriptor.Scoped<IFileSystem, InternalFileSystem>();
        }
    }
}
