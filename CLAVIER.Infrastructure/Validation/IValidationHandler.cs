using CLAVIER.Infrastructure.Commands;
using System.Threading.Tasks;

namespace CLAVIER.Infrastructure.Validation
{
    public interface IValidationHandler<in TCommand> where TCommand : ICommand
    {
        Task<ValidationResult> ExecuteAsync(TCommand command);
    }
}
