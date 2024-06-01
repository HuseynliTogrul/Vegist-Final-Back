using Microsoft.AspNetCore.Mvc;
using Vegist.Data;

namespace Vegist.ViewComponents;

public class SearchViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public SearchViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
