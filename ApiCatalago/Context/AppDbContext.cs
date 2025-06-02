using ApiCatalago.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalago.Context;

public class AppDbContext : IdentityDbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
    {
            
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Produto>()
            .Property(p => p.Preco)
            .HasPrecision(10, 2); // Precision: 10 dígitos no total, 2 após a vírgula
    }


    public DbSet<Produto>? Produtos { get; set; }
    public DbSet<Categoria>? Categorias{ get; set; }


}
