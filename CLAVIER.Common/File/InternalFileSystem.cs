using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using IOFile = System.IO.File;
namespace CLAVIER.Common.File
{
    internal class InternalFileSystem : IFileSystem
    {
        public Task SaveCodeAsync(string id, IEnumerable<string> lines)
        {
            var filePath = $"{Path.GetTempPath()}\\{id}.txt";
            return IOFile.WriteAllTextAsync(filePath, string.Join(Environment.NewLine, lines));
        }

        public IEnumerable<string> ReadCode(string id)
        {
            var filePath = $"{Path.GetTempPath()}\\{id}.txt";
            
            if (IOFile.Exists(filePath))
            {
                IEnumerable<string> lines = IOFile.ReadAllText(filePath).Split(Environment.NewLine);
                return lines;
            }

            return new List<string>();
        }

        public byte[] GetCodeBytes(string id)
        {
            var filePath = $"{Path.GetTempPath()}\\{id}.txt";
            byte[] result = new byte[] { };

            if (IOFile.Exists(filePath))
            {
                result = IOFile.ReadAllBytes(filePath);
            }

            return result;
        }
    }
}
