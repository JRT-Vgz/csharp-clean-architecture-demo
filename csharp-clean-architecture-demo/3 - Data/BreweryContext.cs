using _3___Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3___Data
{
    public class BreweryContext : DbContext
    {
        public BreweryContext(DbContextOptions<BreweryContext> options) : base(options) { }

        public DbSet<BeerModel> Beers { get; set; }
        public DbSet<BrandModel> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BeerModel>().ToTable("Beer");
            modelBuilder.Entity<BrandModel>().ToTable("Brand");
        }
    }
}
