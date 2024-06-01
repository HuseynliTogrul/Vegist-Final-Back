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
    public class ProductMaterialController : Controller
    {
        private readonly AppDbContext _context;

        public ProductMaterialController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var prMaterials = await _context.ProductMaterials.Include(x => x.Product).Include(x => x.Material).ToListAsync();
            return View(prMaterials);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Materials = await _context.Materials.ToListAsync();
            ViewBag.Products = await _context.Products.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductMaterialVm productMaterialVm)
        {
            if (!ModelState.IsValid) return View(productMaterialVm);

            var exist = await _context.ProductMaterials.Include(x => x.Material).AnyAsync(x => x.MaterialId == productMaterialVm.MaterialId);
            if (exist)
            {
                ModelState.AddModelError("", "Material already exist");
                return View(productMaterialVm);
            }
            _context.ProductMaterials.Add((ProductMaterial)productMaterialVm);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ProductMaterial? productMaterial = await _context.ProductMaterials.FirstOrDefaultAsync(x => x.Id == id);
            if (productMaterial is null)
            {
                return NotFound();
            }

            _context.ProductMaterials.Remove(productMaterial);

            productMaterial.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}