using System.ComponentModel.DataAnnotations.Schema;
using Vegist.Models.BaseModel;

namespace Vegist.Models
{
    public class ProductImage : BaseEntity
    {
        public string Url { get; set; } = null!;
        [NotMapped]
        public IFormFile File { get; set; } = null!;
        public bool IsMain { get; set; }
        public bool IsHover { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
