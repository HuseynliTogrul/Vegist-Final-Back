using Microsoft.AspNetCore.Mvc;

namespace Vegist.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
