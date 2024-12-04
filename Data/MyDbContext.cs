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
            .HasOne(m => m.Supplier)
            .WithMany()  // Assumindo que Supplier pode ter muitos Materials
            .HasForeignKey(m => m.IdSupplier)  // Relacionamento com a chave estrangeira
            .OnDelete(DeleteBehavior.Restrict);  // Ou use outro comportamento de exclusão
    }
}
