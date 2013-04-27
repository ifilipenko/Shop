using System.Data.Entity;

namespace Shop.Domain.Model
{
    public class ProductModelContext : DbContext
    {
        public ProductModelContext()
            : base("name=ShopDB")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Shop.Domain.Model.Category> Categories { get; set; }
    }
}