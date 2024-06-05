using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegist.Data;

namespace Vegist.ViewComponents
{
    public class MaterialProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public MaterialProductViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var materials = await _context.Materials
                                             .Where(x => !x.IsDeleted)
                                             .ToListAsync();
            return View(materials);
        }
    }
}
