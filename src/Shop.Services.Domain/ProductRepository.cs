using System;
using System.Data;
using System.Data.SqlClient;
using Shop.Services.Domain.Commands;
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

        private void UpdateProduct(Product existsProduct, Product productUpdates, SqlConnection connection)
        {
            using (var sqlCommand = connection.CreateCommand())
            {
                sqlCommand.CommandText = "UPDATE [dbo].[Products] SET Name = @Name, Category = @Category, Vendor = @Vendor, Description = @Description WHERE Id = @Id";
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@Id", existsProduct.Id);
                sqlCommand.Parameters.AddWithValue("@Name", productUpdates.Name);
                sqlCommand.Parameters.AddWithValue("@Category", productUpdates.Category);
                sqlCommand.Parameters.AddWithValue("@Vendor", productUpdates.Vendor);
                sqlCommand.Parameters.AddWithValue("@Description", productUpdates.Description);

                var id = sqlCommand.ExecuteScalar();

                productUpdates.Id = Convert.ToInt32(id);
            }
        }

        private static Product FindProductById(int id, SqlConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Id, Name, Category, Vendor, Description FROM [dbo].[Products] p WHERE p.Id = @id";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                    {
                        return new Product
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Category = reader.GetString(2),
                                Vendor = reader.GetString(3),
                                Description = reader.GetString(4),
                            };
                    }
                    return null;
                }
            }
        }

        private static void InsertNewProduct(Product product, SqlConnection connection)
        {
            using (var sqlCommand = connection.CreateCommand())
            {
                sqlCommand.CommandText =
                    "INSERT INTO [dbo].[Products] (Name, Category, Vendor, Description) OUTPUT Inserted.ID VALUES(@Name, @Category, @Vendor, @Description)";
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.Parameters.AddWithValue("@Name", product.Name);
                sqlCommand.Parameters.AddWithValue("@Category", product.Category);
                sqlCommand.Parameters.AddWithValue("@Vendor", product.Vendor);
                sqlCommand.Parameters.AddWithValue("@Description", product.Description);

                var id = sqlCommand.ExecuteScalar();

                product.Id = Convert.ToInt32(id);
            }
        }
    }
}