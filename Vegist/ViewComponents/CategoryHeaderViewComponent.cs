using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegist.Data;

namespace Vegist.ViewComponents
{
    public class CategoryHeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public CategoryHeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories
                                                .Where(x => !x.IsDeleted)
                                                .ToListAsync();
            return View(categories);
        }
    }
}
