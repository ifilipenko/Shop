using System.Data.Entity;

namespace Shop.Domain
{
    public class UnitOfWorkScope<TDataContext> : IUnitOfWorkScope<TDataContext> 
        where TDataContext : DbContext, new()
    {
        private IUnitOfWork<TDataContext> _unitOfWork;

        public IUnitOfWork<TDataContext> Get()
        {
            return _unitOfWork;
        }

        public void OpenUnitOfWork()
        {
            _unitOfWork = new EntityFrameworkUnitOfWork<TDataContext>();
        }

        public void CloseUnitOfWork()
        {
            _unitOfWork.Dispose();
            _unitOfWork = null;
        }
    }
}