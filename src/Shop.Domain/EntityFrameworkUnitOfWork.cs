using System.Data.Entity;

namespace Shop.Domain
{
    public class EntityFrameworkUnitOfWork<TDataContext> : IUnitOfWork<TDataContext>
        where TDataContext : DbContext, new()
    {
        public EntityFrameworkUnitOfWork()
        {
            DbContext = new TDataContext();
        }

        public TDataContext DbContext { get; private set; }

        public void SubmitChanges()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            if (DbContext != null)
            {
                DbContext.Dispose();
                DbContext = null;
            }
        }
    }
}