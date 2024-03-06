using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestAPITest.Data.Models;

namespace RestAPITest.Data
{
    public class AppDbContext : IdentityDbContext<APPUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> categories { get; set; }
        public DbSet<Item> items { get; set; }
    }
}
