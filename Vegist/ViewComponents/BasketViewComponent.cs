using Microsoft.AspNetCore.Mvc;
using Vegist.ViewModels;

namespace Vegist.ViewComponents
{
    public class BasketViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            var basket = new BasketVm { Id = id, Items = new List<string> { "Item1", "Item2" } };
            return View(basket);
        }
    }
}
