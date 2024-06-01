using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegist.Data;

namespace Vegist.ViewComponents
{
    public class SizeViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public SizeViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sizes = await _context.Sizes
                                        //.Where(x => x.IsDeleted)
                                        .Include(x => x.ProductSize)
                                        .ToListAsync();
            return View(sizes);
        }
    }
}