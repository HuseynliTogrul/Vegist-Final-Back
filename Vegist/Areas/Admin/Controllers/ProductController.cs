using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegist.Controllers;
using Vegist.Data;
using Vegist.Extentions;
using Vegist.Models;

namespace Vegist.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products
                                                   .Include(x => x.ProductImages)
                                                   .Include(x => x.Category)
                                                   .Where(x => !x.IsDeleted)
                                                   .ToListAsync();
            return View(products);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (_context.Products.Any(p => p.Title == product.Title))
            {
                ModelState.AddModelError("", "Product already exists");
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(product);
            }

            product.ProductImages = new List<ProductImage>();

            if (product.Files != null)
            {
                foreach (var file in product.Files)
                {
                    var errorMessage = await ProcessFileAsync(file, product, isHover: false, isMain: false);
                    if (errorMessage != null)
                    {
                        ModelState.AddModelError("Files", errorMessage);
                        ViewBag.Categories = await _context.Categories.ToListAsync();
                        return View(product);
                    }
                }
            }

            var mainFileErrorMessage = await ProcessFileAsync(product.MainFile, product, isHover: false, isMain: true);
            if (mainFileErrorMessage != null)
            {
                ModelState.AddModelError("MainFile", mainFileErrorMessage);
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(product);
            }

            var hoverFileErrorMessage = await ProcessFileAsync(product.HoverFile, product, isHover: true, isMain: false);
            if (hoverFileErrorMessage != null)
            {
                ModelState.AddModelError("HoverFile", hoverFileErrorMessage);
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View(product);
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private async Task<string> ProcessFileAsync(IFormFile file, Product product, bool isHover, bool isMain)
        {
            if (file == null)
            {
                return null;
            }

            if (!file.CheckFileSize(2))
            {
                return "Files cannot be more than 2MB";
            }

            if (!file.CheckFileType("image"))
            {
                return "Files must be image type!";
            }

            var fileName = await file.SaveFileAsync(_env.WebRootPath, "Client", "assets", "images");
            var productImage = CreateProductImage(fileName, isHover, isMain, product);
            product.ProductImages.Add(productImage);

            return null;
        }


        private ProductImage CreateProductImage(string url, bool isHover, bool isMain, Product product)
        {
            return new ProductImage
            {
                Url = url,
                IsHover = isHover,
                IsMain = isMain,
                Product = product
            };
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id < 1) return View("404");
            ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
            var product = await _context.Products.Include(x => x.ProductImages)
                                                 .Include(x => x.Category)
                                                 .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return View("404");


            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id) return BadRequest();

            var existProduct = await _context.Products.Include(p => p.ProductImages)
                                                      .FirstOrDefaultAsync(p => p.Id == id);
            if (existProduct == null) return NotFound();

            // Update existing product fields
            existProduct.Title = product.Title;
            existProduct.Rating = product.Rating;
            existProduct.SellPrice = product.SellPrice;
            existProduct.DiscPrice = product.DiscPrice;
            existProduct.Description = product.Description;
            existProduct.CategoryId = product.CategoryId;

            if (product.Files != null)
            {
                foreach (var file in product.Files)
                {
                    var errorMessage = await ProcessFileAsync(file, existProduct, isHover: false, isMain: false);
                    if (errorMessage != null)
                    {
                        ModelState.AddModelError("Files", errorMessage);
                        ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
                        return View(product);
                    }
                }
            }

            if (product.MainFile != null)
            {
                var mainImage = existProduct.ProductImages.FirstOrDefault(x => x.IsMain);
                if (mainImage != null)
                {
                    mainImage.Url.DeleteFile(_env.WebRootPath, "Client", "assets", "images");
                    _context.ProductImages.Remove(mainImage);
                }

                var mainFileErrorMessage = await ProcessFileAsync(product.MainFile, existProduct, isHover: false, isMain: true);
                if (mainFileErrorMessage != null)
                {
                    ModelState.AddModelError("MainFile", mainFileErrorMessage);
                    ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
                    return View(product);
                }
            }

            if (product.HoverFile != null)
            {
                var hoverImage = existProduct.ProductImages.FirstOrDefault(x => x.IsHover);
                if (hoverImage != null)
                {
                    hoverImage.Url.DeleteFile(_env.WebRootPath, "Client", "assets", "images");
                    _context.ProductImages.Remove(hoverImage);
                }

                var hoverFileErrorMessage = await ProcessFileAsync(product.HoverFile, existProduct, isHover: true, isMain: false);
                if (hoverFileErrorMessage != null)
                {
                    ModelState.AddModelError("HoverFile", hoverFileErrorMessage);
                    ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
                    return View(product);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            var product = await _context.Products.Include(x => x.Category)
                                                 .Include(x => x.ProductImages)
                                                 .Include(x => x.ProductSizes)
                                                 .ThenInclude(x => x.Size)
                                                 .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var existsImage = await _context.ProductImages.FindAsync(id);
            if (existsImage == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(x => x.Category)
                                                 .Include(x => x.ProductImages)
                                                 .FirstOrDefaultAsync(x => x.Id == existsImage.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            existsImage.Url.DeleteFile(_env.WebRootPath, "Client", "assets", "images");
            _context.ProductImages.Remove(existsImage);
            await _context.SaveChangesAsync();

            return PartialView("_ProductImagePartial", product.ProductImages);
        }


        public async Task<IActionResult> Delete(int id)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            product.IsDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}