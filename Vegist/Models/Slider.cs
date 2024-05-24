using System.ComponentModel.DataAnnotations.Schema;
using Vegist.Models.BaseModel;

namespace Vegist.Models
{
    public class Slider : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        [NotMapped]
        public ICollection<IFormFile>? Files { get; set; }
        [NotMapped]
        public IFormFile MainFile { get; set; } = null!;
        public ICollection<ProductImage>? ProductImages { get; set; }
        public string ImagePath { get; set; } = null!;
    }
}
