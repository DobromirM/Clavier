using IOFile = System.IO.File;
using Microsoft.AspNetCore.Mvc;

namespace CLAVIER.WebClient.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return Content(IOFile.ReadAllText("~/../../CLAVIER.WebClient/ClientApp/public/home.html"), "text/html");
        }
    }
}