using CLAVIER.Common.File;
using CLAVIER.Infrastructure.Queries;

namespace CLAVIER.Domain.Queries.CodeEditor
{
    internal class CodeQueryHandler : IQueryHandler<ReadCodeQuery, ReadCodeResult>,
                                      IQueryHandler<RetrieveCodeBytesQuery, RetrieveCodeBytesResult>
    {
        private readonly IFileSystem _fileSystem;

        public CodeQueryHandler(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public ReadCodeResult RetrieveAsync(ReadCodeQuery query)
        {
            return new ReadCodeResult(_fileSystem.ReadCode(query.Id));
        }

        public RetrieveCodeBytesResult RetrieveAsync(RetrieveCodeBytesQuery query)
        {
            return new RetrieveCodeBytesResult(_fileSystem.GetCodeBytes(query.Id));
        }
    }
}
