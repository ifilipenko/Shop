using System.Data.Entity;

namespace Shop.Domain.Model
{
    public interface IProductModelContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Vendor> Vendors { get; set; }
        DbSet<Category> Categories { get; set; }
        void MarkAsUpdated(object entity);
    }
}