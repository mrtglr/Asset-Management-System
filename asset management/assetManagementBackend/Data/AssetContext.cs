using assetManagementBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace assetManagementBackend.Data
{
    public class AssetContext: DbContext
    {
        public AssetContext(DbContextOptions<AssetContext> options) : base(options) { }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Neighbourhood> Neighbourhoods { get; set; }
        public DbSet<LogOperations> LogOperations { get; set; }
    }
}