using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegist.Data;
using Vegist.Extentions;
using Vegist.Models;
using Vegist.ViewModels;

namespace Vegist.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                                     .Include(x => x.ProductImages)
                                     .Include(x => x.Products)
                                     .Where(x => !x.IsDeleted)
                                     .ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            if (category.File != null)
            {
                var fileError = await ProcessFileAsync(category);
                if (!string.IsNullOrEmpty(fileError))
                {
                    ModelState.AddModelError("File", fileError);
                    return View(category);
                }
            }

            if (await CategoryExistsAsync(category.Name))
            {
                ModelState.AddModelError("Name", "Category name already exists");
                return View(category);
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private async Task<string> ProcessFileAsync(Category category)
        {
            if (!category.File.CheckFileType("image"))
            {
                return "Invalid file type. Only image files are allowed.";
            }

            if (!category.File.CheckFileSize(2))
            {
                return "Invalid file size. Maximum allowed size is 2 MB.";
            }

            string uniqueFileName = await category.File.SaveFileAsync(_env.WebRootPath, "Client", "assets", "images").ConfigureAwait(false);

            var product = await _context.Products.FirstOrDefaultAsync();
            if (product == null)
            {
                return "No valid product found. Ensure there is at least one product in the database.";
            }

            category.ProductImages.Add(new ProductImage
            {
                //ImagePath = uniqueFileName,
                Category = category,
                ProductId = product.Id
            });

            return null;
        }

        private async Task<bool> CategoryExistsAsync(string categoryName)
        {
            return await _context.Categories.AnyAsync(x => x.Name.ToLower() == categoryName.ToLower()).ConfigureAwait(false);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return View("404");
            }
            Category? category = await _context.Categories
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return View("404");
            }
            return View(category);
        }

        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id) return BadRequest();
            Category? existsCategory = await _context.Categories
                                                        .AsNoTracking()
                                                        .FirstOrDefaultAsync(x => x.Id == id);
            if (existsCategory == null) return NotFound();
            if (category.File != null)
            {
                if (!category.File.CheckFileSize(2))
                {
                    ModelState.AddModelError("File", "File size more than 2mb");
                    return View(category);
                }
                if (!category.File.CheckFileType("image"))
                {
                    ModelState.AddModelError("File", "File type is incorrect");
                    return View(category);
                }


                var uniqueFileName = await category.File.
                    SaveFileAsync(_env.WebRootPath, "Client", "assets", "images");

                existsCategory.Name = category.Name;
                _context.Update(existsCategory);
            }
            else
            {
                _context.Categories.Update(category);

            }
            await _context.SaveChangesAsync();
            if (category.Name == null)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Category? category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category is null)
            {
                return NotFound();
            }

            var productImages = await _context.ProductImages.Where(pi => pi.CategoryId == category.Id).ToListAsync();
            foreach (var productImage in productImages)
            {
                var imagePath = Path.Combine(_env.WebRootPath, "Client", "assets", "images", productImage.Url);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _context.ProductImages.Remove(productImage);
            }

            category.IsDeleted = true;
            //_context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}