using Microsoft.EntityFrameworkCore;
using Product_Supplier_Registration.Models;

public class MyDbContext : DbContext
{
    public DbSet<Material> Materials { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    // Construtor que recebe DbContextOptions
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Material>()
            .HasOne<Supplier>() // Sem incluir a propriedade de navegação
            .WithMany()
            .HasForeignKey(m => m.IdSupplier)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

