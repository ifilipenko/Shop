using System.Data.Entity;

namespace Shop.EntityFramework
{
    public interface IDbContextScope
    {
        DbContext DbContext { get; }

        DbSet<T> GetDbSet<T>()
            where T : class;
    }
}