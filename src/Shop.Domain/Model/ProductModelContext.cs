using System.Data;
using System.Data.Entity;

namespace Shop.Domain.Model
{
    public class ProductModelContext : DbContext, IProductModelContext
    {
        public ProductModelContext()
            : base("name=ShopDB")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Category> Categories { get; set; }

        public void MarkAsUpdated(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}