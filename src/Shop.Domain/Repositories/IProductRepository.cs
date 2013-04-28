using System;
using System.Linq;
using System.Linq.Expressions;
using Shop.Domain.Model;

namespace Shop.Domain.Repositories
{
    public interface IProductRepository
    {
        Product FindById(int id);
        void Save(Product product);
        IQueryable<Product> GetAll(params Expression<Func<Product, object>>[] includeProperties);
        void Delete(Product product);
    }
}