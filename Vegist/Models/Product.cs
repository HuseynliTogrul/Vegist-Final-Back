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
    }
}
