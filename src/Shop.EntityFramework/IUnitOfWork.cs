using System;

namespace Shop.EntityFramework
{
    public interface IUnitOfWork : IDisposable
    {
        void CancelChanges();
        void SubmitChanges();
        bool NeedSubmit { get; }
    }
}