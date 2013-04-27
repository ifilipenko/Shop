using System.Data.Entity;

namespace Shop.Domain
{
    public interface IUnitOfWorkScope<out TDataContext>
        where TDataContext : DbContext, new()
    {
        IUnitOfWork<TDataContext> Get();
    }
}