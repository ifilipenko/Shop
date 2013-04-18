using System.Data.SqlClient;
using Shop.Services.Domain.Commands;
using Shop.Services.Domain.Dto;
using Shop.Services.Domain.Settings;

namespace Shop.Services.Domain
{
    public class ProductService : IProductService
    {
        private readonly ApplicationSettings _applicationSettings;

        public ProductService(ApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        /// <summary>
        /// Create or update product
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Generated id when product is new or passed id</returns>
        public int SaveProduct(ProductSaveCommand command)
        {
            return 0;
        }

        /// <summary>
        /// Find product in repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product with given id or null when product is not exists</returns>
        public Product FindById(int id)
        {
            using (var connection = new SqlConnection(_applicationSettings.ConnectionStringName))
            {
                
            }
            return null;
        }
    }
}