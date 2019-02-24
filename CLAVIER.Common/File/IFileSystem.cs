using System.Collections.Generic;
using System.Threading.Tasks;

namespace CLAVIER.Common.File
{
    public interface IFileSystem
    {
        Task SaveCodeAsync(string id, IEnumerable<string> lines);
        
        IEnumerable<string> ReadCode(string id);

        byte[] GetCodeBytes(string id);
    }
}
