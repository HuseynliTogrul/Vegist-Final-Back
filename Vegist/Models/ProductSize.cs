using Vegist.Areas.Admin.ViewModels;
using Vegist.Models.BaseModel;

namespace Vegist.Models
{
    public class ProductSize : BaseEntity
    {
        public Product Product { get; set; } = null!;
        public int ProductId { get; set; }
        public Size? Size { get; set; }
        public int SizeId { get; set; }
        public int Count { get; set; }

        public static explicit operator ProductSize(ProductSizeVm productSizeVm)
        {
            return new ProductSize
            {
                ProductId = productSizeVm.ProductId,
                SizeId = productSizeVm.SizeId,
                Count = productSizeVm.Count
            };
        }
    }
}
