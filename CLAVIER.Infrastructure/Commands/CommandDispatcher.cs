using CLAVIER.Infrastructure.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CLAVIER.Infrastructure.Commands
{
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<ValidationResult> DispatchAsync<TCommand>(TCommand command, bool validate = false) where TCommand : ICommand
        {
            var result = new ValidationResult
            {
                Success = true
            };

            if (validate)
            {
                var validationHandler = _serviceProvider.GetRequiredService<IValidationHandler<TCommand>>();

                result = await validationHandler.ExecuteAsync(command);

                if (!result.Success)
                {
                    return result;
                }
            }


            try
            {
                var commandHandler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();

                await commandHandler.ExecuteAsync(command);
            }
            catch (Exception)
            {
                result.Success = false;
            }

            return result;
        }
    }
}
