using Microsoft.EntityFrameworkCore;
using SmartMeter.Data.Entities;


namespace SmartMeter.Data.Context
{
    public class SmartMeterDbContext : DbContext
    {
        public SmartMeterDbContext(DbContextOptions<SmartMeterDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Consumer> Consumers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Consumer>().ToTable("Consumer");
        }
    }
}
