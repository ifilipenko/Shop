using Shop.Services.Domain.Commands;

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
    }
}