using Microsoft.EntityFrameworkCore;

namespace Selu383.SP24.Api
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }

        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>()
                .Property(x => x.Name)
                .IsRequired().HasMaxLength(120);

            modelBuilder.Entity<Hotel>()
                .Property(x => x.Address)
                .IsRequired();

        }
    }

}

