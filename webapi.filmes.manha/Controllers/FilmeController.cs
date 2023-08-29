using Microsoft.AspNetCore.Mvc;

namespace webapi.filmes.manha.Controllers
{
    public class FilmeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
