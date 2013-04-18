using Shop.Services.Domain.Commands;
using Shop.Services.Domain.Dto;

namespace Shop.Services.Domain
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// Create or update product
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Generated id when product is new or passed id</returns>
        public int SaveProduct(ProductSaveCommand command)
        {
            return 0;
        }

        public Product FindById(int id)
        {
            return null;
        }
    }
}