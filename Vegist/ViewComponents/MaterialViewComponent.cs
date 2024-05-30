using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vegist.Data;

namespace Vegist.ViewComponents
{
    public class MaterialViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public MaterialViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var materials = await _context.Materials.Include(x => x.ProductMaterial).ToListAsync();
            return View(materials);
        }
    }
}