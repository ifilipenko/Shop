using Shop.Services.Domain.Commands;
using Shop.Services.Domain.Dto;

namespace Shop.Services.Domain
{
    public interface IProductService
    {
        int SaveProduct(ProductSaveCommand command);
        Product FindById(int id);
    }
}