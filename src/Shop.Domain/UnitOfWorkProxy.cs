using System.Data.Entity;

namespace Shop.Domain
{
    public class UnitOfWorkProxy<TDataContext> : IUnitOfWork<TDataContext> 
        where TDataContext : DbContext, new()
    {
        private readonly IUnitOfWorkScope<TDataContext> _scope;

        public UnitOfWorkProxy(IUnitOfWorkScope<TDataContext> scope)
        {
            _scope = scope;
        }

        private IUnitOfWork<TDataContext> UnitOfWork 
        {
            get { return _scope.Get(); }
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }

        public TDataContext DbContext
        {
            get { return UnitOfWork.DbContext; }
        }

        public void SubmitChanges()
        {
            UnitOfWork.SubmitChanges();
        }
    }
}