using CLAVIER.Common.File;
using Microsoft.AspNetCore.Mvc;

namespace CLAVIER.Api.Controllers
{
    [Route("/file")]
    public class FileController : Controller
    {
        private readonly IFileSystem _fileSystem;

        public FileController(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        [Route("download/{id}")]
        public ActionResult Download(string id)
        {
            var data = _fileSystem.GetCodeBytes(id);

            return File(data, "application/octet-stream", $"code-{id}.txt");
        }
    }
}