using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegist.Data;
using Vegist.Extentions;
using Vegist.Migrations;
using Vegist.Models;

namespace Vegist.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var sliders = await _context.Sliders
                                            .Include(x => x.ProductImages)
                                            .Where(x => !x.IsDeleted)
                                            .ToListAsync();
            return View(sliders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
                return View(slider);
            if (!slider.MainFile.CheckFileType("image"))
            {
                ModelState.AddModelError("", "Invalid File");
                return View(slider);
            }
            if (!slider.MainFile.CheckFileSize(2))
            {
                ModelState.AddModelError("", "Invalid File Size");
                return View(slider);
            }
            var isExist = await _context.Sliders.AnyAsync(x => x.Name.ToLower() == slider.Name.ToLower());

            if (isExist)
            {
                ModelState.AddModelError("Name", "slider name is already exist");
                return View();

            }
            string uniqueFileName = await slider.MainFile.SaveFileAsync(_env.WebRootPath, "Client", "assets", "images");

            Slider newSlider = new Slider
            {
                Name = slider.Name,
                Description = slider.Description,
            };

            await _context.Sliders.AddAsync(newSlider);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return View("404");
            }
            Slider? Slider = await _context.Sliders
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync(x => x.Id == id);
            if (Slider == null)
            {
                return View("404");
            }
            return View(Slider);
        }

        public async Task<IActionResult> Update(int id, Slider Slider)
        {
            if (id != Slider.Id) return BadRequest();
            Slider? existsSlider = await _context.Sliders
                                                        .AsNoTracking()
                                                        .FirstOrDefaultAsync(x => x.Id == id);
            if (existsSlider == null) return NotFound();
            if (Slider.MainFile != null)
            {
                if (!Slider.MainFile.CheckFileSize(2))
                {
                    ModelState.AddModelError("File", "File size more than 2mb");
                    return View(Slider);
                }
                if (!Slider.MainFile.CheckFileType("image"))
                {
                    ModelState.AddModelError("File", "File type is incorrect");
                    return View(Slider);
                }


                var uniqueFileName = await Slider.MainFile.
                    SaveFileAsync(_env.WebRootPath, "Client", "assets", "images");

                existsSlider.Name = Slider.Name;
                _context.Update(existsSlider);
            }
            else
            {
                _context.Sliders.Update(Slider);

            }
            await _context.SaveChangesAsync();
            if (Slider.Name == null)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Slider? Slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (Slider is null)
            {
                return NotFound();
            }
            _context.Sliders.Remove(Slider);
            var path = Path.Combine(_env.WebRootPath, "Client", "assets", "images");
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            Slider.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
