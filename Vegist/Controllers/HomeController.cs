using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Vegist.Data;
using Vegist.ViewModels;

namespace Vegist.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(ProductSearchVm vm)
        {
            var products = _context.Products.Where(x => !x.IsDeleted).Include(x => x.ProductImages).AsQueryable();

            if (vm.Name != null)
            {
                products = products.Where(x => x.Title.ToLower().StartsWith(vm.Name.ToLower()));
            }
            else
            {
                products = products.Where(x => x.Title.ToLower().StartsWith(vm.Name.ToLower()));
            }
            return View(await products.ToListAsync());
        }
    }
}