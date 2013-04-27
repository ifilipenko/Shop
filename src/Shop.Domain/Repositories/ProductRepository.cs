using System.Collections.Generic;
using System.Data;
using System.Linq;
using Shop.Domain.Model;

namespace Shop.Domain.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IUnitOfWork<ProductModelContext> _unitOfWork;

        public ProductRepository(IUnitOfWork<ProductModelContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Product FindById(int id)
        {
            return _unitOfWork.DbContext.Products.Find(id);
        }

        public void Save(Product product)
        {
            if (product.Id == 0)
            {
                _unitOfWork.DbContext.Products.Add(product);
            }
            else
            {
                _unitOfWork.DbContext.Entry(product).State = EntityState.Modified;
            }
            _unitOfWork.SubmitChanges();
        }

        public IQueryable<Product> GetAll()
        {
            return _unitOfWork.DbContext.Products.OrderBy(n => n.Id);
        }

        public void Delete(Product product)
        {
            _unitOfWork.DbContext.Products.Remove(product);
        }
    }
}