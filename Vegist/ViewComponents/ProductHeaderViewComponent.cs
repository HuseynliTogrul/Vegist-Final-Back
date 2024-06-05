using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegist.Data;

namespace Vegist.ViewComponents
{
    public class ProductHeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ProductHeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _context.Products
                                                .Where(x => !x.IsDeleted)
                                                .ToListAsync();
            return View(products);
        }
    }
}
