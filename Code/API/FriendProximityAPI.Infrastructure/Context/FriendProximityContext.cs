using FriendProximityAPI.Domain.Entities;
using FriendProximityAPI.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FriendProximityAPI.Infrastructure.Context
{
    public class FriendProximityContext : DbContext
    {
        private readonly IConfiguration configuration;

        public FriendProximityContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<Friend> Friends { get; set; }
        public DbSet<Point> Points { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FriendMap());
            modelBuilder.ApplyConfiguration(new PointMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TestLocal");
        }
    }
}
