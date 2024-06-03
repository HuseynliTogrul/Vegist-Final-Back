using System.ComponentModel.DataAnnotations.Schema;
using Vegist.Models.BaseModel;

namespace Vegist.Models
{
    public class Product : BaseEntity
    {
        public string Title { get; set; } = null!;
        public decimal Rating { get; set; }
        public decimal SellPrice { get; set; }
        public decimal DiscPrice { get; set; }
        public string Description { get; set; } = null!;
        [NotMapped]
        public List<IFormFile> Files { get; set; } = null!;
        [NotMapped]
        public IFormFile MainFile { get; set; } = null!;
        [NotMapped]
        public IFormFile HoverFile { get; set; } = null!;
        public List<ProductImage> ProductImages { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<ProductSize>? ProductSizes { get; set; }
        public ICollection<ProductMaterial>? ProductMaterials { get; set; }
        public Product()
        {
            ProductSizes = new HashSet<ProductSize>();
        }
    }
}
