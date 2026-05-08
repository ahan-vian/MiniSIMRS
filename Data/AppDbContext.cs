using Microsoft.EntityFrameworkCore;
using MiniSIMRS.Models;

namespace MiniSIMRS.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Dokter> Dokters { get; set; }
    public DbSet<Pasien> Pasiens { get; set; }
    public DbSet<RekamMedis> RekamMediss { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pasien>()
            .HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<Dokter>()
            .HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<RekamMedis>()
            .HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<User>()
            .HasQueryFilter(p => !p.IsDeleted);
        base.OnModelCreating(modelBuilder);
    }

}