using System.Linq;
using Shop.Domain.Model;

namespace Shop.Domain.Repositories
{
    public interface IProductRepository
    {
        Product FindById(int id);
        void Save(Product product);
        IQueryable<Product> GetAll();
        void Delete(Product product);
    }
}