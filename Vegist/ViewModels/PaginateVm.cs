using Vegist.Models;

namespace Vegist.ViewModels
{
    public class PaginateVm
    {
        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }
        public List<Product>? Products { get; set; }
    }
}