using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vegist.Models
{
    public class AppUser :IdentityUser
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
    }
}
