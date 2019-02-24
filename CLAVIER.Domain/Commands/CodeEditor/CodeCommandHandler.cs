using CLAVIER.Common.File;
using CLAVIER.Infrastructure.Commands;
using CLAVIER.Infrastructure.Commands.CodeEditor;
using System.Threading.Tasks;
using CLAVIER.Infrastructure.Queries;

namespace CLAVIER.Domain.Commands.CodeEditor
{
    internal class CodeCommandHandler : ICommandHandler<UpdateCodeCommand>
    {
        private readonly IFileSystem _fileSystem;

        public CodeCommandHandler(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public Task ExecuteAsync(UpdateCodeCommand command)
        {
            return _fileSystem.SaveCodeAsync(command.Id, command.Lines);            
        }
    }
}
