using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TreasureHunt.Application.Models;
// using TreasureHunt.Infrastructure.Data.Entities;

namespace TreasureHunt.Infrastructure.Context
{
    // public class TreasureDbContextFactory : IDesignTimeDbContextFactory<TreasureDbContext>
    // {
    //     public TreasureDbContext CreateDbContext(string[] args)
    //     {
    //         var optionsBuilder = new DbContextOptionsBuilder<TreasureDbContext>();
    //         var ConnectionforMigration = "Server=localhost,1433; Initial Catalog=TreasureDb; User ID=SA;Password=Password123 ;TrustServerCertificate=True";
    //         optionsBuilder.UseSqlServer(ConnectionforMigration);
    //         return new TreasureDbContext(optionsBuilder.Options);
    //     }
    // }

    public class TreasureDbContext : DbContext
    {
        public TreasureDbContext(DbContextOptions<TreasureDbContext> options) : base(options) { }

        public DbSet<TreasureMap> TreasureMaps { get; set; }
        public DbSet<MapCell> MapCells { get; set; }
        public DbSet<Solution> Solutions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<MapCell>()
            //     .HasOne(cell => cell.TreasureMap)
            //     .WithMany(map => map.Cells)
            //     .HasForeignKey(cell => cell.MapId);

            // modelBuilder.Entity<Solution>()
            //     .HasOne(sol => sol.TreasureMap)
            //     .WithMany(map => map.Solutions)
            //     .HasForeignKey(sol => sol.MapId);

            modelBuilder.Entity<MapCell>(entity =>
            {
                entity.HasOne<TreasureMap>()
                    .WithMany(map => map.Cells)
                    .HasForeignKey(cell => cell.MapId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Solution>(entity =>
            {
                entity.HasOne<TreasureMap>()
                    .WithMany(map => map.Solutions)
                    .HasForeignKey(sol => sol.MapId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}