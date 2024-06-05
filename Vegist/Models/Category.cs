using System.ComponentModel.DataAnnotations.Schema;
using Vegist.Models.BaseModel;

namespace Vegist.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile? File { get; set; }
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public List<Product> Products { get; set; } = new List<Product>();
        //public string ImagePath { get; set; } = string.Empty;
    }
}
