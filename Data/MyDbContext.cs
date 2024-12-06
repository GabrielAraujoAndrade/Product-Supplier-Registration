using Microsoft.EntityFrameworkCore;
using Product_Supplier_Registration.Models;

public class MyDbContext : DbContext
{
    public DbSet<Material> Materials { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }


    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Material>()
            .HasOne<Supplier>() 
            .WithMany()
            .HasForeignKey(m => m.IdSupplier)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

