using GeoSense.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using GeoSense.API.Infrastructure.Mappings;

namespace GeoSense.API.Infrastructure.Contexts
{
    public class MotoContext : DbContext
    {
        public MotoContext(DbContextOptions<MotoContext> options)
            : base(options)
        {
        }

        public DbSet<Moto> Moto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MotoMapping());
        }
    }

}
