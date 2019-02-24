using CLAVIER.Infrastructure.Validation;
using System.Threading.Tasks;

namespace CLAVIER.Infrastructure.Commands
{
    public interface ICommandDispatcher
    {
        Task<ValidationResult> DispatchAsync<TCommand>(TCommand command, bool validate = false) where TCommand : ICommand;
    }
}
