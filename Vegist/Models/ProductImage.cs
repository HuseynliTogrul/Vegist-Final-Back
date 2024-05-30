using System.ComponentModel.DataAnnotations.Schema;
using Vegist.Models.BaseModel;

namespace Vegist.Models
{
    public class ProductImage : BaseEntity
    {
        public string Url { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile? File { get; set; }
        public bool IsMain { get; set; }
        public bool IsHover { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public int? SliderId { get; set; }
        public Slider? Slider { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}
