using Vegist.Areas.Admin.ViewModels;
using Vegist.Models.BaseModel;

namespace Vegist.Models
{
    public class ProductMaterial : BaseEntity
    {
        public Product Product { get; set; } = null!;
        public int ProductId { get; set; }
        public Material? Material { get; set; }
        public int MaterialId { get; set; }
        public int Count { get; set; }

        public static explicit operator ProductMaterial(ProductMaterialVm productMaterialVm)
        {
            return new ProductMaterial
            {
                ProductId = productMaterialVm.ProductId,
                MaterialId = productMaterialVm.MaterialId,
                Count = productMaterialVm.Count
            };
        }
    }
}
