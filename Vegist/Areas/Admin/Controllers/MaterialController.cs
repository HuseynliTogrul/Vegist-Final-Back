using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegist.Data;
using Vegist.Models;

namespace Vegist.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]

    public class MaterialController : Controller
    {
        private readonly AppDbContext _context;

        public MaterialController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var materials = await _context.Materials.ToListAsync();
            return View(materials);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Material material)
        {
            material.Name = material.Name.ToUpper().Trim();
            if (_context.Materials.Any(x => x.Name.ToUpper().Trim() == material.Name))
            {
                ModelState.AddModelError("", "Material already exist");
                return View();
            }
            await _context.Materials.AddAsync(material);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}