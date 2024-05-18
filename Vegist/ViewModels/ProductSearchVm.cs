using Vegist.Models;

namespace Vegist.ViewModels
{
    public class ProductSearchVm
    {
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
    }
}