using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegist.Data;

namespace Vegist.ViewComponents
{
    public class SizeProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public SizeProductViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sizes = await _context.Sizes
                                             .Where(x => !x.IsDeleted)
                                             .ToListAsync();
            return View(sizes);
        }
    }
}
