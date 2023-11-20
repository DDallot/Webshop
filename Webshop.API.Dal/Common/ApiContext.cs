using Microsoft.EntityFrameworkCore;
using Webshop.API.Dal.CartProductDal;
using Webshop.API.Dal.ProductDal;
using Webshop.API.Dal.ShoppingCartDal;

namespace Webshop.API.Dal.Common;

public class ApiContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryWebshop");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new Product { Identifier = 1, Ean = "00001111", Name = "Heineken Lager", Quantity = 6, Price = 1.99m, Currency = "dollar" },
            new Product { Identifier = 2, Ean = "00002222", Name = "Uiltje IPA", Quantity = 4, Price = 2.49m, Currency = "dollar" },
            new Product { Identifier = 3, Ean = "00003333", Name = "Bud Light", Quantity = 22, Price = 0.99m, Currency = "dollar" },
            new Product { Identifier = 4, Ean = "00004444", Name = "Mikkeler IPA", Quantity = 23, Price = 16.99m, Currency = "dollar" }
        );

        modelBuilder.Entity<CartProduct>()
            .HasOne(c => c.ShoppingCart);

        modelBuilder.Entity<ShoppingCart>()
            .HasMany(c => c.CartProducts);
    }
}