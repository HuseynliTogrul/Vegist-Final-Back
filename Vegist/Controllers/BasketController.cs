using Microsoft.AspNetCore.Mvc;

namespace Vegist.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
