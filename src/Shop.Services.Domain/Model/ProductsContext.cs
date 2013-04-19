using System.Data.Entity;

namespace Shop.Services.Domain.Model
{
    public class ProductsContext : DbContext
    {
        public ProductsContext()
            : base("ShopDB")
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}