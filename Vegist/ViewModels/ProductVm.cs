using Vegist.Models;

namespace Vegist.ViewModels
{
    public class ProductVm
    {
        public Product? Product { get; set; }
        public List<Product>? Products { get; set; }
        public List<Category>? Categories { get; set; }
        public PaginateVm? PaginateVm { get; set; }
    }
}