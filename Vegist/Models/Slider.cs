using System.ComponentModel.DataAnnotations.Schema;
using Vegist.Models.BaseModel;

namespace Vegist.Models
{
    public class Slider : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        [NotMapped]
        public IFormFile Files { get; set; } = null!;
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        //public string ImagePath { get; set; } = string.Empty;
    }
}
