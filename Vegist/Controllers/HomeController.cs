using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Vegist.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}