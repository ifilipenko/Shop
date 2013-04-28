using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Shop.Domain.Model;

namespace Shop.Domain.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductModelContext _dbContext;

        public ProductRepository(IProductModelContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Product FindById(int id)
        {
            return _dbContext.Products.Find(id);
        }

        public void Save(Product product)
        {
            if (product.Id == 0)
            {
                _dbContext.Products.Add(product);
            }
            else
            {
                _dbContext.MarkAsUpdated(product);
            }
        }

        public IQueryable<Product> GetAll(params Expression<Func<Product, object>>[] includeProperties)
        {
            IQueryable<Product> products = _dbContext.Products;
            foreach (var property in includeProperties)
            {
                products = products.Include(property);
            }
            return products.OrderBy(n => n.Id);
        }

        public void Delete(Product product)
        {
            _dbContext.Products.Remove(product);
        }
    }
}