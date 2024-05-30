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
            {
                return View(slider);
            }

            if (slider.Files != null)
            {
                var fileError = await ProcessFileAsync(slider);
                if (!string.IsNullOrEmpty(fileError))
                {
                    ModelState.AddModelError("File", fileError);
                    return View(slider);
                }
            }

            if (await SliderExistsAsync(slider.Name))
            {
                ModelState.AddModelError("Name", "Slider name already exists.");
                return View(slider);
            }

            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private async Task<string> ProcessFileAsync(Slider slider)
        {
            if (!slider.Files.CheckFileType("image"))
            {
                return "Invalid file type. Only image files are allowed.";
            }

            if (!slider.Files.CheckFileSize(2))
            {
                return "Invalid file size. Maximum allowed size is 2 MB.";
            }

            string uniqueFileName = await slider.Files.SaveFileAsync(_env.WebRootPath, "Client", "assets", "images").ConfigureAwait(false);

            var product = await _context.Products.FirstOrDefaultAsync();
            if (product == null)
            {
                return "No valid product found. Ensure there is at least one product in the database.";
            }

            slider.ProductImages.Add(new ProductImage
            {
                ImagePath = uniqueFileName,
                SliderId = slider.Id,
                Url = uniqueFileName,
            });

            return null;
        }

        private async Task<bool> SliderExistsAsync(string sliderName)
        {
            return await _context.Sliders.AnyAsync(x => x.Name.ToLower() == sliderName.ToLower()).ConfigureAwait(false);
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
            if (Slider.Files != null)
            {
                if (!Slider.Files.CheckFileSize(2))
                {
                    ModelState.AddModelError("File", "File size more than 2mb");
                    return View(Slider);
                }
                if (!Slider.Files.CheckFileType("image"))
                {
                    ModelState.AddModelError("File", "File type is incorrect");
                    return View(Slider);
                }


                var uniqueFileName = await Slider.Files.
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
            Slider? slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider is null)
            {
                return NotFound();
            }

            var productImages = await _context.ProductImages.Where(pi => pi.SliderId == slider.Id).ToListAsync();
            foreach (var productImage in productImages)
            {
                var imagePath = Path.Combine(_env.WebRootPath, "Client", "assets", "images", productImage.ImagePath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _context.ProductImages.Remove(productImage);
            }

            slider.IsDeleted = true;
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
