using System.Collections.Generic;

namespace CLAVIER.Infrastructure.Commands.CodeEditor
{
    public class UpdateCodeCommand : ICommand
    {
        public string Id { get; }
        public IEnumerable<string> Lines { get; }

        public UpdateCodeCommand(string id, IEnumerable<string> lines)
        {
            this.Id = id;
            this.Lines = lines;
        }
    }
}
