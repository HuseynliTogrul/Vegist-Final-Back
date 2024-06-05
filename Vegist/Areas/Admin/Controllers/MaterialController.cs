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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Material? material = await _context.Materials.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }

        public async Task<IActionResult> Update(int id, Material material)
        {
            if (id != material.Id) return BadRequest();
            Material? existsMaterial = await _context.Materials.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (existsMaterial == null) return NotFound();
            if (material != null) 
            {
                existsMaterial.Name = material.Name;
                _context.Update(existsMaterial);
            }
            else
            {
                _context.Materials.Update(material);
            }

            if (material.Name == null)
            {
                return RedirectToAction("Edit", new { id = id });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Material? material = await _context.Materials.FirstOrDefaultAsync(x => x.Id == id);
            if (material is null)
            {
                return NotFound();
            }
            _context.Materials.Remove(material);
            material.IsDeleted = true;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}