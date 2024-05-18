using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Vegist.Data
{
    public class AppDbContext:IdentityDbContext 
    {
        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
