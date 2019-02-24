using System;
using System.Collections.Generic;
using System.Text;

namespace CLAVIER.Infrastructure.Commands.CodeEditor
{
    public class UpdateCodeHighlightsCommand : ICommand
    {
        public int Id { get; }
        public IEnumerable<int> HighlightedLines { get; }

        public UpdateCodeHighlightsCommand(int id, IEnumerable<int> highlightedLines)
        {
            this.Id = id;
            this.HighlightedLines = highlightedLines;
        }
    }
}
