using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiProductCRUD.Models;

namespace WebApiProductCRUD.Data
{
    public class AppDbContext : IdentityDbContext<DbUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<DbUser> DbUsers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
