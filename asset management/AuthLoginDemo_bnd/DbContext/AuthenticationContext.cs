using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace AuthLoginDemo_bnd.Models
{
    public class AuthenticationContext : DbContext
    {
        public AuthenticationContext(DbContextOptions options):base(options) { }
        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Neighbourhood> Neighbourhoods { get; set; }
        public DbSet<LogOperations> LogOperations { get; set; }
    }
}