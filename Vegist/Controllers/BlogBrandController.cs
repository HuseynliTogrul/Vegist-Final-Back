using Microsoft.AspNetCore.Mvc;

namespace Vegist.Controllers
{
    public class BlogBrandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
