using Vegist.Models.BaseModel;

namespace Vegist.Models
{
    public class Size : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<ProductSize> ProductSize { get; set; }
        public Size()
        {
            ProductSize = new HashSet<ProductSize>();
        }
    }
}