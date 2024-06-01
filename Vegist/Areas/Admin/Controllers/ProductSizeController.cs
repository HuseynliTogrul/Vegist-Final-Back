using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegist.Areas.Admin.ViewModels;
using Vegist.Data;
using Vegist.Models;

namespace Vegist.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductSizeController : Controller
    {
        private readonly AppDbContext _context;

        public ProductSizeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var prSizes = await _context.ProductSizes.Include(x => x.Product).Include(x => x.Size).ToListAsync();
            return View(prSizes);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Sizes = await _context.Sizes.ToListAsync();
            ViewBag.Products = await _context.Products.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductSizeVm productSizeVm)
        {
            if (!ModelState.IsValid) return View(productSizeVm);

            var exist = await _context.ProductSizes.Include(x => x.Size).AnyAsync(x => x.SizeId == productSizeVm.SizeId);
            if (exist)
            {
                ModelState.AddModelError("", "Size already exist");
                return View(productSizeVm);
            }
            _context.ProductSizes.Add((ProductSize)productSizeVm);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ProductSize? productSize = await _context.ProductSizes.FirstOrDefaultAsync(x => x.Id == id);
            if (productSize is null)
            {
                return NotFound();
            }

            _context.ProductSizes.Remove(productSize);

            productSize.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}