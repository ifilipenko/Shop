namespace Shop.EntityFramework
{
    public interface IUnitOfWorkScope
    {
        IUnitOfWork Get();
        void CloseUnitOfWork();
        void OpenUnitOfWork();
    }
}