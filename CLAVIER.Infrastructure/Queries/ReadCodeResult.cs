using System.Collections.Generic;

namespace CLAVIER.Infrastructure.Queries
{
    public class ReadCodeResult : IQueryResult
    {
        public IEnumerable<string> Lines { get; }

        public ReadCodeResult(IEnumerable<string> lines)
        {
            this.Lines = lines;
        }
        
    }
}