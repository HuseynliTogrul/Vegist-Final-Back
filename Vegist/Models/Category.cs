using System.ComponentModel.DataAnnotations.Schema;
using Vegist.Models.BaseModel;

namespace Vegist.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;
        [NotMapped]
        public IFormFile? File { get; set; }
        public List<ProductImage> ProductImages { get; set; } = null!;
        public List<Product> Products { get; set; } = null!;
    }
}
