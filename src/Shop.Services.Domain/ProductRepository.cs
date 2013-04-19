using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Shop.Services.Domain.Dto;
using Shop.Services.Domain.Settings;

namespace Shop.Services.Domain
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationSettings _applicationSettings;

        public ProductRepository(ApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        /// <summary>
        /// Create or update product. When product is new, new id is set to passed product instance
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Generated id when product is new or passed id</returns>
        public void Save(Product product)
        {
            using (var connection = new SqlConnection(_applicationSettings.ConnectionStringName))
            {
                connection.Open();
                var existsProduct = FindProductById(product.Id, connection);
                if (existsProduct == null)
                {
                    InsertNewProduct(product, connection);
                }
                else
                {
                    UpdateProduct(existsProduct, product, connection);
                }
            }
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
                connection.Open();
                return FindProductById(id, connection);
            }
        }

        private static Product FindProductById(int id, SqlConnection connection)
        {
            var product = connection.Query<Product>("SELECT * FROM [dbo].[Products] p WHERE p.Id = @id", new {id})
                                    .FirstOrDefault();
            return product;
        }

        private void UpdateProduct(Product existsProduct, Product productUpdates, SqlConnection connection)
        {
            connection.Execute(
                "UPDATE [dbo].[Products] SET Name = @Name, Category = @Category, Vendor = @Vendor, Description = @Description WHERE Id = @Id",
                new
                    {
                        existsProduct.Id,
                        productUpdates.Name,
                        productUpdates.Category,
                        productUpdates.Vendor,
                        productUpdates.Description
                    });
        }

        private static void InsertNewProduct(Product product, SqlConnection connection)
        {
            var id = connection.Query<int>(
                    "INSERT INTO [dbo].[Products] (Name, Category, Vendor, Description) OUTPUT Inserted.ID VALUES(@Name, @Category, @Vendor, @Description)",
                    new
                        {
                            product.Name,
                            product.Category,
                            product.Vendor,
                            product.Description
                        }).First();
            product.Id = Convert.ToInt32(id);
        }
    }
}