using Microsoft.AspNetCore.Mvc;

namespace Vegist.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
