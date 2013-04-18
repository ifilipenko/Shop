using Shop.Services.Domain.Commands;

namespace Shop.Services.Domain
{
    public interface IProductService
    {
        int SaveProduct(ProductSaveCommand command);
    }
}