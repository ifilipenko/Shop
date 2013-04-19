using Shop.Services.Domain.Model;

namespace Shop.Services.Domain
{
    public interface IProductRepository
    {
        void Save(Product product);
        Product FindById(int id);
    }
}