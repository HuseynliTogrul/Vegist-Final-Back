using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegist.Data;
using Vegist.Extentions;
using Vegist.Migrations;
using Vegist.Models;

namespace Vegist.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]

    public class SizeController : Controller
    {
        private readonly AppDbContext _context;

        public SizeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sizes = await _context.Sizes.ToListAsync();
            return View(sizes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Size size)
        {
            size.Name = size.Name.ToUpper().Trim();
            if (_context.Sizes.Any(x => x.Name.ToUpper().Trim() == size.Name))
            {
                ModelState.AddModelError("", "Size already exist");
                return View();
            }
            await _context.Sizes.AddAsync(size);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Size? size = await _context.Sizes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (size == null)
            {
                return NotFound();
            }
            return View(size);
        }

        public async Task<IActionResult> Update(int id, Size size)
        {
            if (id != size.Id) return BadRequest();
            Size? existsSize = await _context.Sizes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (existsSize == null) return NotFound();
            if (size != null)
            {
                existsSize.Name = size.Name;
                _context.Update(existsSize);
            }
            else
            {
                _context.Sizes.Update(size);
            }

            if (size.Name == null)
            {
                return RedirectToAction("Edit", new { id = id });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Size? size = await _context.Sizes.FirstOrDefaultAsync(x => x.Id == id);
            if (size is null)
            {
                return NotFound();
            }
            _context.Sizes.Remove(size);
            size.IsDeleted = true;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}