using CLAVIER.Domain.Extensions;
using CLAVIER.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CLAVIER.Tests
{
    public class BaseTest
    {
        protected readonly IServiceProvider _serviceProvider;

        public BaseTest()
        {
            var services = new ServiceCollection();
            RegisterDependencies(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        protected virtual void RegisterDependencies(IServiceCollection source)
        {
            source.AddInfrastructure();
            source.AddDomain();
        }
    }
}
