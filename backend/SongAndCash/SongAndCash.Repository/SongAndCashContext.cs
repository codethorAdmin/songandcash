using Microsoft.EntityFrameworkCore;
using SongAndCash.Model.Entity;

namespace SongAndCash.Repository;

public class SongAndCashContext(DbContextOptions<SongAndCashContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<RecoverableSale> RecoverableSales { get; set; }
    public DbSet<Contract> Contracts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.SpotifyLink).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Email).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Username).IsRequired().HasMaxLength(60);
        });

        modelBuilder.Entity<RecoverableSale>(entity =>
        {
            entity.ToTable("RecoverableSales");
            entity.HasKey(p => p.Id);
            entity
                .HasOne(p => p.User)
                .WithMany(p => p.RecoverableSales)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.ToTable("Contracts");
            entity.HasKey(p => p.Id);
            entity
                .HasOne(p => p.RecoverableSale)
                .WithOne(p => p.Contract)
                .HasForeignKey<RecoverableSale>(x => x.ContractId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
