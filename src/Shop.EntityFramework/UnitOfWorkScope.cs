using System;
using System.Data.Entity;
using System.Diagnostics;

namespace Shop.EntityFramework
{
    public class UnitOfWorkScope<TDbContext> : IUnitOfWorkScope, IDbContextScope
        where TDbContext : DbContext, new()
    {
        private UnitOfWork _unitOfWork;
        
        public IUnitOfWork Get()
        {
            Debug.Assert(_unitOfWork != null, "Before use UnitOfWorkScope need invoke OpenUnitOfWork()");
            return _unitOfWork;
        }

        public void OpenUnitOfWork()
        {
            if (_unitOfWork != null)
                throw new InvalidOperationException("Another unit of work already opened");
            _unitOfWork = new UnitOfWork(() => new TDbContext());
        }

        public void CloseUnitOfWork()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
                _unitOfWork = null;
            }
        }

        public TDbContext DbContext
        {
            get
            {
                Debug.Assert(_unitOfWork != null, "Before use DbDontextScope need invoke OpenUnitOfWork()");
                return (TDbContext)_unitOfWork.Context;
            }
        }

        DbContext IDbContextScope.DbContext
        {
            get
            {
                Debug.Assert(_unitOfWork != null, "Before use DbDontextScope need invoke OpenUnitOfWork()");
                return _unitOfWork.Context;
            }
        }

        public DbSet<T> GetDbSet<T>()
            where T : class
        {
            Debug.Assert(_unitOfWork != null, "Before use DbDontextScope need invoke OpenUnitOfWork()");
            return _unitOfWork.Context.Set<T>();
        }
    }
}