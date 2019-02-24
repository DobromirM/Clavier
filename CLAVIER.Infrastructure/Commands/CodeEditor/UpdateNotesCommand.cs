using System.Collections.Generic;

namespace CLAVIER.Infrastructure.Commands.CodeEditor
{
    public class UpdateNotesCommand : ICommand
    {
        public int Id { get; }
        public IEnumerable<string> Lines { get; }

        public UpdateNotesCommand(int id, IEnumerable<string> lines)
        {
            this.Id = id;
            this.Lines = lines;
        }
    }
}
