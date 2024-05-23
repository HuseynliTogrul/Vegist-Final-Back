using Vegist.Models.BaseModel;

namespace Vegist.Models
{
    public class Material : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<ProductMaterial> ProductMaterial { get; set; }
        public Material()
        {
            ProductMaterial = new HashSet<ProductMaterial>();
        }
    }
} 
