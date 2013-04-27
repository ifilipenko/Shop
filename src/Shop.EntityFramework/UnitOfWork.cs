using System;
using System.Data.Entity;

namespace Shop.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private Lazy<DbContext> _dbContext;
        private bool _needSubmit;

        public UnitOfWork(Func<DbContext> dbContextFactory)
        {
            _needSubmit = true;
            _dbContext = new Lazy<DbContext>(dbContextFactory);
        }

        public DbContext Context
        {
            get { return _dbContext.Value; }
        }

        public void CancelChanges()
        {
            NeedSubmit = false;
        }

        public void SubmitChanges()
        {
            _dbContext.Value.SaveChanges();
        }

        public bool NeedSubmit
        {
            get { return _needSubmit && _dbContext.IsValueCreated; }
            private set { _needSubmit = value; }
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                if (_dbContext.IsValueCreated)
                {
                    _dbContext.Value.Dispose();
                }
                _dbContext = null;
            }
        }
    }
}