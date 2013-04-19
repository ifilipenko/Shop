using System;
using System.Data;
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
            using (var connection = new SqlConnection(_applicationSettings.ConnectionStringName))
            {
                connection.Open();
                using (var sqlCommand = connection.CreateCommand())
                {
                    sqlCommand.CommandText = "INSERT INTO [dbo].[Products] (Name, Category, Vendor, Description) OUTPUT Inserted.ID VALUES(@Name, @Category, @Vendor, @Description)";
                    sqlCommand.CommandType = CommandType.Text;

                    sqlCommand.Parameters.AddWithValue("@Name", command.Name);
                    sqlCommand.Parameters.AddWithValue("@Category", command.Category);
                    sqlCommand.Parameters.AddWithValue("@Vendor", command.Vendor);
                    sqlCommand.Parameters.AddWithValue("@Description", command.Description);

                    var id = sqlCommand.ExecuteScalar();

                    return Convert.ToInt32(id);
                }
            }
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
                connection.Open();
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
        }
    }
}