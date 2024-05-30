using Microsoft.AspNetCore.Mvc;
using Vegist.Models;

namespace Vegist.Controllers
{ 
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(AppUser appUser)
        {
            return View();
        }
    }
}
