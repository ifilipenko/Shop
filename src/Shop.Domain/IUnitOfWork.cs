using System;
using System.Data.Entity;

namespace Shop.Domain
{
    public interface IUnitOfWork<out TDataContext> : IDisposable
        where TDataContext : DbContext
    {
        TDataContext DbContext { get; }
        void SubmitChanges();
    }
}